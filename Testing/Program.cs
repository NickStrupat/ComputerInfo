using System;
using System.Reflection;
using NickStrupat;

namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            var ci = new ComputerInfo();
            foreach (var prop in typeof(ComputerInfo).GetProperties(BindingFlags.Instance | BindingFlags.Public))
                Console.WriteLine(prop.Name + ":\t" + prop.GetValue(ci));
        }
    }
}
