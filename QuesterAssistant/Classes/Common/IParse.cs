using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuesterAssistant.Classes.Common
{
    interface IParse<T> where T : class
    {
        void Parse(T source);
        void Init();
    }
}
