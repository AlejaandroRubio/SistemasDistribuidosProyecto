#version 330 core

layout (location = 0) in vec3 aPosition; // Coordenadas del Vertice
layout (location = 1) in vec2 aTexCoord; // Coordenadas de la textura

out vec2 texCoord;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

uniform float time; // Variable de tiempo para la animacion

// Ajusta la coordenada Y de cada vertice basado en un seno

uniform float frequency;
uniform float amplitude; 
void main() 
{
    vec3 newPosition = aPosition;
    newPosition.y += amplitude * sin(time * frequency + aPosition.x);

    // Aplicar la matriz Modelo-View-Projection
    gl_Position = vec4(newPosition, 1.0) * model * view * projection;

    // Pasar las texturas
    texCoord = aTexCoord;
}