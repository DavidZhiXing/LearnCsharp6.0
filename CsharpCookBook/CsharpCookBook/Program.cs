using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpCookBook
{
    class Program
    {
        static void Main(string[] args)
        {
            var arguments = (from a in args
                             select new Argument(a)).ToArray();
            Console.Write("Command Line:");
            foreach (var item in arguments)
            {
                Console.WriteLine($"{item.Original}");
            }
            Console.WriteLine("");
           // Console.ReadLine();

            ArgumentSemanticAnalyzer analyzer = new ArgumentSemanticAnalyzer();
            analyzer.AddArgumentVerifier(
                new ArgumentDefintion("output",
                    "/output:[path to output]",
                    "Specifies the location of the output file.",
                    x => x.IsComplexSwitch));
            analyzer.AddArgumentVerifier(
                new ArgumentDefintion("trialMode",
                    "/trialMode",
                    "If this is specified it places the product into trial mode.",
                    x => x.IsSimpleSwitch));
            analyzer.AddArgumentVerifier(
                    new ArgumentDefintion("DEBUGOUTPUT",
                        "/debugoutput:[value1];[value2];[value3];",
                        "A listing of the files the debug output.",
                        x => x.IsComplexSwitch));
            analyzer.AddArgumentVerifier(
                    new ArgumentDefintion("",
                        "[literal value]",
                        "literal value.",
                        x => x.IsSimpleSwitch));
            if (!analyzer.VerifyArguments(arguments))
            {
                string invalidArguments = analyzer.InvalidArgumentsDisplay();
                Console.WriteLine(invalidArguments);
                ShowUsage(analyzer);
                Console.ReadLine();
                return;
            }
            string output = string.Empty;
            bool trialmode = false;
            IEnumerable<string> debugOutput = null;
            List<string> literals = new List<string>();
            analyzer.AddArgumentAction("OUTPUT",x => { output = x.SubArgument[0]; });
            analyzer.AddArgumentAction("TRIALMODE", x => { trialmode = true; });

            analyzer.AddArgumentAction("DEBUGOUTPUT", x => { debugOutput = x.SubArgument;});
            analyzer.AddArgumentAction("", x => { literals.Add(x.Original); });

            analyzer.EvaluateArgument(arguments);

            Console.WriteLine("");
            Console.WriteLine($"OUTPUT:{output}");
            Console.WriteLine($"TRIALMODE:{trialmode}");
            if (debugOutput != null)
            {
                foreach (var item in debugOutput)
                {
                    Console.WriteLine($"DEBUGOUTPUT:{item}");

                }
            }
            foreach (var item in literals)
            {
                Console.WriteLine($"LITERALS:{item}");

            }
            Console.ReadLine();
        }

        private static void ShowUsage(ArgumentSemanticAnalyzer analyzer)
        {
            Console.WriteLine("Program.exe allows the following arguments:");
            foreach (var item in analyzer.ArgumentDefintions)
            {
                Console.WriteLine($"\t{item.ArgumentSwitch}:({ item.Description}){ Environment.NewLine}\tSyntax: { item.Syntax} ");
            }

        }
    }
}
