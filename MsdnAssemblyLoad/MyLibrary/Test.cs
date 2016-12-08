using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary
{
    public class Test : MarshalByRefObject
    {
        public Test()
        {
            Console.WriteLine($"Ctor of Test is working.");
        }

        public void Hello(string name)
        {
            Console.WriteLine($"Hello {name}! It's instance method which is working in {AppDomain.CurrentDomain}");
        }
    }
}
