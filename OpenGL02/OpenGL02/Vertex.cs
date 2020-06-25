using OpenTK;

namespace OpenGL02
{
    public struct Vertex
    {
        public Vertex(Vector3 position, Vector3 color)
        {
            this.position = position;
            this.color = color;
        }

        public Vector3 position;
        public Vector3 color;
    }
}