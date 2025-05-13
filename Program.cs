using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Different sub/constructor classes working together to make the project function
          new voice_greeting() { };
            new AsciiArt() { };
            new UserInterface() { };
            new Answers_Function() { };
            new memoryStorage()
        }


    }
}
