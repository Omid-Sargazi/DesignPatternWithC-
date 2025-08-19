using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedCSharp.Delegate
{
    public delegate void ClickHandler(string message);
    public class Button
    {
        public event ClickHandler OnClick;


        public  void Click(string message)
        {
            if(OnClick!=null)
            {
                OnClick(message);
            }
        }
    }
}
