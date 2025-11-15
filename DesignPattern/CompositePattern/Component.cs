using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.CompositePattern
{
    public interface IComponent
    {
        int GetSize();
        string GetName();
    }

    public class Leaf : IComponent
    {
        public string Name;
        public int Size;

        public Leaf(int size, string name)
        {
            Size = size;
            Name = name;
        }
        public int GetSize()
        {
            return Size;
        }

        public string GetName()
        {
            return Name;
        }
    }

    public class Composite : IComponent
    {
        private string Name;
        private List<IComponent> _components = new List<IComponent>();

        public Composite(string name)
        {
            Name = name;
        }

        public void AddComposite(IComponent component)
        {
            _components.Add(component);
        }

        public void RemoveComponent(IComponent component)
        {
            _components.Remove(component);
        }
        public int GetSize()
        {
            int totalSize = 0;
            foreach (var component in _components)
            {
                totalSize += component.GetSize();
            }
            return totalSize;
        }

        public string GetName()
        {
            return Name;
        }
    }
}
