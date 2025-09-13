using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsWithC_.Decorator
{
    public interface ITextProcessor
    {
        string Process(string txt);
        string GetDescription();

    }

    public class BasicTextProcessor:ITextProcessor
    {
        public string Process(string txt)
        {
            return txt;
        }

        public string GetDescription()
        {
            return "Process base txt";
        }
    }

    public abstract class TxtProcessDecorator : ITextProcessor
    {
        protected ITextProcessor _textProcessor;

        public TxtProcessDecorator(ITextProcessor textProcessor)
        {
            _textProcessor = textProcessor;
        }

        public virtual string Process(string txt) => _textProcessor.Process(txt);

        public virtual string GetDescription() => _textProcessor.GetDescription();
    }

    public class CompressionDecorator : TxtProcessDecorator
    {
        public CompressionDecorator(ITextProcessor textProcessor) : base(textProcessor)
        {
        }

        public override string Process(string txt)
        {
            
            return base.Process(txt) +"Compress Txt";
        }

        public override string GetDescription()
        {
            return base.GetDescription() + "Compress";
        }

        private string Compress(string txt)
        {
            return $"compressed {txt}";
        }
    }

    public class EncryptionDecorator : TxtProcessDecorator
    {
        public EncryptionDecorator(ITextProcessor textProcessor) : base(textProcessor)
        {
        }

        public override string GetDescription()
        {
            return base.GetDescription() +"Encription";
        }

        public override string Process(string txt)
        {
            return base.Process(txt) + "Encript";
        }
    }
}
