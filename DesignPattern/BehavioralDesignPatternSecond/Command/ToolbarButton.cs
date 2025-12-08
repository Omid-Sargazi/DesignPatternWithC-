using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.BehavioralDesignPatternSecond.Command
{
    public interface ICommand2
    {
        void Execute();
        void Undo();
    }

    public class Document
    {
        private string _content = "";
        private Stack<string> _history = new Stack<string>();

        public void Write(string text)
        {
            _history.Push(_content);
            _content += text;
            Console.WriteLine($"Document content: {_content}");
        }

        public void Erase(int length)
        {
            _history.Push(_content);
            if (length >= _content.Length)
                _content = "";
            else
                _content = _content.Substring(0, _content.Length - length);
            Console.WriteLine($"Document content: {_content}");
        }

        public void Restore(string previousContent)
        {
            _content = previousContent;
            Console.WriteLine($"Document content: {_content}");
        }

        public string GetContent() => _content;
    }
}
