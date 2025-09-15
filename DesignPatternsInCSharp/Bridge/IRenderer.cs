using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsInCSharp.Bridge
{
    public interface IRenderer
    {
        void RenderCircle(float radius);
        void RenderSquare(float  side);
    }

    public class WindowsRender : IRenderer
    {
        
        public void RenderCircle(float radius)
        {
            Console.WriteLine($"Render Circle by Window: radius{radius}");
        }

        public void RenderSquare(float side)
        {
            Console.WriteLine($"Render Square by Window: radius{side}");

        }
    }

    public class MacRender : IRenderer
    {
        public void RenderCircle(float radius)
        {
            Console.WriteLine($"Render Circle by Mac: radius{radius}");
        }

        public void RenderSquare(float side)
        {
            Console.WriteLine($"Render Square by Mac: radius{side}");

        }
    }

    public abstract class BaseRender
    {
        protected readonly IRenderer _renderer;
        public string Color { get; set; }
        public float PositionX { get; set; }
        public float PositionY { get; set; }

        public BaseRender(IRenderer render)
        {
            _renderer = render;
            Color = "BLack";
        }

        public abstract void Draw();

    }

    public class Circle : BaseRender
    {
        private float _radius;

      
        public Circle(IRenderer render,float radius) : base(render)
        {
            _radius = radius;
        }


        public override void Draw()
        {
            _renderer.RenderCircle(_radius);
        }
    }

    public class Rectangle : BaseRender
    {
        private float _side;

       
        public Rectangle(IRenderer render, float side) : base(render)
        {
            _side = side;
        }

        public override void Draw()
        {
            _renderer.RenderSquare(_side);
        }
    }
}
