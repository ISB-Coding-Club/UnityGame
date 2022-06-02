using System;
using UnityEngine;
using Utilities;

namespace Utilities
{
    public class LoggingSystem
    {
        private bool ForceEnabled;
        private string Name;

        public LoggingSystem(string Name, bool Force) {
            this.ForceEnabled = Force;
            this.Name = Name;
        }

        public LoggingSystem(string Name) {
            this.ForceEnabled = false;
            this.Name = Name;
        }

        public void Info(string Message) {
            if (ForceEnabled || Debug.isDebugBuild) {
                Debug.Log(LogColorizer.Colorize(String.Format("[:cyan:b;{0} -> Info:green:b;]:cyan:b; {1}:white:b;", Name.Replace(":", "__colon__"), Message.Replace(":", "__colon__"))).Replace("__colon__", ":"));
            }
        }

        public void Warn(string Message) {
            if (ForceEnabled || Debug.isDebugBuild) {
                Debug.Log(LogColorizer.Colorize(String.Format("[:gold:b;{0} -> Warn:yellow:b;]:gold:b; {1}:white:b;", Name.Replace(":", "__colon__"), Message.Replace(":", "__colon__"))).Replace("__colon__", ":"));
            }
        }

        public void Error(string Message) {
            if (ForceEnabled || Debug.isDebugBuild) {
                Debug.Log(LogColorizer.Colorize(String.Format("[:red:b;{0} -> Error:red:b;]:red:b; {1}:white:b;", Name.Replace(":", "__colon__"), Message.Replace(":", "__colon__"))).Replace("__colon__", ":"));
            }
        }
    }
}