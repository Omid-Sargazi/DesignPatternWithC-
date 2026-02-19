using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsInCSharp.Bridge.IRenderer
{
    public interface IRenderer
    {
        void RenderCircle(float radius);
        void RenderSquare(float side);
    }

    public class OpenGLRenderer : IRenderer
    {
        public void RenderCircle(float r) => Console.WriteLine($"OpenGL: Drawing circle r={r}");
        public void RenderSquare(float s) => Console.WriteLine($"OpenGL: Drawing square s={s}");
    }

    public class DirectXRenderer : IRenderer
    {
        public void RenderCircle(float r) => Console.WriteLine($"DirectX: Drawing circle r={r}");
        public void RenderSquare(float s) => Console.WriteLine($"DirectX: Drawing square s={s}");
    }
}
