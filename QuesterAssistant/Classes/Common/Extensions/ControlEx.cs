using System.Windows.Forms;

namespace QuesterAssistant.Classes.Common.Extensions
{
    internal static class ControlEx
    {
        public static void BindAdd<TEditor, TSource>
            (this TEditor editor, TSource source, string editorProperty, string sourceProperty, DataSourceUpdateMode updateMode = DataSourceUpdateMode.OnPropertyChanged)
                where TEditor : IBindableComponent
                where TSource : BindingSource
        {
            editor.DataBindings.Add(editorProperty, source, sourceProperty, false, updateMode);
        }
    }
}
