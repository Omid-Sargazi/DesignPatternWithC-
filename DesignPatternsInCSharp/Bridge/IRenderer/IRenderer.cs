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

    public abstract class Shape
    {
        protected IRenderer _renderer;
        public Shape(IRenderer renderer) => _renderer = renderer;
        public abstract void Draw();
    }

    public class Circle : Shape
    {
        private float _radius;
        public Circle(IRenderer renderer, float radius) : base(renderer) => _radius = radius;
        public override void Draw() => _renderer.RenderCircle(_radius);
    }

    public class Square : Shape
    {
        private float _side;
        public Square(IRenderer renderer, float side) : base(renderer) => _side = side;
        public override void Draw() => _renderer.RenderSquare(_side);
    }
}
