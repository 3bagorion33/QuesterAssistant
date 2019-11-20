using System.Windows.Forms;

namespace QuesterAssistant.Classes.Extensions
{
    internal static class ControlEx
    {
        public static void BindAdd<TEditor>
            (this TEditor editor, object source, string editorProperty, string sourceProperty, DataSourceUpdateMode updateMode = DataSourceUpdateMode.OnPropertyChanged)
                where TEditor : IBindableComponent
        {
            if (source != null)
            {
                editor.DataBindings.Add(editorProperty, source, sourceProperty, false, updateMode);
            }
        }
    }
}
