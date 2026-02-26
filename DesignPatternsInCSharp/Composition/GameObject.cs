using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsInCSharp.Composition
{
    public class GameObject
    {
        public IRenderer Renderer { get; set; }
        public IPhysics Physics { get; set; }

        public void Update()
        {
            Physics?.Update();
            Renderer?.Render();
        }
    }

    public interface IRenderer { void Render(); }
    public interface IPhysics { void Update(); }

}
