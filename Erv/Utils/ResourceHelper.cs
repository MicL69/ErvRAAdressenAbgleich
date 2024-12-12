using System;
using System.IO;
using System.Reflection;

namespace Erv.Utils
{
    internal class ResourceHelper
    {
        private static readonly object LockObject = new object();
        private const string ResourcePath = "Erv.Resources.";

        public (bool Success, Exception? Exception) EnsureFileExists(string fullName) {
            try {
                lock (LockObject) {
                    if (!File.Exists(fullName)) {
                        var fileName = Path.GetFileName(fullName);
                        ExtractFromResource($"{ResourcePath}{fileName}", fullName);
                    }
                }
            } catch (Exception ex) {
                return (false, ex);
            }
            return (true, null);
        }

        private void ExtractFromResource(string resourceName, string targetName) {
            var asm = Assembly.GetAssembly(this.GetType());

            if (asm == null) {
                return;
            }
            using var fs = asm.GetManifestResourceStream(resourceName);
            if (fs == null) {
                return;
            }
            var buffer = new byte[fs.Length];
            _ = fs.Read(buffer, 0, buffer.Length);
            using var sw = new BinaryWriter(File.Open(targetName, FileMode.OpenOrCreate));
            sw.Write(buffer);
        }
    }
}
