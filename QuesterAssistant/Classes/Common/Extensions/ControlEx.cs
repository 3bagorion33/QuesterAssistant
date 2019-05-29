using System.Windows.Forms;

namespace QuesterAssistant.Classes.Common.Extensions
{
    internal static class ControlEx
    {
        public static void BindAdd<TEditor>
            (this TEditor editor, object source, string editorProperty, string sourceProperty, DataSourceUpdateMode updateMode = DataSourceUpdateMode.OnPropertyChanged)
                where TEditor : IBindableComponent
        {
            editor.DataBindings.Add(editorProperty, source, sourceProperty, false, updateMode);
        }
    }
}
