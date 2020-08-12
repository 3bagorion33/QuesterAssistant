using Astral.Classes.ItemFilter;
using DevExpress.XtraEditors;

namespace QuesterAssistant.Classes.ItemFilter.Forms
{
    internal partial class ItemFilterForm : XtraForm
    {
        private ItemFilterForm()
        {
            InitializeComponent();
        }

        public static void Show(ItemFilterCore itemFilterCore, ItemFilterCoreType itemFilterCoreType = ItemFilterCoreType.Items)
        {
            new ItemFilterForm
            {
                itemFilterUC = {Type = itemFilterCoreType, Filter = itemFilterCore}
            }.ShowDialog();
        }
    }
}
