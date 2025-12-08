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


    public class WriteCommand : ICommand2
    {
        private Document _document;
        private string _text;

        public WriteCommand(Document document, string text)
        {
            _document = document;
            _text = text;
        }

        public void Execute()
        {
            _document.Write(_text);
        }

        public void Undo()
        {
            // In a real scenario, we'd use proper memento or history tracking
            _document.Erase(_text.Length);
        }
    }

    public class EraseCommand : ICommand2
    {
        private Document _document;
        private int _length;
        private string _erasedText;

        public EraseCommand(Document document, int length)
        {
            _document = document;
            _length = length;
            // Store what we're about to erase for undo
            string current = document.GetContent();
            _erasedText = current.Length > length
                ? current.Substring(current.Length - length)
                : current;
        }

        public void Execute()
        {
            _document.Erase(_length);
        }

        public void Undo()
        {
            _document.Write(_erasedText);
        }
    }

    // 4. Invoker (UI Components, Scheduler, etc.)
    public class Button
    {
        private ICommand2 _command;
        private string _label;

        public Button(string label, ICommand2 command)
        {
            _label = label;
            _command = command;
        }

        public void Click()
        {
            Console.WriteLine($"\nButton '{_label}' clicked:");
            _command.Execute();
        }

        public void SetCommand(ICommand2 command)
        {
            _command = command;
        }
    }

    public class KeyboardShortcut
    {
        private Dictionary<ConsoleKey, ICommand2> _shortcuts = new Dictionary<ConsoleKey, ICommand2>();

        public void RegisterShortcut(ConsoleKey key, ICommand2 command)
        {
            _shortcuts[key] = command;
        }

        public void HandleKeyPress(ConsoleKey key)
        {
            if (_shortcuts.ContainsKey(key))
            {
                Console.WriteLine($"\nShortcut '{key}' pressed:");
                _shortcuts[key].Execute();
            }
        }
    }
}
