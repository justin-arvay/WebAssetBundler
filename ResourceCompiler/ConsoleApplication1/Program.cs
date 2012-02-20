using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write(Path.Combine("path", "file.js"));
            Console.ReadKey(true);
        }
    }
}
