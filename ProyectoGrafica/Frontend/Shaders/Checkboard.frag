#version 330 core

in vec2 texCoord;

out vec4 FragColor;

uniform sampler2D texture0;

void main()
{
    // Tamaño de cada agrupacion de pixeles
    float clusterSize = 60.0;

    // Calcular los indices de cada agrupacion para X e Y
    float clusterX = floor(gl_FragCoord.x / clusterSize);
    float clusterY = floor(gl_FragCoord.y / clusterSize);

    // Usar los indices del claster para crear el patron de colores cambiante
    float pattern = mod(clusterX + clusterY, 2.0);

    // Colores
    vec3 color = vec3(0.2f * pattern, 0.8f * pattern, 0.2f * pattern);

    // Asignar el color a la geometria
    FragColor = vec4(color, 1.0);
}