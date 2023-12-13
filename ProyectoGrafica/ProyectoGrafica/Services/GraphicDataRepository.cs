using Graficas.Models;
using ProyectoGrafica.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using OpenTK;
using Z.Expressions;
using System.Data;
using System.Web.Helpers;

namespace Graficas.Services
{
    public class GraphicDataRepository
    {
        #region PATHS
        static string relativePath = "Archivos_Compartidos\\Global.txt";
        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);

        static string relatieRootFolder = "Archivos_Compartidos\\temp";
        string rootFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relatieRootFolder);

        static string relatieMeshPath = "Archivos_Compartidos\\temp\\mesh.txt";
        string meshPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relatieMeshPath);
        #endregion

        public GraphicDataRepository()
        {
            var ctx = HttpContext.Current;
            if (ctx == null)
                throw new ArgumentNullException(nameof(ctx));
        }


        #region GET
        public OpenGLGeneratedMesh GetOpenGLData()
        {
            return ReconstructMesh();
        }

        OpenGLGeneratedMesh ReconstructMesh()
        {
            OpenGLGeneratedMesh mesh = new OpenGLGeneratedMesh();

            string[] tJs = File.ReadAllLines(meshPath);
            // Usado para recorrer cada linea
            char[] tempCharArr = { };

            // Estados
            int whatImGenerating = 0;
            Vector3 tempVertex = new Vector3();
            Vector2 tempTexCoord = new Vector2();
            bool loopDone = true;

            for (int i = 0; i < tJs.Length; i++)
            {
                tempCharArr = tJs[i].ToCharArray();
                
                if (whatImGenerating == 0)
                    tempVertex = new Vector3();
                
                if (whatImGenerating == 2)
                    tempTexCoord = new Vector2();

                for (int j = 0; j < tJs[i].Length; j++)
                {
                    // PASO 1: Generar lista de vertices
                    if (whatImGenerating == 0)
                    {
                        if (tempCharArr[j] == 120) // x
                        { 
                            float.TryParse(GetNumbersFromJson(ref tempCharArr, j), out tempVertex.X);
                        }

                        if (tempCharArr[j] == 121) // y
                        {
                            float.TryParse(GetNumbersFromJson(ref tempCharArr, j), out tempVertex.Y);
                        }

                        if (tempCharArr[j] == 122) // z
                        {
                            float.TryParse(GetNumbersFromJson(ref tempCharArr, j), out tempVertex.Z);

                            mesh.tempVertexList.Add(tempVertex);
                        }


                        if (tempCharArr[j] == 125 && loopDone) // }
                        {
                            whatImGenerating++;
                            loopDone = false;
                        }
                    }


                    if (whatImGenerating == 1)
                    {
                        if (j < 1)
                        { 
                            uint tempIndex;
                            string line = new string(tempCharArr);

                            // No incluir nuevas lineas
                            line = line.Trim('\r', '\n');

                            uint.TryParse(line, out tempIndex);

                            if (!string.IsNullOrEmpty(line) && tempCharArr[0] != 125 && tempCharArr[0] != 123 &&
                                (tempCharArr[0] == '0' || tempCharArr[0] == '1' || tempCharArr[0] == '2' || tempCharArr[0] == '3' ||
                                 tempCharArr[0] == '4' || tempCharArr[0] == '5' || tempCharArr[0] == '6' || tempCharArr[0] == '7' ||
                                 tempCharArr[0] == '8' || tempCharArr[0] == '9')) // 125 = } | 123 = {
                            {
                                mesh.indices.Add(tempIndex);
                            }

                            if (tempCharArr[j] == 125 && loopDone) // }
                            {
                                whatImGenerating++;
                                loopDone = false;
                            }
                        }
                    }

                    // PASO 3: Generar lista de Coordenadas de Texturas
                    if (whatImGenerating == 2)
                    {
                        if (tempCharArr[j] == 85) // U
                        {
                            float.TryParse(GetNumbersFromJson(ref tempCharArr, j), out tempTexCoord.X);
                        }

                        if (tempCharArr[j] == 86) // V
                        {
                            float.TryParse(GetNumbersFromJson(ref tempCharArr, j), out tempTexCoord.Y);

                            mesh.tempTexturesCoords.Add(tempTexCoord);
                        }

                        if (tempCharArr[j] == 125 && loopDone) // }
                        {
                            whatImGenerating++;
                            loopDone = false;
                        }
                    }
                }
                loopDone = true;
            }



            return mesh;
        }
        #endregion

        #region POST
        public bool SaveGeneratedMesh(GraphicsDataRequest data)
        {
            // Paso 0 Borrar los TXT temporales si es que existen
            DeleteMeshTemporalTXTs();
            // Paso 1 Generar la geometria
            OpenGLGeneratedMesh tempMesh = GenerateMeshPlane(data.x, data.z);
            // Paso 2 Calcular la Y en funcion de la formula
            if (data.premadeFunctionIndex == -1 || data.premadeFunctionIndex == 11)
                CalculateY(ref tempMesh, data.formula);
            else
                PremadeFunctions(ref tempMesh, data.premadeFunctionIndex);
            // Paso 3 Guardar la informacion a un TXT
            WritePostToTXT(tempMesh);

            return true;
        }

        void DeleteMeshTemporalTXTs()
        {
            string[] files = Directory.GetFiles(rootFolder);
            foreach (string file in files) 
            {
                File.Delete(file);
            }
        }

        OpenGLGeneratedMesh GenerateMeshPlane(int x, int y)
        {
            OpenGLGeneratedMesh openGLGeneratedMesh = new OpenGLGeneratedMesh();

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

            openGLGeneratedMesh.tempVertexList = tempVertexList;

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

            openGLGeneratedMesh.indices = indices;

            List<Vector2> tempTextures = new List<Vector2>();

            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    // Normalizar las coordenadas de textura en el rango [0, 1]
                    float u = (float)j / (x) * x;
                    float v = (float)i / (y) * y;

                    tempTextures.Add(new Vector2(u, v));
                }
            }

            openGLGeneratedMesh.tempTexturesCoords = tempTextures;


            return openGLGeneratedMesh;
        }
        #endregion

        #region FORMULAS
        void CalculateY(ref OpenGLGeneratedMesh mesh, String formula)
        {
            formula = formula.ToLower();
            
            for (int i = 0; i < mesh.tempVertexList.Count; i++)
            {
                float newY= Eval.Execute<float>(formula, new {x = mesh.tempVertexList[i].X, z = mesh.tempVertexList[i].Z });

                mesh.tempVertexList[i] = new Vector3(mesh.tempVertexList[i].X, newY, mesh.tempVertexList[i].Z);
            }
            
        }
        
        void PremadeFunctions(ref OpenGLGeneratedMesh mesh, int actionNumber)
        {
            float newX = 0;
            float newY = 0;
            float newZ = 0;

            for (int i = 0; i < mesh.tempVertexList.Count; i++)
            {
                switch (actionNumber)
                {
                    case 0:
                        // Escalera
                        float stepSize = 5.0f;
                        newY = (float)(stepSize * Math.Floor(mesh.tempVertexList[i].Z / stepSize));
                        mesh.tempVertexList[i] = new Vector3(mesh.tempVertexList[i].X, newY, mesh.tempVertexList[i].Z);
                        break;
                    case 1:
                        // Escalera Ondulante
                        newY = (float)(Math.Sqrt(mesh.tempVertexList[i].X * mesh.tempVertexList[i].X + mesh.tempVertexList[i].Z * mesh.tempVertexList[i].Z) + 3 * Math.Cos(Math.Sqrt(mesh.tempVertexList[i].X * mesh.tempVertexList[i].X + mesh.tempVertexList[i].Z * mesh.tempVertexList[i].Z)) + 5);
                        mesh.tempVertexList[i] = new Vector3(mesh.tempVertexList[i].X, newY, mesh.tempVertexList[i].Z);
                        break;
                    case 2:
                        // Patron Piramidal
                        float size = 10.0f;
                        int numTilesX = (int)(mesh.tempVertexList[i].X / size);
                        int numTilesZ = (int)(mesh.tempVertexList[i].Z / size);
                        newY = (numTilesX + numTilesZ) % 2 == 0 ? 0 : 10;
                        mesh.tempVertexList[i] = new Vector3(mesh.tempVertexList[i].X, newY, mesh.tempVertexList[i].Z);
                        break;
                    case 3:
                        // Cuadrados Concentricos
                        float squareSize = 10.0f;
                        newX = Math.Abs(mesh.tempVertexList[i].X % squareSize - squareSize / 2.0f);
                        newZ = Math.Abs(mesh.tempVertexList[i].Z % squareSize - squareSize / 2.0f);
                        newY = Math.Max(newX, newZ);
                        mesh.tempVertexList[i] = new Vector3(newX, newY, newZ);
                        break;
                    case 4:
                        // Tumularios
                        float frequency = 0.1f;
                        float amplitude = 5.0f;
                        newY = (float)(amplitude * Math.Abs(Math.Sin(frequency * mesh.tempVertexList[i].X) + Math.Sin(frequency * mesh.tempVertexList[i].Z)));
                        mesh.tempVertexList[i] = new Vector3(mesh.tempVertexList[i].X, newY, mesh.tempVertexList[i].Z);
                        break;
                    case 5:
                        // Retorcimiento
                        float twistFrequency = 0.1f;
                        float twistAmplitude = 5.0f;
                        float twistAngle = (float)(twistAmplitude * Math.Sin(twistFrequency * mesh.tempVertexList[i].X));
                        newX = mesh.tempVertexList[i].X * (float)Math.Cos(twistAngle) - mesh.tempVertexList[i].Z * (float)Math.Sin(twistAngle);
                        newZ = mesh.tempVertexList[i].X * (float)Math.Sin(twistAngle) + mesh.tempVertexList[i].Z * (float)Math.Cos(twistAngle);
                        newY = (float)(10.0f * Math.Sin(0.1 * newX) + 10.0f * Math.Cos(0.1 * newZ));
                        mesh.tempVertexList[i] = new Vector3(newX, newY, newZ);
                        break;
                    case 6:
                        // Coliseo
                        float centerX = 50.0f;
                        float centerZ = 50.0f;
                        float distance = (float)Math.Sqrt((mesh.tempVertexList[i].X - centerX) * (mesh.tempVertexList[i].X - centerX) + (mesh.tempVertexList[i].Z - centerZ) * (mesh.tempVertexList[i].Z - centerZ));
                        float circleSpacing = 10.0f;
                        newY = (float)(10.0f * Math.Floor(distance / circleSpacing));
                        mesh.tempVertexList[i] = new Vector3(mesh.tempVertexList[i].X, newY, mesh.tempVertexList[i].Z);
                        break;
                    case 7:
                        // Efecto Raro - Ver desde Arriba
                        float hexSize = 10.0f;
                        newX = mesh.tempVertexList[i].X * (float)Math.Sqrt(3.0) / 2.0f / hexSize;
                        newZ = mesh.tempVertexList[i].Z / 1.5f / hexSize;
                        newY = (float)(Math.Sin(newX * newX + newZ * newZ) * 10.0f);
                        mesh.tempVertexList[i] = new Vector3(newX, newY, newZ);
                        break;
                    case 8:
                        // Minimonticulos
                        float gridSize = 10.0f;
                        float sphereRadius = 2.0f;
                        float gridX = (float)Math.Floor(mesh.tempVertexList[i].X / gridSize);
                        float gridZ = (float)Math.Floor(mesh.tempVertexList[i].Z / gridSize);
                        float distanceToSphere = (float)Math.Sqrt((mesh.tempVertexList[i].X - gridX * gridSize) * (mesh.tempVertexList[i].X - gridX * gridSize) + (mesh.tempVertexList[i].Z - gridZ * gridSize) * (mesh.tempVertexList[i].Z - gridZ * gridSize));
                        newY = Math.Max(sphereRadius - distanceToSphere, 0.0f);
                        mesh.tempVertexList[i] = new Vector3(mesh.tempVertexList[i].X, newY, mesh.tempVertexList[i].Z);
                        break;
                    case 9:
                        // Mar de Fibras
                        float frequencyX = 0.1f;
                        float frequencyZ = 0.1f;
                        float amplitude2 = 5.0f;
                        float sineX = (float)(amplitude2 * Math.Sin(frequencyX * mesh.tempVertexList[i].X));
                        float sineZ = (float)(amplitude2 * Math.Sin(frequencyZ * mesh.tempVertexList[i].Z));
                        float modulation = (float)(10.0f * Math.Sin(Math.Sqrt(mesh.tempVertexList[i].X * mesh.tempVertexList[i].X + mesh.tempVertexList[i].Z * mesh.tempVertexList[i].Z)));
                        newY = sineX + sineZ + modulation;
                        mesh.tempVertexList[i] = new Vector3(mesh.tempVertexList[i].X, newY, mesh.tempVertexList[i].Z);
                        break;
                    case 10:
                        // Valle de la Muerte
                        float swirlFrequency = 0.1f;
                        float swirlAmplitude = 5.0f;
                        float swirlAngle = (float)(swirlAmplitude * Math.Sin(swirlFrequency * Math.Sqrt(mesh.tempVertexList[i].X * mesh.tempVertexList[i].X + mesh.tempVertexList[i].Z * mesh.tempVertexList[i].Z)));
                        newX = mesh.tempVertexList[i].X * (float)Math.Cos(swirlAngle) - mesh.tempVertexList[i].Z * (float)Math.Sin(swirlAngle);
                        newZ = mesh.tempVertexList[i].X * (float)Math.Sin(swirlAngle) + mesh.tempVertexList[i].Z * (float)Math.Cos(swirlAngle);
                        newY = (float)(10.0f * Math.Sin(0.1 * newX) * Math.Cos(0.1 * newZ));
                        mesh.tempVertexList[i] = new Vector3(newX, newY, newZ);
                        break;
                }
            }
        }
        #endregion

        #region WritePostToTXT
        void WritePostToTXT(OpenGLGeneratedMesh data)
        {
            CreateMeshTemporalTXTs();

            // Escribir los vertices
            using (StreamWriter sw = File.AppendText(meshPath))
            {
                for (int i = 0; i < data.tempVertexList.Count; i++)
                {
                    if (i == 0)
                        sw.WriteLine("{ ");

                    sw.WriteLine("x:/" + data.tempVertexList[i].X + "| y:/" + data.tempVertexList[i].Y + "| z:/" + data.tempVertexList[i].Z);
                   
                    if (i == data.tempVertexList.Count - 1)
                        sw.WriteLine("} ");

                }
            }
            // Escribir los indices (No hace falta estructurarlos, solo leerlos)
            using (StreamWriter sw = File.AppendText(meshPath))
            {
                for (int i = 0; i < data.indices.Count; i++)
                {
                    if (i == 0)
                        sw.WriteLine("{ ");

                    sw.WriteLine(data.indices[i]);

                    if (i == data.indices.Count - 1)
                        sw.WriteLine("} ");
                }
            }
            // Escribir las texturas
            using (StreamWriter sw = File.AppendText(meshPath))
            {
                for (int i = 0; i < data.tempTexturesCoords.Count; i++)
                {
                    if (i == 0)
                        sw.WriteLine("{ ");


                    sw.WriteLine("U:/" + data.tempTexturesCoords[i].X + "| V:/" + data.tempTexturesCoords[i].Y);

                    if (i == data.indices.Count - 1)
                        sw.WriteLine("} ");
                }
            }
        }
        
        void CreateMeshTemporalTXTs()
        {
            if (!File.Exists(meshPath))
            {
                // Crear Mesh
                using (StreamWriter sw = File.CreateText(meshPath))
                    sw.WriteLine("");
            }
        }
        #endregion

        #region GetNumbersFromJson
        string GetNumbersFromJson(ref char[] tJs, int i)
        {
            string tempValue = "";
            int j = 0;
            /* Idenitificar X, Y, Z para conseguir el numero asignado a cada una */
            j = i + 3;
            while (true)
            {
                tempValue += tJs[j];

                if (j < tJs.Length - 1)
                    j++;
                else
                    break;

                if (tJs[j] == 124) // 124 = |
                    break;
            }
            return tempValue;
        }
        #endregion
    }
}