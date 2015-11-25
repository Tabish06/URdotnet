using RDotNet;
using RDotNet.NativeLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            REngine.SetEnvironmentVariables(); // <-- May be omitted; the next line would call it.
            REngine engine = REngine.GetInstance();
            engine.Evaluate("load('~/Shiny.RData')");
            Console.WriteLine("Enter Date:");
            engine.Evaluate("mumpsData<-seasonalData('Mumps')");

            engine.Evaluate("dengueData<-seasonalData('Dengue.Fever')");
            string date1 = Console.ReadLine();
            
            //<--CharacterVector date_cv = engine.CreateCharacter(date1);
            
            engine.Evaluate("date<-'"+date1+"'");
            /*var date_in_r = engine.Evaluate("date");
            Console.WriteLine("Date in R"+date_in_r);*/
            engine.Evaluate("weekno<-strftime(as.Date(date),'%w')");
            
            var m = "Mumps Danger " + engine.Evaluate("length(mumpsData[mumpsData==weekno])>=1").AsCharacter()[0];
            var d = "Dengue Danger " + engine.Evaluate("length(dengueData[dengueData==weekno])>=1").AsCharacter()[0];

            Console.WriteLine(m);
            Console.WriteLine(d);
            // A somewhat contrived but customary Hello World:
            /*CharacterVector charVec = engine.CreateCharacterVector(new[] { "Hello, R world!, .NET speaking" });
            engine.SetSymbol("greetings", charVec);
            
            engine.Evaluate("str(greetings)"); // print out in the console
            string[] a = engine.Evaluate("'Hi there .NET, from the R engine'").AsCharacter().ToArray();
            Console.WriteLine("R answered: '{0}'", a[0]);*/
            Console.WriteLine("Press any key to exit the program");
            Console.ReadKey();
            engine.Dispose();
        }
    }
}
