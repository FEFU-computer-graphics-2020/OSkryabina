using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL
{
    public class Program
    {
        public static void Main()
        {
            var application = new Application(800, 600, "OpenGl");
            application.Run(60);
        }
    }
}