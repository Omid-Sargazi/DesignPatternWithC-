using DesignPattern.BridgePattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.PatternOfBridge
{
    public interface IRenderer
    {
       void RenderCircle(float radius);
        void RederTriangle(float width, float height);
        void RenderRectangle(float side);
    }

    public class WinRender : IRenderer
    {
        public void RederTriangle(float width, float height)
        {
            Console.WriteLine($"Drawing Triangle on window with width :{width} and height:{height}");
        }

        public void RenderCircle(float radius)
        {
            Console.WriteLine($"Drawing Circle with radius:{radius}");
        }

        public void RenderRectangle(float side)
        {
            Console.WriteLine($"Drawing Rectangl with side:{side}");
        }
    }

    public class MacRendere : IRenderer
    {
        public void RederTriangle(float width, float height)
        {
            Console.WriteLine($"Drawing Triangle on Mac with width :{width} and height:{height}");

        }

        public void RenderCircle(float radius)
        {
            Console.WriteLine($"Drawing Circle with radius in Mac:{radius}");

        }

        public void RenderRectangle(float side)
        {
            Console.WriteLine($"Drawing Rectangl with side in Mac:{side}");

        }
    }

    public abstract class BaseRender
    {
        protected readonly IRenderer _renderer;
        protected BaseRender(IRenderer renderer)
        {
            _renderer = renderer;
        }

        public abstract void Render();
    }

    public class RenderCircle : BaseRender
    {
        private readonly float _radius;
        public RenderCircle(float redius,IRenderer renderer) : base(renderer)
        {
            _radius = redius;
        }

        public override void Render()
        {
            _renderer.RenderCircle(_radius);
        }
    }

    public class RenderTriangle : BaseRender
    {
        private readonly float _width;
        private readonly float _height;
        public RenderTriangle(float width, float height,IRenderer renderer) : base(renderer)
        {
            _width = width;
            _height = height;
        }

        public override void Render()
        {
            _renderer.RederTriangle(_width, _height);
        }
    }

    public class RenderRectangle : BaseRender
    {
        private readonly float _width;
        public RenderRectangle(float width,IRenderer renderer) : base(renderer)
        {
            _width = width;
        }

        public override void Render()
        {
            _renderer.RenderRectangle(_width);
        }
    }
}
