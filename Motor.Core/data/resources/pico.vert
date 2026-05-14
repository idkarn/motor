#version 330
in vec3 vertexPosition;
in vec2 vertexTexCoord;
uniform mat4 mvp;

out vec2 fragTexCoord;

void main() {
    vec4 pos = mvp * vec4(vertexPosition, 1.0);
    
    // Vertex Snapping: Forces vertices to stick to a low-res grid
    float snap = 40.0; 
    pos.xyz /= pos.w;
    pos.xy = floor(pos.xy * snap) / snap;
    pos.xyz *= pos.w;
    
    gl_Position = pos;
    fragTexCoord = vertexTexCoord;
}
