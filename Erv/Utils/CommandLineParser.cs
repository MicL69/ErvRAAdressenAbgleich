using System;
using System.Collections.Generic;
using System.Linq;

namespace Erv.Utils
{
    internal static class CommandLineParser
    {
        #region properties
        public static Dictionary<string, string> CommandLineArgs { get; } = new Lazy<Dictionary<string, string>>(() => new Dictionary<string, string>()).Value;
        #endregion properties

        #region cstor

        static CommandLineParser() {
            ParseCommandLineArgs();
        }
        #endregion cstor

        #region methods
        private static void ParseCommandLineArgs() {
            var args = Environment.GetCommandLineArgs().Skip(1).ToList();
            if (!args.Any()) return;

            ParseArgs(args);
        }

        public static void ParseArgs(List<string> args) {
            if (!args.Any()) return;

            args.ForEach(a => {
                if (!a.StartsWith("/")) {
                    CommandLineArgs.Add(a, a);
                } else {
                    var splits = a.Split(new[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                    if (splits[0].StartsWith("/")) splits[0] = splits[0].Substring(1);

                    var key = splits[0].ToLower();
                    if (CommandLineArgs.ContainsKey(key)) {
                        CommandLineArgs[key] = splits.Length > 1 ? string.Join(":", splits.Skip(1)) : string.Empty;
                    } else {
                        CommandLineArgs.Add(key, splits.Length > 1 ? string.Join(":", splits.Skip(1)) : string.Empty);
                    }
                }
            });
        }
        #endregion methods
    }

}
