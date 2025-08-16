using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DesignPatternsWithC_.StructuralPatterns.BridgePattern.GraphicRender;

namespace DesignPatternsWithC_.StructuralPatterns.BridgePattern
{
    public class GraphicRender
    {
       
        public interface IRenderer
        {
            void RenderCircle(float radius);
            void RenderSquare(float side);
        }
        public class WindowsRender : IRenderer
        {
            public void RenderCircle(float radius)
            {
                Console.WriteLine($"Rendering circle with radius {radius} on Windows.");
            }

            public void RenderSquare(float side)
            {
                Console.WriteLine($"Rendering square with side {side} on Windows.");
            }
        }

        public class LinuxRenderer : IRenderer
        {
            public void RenderCircle(float radius)
            {
                Console.WriteLine($"Rendering circle with radius {radius} on Linux.");
            }

            public void RenderSquare(float side)
            {
                Console.WriteLine($"Rendering square with side {side} on Linux.");
            }
        }

        public abstract class Shape
        {
            protected IRenderer _renderer;
            protected Shape(IRenderer renderer)
            {
                _renderer = renderer;
            }

            public abstract void Draw();
        }

        public class Circle : Shape
        {
            private float _radius;
            public Circle(IRenderer renderer, float radius) : base(renderer)
            {
                _radius = radius;
            }

            public override void Draw()
            {
                _renderer.RenderCircle( _radius );
            }
        }

        public class Square : Shape
        {
            private float _side;
            public Square(IRenderer renderer,float side) : base(renderer)
            {
            }

            public override void Draw()
            {
                _renderer.RenderSquare( _side );
            }
        }
    }

   
   
}
