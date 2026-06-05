using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Motor.Core.Modifiers.Controller;
using System.Reflection;
using System.Runtime.Loader;

namespace Motor.Core;

class ScriptManager
{
    static ScriptManager? Instance;
    private Dictionary<string, Type> _scriptTypes = [];

    static public void LoadScripts(string scriptsFolder)
    {
        Instance ??= new();
        Instance._scriptTypes.Clear();

        var root = ResolveScriptsFolder(scriptsFolder);
        var scriptFiles = Directory.EnumerateFiles(root, "*.cs", SearchOption.AllDirectories).ToArray();

        if (scriptFiles.Length == 0)
            throw new Exception($"No script files were found in '{root}'.");

        var syntaxTrees = scriptFiles
            .Select(File.ReadAllText)
            .Select(text => CSharpSyntaxTree.ParseText(text))
            .ToArray();

        var compilation = CSharpCompilation.Create("UserScripts")
            .WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
            .AddReferences(
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location), // System.Runtime
                MetadataReference.CreateFromFile(Assembly.Load("System.Runtime").Location),
                MetadataReference.CreateFromFile(Assembly.Load("System.Numerics.Vectors").Location),
                MetadataReference.CreateFromFile(typeof(Console).Assembly.Location), // System.Console
                MetadataReference.CreateFromFile(typeof(Game).Assembly.Location), // Your Engine DLL
                MetadataReference.CreateFromFile(typeof(Controller).Assembly.Location) // Your Engine DLL
            )
            .AddSyntaxTrees(syntaxTrees);

        using var ms = new MemoryStream();
        var result = compilation.Emit(ms);

        if (!result.Success)
        {
            var failures = result.Diagnostics.Where(d => d.IsWarningAsError || d.Severity == DiagnosticSeverity.Error);
            foreach (var diagnostic in failures)
                Console.Error.WriteLine($"{diagnostic.Id}: {diagnostic.GetMessage()}");

            throw new Exception("Compilation failed");
        }

        ms.Seek(0, SeekOrigin.Begin);
        var assembly = AssemblyLoadContext.Default.LoadFromStream(ms);

        foreach (var type in assembly.GetTypes())
        {
            if (type.IsAssignableTo(typeof(IController)))
            {
                Instance._scriptTypes[type.Name] = type;
                Console.WriteLine($"Loaded script: {type.Name}");
            }
        }
    }

    static string ResolveScriptsFolder(string scriptsFolder)
    {
        if (Path.IsPathRooted(scriptsFolder) && Directory.Exists(scriptsFolder))
            return scriptsFolder;

        foreach (var basePath in new[] { AppContext.BaseDirectory, Directory.GetCurrentDirectory() })
        {
            var current = Path.GetFullPath(basePath);

            while (true)
            {
                var candidate = Path.Combine(current, scriptsFolder);
                if (Directory.Exists(candidate))
                    return candidate;

                var parent = Directory.GetParent(current);
                if (parent is null)
                    break;

                current = parent.FullName;
            }
        }

        throw new Exception($"[ERR] Unable to locate scripts folder '{scriptsFolder}'.");
    }

    static public IController? GetScriptByName(string name)
    {
        if (Instance == null)
            throw new Exception("[ERR] ScriptManager is not initialized yet!");

        if (!Instance._scriptTypes.TryGetValue(name, out var scriptType))
            throw new Exception($"[ERR] Unknown script controller: {name}");

        return Activator.CreateInstance(scriptType) as IController
            ?? throw new Exception($"[ERR] Script controller '{name}' does not implement IController.");
    }
}
