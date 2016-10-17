using System.Collections.Generic;
using System.Windows.Forms;
namespace SimpleWebBrowser
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        public System.ComponentModel.IContainer components = null;
        public HistoryEntity.History history = new HistoryEntity.History();
        public List<ToolStripMenuItem> historyMenu = new List<ToolStripMenuItem>();

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.components = new System.ComponentModel.Container();
            this.tabControl2 = new TabControl();
            this.contextMenuStrip1 = new ContextMenuStrip(this.components);
            this.newTabToolStripMenuItem = new ToolStripMenuItem();
            this.closeTabToolStripMenuItem = new ToolStripMenuItem();
            this.tabControl2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl2
            // 
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.tabControl2.Dock = DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Name = "tabControl1";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(1894, 1009);
            this.tabControl2.TabIndex = 3;

            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.contextMenuStrip1.Items.AddRange(new ToolStripItem[] {
            this.newTabToolStripMenuItem,
            this.closeTabToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(217, 80);
            // 
            // newTabToolStripMenuItem
            // 
            this.newTabToolStripMenuItem.Name = "newTabToolStripMenuItem";
            this.newTabToolStripMenuItem.Size = new System.Drawing.Size(216, 38);
            this.newTabToolStripMenuItem.Text = Properties.Resources.OpenTab;
            // 
            // closeTabToolStripMenuItem
            // 
            this.closeTabToolStripMenuItem.Name = "closeTabToolStripMenuItem";
            this.closeTabToolStripMenuItem.Size = new System.Drawing.Size(216, 38);
            this.closeTabToolStripMenuItem.Text = Properties.Resources.CloseTab;


            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(1894, 1009);
            this.Controls.Add(this.tabControl2);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new Padding(6);
            this.Name = "Main";
            this.Text = Properties.Resources.WindowText;
            this.WindowState = FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Main_Load);
            this.closeTabToolStripMenuItem.Click += new System.EventHandler(this.CloseTab);
            this.newTabToolStripMenuItem.Click += new System.EventHandler(this.NewTab);
            this.KeyDown += new KeyEventHandler(this.Shortcuts);
            this.tabControl2.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();


        }

        #endregion

        private TabControl tabControl2;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem newTabToolStripMenuItem;
        private ToolStripMenuItem closeTabToolStripMenuItem;

    }
}

