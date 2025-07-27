using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.BridgePattern
{
    public interface IRenderEngine
    {
        void RenderShape(string shapeName);
    }

    public class OpenGLRender : IRenderEngine
    {
        public void RenderShape(string shapeName)
        {
            Console.WriteLine($"رندر {shapeName} با موتور OpenGL");
        }
    }
    public class DirectXRenderer : IRenderEngine
    {
        public void RenderShape(string shapeName)
        {
            Console.WriteLine($"رندر {shapeName} با موتور DirectX");
        }
    }

    public class VulkanRenderer : IRenderEngine
    {
        public void RenderShape(string shapeName)
        {
            Console.WriteLine($"رندر {shapeName} با موتور Vulkan");
        }
    }

    public abstract class Shape
    {
        protected IRenderEngine _renderEngine;
        protected Shape(IRenderEngine engine)
        {
            _renderEngine = engine;
        }

        public abstract void Draw();
    }

    public class Circle : Shape
    {
        public Circle(IRenderEngine engine) : base(engine)
        {
        }

        public override void Draw()
        {
            _renderEngine.RenderShape("Circle");
        }
    }

    public class Square : Shape
    {
        public Square(IRenderEngine engine) : base(engine)
        {
        }

        public override void Draw()
        {
            _renderEngine.RenderShape("Square");
        }
    }

    public class Triangle : Shape
    {
        public Triangle(IRenderEngine engine) : base(engine)
        {
        }

        public override void Draw()
        {
            _renderEngine.RenderShape("Triangle");
        }
    }
}
