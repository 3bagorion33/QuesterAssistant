using System;
using System.ComponentModel;
using System.Drawing.Design;
using DevExpress.XtraEditors;
using MyNW.Classes;
using MyNW.Internals;
using QuesterAssistant.UIEditors.Forms;

namespace QuesterAssistant.UIEditors
{
    internal class PositionNodeEditor : UITypeEditor
    {
        private Vector3 acquisition()
        {
            var mouseOverNode = EntityManager.LocalPlayer.Player.InteractStatus.MouseOverNode;
            if (mouseOverNode.IsValid)
                return mouseOverNode.Location;
            XtraMessageBox.Show("No node under mouse.");
            return new Vector3();
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            PositionEditorForm.Show(value as Vector3, acquisition);
            return value;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
    }
}
