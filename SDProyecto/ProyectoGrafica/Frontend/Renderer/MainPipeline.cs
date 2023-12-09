using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using ProyectoGrafica.Models;
using StbImageSharp;
using NCalc;

namespace Frontend.Renderer
{
    internal class MainPipeline : OpenTK.GameWindow
    {
        // No hacen falta repetir vertices, si dos vertices tienen las mismas coordenadas, 
        // Se comparten entre triangulos como 1 solo
        // En sentido horario
        List<Vector3> newVertices = new List<Vector3>()
        {
            // Poligono Frontal
            new Vector3(-0.5f, 0.5f, 0.5f),
            new Vector3(0.5f, 0.5f, 0.5f),
            new Vector3(0.5f, -0.5f, 0.5f),
            new Vector3(-0.5f, -0.5f, 0.5f),     
            // Poligono Derecho
            new Vector3(0.5f, 0.5f, -0.5f),
            new Vector3(-0.5f, 0.5f, -0.5f),
            new Vector3(-0.5f, -0.5f, -0.5f),
            new Vector3(0.5f, -0.5f, -0.5f),    

        };

        // En sentido horario
        List<Vector2> textCoords = new List<Vector2>()
        { 
            new Vector2 (0.0f, 1.0f),   // Arriba Izquierda
            new Vector2 (1.0f, 1.0f),   // Arriba Derecha
            new Vector2 (1.0f, 0.0f),   // Abajo Derecha
            new Vector2 (0.0f, 0.0f),    // Abajo Izquierda

            new Vector2 (0.0f, 1.0f),   // Arriba Izquierda
            new Vector2 (1.0f, 1.0f),   // Arriba Derecha
            new Vector2 (1.0f, 0.0f),   // Abajo Derecha
            new Vector2 (0.0f, 0.0f),    // Abajo Izquierda

            new Vector2 (0.0f, 1.0f),   // Arriba Izquierda
            new Vector2 (1.0f, 1.0f),   // Arriba Derecha
            new Vector2 (1.0f, 0.0f),   // Abajo Derecha
            new Vector2 (0.0f, 0.0f),    // Abajo Izquierda

            new Vector2 (0.0f, 1.0f),   // Arriba Izquierda
            new Vector2 (1.0f, 1.0f),   // Arriba Derecha
            new Vector2 (1.0f, 0.0f),   // Abajo Derecha
            new Vector2 (0.0f, 0.0f),    // Abajo Izquierda

            new Vector2 (0.0f, 1.0f),   // Arriba Izquierda
            new Vector2 (1.0f, 1.0f),   // Arriba Derecha
            new Vector2 (1.0f, 0.0f),   // Abajo Derecha
            new Vector2 (0.0f, 0.0f),    // Abajo Izquierda

            new Vector2 (0.0f, 1.0f),   // Arriba Izquierda
            new Vector2 (1.0f, 1.0f),   // Arriba Derecha
            new Vector2 (1.0f, 0.0f),   // Abajo Derecha
            new Vector2 (0.0f, 0.0f)    // Abajo Izquierda

        };

        List<uint> newIndices = new List<uint>()
        {
           
            0, 1, 2,            // Triangulo superior
            2, 3, 0,             // Triangulo Inferior


        };

        // ----------- Variables Render Pipeline ----------- //

        int vaoHandle;                  // Vertex Array Object
        int vboHandle;                  // Vertex Buffer Object
        int eboHandle;                  // Index/Element Buffer Object

        int shaderProgramHandle;

        int textureIDHandle;
        int textureVBOHandle;

        // ----------- Variables Transformaciones ----------- //
        float yRotation = 0;

        Camera camera;
        KeyboardState keyboard;
        MouseState mouse;


        int screenWidth;
        int screenHeight;

        public MainPipeline(int width, int height) : base(width, height, OpenTK.Graphics.GraphicsMode.Default, "3D GrAPI")
        {
            screenWidth = width;
            screenHeight = height;
        }


        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
            base.OnResize(e);
        }

        // Llamado al iniciar el programa
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // GenerateMeshPlane(2, 2);

            // CalculateY("Cos(x * x) / z");

            vaoHandle = GL.GenVertexArray();                                                                                                    // Contenedor vacio para cargar objetos
            // Enlazar VAO (Vertex Array Object)      
            GL.BindVertexArray(vaoHandle);

            vboHandle = GL.GenBuffer();                                                                                                         // Vertex Buffer Object, carga los vertices a la gpu
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboHandle);
            GL.BufferData(BufferTarget.ArrayBuffer, newVertices.Count * Vector3.SizeInBytes, newVertices.ToArray(), BufferUsageHint.StaticDraw);


            // Poner vertice VBO en la ranura 0 del vao
            // ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);                                                           //   Poner VBO en la ranura 0 de VAO
                                                                                                                                                // - Indice
                                                                                                                                                // - Tamaño de cada Vector que compone un vertice
                                                                                                                                                // - Tipo de dato
                                                                                                                                                // - Normalizar el dato (0-1), No hace falta, ya esta normalizado
                                                                                                                                                // - Si tienes color ademas de coordenadas para el vertice el strife saltaria el numero de posiciones que le digas
                                                                                                                                                // - Offset, donde empieza el vector
                                                                                                                                                // ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
            GL.EnableVertexArrayAttrib(vaoHandle, 0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            // Textura del VBO 
            textureVBOHandle = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, textureVBOHandle);
            GL.BufferData(BufferTarget.ArrayBuffer, textCoords.Count * Vector2.SizeInBytes, textCoords.ToArray(), BufferUsageHint.StaticDraw);

            // Poner VBO en la ranura 1 de VAO
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 0, 0);
            GL.EnableVertexArrayAttrib(vaoHandle, 1);



            // Desenlazar vbo y vao respectivamente
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);

            SetUpIndices();

            SetUpShaders();

            SetUpTextures();


            GL.Enable(EnableCap.DepthTest);

            keyboard = new KeyboardState();
            mouse    = new MouseState();
            camera   = new Camera(screenWidth, screenHeight, new Vector3(0, 10, 0));

        }

        // Llamado al cerrar el programa
        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);

            GL.DeleteVertexArray(vaoHandle);
            GL.DeleteBuffer(vboHandle);
            GL.DeleteBuffer(eboHandle);
            GL.DeleteTexture(textureIDHandle);
            GL.DeleteProgram(shaderProgramHandle);
        }


        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.ClearColor(0.561f, 0.3f, 0.54f, 1);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // Dibujar vertices
            GL.UseProgram(shaderProgramHandle);
            // Adjuntas al programa el objeto que quieres renderizar
            GL.BindVertexArray(vaoHandle);
            // Y los indices de los triangulos que vas a renderizar
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, eboHandle);
            // Textura
            GL.BindTexture(TextureTarget.Texture2D, textureIDHandle);

            MatrixTransformations();

            // Se van a dibujar dos triangulos, cada triangulo tiene 3 vertices, la longitud de los indices es 6, 6 / 3 = 2 triangulos
            GL.DrawElements(PrimitiveType.Triangles, newIndices.Count, DrawElementsType.UnsignedInt, 0);

            // Cambia entre el front buffer y el back buffer, el frame actual y el siguiente
            Context.SwapBuffers();

           
        }


        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            keyboard = Keyboard.GetState();
            mouse = Mouse.GetState();
            base.OnUpdateFrame(e);
            camera.Update(keyboard, mouse, e);
            //CursorGrabbed = true;
            CursorVisible = false;
        }

        // Metodo para cargar shaders escritos en GLSL a un string //
        public string LoadShaderSource(string filePath)
        {
            string shaderSource = "";

            try
            {
                using (StreamReader reader = new StreamReader("../../Shaders/" + filePath))
                {
                    shaderSource = reader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al cargar el shader -> " + e.Message);
            }

            return shaderSource;

        }

        void SetUpIndices()
        {
            // Crear buffer de indices
            eboHandle = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, eboHandle);
            GL.BufferData(BufferTarget.ElementArrayBuffer, newIndices.Count * sizeof(uint), newIndices.ToArray(), BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        }

        void SetUpShaders()
        {
            // Crear shader Program
            shaderProgramHandle = GL.CreateProgram();
            // Shader para visualizar los vertices
            int vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, LoadShaderSource("Default.vert"));
            GL.CompileShader(vertexShader);

            // Shader para rasterizar los pixeles entre los vertices
            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, LoadShaderSource("Default.frag"));
            GL.CompileShader(fragmentShader);

            // Composicion del shaderProgram
            GL.AttachShader(shaderProgramHandle, vertexShader);
            GL.AttachShader(shaderProgramHandle, fragmentShader);
            GL.LinkProgram(shaderProgramHandle);

            // Limpiar memoria
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);
        }

        void SetUpTextures()
        {
            // Texturas
            textureIDHandle = GL.GenTexture();                                                                                                  // Genera un Atlas de 16 texturas de resolucion 1024x1024
            GL.ActiveTexture(TextureUnit.Texture0);                                                                                             // Activar la texture en la unidad
            GL.BindTexture(TextureTarget.Texture2D, textureIDHandle);

            // Parametros
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);                       // Como se comporta la textura si se pasa de la anchura maxima, en este caso se repite
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);                       // Como se comporta la textura si se pasa de la altura maxima, en este caso se repite
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);                     // Filtro para cuando se escala la textura a un tamano mas pequeno, Nearest (Sin Suavizado, ni desenfoque o fundido entre pixeles)
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);                     // Filtro para cuando se escala la textura a un tamano mas grande, Nearest (Sin Suavizado, ni desenfoque o fundido entre pixeles)

            // Cargar Textura
            StbImage.stbi_set_flip_vertically_on_load(1);                                                                                       // El sistema de coordenadas de OPENGL UV es de 0 a 1 en Y, En StbImage es de 1 a 0
            Stream texture = File.OpenRead("../../Textures/Grid.png");
            ImageResult testTexture = ImageResult.FromStream(texture, ColorComponents.RedGreenBlueAlpha);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, testTexture.Width, testTexture.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, testTexture.Data);
            GL.BindTexture(TextureTarget.Texture2D, 0);                                                                                    // Desenlazar la textura

        }

        void MatrixTransformations()
        {
            // Matriz de transformacion
            Matrix4 model = Matrix4.Identity;
            Matrix4 view = camera.GetViewMatrix();
            Matrix4 projection = camera.GetProjectionMatrix();

            // model = Matrix4.CreateRotationY(yRotation);
            // yRotation += 0.1f;

            Matrix4 translation = Matrix4.CreateTranslation(0f, 0f, -3f);
            model *= translation;

            int modelLocation = GL.GetUniformLocation(shaderProgramHandle, "model");            // Se lo pasa al default.vert
            int viewLocation = GL.GetUniformLocation(shaderProgramHandle, "view");              // Se lo pasa al default.vert
            int projectionLocation = GL.GetUniformLocation(shaderProgramHandle, "projection");  // Se lo pasa al default.vert

            GL.UniformMatrix4(modelLocation, true, ref model);
            GL.UniformMatrix4(viewLocation, true, ref view);
            GL.UniformMatrix4(projectionLocation, true, ref projection);
        }

        public void SetUpVerticesIndexAndTextures(List<Vector3> vertices, List<uint> indices, List<Vector2> textures)
        {
            newVertices = vertices;
            newIndices = indices;
            textCoords = textures;
        }



        void GenerateMeshPlane(int x, int y)
        {
 

            List<Vector3> tempVertexList = new List<Vector3>();

            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    // Crea una nueva instancia de Vector3 para cada vértice
                    Vector3 tempV = new Vector3(j, 0, i);
                    tempVertexList.Add(tempV);
                }
            }

            newVertices = tempVertexList;

            List<uint> indices = new List<uint>();

            for (int i = 0; i < y - 1; i++)
            {
                for (int j = 0; j < x - 1; j++)
                {
                    // Índices de los cuatro vértices de un cuadrado
                    uint topLeft = (uint)(i * x + j);
                    uint topRight = (uint)(i * x + (j + 1));
                    uint bottomLeft = (uint)((i + 1) * x + j);
                    uint bottomRight = (uint)((i + 1) * x + (j + 1));

                    // Triángulo superior del cuadrado
                    indices.Add(topLeft);
                    indices.Add(topRight);
                    indices.Add(bottomLeft);

                    // Triángulo inferior del cuadrado
                    indices.Add(bottomLeft);
                    indices.Add(topRight);

                    indices.Add(bottomRight);
                }
            }

            newIndices = indices;

            List<Vector2> tempTextures = new List<Vector2>();

            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    // Normalizar las coordenadas de textura en el rango [0, 1]
                    float u = (float)j / (x - 1) * x;
                    float v = (float)i / (y - 1) * y;

                    tempTextures.Add(new Vector2(u, v));
                }
            }

            textCoords = tempTextures;

        }




    }
}