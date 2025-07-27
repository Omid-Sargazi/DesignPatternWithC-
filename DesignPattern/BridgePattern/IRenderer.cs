using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.BridgePattern
{
    public interface IRenderer
    {
        void RenderCircle(float radius);
        void RenderRectangle(float side);
        void RenderTriangle(float side, float height);
    }

    public class WinRender : IRenderer
    {
        public void RenderCircle(float radius)
        {
            throw new NotImplementedException();
        }

        public void RenderRectangle(float side)
        {
            throw new NotImplementedException();
        }

        public void RenderTriangle(float side, float height)
        {
            throw new NotImplementedException();
        }
    }

    public class MacRender : IRenderer
    {
        public void RenderCircle(float radius)
        {
            throw new NotImplementedException();
        }

        public void RenderRectangle(float side)
        {
            throw new NotImplementedException();
        }

        public void RenderTriangle(float side, float height)
        {
            throw new NotImplementedException();
        }
    }

    public class WebRender : IRenderer
    {
        public void RenderCircle(float radius)
        {
            throw new NotImplementedException();
        }

        public void RenderRectangle(float side)
        {
            throw new NotImplementedException();
        }

        public void RenderTriangle(float side, float height)
        {
            throw new NotImplementedException();
        }
    }

    public abstract class BaseRender
    {
        protected IRenderer _renderer;
        protected BaseRender(IRenderer renderer)
        {
            _renderer = renderer;
        }

        public abstract void Render();
    }

    public class RenderCircle : BaseRender
    {
        protected float _radius;
       
        public RenderCircle(float radius,IRenderer renderer) : base(renderer)
        {
            _radius= radius;
        }

        public override void Render()
        {
            _renderer.RenderCircle(_radius);
        }
    }

    public class TriangleRender : BaseRender
    {
        protected float _side;
        protected float _height;
        public TriangleRender(float side, float
             height,IRenderer renderer) : base(renderer)
        {
            _side= side;
            _height= height;
        }

        public override void Render()
        {
            _renderer.RenderTriangle(_side, _height);
        }
    }

    public class RectangleRender : BaseRender
    {
        protected float _side;
        public RectangleRender(float side,IRenderer renderer) : base(renderer)
        {
            _side= side;
        }

        public override void Render()
        {
            _renderer.RenderRectangle(_side);
        }
    }
}
