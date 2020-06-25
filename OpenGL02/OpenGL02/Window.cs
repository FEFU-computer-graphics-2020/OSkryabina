using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.Runtime.CompilerServices;
using ImGuiNET;

namespace OpenGL02
{
    class Window : GameWindow
    {
        public Window(int width, int height, string title) : base(width, height, GraphicsMode.Default, title)
        {

        }

        int VertexBufferObject; // VBO
        int VertexArrayObject; // VAO
        int IndexBufferObject; // IBO

        private Mesh mesh;

        private Shader shader;

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
            controller.SetWindowSize(Width, Height);

            base.OnResize(e);
        }

        private ImGuiController controller;

        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(0.02f, 0.02f, 0.02f, 1.0f);
            GL.Enable(EnableCap.DepthTest);

            shader = new Shader("C:/Users/INIPSO/source/repos/OpenGL02/OpenGL02/shaders/shader.v", "C:/Users/INIPSO/source/repos/OpenGL02/OpenGL02/shaders/shader.f");

            controller = new ImGuiController();

            VertexArrayObject = GL.GenVertexArray();
            VertexBufferObject = GL.GenBuffer();

            GL.BindVertexArray(VertexArrayObject);

            mesh = MeshLoader.LoadMesh("C:/Users/INIPSO/source/repos/OpenGL02/OpenGL02/mesh/sss.obj");

            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, mesh.Vertices.Length * Unsafe.SizeOf<Vertex>(), mesh.Vertices, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(shader.GetAttributeLocation("aPosition"), 3, VertexAttribPointerType.Float, false, Unsafe.SizeOf<Vertex>(), 0);
            GL.VertexAttribPointer(shader.GetAttributeLocation("aColor"), 3, VertexAttribPointerType.Float, false, Unsafe.SizeOf<Vertex>(), Unsafe.SizeOf<Vector3>());
            GL.EnableVertexAttribArray(shader.GetAttributeLocation("aPosition"));
            GL.EnableVertexAttribArray(shader.GetAttributeLocation("aColor"));

            IndexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, IndexBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, mesh.Indeces.Length * sizeof(int), mesh.Indeces, BufferUsageHint.StaticDraw);

            base.OnLoad(e);
        }

        private float scale = 1.0f;
        private float angle = 0.0f;
        private float angle_z = 0.0f;
        private float dist = 5.0f;
        private bool persp = false;

        protected override void OnRenderFrame(FrameEventArgs e)
        {

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            shader.Use();

            controller.NewFrame(this);

            ImGui.SliderFloat("Scale", ref scale, 0, 10);
            ImGui.SliderFloat("Angle", ref angle, -3.14f, 3.14f);
            ImGui.SliderFloat("Angle_z", ref angle_z, -3.14f, 3.14f);
            ImGui.SliderFloat("Distance", ref dist, 0, 10.0f);
            ImGui.Checkbox("Perspective", ref persp);

            shader.SetUniform("scaleFactor", scale);

            var model = Matrix4.CreateRotationY(angle) * Matrix4.CreateRotationX(angle_z) * Matrix4.CreateTranslation(0, 0, -dist);

            shader.SetUniform("model", model);

            var projection = persp
                ? Matrix4.CreatePerspectiveFieldOfView((float)(Math.PI / 2), (float)Width / Height, 0.1f, 100.0f)
                : Matrix4.CreateOrthographic(10, 10, -7, 7);

            shader.SetUniform("projection", projection);

            GL.DrawElements(PrimitiveType.Triangles, mesh.Indeces.Length, DrawElementsType.UnsignedInt, 0);

            controller.Render();

            Context.SwapBuffers();
            base.OnRenderFrame(e);
        }
    }
}