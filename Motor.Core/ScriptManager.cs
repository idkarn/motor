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

        var code = File.ReadAllText(Path.Combine(scriptsFolder, "ButtonScript.cs"));

        // 1. Parse the syntax tree
        var syntaxTree = CSharpSyntaxTree.ParseText(code);

        // 2. Define compilation options (Output: Library/DLL)
        var compilation = CSharpCompilation.Create("UserScripts")
            .WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
            .AddReferences(
                // You MUST manually provide references in Roslyn
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location), // System.Runtime
                MetadataReference.CreateFromFile(Assembly.Load("System.Runtime").Location),
                MetadataReference.CreateFromFile(Assembly.Load("System.Numerics.Vectors").Location),
                MetadataReference.CreateFromFile(typeof(Console).Assembly.Location), // System.Console
                                                                                     // MetadataReference.CreateFromFile(typeof(Vector3).Assembly.Location), // System.Numerics.Vector
                MetadataReference.CreateFromFile(typeof(Game).Assembly.Location), // Your Engine DLL
                MetadataReference.CreateFromFile(typeof(Controller).Assembly.Location) // Your Engine DLL
            )
            .AddSyntaxTrees(syntaxTree);

        // 3. Emit (Compile) to memory
        using var ms = new MemoryStream();
        var result = compilation.Emit(ms);

        if (!result.Success)
        {
            var failures = result.Diagnostics.Where(d => d.IsWarningAsError || d.Severity == DiagnosticSeverity.Error);
            foreach (var diagnostic in failures)
                Console.Error.WriteLine($"{diagnostic.Id}: {diagnostic.GetMessage()}");

            throw new Exception("Compilation failed");
        }

        // 4. Load the resulting assembly
        ms.Seek(0, SeekOrigin.Begin);
        var assembly = AssemblyLoadContext.Default.LoadFromStream(ms);

        // 5. Discover types
        foreach (var type in assembly.GetTypes())
        {
            if (type.IsAssignableTo(typeof(IController)))
            {
                Instance._scriptTypes[type.Name] = type;
                Console.WriteLine($"Loaded script: {type.Name}");
            }
        }
    }

    static public IController? GetScriptByName(string name)
    {
        if (Instance == null)
            throw new Exception("[ERR] ScriptManager is not initialized yet!");

        return Activator.CreateInstance(Instance._scriptTypes[name]) as IController;
    }
}
