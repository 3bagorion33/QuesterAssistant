using System;
using System.ComponentModel;
using System.Drawing.Design;
using QuesterAssistant.Classes;
using QuesterAssistant.Panels;
using Task = System.Threading.Tasks.Task;

namespace QuesterAssistant.UIEditors
{
    internal class DebugActionEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (context.Instance is IDebugAction action)
            {
                action.GatherDebugInfos();
                Task.Factory.StartNew(() => QMessageBox.ShowInfo(action.GetDebugInfos()));
            }
            return value;
        }
    }
}