using System;
using OpenGL;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace OpenGL
{
    class Application : GameWindow
    {
        public Application(int width, int height, string title) : base(width, height, GraphicsMode.Default, title)
        {

        }

        float[] vertices = {
            -0.5f, -0.5f, -0.0f, //Bottom-left vertex
            0.5f, -0.5f, 0.0f, //Bottom-right vertex
            0.0f,  0.5f, 0.0f  //Top vertex
        };

        float[] colors = {
            1.5f, 0.5f, 0.0f, //Bottom-left vertex
            0.5f, 1.5f, 0.0f, //Bottom-right vertex
            0.0f, 0.5f, 1.0f  //Top vertex
        };

        int VertexBufferObject; // VBO
        int ColorBufferObject;
        int VertexArrayObject; // VAO

        private Shader shader;

        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(0.3f, 0.3f, 0.3f, 1.0f);

            VertexArrayObject = GL.GenVertexArray();
            VertexBufferObject = GL.GenBuffer();
            ColorBufferObject = GL.GenBuffer();

            GL.BindVertexArray(VertexArrayObject);

            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, sizeof(float) * 3, 0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, ColorBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, colors.Length * sizeof(float), colors, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, sizeof(float) * 3, 0);

            GL.EnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(1);

            shader = new Shader("shader.v", "shader.f");

            base.OnLoad(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            shader.Use();
            GL.BindVertexArray(VertexArrayObject);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

            Context.SwapBuffers();

            base.OnRenderFrame(e);
        }
    }
}
