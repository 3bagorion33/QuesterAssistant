using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Launcher.Classes
{
    internal static class Assemblies
    {
        private static bool resolved;
        public static void Resolve()
        {
            if (!resolved)
            {
                AppDomain.CurrentDomain.AssemblyResolve += AssemblyResolve;
                resolved = true;
            }
        }

        private static Assembly AssemblyResolve(object sender, ResolveEventArgs args)
        {
            if (args.Name.StartsWith("DevExpress"))
            {
                DirectoryInfo info = new DirectoryInfo(new DirectoryInfo(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)).Parent.FullName + @"\DevExpress");
                string path = $@"{info}\{args.Name.Split(',')[0]}.dll";

                if (path.EndsWith("resources.dll"))
                {
                    return null;
                }
                if (info.Exists)
                {
                    try
                    {
                        return Assembly.LoadFrom(path);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Unable to find assembly : " + path);
                    }
                }
            }

            if (args.Name.Contains(nameof(QuesterAssistant)))
            {
                string path = $"{new DirectoryInfo(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)).FullName}\\Plugins\\QuesterAssistant.dll";
                try
                {
                    return Assembly.LoadFile(path);
                }
                catch (Exception)
                {
                    MessageBox.Show("Unable to find assembly : " + path);
                }
            }
            return null;
        }
    }
}
