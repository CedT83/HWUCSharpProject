using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace SimpleWebBrowser
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            this.tabControl2.SuspendLayout();
            this.history.ReadXML();
            foreach (var element in history.toList())
            {
                ToolStripMenuItem item = new ToolStripMenuItem();
                item.Name = element.url;
                item.Size = new System.Drawing.Size(216, 38);
                item.Text = element.name;
                historyMenu.Add(item);
                item.Click += new System.EventHandler(OpenUrl);
            }
            this.tabControl2.Controls.Add(new Tab(this, historyMenu.ToArray()));
            this.tabControl2.ResumeLayout(false);
            this.tabControl2.PerformLayout();

        }

        public void reloadBtn(object sender, EventArgs e)
        {
            Tab CurrentTab = (Tab)this.tabControl2.SelectedTab;
            Request.CreateRequest(history, CurrentTab);
        } 

        public void NewWindow(object sender, EventArgs e)
        {
            Process newWindow = new Process();
            newWindow.StartInfo.FileName = Application.ExecutablePath;
            newWindow.StartInfo.CreateNoWindow = false;
            newWindow.Start();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        public void NewTab(object sender, EventArgs e)
        {
            this.tabControl2.SuspendLayout();
            this.tabControl2.Controls.Add(new Tab(this, historyMenu.ToArray()));
            this.tabControl2.ResumeLayout(false);
            this.tabControl2.PerformLayout();
        }

        public void CloseTab(object sender, EventArgs e)
        {

            this.tabControl2.Controls.Remove(this.tabControl2.SelectedTab);
            if (this.tabControl2.TabPages.Count == 0)
            {
                Application.Exit();
            }
        }

        public void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                Tab CurrentTab = (Tab)this.tabControl2.SelectedTab;
                Request.CreateRequest(history, CurrentTab);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            history.WriteXML();
            base.OnFormClosing(e);
        }

        public  void ExitApp(object sender, EventArgs e)
        {
            history.WriteXML();
            Application.Exit();
        }

        public void OpenUrl(object sender, EventArgs e)
        {
            this.tabControl2.SuspendLayout();
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            string url =history.history.First(eval => eval.name.Equals(item.Text)).url.ToString();
            Tab temp = new Tab(this, historyMenu.ToArray());
            temp.urlEntry.Text=url;
            this.tabControl2.Controls.Add(temp);
            this.tabControl2.ResumeLayout(false);
            this.tabControl2.PerformLayout();
            Request.CreateRequest(history, temp);
            history.Add(item.Text, url);
        }

        public void Shortcuts(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.T)
            {
                //NewTab(this.newTabToolStripMenuItem, e);
            }

            if (e.KeyCode == Keys.W)
            {
                //CloseTab(this.newTabToolStripMenuItem, e);
            }

            if (e.KeyCode == Keys.H)
            {
                //CloseTab(this.newTabToolStripMenuItem, e);
            }
        }
    }
}
