using DesignPatternsInCSharp.Composition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsInCSharp.CompositionStrategyDecorator
{
    public interface IRenderEngine
    {
        void Draw();
    }

    public class DirectXRenderer : IRenderEngine
    {
        public void Draw() => Console.WriteLine("DX Render");
    }

    public class OpenGLRenderer : IRenderEngine
    {
        public void Draw() => Console.WriteLine("OpenGL Render");
    }

    public class GameObject
    {
        public IRenderEngine Renderer { get; set; }
        public IPhysics Physics { get; set; }

        public void Update()
        {
            Physics?.Update();
            Renderer?.Draw();
        }
    }

}
