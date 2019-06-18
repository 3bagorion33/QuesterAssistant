using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils.Extensions;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using MyNW.Internals;
using QuesterAssistant.Classes;

namespace QuesterAssistant.UIEditors.Forms
{
    internal partial class DonateTaskEditorForm : XtraForm
    {
        private bool okClicked;
        private Coffer toEditCoffer;
        private List<Coffer> CofferList =>
            EntityManager.LocalPlayer.IsValid &&
            !EntityManager.LocalPlayer.IsLoading &&
            EntityManager.LocalPlayer.Player.PlayerGuild.GuildID > 0
                ?
                EntityManager.LocalPlayer.Player.PlayerGuild.GroupProjectContainer.ProjectList
                    .Find(p => p.ProjectDef.Name == "Nw_Stronghold").CofferNumericData
                    .FindAll(d => !string.IsNullOrEmpty(d.CofferNumericDef.Name))
                    .Select(d => new Coffer(d))
                    .OrderBy(i => i.DisplayName)
                    .ToList()
                : new List<Coffer>();

        private Coffer CurrentCoffer => bsrcCofferList.Current as Coffer;

        private DonateTaskEditorForm()
        {
            InitializeComponent();
        }

        private void LoadData(Coffer toEditCoffer)
        {
            this.toEditCoffer = toEditCoffer;
        }

        public static Coffer Show(Coffer toEditCoffer = null)
        {
            var cofferEditor = new DonateTaskEditorForm();
            cofferEditor.LoadData(toEditCoffer);
            cofferEditor.ShowDialog();
            if (!cofferEditor.okClicked) return null;
            cofferEditor.CurrentCoffer.RemoveZero();
            return cofferEditor.CurrentCoffer;

        }

        private void DonateTaskForm_Load(object sender, EventArgs e)
        {
            bsrcCofferList.DataSource = CofferList;

            gctlCofferList.InvokeSafe(() =>
            {
                gctlCofferList.ForceInitialize();

                if (toEditCoffer != null && CofferList.Any())
                {
                    int idx = CofferList.FindIndex(c => c.InternalName == toEditCoffer.InternalName);
                    gviewCofferList.FocusedRowHandle = idx;
                    CurrentCoffer.Parse(toEditCoffer);
                    gctlItemList.RefreshDataSource();
                }

                gviewItemList.CustomColumnGroup += gviewItemList_CustomColumnGroup;
                gviewItemList.CustomColumnSort += gviewItemList_CustomColumnSort;
                gviewItemList.CustomDrawGroupRow += gviewItemList_CustomDrawGroupRow;
            });
        }

        private void gviewItemList_CustomColumnSort(object sender, CustomColumnSortEventArgs e)
        {
            if (e.Column.CustomizationSearchCaption == nameof(Coffer.Item.Donate))
            {
                var value1 = (int) e.Value1;
                var value2 = (int) e.Value2;

                int Result()
                {
                    if (value1 == value2) return 0;
                    if (value1 < 0 || value2 == 0) return -1;
                    if (value2 < 0 || value1 == 0) return 1;
                    return 0;
                }

                e.Result = Result();
                e.Handled = true;
            }
        }

        private void gviewItemList_CustomDrawGroupRow(object sender, RowObjectCustomDrawEventArgs e)
        {
            GridGroupRowInfo info = e.Info as GridGroupRowInfo;
            if (info.Column.CustomizationSearchCaption == nameof(Coffer.Item.Donate))
            {
                var flag = int.Parse(info.GroupValueText);
                info.GroupText = flag == 0 ? "Will not be donated" : "Will be donated";
            }
        }

        private void gviewItemList_CustomColumnGroup(object sender, CustomColumnSortEventArgs e)
        {
            if (e.Column.CustomizationSearchCaption == nameof(Coffer.Item.Donate))
            {
                var value1 = (int) e.Value1 != 0;
                var value2 = (int) e.Value2 != 0;
                e.Result = value1 ^ value2 ? 1 : 0;
                e.Handled = true;
            }
        }

        private void HandleMouseWheel(object sender, MouseEventArgs e)
        {
            (e as DevExpress.Utils.DXMouseEventArgs).Handled = true;
        }

        private void gviewCofferList_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            bsrcItemList.DataSource = CurrentCoffer.Items;
            gctlItemList.ForceInitialize();
            CurrentCoffer.HashChanged += gctlItemList.RefreshDataSource;

            foreach (GridColumn c in gviewItemList.Columns)
            {
                c.MinWidth = 50;

                switch (c.CustomizationSearchCaption)
                {
                    case nameof(Coffer.Item):
                        c.Width = c.GetBestWidth();
                        c.SortIndex = 0;
                        gviewItemList.AutoFillColumn = c;
                        c.OptionsColumn.AllowEdit = false;
                        break;

                    case nameof(Coffer.Item.Donate):
                        c.MinWidth = 64;
                        c.ColumnEdit = riNumEdit;
                        c.ShowButtonMode = ShowButtonModeEnum.ShowAlways;
                        c.GroupIndex = 0;
                        c.SortMode = ColumnSortMode.Custom;
                        break;

                    default:
                        c.OptionsColumn.AllowEdit = false;
                        break;
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            ActiveControl = null;
            okClicked = true;
            Close();
        }
        private void gviewItemList_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            GridView view = sender as GridView;
            GridHitInfo hitInfo = view.CalcHitInfo(e.Point);
            if (hitInfo.InRowCell)
            {
                view.FocusedRowHandle = hitInfo.RowHandle;
                pmItemList.ShowPopup(bmItemList, view.GridControl.PointToScreen(e.Point));
            }
        }

        private void miSetToAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gviewItemList.FocusedRowHandle > -1)
            {
                (bsrcItemList.DataSource as List<Coffer.Item>)
                    .ForEach(i => i.Donate = (bsrcItemList.Current as Coffer.Item).Donate);
            }
        }

        private void gviewItemList_ShownEditor(object sender, EventArgs e)
        {
            var item = gviewItemList.GetRow(gviewItemList.FocusedRowHandle) as Coffer.Item;
            var edit = gviewItemList.ActiveEditor as SpinEdit;
            edit.Properties.Increment = item.BatchSize;
        }
    }
}
