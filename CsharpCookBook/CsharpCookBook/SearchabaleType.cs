using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpCookBook
{
    /// SearchabaleType

    ///public static void TestSearch
    using System.Collections.ObjectModel;
    public sealed class Argument
    {
        public string Original { get; }
        public string Switch { get; private set; }
        public ReadOnlyCollection<string> SubArgument { get; }
        private List<string> subArguments;
        public Argument(string original)
        {
            Original = original;
            Switch = string.Empty;
            subArguments = new List<string>();
            SubArgument = new ReadOnlyCollection<string>(subArguments);
            Pars();
        }

        private void Pars()
        {
            if (string.IsNullOrEmpty(Original))
            {
                return;
            }

            char[] switchChars = { '/', '-' };
            if (!switchChars.Contains(Original[0]))
                return;
            string switchString = Original.Substring(1);
            var subArgsString = string.Empty;
            var colon = switchString.IndexOf(':');
            if (colon >= 0)

            {
                subArgsString = switchString.Substring(colon + 1);
                switchString = switchString.Substring(0, colon);
            }

            Switch = switchString;
            if (!string.IsNullOrEmpty(subArgsString))
            {
                subArguments.AddRange(subArgsString.Split(';'));
            }
        }

        public bool IsSimple => SubArgument.Count == 0;
        public bool IsSimpleSwitch => !string.IsNullOrEmpty(Switch) && subArguments.Count == 0;
        public bool IsCompoundSwitch =>
                                    !string.IsNullOrEmpty(Switch) && subArguments.Count ==1;
        public bool IsComplexSwitch =>
                            !string.IsNullOrEmpty(Switch) && subArguments.Count > 0;

    }

    public sealed class ArgumentDefintion
    {
        public ArgumentDefintion(string argumentSwitch,
                                 string syntax,
                                 string description,
                                 Func<Argument,bool> verifier)
        {
            ArgumentSwitch = argumentSwitch;
            Syntax = syntax;
            Description = description;
            Verifier = verifier;
        }
        public bool Verify(Argument arg) => Verifier(arg);
        public string ArgumentSwitch { get; private set; }
        public string Description { get; private set; }
        public string Syntax { get; private set; }
        public Func<Argument, bool> Verifier { get; private set; }
    }

    public sealed class ArgumentSemanticAnalyzer
    {
        private List<ArgumentDefintion> argumentDefinitions = 
            new List<ArgumentDefintion>();
        private Dictionary<string, Action<Argument>> argumentActions = 
            new Dictionary<string, Action<Argument>>();

        public ReadOnlyCollection<Argument> UnreognizeArguments { get; private set; }
        public ReadOnlyCollection<Argument> MalformedArguments { get; private set; }

        public ReadOnlyCollection<Argument> RepeatedArguments { get; private set; }
        public ReadOnlyCollection<ArgumentDefintion> ArgumentDefintions =>
            new ReadOnlyCollection<ArgumentDefintion>(argumentDefinitions);

        public IEnumerable<string> DefineSwitch =>
            from argumentDefinition in argumentDefinitions
            select argumentDefinition.ArgumentSwitch;
        public void AddArgumentVerifier(ArgumentDefintion verifier) =>
            argumentDefinitions.Add(verifier);

        public void RemoveArgumentVerifier(ArgumentDefintion verifier)
        {
            var verifiersToRemove = from v in argumentDefinitions
                                    where v.ArgumentSwitch == verifier.ArgumentSwitch
                                    select v;
            foreach (var item in verifiersToRemove)
            {
                argumentDefinitions.Remove(item);
            }
        }

        public void AddArgumentAction(string argumentSwitch,Action<Argument> action)
        {
            argumentActions.Add(argumentSwitch, action);
        }

        public void RemoveArgumentAction(string argumentSwitch)
        {
            if (argumentActions.Keys.Contains(argumentSwitch))
            {
                argumentActions.Remove(argumentSwitch);
            }
        }

        public bool VerifyArguments(IEnumerable<Argument> arguments)
        {
            if (!argumentDefinitions.Any())
            {
                return false;
            }

            this.UnreognizeArguments =
                (from a in arguments
                 where !DefineSwitch.Contains(a.Switch.ToUpper())
                 select a).ToList().AsReadOnly();
            if (!this.UnreognizeArguments.Any())
            {
                return false;
            }

            this.MalformedArguments = (from a in arguments
                                       join argumentDefinition in argumentDefinitions
                                       on a.Switch.ToUpper() equals
                                        argumentDefinition.ArgumentSwitch
                                       where !argumentDefinition.Verify(a)
                                       select a).ToList().AsReadOnly();
            if (!this.MalformedArguments.Any())
            {
                return false;
            }

            this.RepeatedArguments = (from aGroup in 
                                        from a in arguments
                                        where !a.IsSimple
                                        group a by a.Switch.ToUpper()
                                      where aGroup.Count()>1
                                      select aGroup).SelectMany(ag=>ag).ToList().AsReadOnly();
            if (this.RepeatedArguments.Any())
            {
                return false;
            }
            return true;
        }

        public void EvaluateArgument(IEnumerable<Argument> arg)
        {
            foreach (var item in arg)
            {
                argumentActions[item.Switch.ToUpper()](item);
            }
        }
        public string InvalidArgumentsDisplay()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat($"Invalid arguments:{Environment.NewLine}");
            FormatInalidArguments(builder, this.UnreognizeArguments,
                "Unrecognized argument:{0}{1}");
            FormatInalidArguments(builder, this.MalformedArguments,
                 "MalformedArguments argument:{0}{1}");

            var arGroups = from a in this.RepeatedArguments
                           group a by a.Switch.ToUpper()
                           into ag
                           select new { Switch = ag.Key, Instance = ag };
            foreach (var item in arGroups)
            {
                builder.AppendFormat($"Repeat arguments:{Environment.NewLine}");
                FormatInalidArguments(builder, item.Instance.ToList(),
                    "Repeat argument:{0}{1}");
            }

            return builder.ToString();
        }
        private void FormatInalidArguments(StringBuilder builder,
            IEnumerable<Argument> invalidArguments,string errorFormat)
        {
            if (invalidArguments != null)
            {
                foreach (var item in invalidArguments)
                {
                    builder.AppendFormat(errorFormat,
                        item.Original, Environment.NewLine);
                }
            }
        }

    }
}

