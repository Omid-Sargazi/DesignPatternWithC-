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

    
}
