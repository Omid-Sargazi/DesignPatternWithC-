using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsInCSharp.StructuralDesignPattern
{
    public interface IContentComponent
    {
        void Add(IContentComponent  component);
        void Remove(IContentComponent component);
        int GetTotalWords();
        double GetReadingTime(int wordPerMinu = 200);
        void DispalyHirachy(int indent=0);
        List<IContentComponent> Search(string keyWord);
    }

    
}

//2680210255
//P @risa1373