using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using MyNW.Internals;
using QuesterAssistant.UIEditors.Forms;

namespace QuesterAssistant.UIEditors
{
    class PositionNodeEditor : UITypeEditor
    {
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            while (TargetSelectForm.TargetGuiRequest("Target the node and press ok.") == DialogResult.OK)
            {
                if (EntityManager.LocalPlayer.Player.InteractStatus.pMouseOverNode != IntPtr.Zero)
                {
                    return EntityManager.LocalPlayer.Player.InteractStatus.TargetableNodes
                               .Find(n => n.IsValid && n.IsMouseOver)?.WorldInteractionNode.Location.Clone() ?? value;
                }
            }
            return value;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
    }
}
