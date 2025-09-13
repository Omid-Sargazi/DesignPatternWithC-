using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsWithC_.Composite
{
    public interface IWebComponent
    {
        string Id { get; set; }
        string CssClass { get; set; }
        string Style { get; set; }

        string Render();
        string GetHTML();
    }

    public class TextElement : IWebComponent
    {
        public string Id { get; set; }
        public string CssClass { get; set; }
        public string Style { get; set; }
        public string Content { get; set; }
        public string Render()
        {
            return $"<span id='{Id}' class='{CssClass}' style='{Style}'>{Content}</span>";
        }

        public string GetHTML() => Render();
    }

    public class ImageElement : IWebComponent
    {
        public string Id { get; set; }
        public string CssClass { get; set; }
        public string Style { get; set; }
        public string Src { get; set; }
        public string Alt { get; set; }
        public string Render()
        {
            return $"<img id='{Id}' class='{CssClass}' style='{Style}' src='{Src}' alt='{Alt}'>";
        }

        public string GetHTML() => Render();
    }

    public class Container : IWebComponent
    {
        protected List<IWebComponent> _children = new List<IWebComponent>();
        public string Id { get; set; }
        public string CssClass { get; set; }
        public string Style { get; set; }
        public string TagName { get; set; } = "div";

        public void AddComponent(IWebComponent component)
        {
            _children.Add(component);
        }

        public void RemoveComponent(IWebComponent component)
        {
            
            _children.Remove(component);
        }
        public string Render()
        {
            var childrenHtml = string.Join("", _children.Select(c => c.Render()));
            return $"<{TagName} id='{Id}' class='{CssClass}' style='{Style}'>{childrenHtml}</{TagName}>";
        }

        public string GetHTML()
        {
            return Render();
        }

        public IEnumerable<IWebComponent> GetComponents()
        {
            return _children.AsReadOnly();
        }
    }
}

