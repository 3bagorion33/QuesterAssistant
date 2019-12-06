namespace QuesterAssistant.InsigniaManager
{
    partial class InsigniaManagerForm
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.grctlProfile = new DevExpress.XtraEditors.GroupControl();
            this.gctlMounts = new DevExpress.XtraGrid.GridControl();
            this.gridMounts = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnInsert = new DevExpress.XtraEditors.SimpleButton();
            this.btnExtract = new DevExpress.XtraEditors.SimpleButton();
            this.btnGet = new DevExpress.XtraEditors.SimpleButton();
            this.lcProfilesList = new QuesterAssistant.Panels.ListControl();
            this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.grctlProfile)).BeginInit();
            this.grctlProfile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gctlMounts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridMounts)).BeginInit();
            this.SuspendLayout();
            // 
            // grctlProfile
            // 
            this.grctlProfile.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.grctlProfile.Controls.Add(this.gctlMounts);
            this.grctlProfile.Controls.Add(this.btnInsert);
            this.grctlProfile.Controls.Add(this.btnExtract);
            this.grctlProfile.Controls.Add(this.btnGet);
            this.grctlProfile.Controls.Add(this.lcProfilesList);
            this.grctlProfile.Location = new System.Drawing.Point(0, 0);
            this.grctlProfile.Margin = new System.Windows.Forms.Padding(1);
            this.grctlProfile.Name = "grctlProfile";
            this.grctlProfile.Size = new System.Drawing.Size(370, 372);
            this.grctlProfile.TabIndex = 0;
            this.grctlProfile.Text = "Preset";
            // 
            // gctlMounts
            // 
            this.gctlMounts.Location = new System.Drawing.Point(3, 175);
            this.gctlMounts.MainView = this.gridMounts;
            this.gctlMounts.Name = "gctlMounts";
            this.gctlMounts.Size = new System.Drawing.Size(364, 194);
            this.gctlMounts.TabIndex = 6;
            this.gctlMounts.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridMounts});
            // 
            // gridMounts
            // 
            this.gridMounts.GridControl = this.gctlMounts;
            this.gridMounts.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Never;
            this.gridMounts.Name = "gridMounts";
            this.gridMounts.OptionsBehavior.AutoExpandAllGroups = true;
            this.gridMounts.OptionsBehavior.AutoUpdateTotalSummary = false;
            this.gridMounts.OptionsBehavior.Editable = false;
            this.gridMounts.OptionsCustomization.AllowColumnMoving = false;
            this.gridMounts.OptionsCustomization.AllowFilter = false;
            this.gridMounts.OptionsCustomization.AllowGroup = false;
            this.gridMounts.OptionsCustomization.AllowSort = false;
            this.gridMounts.OptionsDetail.AllowOnlyOneMasterRowExpanded = true;
            this.gridMounts.OptionsDetail.DetailMode = DevExpress.XtraGrid.Views.Grid.DetailMode.Embedded;
            this.gridMounts.OptionsDetail.ShowDetailTabs = false;
            this.gridMounts.OptionsDetail.ShowEmbeddedDetailIndent = DevExpress.Utils.DefaultBoolean.False;
            this.gridMounts.OptionsFilter.AllowColumnMRUFilterList = false;
            this.gridMounts.OptionsFilter.AllowMRUFilterList = false;
            this.gridMounts.OptionsFind.AllowFindPanel = false;
            this.gridMounts.OptionsMenu.EnableColumnMenu = false;
            this.gridMounts.OptionsMenu.EnableFooterMenu = false;
            this.gridMounts.OptionsMenu.EnableGroupPanelMenu = false;
            this.gridMounts.OptionsPrint.ExpandAllDetails = true;
            this.gridMounts.OptionsPrint.UsePrintStyles = false;
            this.gridMounts.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridMounts.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gridMounts.OptionsSelection.UseIndicatorForSelection = false;
            this.gridMounts.OptionsView.ShowGroupPanel = false;
            this.gridMounts.OptionsView.ShowIndicator = false;
            this.gridMounts.ScrollStyle = DevExpress.XtraGrid.Views.Grid.ScrollStyleFlags.None;
            this.gridMounts.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Never;
            // 
            // btnInsert
            // 
            this.btnInsert.Location = new System.Drawing.Point(292, 146);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(75, 23);
            this.btnInsert.TabIndex = 5;
            this.btnInsert.Text = "Insert";
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // btnExtract
            // 
            this.btnExtract.Location = new System.Drawing.Point(292, 88);
            this.btnExtract.Name = "btnExtract";
            this.btnExtract.Size = new System.Drawing.Size(75, 23);
            this.btnExtract.TabIndex = 4;
            this.btnExtract.Text = "Extract";
            this.btnExtract.Click += new System.EventHandler(this.btnExtract_Click);
            // 
            // btnGet
            // 
            this.btnGet.Location = new System.Drawing.Point(292, 117);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(75, 23);
            this.btnGet.TabIndex = 4;
            this.btnGet.Text = "Get";
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // lcProfilesList
            // 
            this.lcProfilesList.Location = new System.Drawing.Point(57, 2);
            this.lcProfilesList.Margin = new System.Windows.Forms.Padding(0);
            this.lcProfilesList.Name = "lcProfilesList";
            this.lcProfilesList.Size = new System.Drawing.Size(310, 20);
            this.lcProfilesList.TabIndex = 0;
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Office 2013 Light Gray";
            // 
            // InsigniaManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grctlProfile);
            this.Name = "InsigniaManagerForm";
            this.Size = new System.Drawing.Size(370, 372);
            this.Load += new System.EventHandler(this.InsigniaManagerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grctlProfile)).EndInit();
            this.grctlProfile.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gctlMounts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridMounts)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl grctlProfile;
        private Panels.ListControl lcProfilesList;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private DevExpress.XtraEditors.SimpleButton btnInsert;
        private DevExpress.XtraEditors.SimpleButton btnGet;
        private DevExpress.XtraGrid.GridControl gctlMounts;
        private DevExpress.XtraGrid.Views.Grid.GridView gridMounts;
        private DevExpress.XtraEditors.SimpleButton btnExtract;
    }
}
