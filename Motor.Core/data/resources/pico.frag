#version 330
in vec2 fragTexCoord;
uniform sampler2D texture0;

out vec4 finalColor;

void main() {
    // Simple point-filtered texture lookup
    finalColor = texture(texture0, fragTexCoord);
    
    // Optional: Kill transparent pixels (for decals/fences)
    if (finalColor.a < 0.5) discard;
}
