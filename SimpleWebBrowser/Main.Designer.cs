using System.Collections.Generic;
using System.Windows.Forms;
namespace FireDogeWebBrowser
{
    partial class Main
    {
        /// <summary>
        /// Contains the composants of the GUI
        /// </summary>
        public System.ComponentModel.IContainer components = null;
        private HistoryEntity.History history = new HistoryEntity.History();
        private FavouritesEntity.Favourites favourites = new FavouritesEntity.Favourites();
        /// <summary>
        /// List of the items in the history
        /// </summary>
        public List<ToolStripMenuItem> historyMenu = new List<ToolStripMenuItem>();
        /// <summary>
        /// List of the items in the favourites
        /// </summary>
        public List<ToolStripMenuItem> favouritesMenu = new List<ToolStripMenuItem>();

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            //We instantiate all the objects
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.components = new System.ComponentModel.Container();
            this.tabControl2 = new TabControl();
            this.contextMenuStrip1 = new ContextMenuStrip(this.components);
            this.newTabToolStripMenuItem = new ToolStripMenuItem();
            this.closeTabToolStripMenuItem = new ToolStripMenuItem();
            this.tabControl2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            //We suspend the layout to avoid graphical issues
            this.SuspendLayout();

            // 
            // To customize tabControl2 
            // 
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.tabControl2.Dock = DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Padding = new System.Drawing.Point(0, 0);
            this.tabControl2.Margin = new Padding(0);
            this.tabControl2.Name = "tabControl1";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(1894, 1009);
            this.tabControl2.TabIndex = 3;

            // 
            // To customize contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.contextMenuStrip1.Items.AddRange(new ToolStripItem[] {
            this.newTabToolStripMenuItem,
            this.closeTabToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(217, 80);
            // 
            // To customize newTabToolStripMenuItem
            // 
            this.newTabToolStripMenuItem.Name = "newTabToolStripMenuItem";
            this.newTabToolStripMenuItem.Size = new System.Drawing.Size(216, 38);
            this.newTabToolStripMenuItem.Text = Properties.Resources.OpenTab;
            // 
            // To customize closeTabToolStripMenuItem
            // 
            this.closeTabToolStripMenuItem.Name = "closeTabToolStripMenuItem";
            this.closeTabToolStripMenuItem.Size = new System.Drawing.Size(216, 38);
            this.closeTabToolStripMenuItem.Text = Properties.Resources.CloseTab;


            // 
            // To customize Main Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(204, 204, 204);
            this.ClientSize = new System.Drawing.Size(1894, 1009);
            this.Controls.Add(this.tabControl2);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new Padding(0);
            this.Padding = new Padding(0);
            this.Name = "Main";
            this.Text = Properties.Resources.WindowText;
            this.WindowState = FormWindowState.Maximized;

            //We add some events on some controls 
            this.Load += new System.EventHandler(this.Main_Load);
            this.closeTabToolStripMenuItem.Click += new System.EventHandler(this.CloseTab);
            this.newTabToolStripMenuItem.Click += new System.EventHandler(this.NewTab);
            this.tabControl2.Selected += new TabControlEventHandler(this.LinkMenuStrip);
            this.KeyDown += new KeyEventHandler(this.Shortcuts);

            //We perform the layout to see the changes on the controls
            this.tabControl2.ResumeLayout(true);
            this.contextMenuStrip1.ResumeLayout(true);
            this.ResumeLayout(true);


        }

        #endregion

        //Auto generated by the designer 
        private TabControl tabControl2;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem newTabToolStripMenuItem;
        private ToolStripMenuItem closeTabToolStripMenuItem;
    }
}

