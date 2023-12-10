using OpenTK;
using OpenTK.Input;
using System;
using System.Windows.Forms;

namespace Frontend.Renderer
{
    internal class Camera
    {
        // Constantes
        private float SPEED = 10f;
        private float SCREENWIDTH;
        private float SCREENHEIGHT;
        private float SENSITIVITY = 20.0f;


        public Vector3 position;

        public Vector3 up    =  Vector3.UnitY;
        public Vector3 front = -Vector3.UnitZ;
        public Vector3 right =  Vector3.UnitX;


        // Rotaciones //
        private float pitch;
        private float yaw = -90f;

        private bool firstMove = true;
        public Vector2 lastPosition;

        public Camera(float width, float height, Vector3 position) 
        {
            SCREENWIDTH = width;
            SCREENHEIGHT = height;
            this.position = position;
        }

        public Matrix4 GetViewMatrix()
        {
            return Matrix4.LookAt(position, position + front, up);
        }

        public Matrix4 GetProjectionMatrix() 
        {
            return Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), SCREENWIDTH / SCREENHEIGHT, 0.1f, 1000.0f);
        }

        private void UpdateVectors()
        {
            if (pitch > 89.0f)
                pitch = 89.0f;

            if (pitch < -89.0f)
                pitch = -89.0f;


            front.X = (float)(Math.Cos(MathHelper.DegreesToRadians(pitch)) * Math.Cos(MathHelper.DegreesToRadians(yaw)));
            front.Y = (float)Math.Sin(MathHelper.DegreesToRadians(pitch));
            front.Z = (float)(Math.Cos(MathHelper.DegreesToRadians(pitch)) * Math.Sin(MathHelper.DegreesToRadians(yaw)));

            front = Vector3.Normalize(front);

            right = Vector3.Normalize(Vector3.Cross(front, Vector3.UnitY));
            up = Vector3.Normalize(Vector3.Cross(right, front));

        }

        public void InputController(KeyboardState input, MouseState mouse, FrameEventArgs e)
        {
            if (input.IsKeyDown(Key.W))
                position += front * SPEED * (float)e.Time;
            
            if(input.IsKeyDown(Key.A))
                position -= right * SPEED * (float)e.Time;
            
            if(input.IsKeyDown(Key.S))
                position -= front * SPEED * (float)e.Time;
            
            if(input.IsKeyDown(Key.D))
                position += right * SPEED * (float)e.Time;

            if (input.IsKeyDown(Key.Space) || input.IsKeyDown(Key.E))
                position.Y += SPEED * (float)e.Time;

            if (input.IsKeyDown(Key.ShiftLeft) || input.IsKeyDown(Key.Q))
                position.Y -= SPEED * (float)e.Time;

            if (firstMove)
            {
                lastPosition = new Vector2(mouse.X, mouse.Y);
                firstMove = false;
            }
            else 
            {
                var deltaX = mouse.X - lastPosition.X;
                var deltaY = mouse.Y - lastPosition.Y;

                lastPosition = new Vector2(mouse.X, mouse.Y);

                yaw += deltaX * SENSITIVITY * (float)e.Time;
                pitch -= deltaY * SENSITIVITY * (float)e.Time;
            }
            UpdateVectors();
        }

        public void Update(KeyboardState input, MouseState mouse, FrameEventArgs e) 
        {
            InputController(input, mouse, e); 
        }
    }
}
