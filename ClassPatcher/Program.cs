using System;
using System.Collections.Generic;
using Mono.Cecil;

namespace ClassPatcher
{
    class Program
    {
        static void Main(string[] args)
        {
            AssemblyDefinition adef;
            string action = args[0];

            if (args.Length == 0) return;

            if (args.Length == 1)
            { adef = AssemblyDefinition.ReadAssembly("Astral.exe"); }
            else
            { adef = AssemblyDefinition.ReadAssembly(args[1]); }

            /// Тут получается список всех классов приложения. Среди них есть ItemIdFilterEditor
            
            using (IEnumerator<TypeDefinition> enumerator = adef.MainModule.GetTypes().GetEnumerator())
            {
                while (enumerator.MoveNext())
                    if (enumerator.Current.Name == "ItemIdFilterEditor" ||
                        enumerator.Current.Name == "ItemIdEditor" ||
                        enumerator.Current.Name == "GetMailItems" ||
                        enumerator.Current.Name == "Roles" ||
                        enumerator.Current.Name == "NPCVendorInfos" ||
                        enumerator.Current.Name == "BuyOptionsEditor" ||
                        enumerator.Current.Name == "DialogKeyEditor" ||
                        enumerator.Current.Name == "DialogEditor" ||
                        enumerator.Current.Name == "Core" ||
                        enumerator.Current.Name == "ProcessInfos" ||
                        enumerator.Current.Name == "RemoteContactEditor" ||

                        action == "/show")
                    {
                        Console.WriteLine(enumerator.Current.Name);
                        if (action == "/patch" || action == "/view")
                        {
                            Console.WriteLine(enumerator.Current.IsPublic);
                            if (action == "/patch")
                            {
                                enumerator.Current.IsPublic = true;
                                Console.WriteLine(enumerator.Current.IsPublic);
                            }
                        }
                    }
            }
            if (args.Length == 1 && action == "/patch") adef.Write("Astral_updated.exe");
        }
    }
}
