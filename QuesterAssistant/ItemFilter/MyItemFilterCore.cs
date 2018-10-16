using Astral;
using Astral.Classes;
using Astral.Classes.ItemFilter;
using Astral.Professions.Functions;
using Astral.Controllers;
using Astral.Logic.Classes.Map;
using Astral.Logic.NW;
using Astral.Quester.Classes;
using Astral.Quester.UIEditors;
using MyNW.Classes;
using MyNW.Internals;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace QuesterAssistant.MyItemFilter
{
    class MyItemFilterCore
    {
        // Properties
        List<MyItemFilterEntry> Entries { get; set; } = new List<MyItemFilterEntry>();

        // Constructor
        internal MyItemFilterCore(ItemFilterCore filterCore)
        {
            using (List<ItemFilterEntry>.Enumerator enumerator = filterCore.Entries.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    MyItemFilterEntry myItemFilterEntry = new MyItemFilterEntry(enumerator.Current);
                    Entries.Add((MyItemFilterEntry)enumerator.Current);
                }
            }
        }

        // Translator
        public static explicit operator MyItemFilterCore(ItemFilterCore filterCore)
        {
            return new MyItemFilterCore(filterCore);
        }

        // method_0
        internal bool IsMatch(Item item)
        {
            bool flag = false;
            using (List<MyItemFilterEntry>.Enumerator enumerator = this.Entries.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    MyItemFilterEntry current = enumerator.Current;
                    if (current.StrType(item))
                    {
                        switch (current.Mode)
                        {
                            case ItemFilterMode.Include:
                                Core.DebugWriteLine("Item Include!");
                                flag = true;
                                break;
                            case ItemFilterMode.Exclude:
                                flag = false;
                                break;
                        }
                    }
                }
            }
            return flag;
        }
    }
}
