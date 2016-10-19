using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace FireDogeWebBrowser
{
    public partial class Main : Form
    {
        /// <summary>
        /// The void constructor of the class Main.
        /// <remarks>
        /// Launches the method that creates the GUI
        /// </remarks>
        /// </summary>
        public Main()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This event handler is called when the form Main is loaded
        /// </summary>
        /// <param name="sender">Refers to the form Main</param>
        /// <param name="e">Refers to the event "onLoad" of the form</param>
        private void Main_Load(object sender, EventArgs e)
        {
            //We suspend the layout to not have interferences
            this.tabControl2.SuspendLayout();
            //Here we retrieve the history and favourites from the XML files
            this.history.ReadXML();
            //Once the file read, we create a ToolStripMenuItem with or each element 
            //And we add them to our List<ToolStripMenuItem named historyMenu for the history
            foreach (var element in history.toList())
            {
                ToolStripMenuItem item = new ToolStripMenuItem();
                item.Name = element.url;
                item.Size = new System.Drawing.Size(216, 38);
                item.Text = element.name;
                historyMenu.Add(item);
                //Enable the  click on the item to open the page with a single click
                item.Click += new EventHandler(OpenUrlFromHistory);
            }

            this.favourites.ReadXML();
            //Here the List<ToolStripMenuItem> is named favouritesMenu for the favourites
            foreach (var element in favourites.toList())
            {
                ToolStripMenuItem item = new ToolStripMenuItem();
                item.Name = element.url;
                item.Size = new System.Drawing.Size(216, 38);
                item.Text = element.name;
                favouritesMenu.Add(item);
                item.Click += new EventHandler(OpenUrlFromFavourites);
            }

            //We create the first tab, to not have an empty page
            Tab firstTab = new Tab(this);
            this.tabControl2.Controls.Add(firstTab);

            //We load the home page if defined.
            //We use the Ressource file in Properties.Settings.Default
            if (!Properties.Settings.Default.Homepage.Equals(""))
            {
                firstTab.urlEntry.Text = Properties.Settings.Default.Homepage;
                //We request the page as any other web page
                Request.CreateRequest(history, firstTab);
            }                
            //We resume the layout to apply the changes
            this.tabControl2.ResumeLayout(false);
            this.tabControl2.PerformLayout(); 

        }

        /// <summary>
        /// This event handler is for the click on the reload button of the tabs
        /// </summary>
        /// <param name="sender">Refers to the button</param>
        /// <param name="e">Refers to the Click event</param>
        public void reloadBtn(object sender, EventArgs e)
        {
            //We get the tab from which the button belongs
            Tab CurrentTab = (Tab)this.tabControl2.SelectedTab;
            //We make sure the history is not empty
            if(CurrentTab.localHistory.LongCount() > 1)
            {
                //We set the URL bar to the last entry and retrieve the page
                CurrentTab.urlEntry.Text = CurrentTab.localHistory.Last();
                Request.CreateRequest(history, CurrentTab);
            }
        }

        /// <summary>
        /// This event handler is for the click on the goNext button of the tabs
        /// </summary>
        /// <param name="sender">Refers to the button</param>
        /// <param name="e">Refers to the Click event</param>
        public void goNext(object sender, EventArgs e)
        {
            //We get the tab from which the button belongs
            Tab CurrentTab = (Tab)this.tabControl2.SelectedTab;
            //We make sure the history is not empty
            if (CurrentTab.localHistory.Any())
            {
                //We set the URL bar to the last entry and retrieve the page
                CurrentTab.urlEntry.Text = CurrentTab.localHistory.MoveNext;
                Request.CreateRequest(history, CurrentTab);
            }
        }

        /// <summary>
        /// This event handler is for the click on the goPrevious button of the tabs
        /// </summary>
        /// <param name="sender">Refers to the button</param>
        /// <param name="e">Refers to the Click event</param>
        public void goPrevious(object sender, EventArgs e)
        {
            Tab CurrentTab = (Tab)this.tabControl2.SelectedTab;
            if (CurrentTab.localHistory.Any())
            {
                CurrentTab.urlEntry.Text = CurrentTab.localHistory.MovePrevious;
                Request.CreateRequest(history, CurrentTab);
            }
        }

        /// <summary>
        /// This event handler opens a new instance of the application into a new process
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void NewWindow(object sender, EventArgs e)
        {
            //We create a new process
            Process newWindow = new Process();
            //We tell our process which program he has to run
            newWindow.StartInfo.FileName = Application.ExecutablePath;
            newWindow.StartInfo.CreateNoWindow = false;
            //We run the process
            newWindow.Start();
        }

        /// <summary>
        /// This event handler opens a new Tab in the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void NewTab(object sender, EventArgs e)
        {
            //We resume the layout to avoid interferences 
            this.tabControl2.SuspendLayout();
            //Create and add the Tab
            this.tabControl2.Controls.Add(new Tab(this));
            //Refresh control
            this.tabControl2.ResumeLayout(false);
            this.tabControl2.PerformLayout();
        }

        /// <summary>
        /// This event handler closes the current tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CloseTab(object sender, EventArgs e)
        {
            //We remove the tab from the list of controls containing all the tabs
            this.tabControl2.Controls.Remove(this.tabControl2.SelectedTab);
            //if number of tabs is 0 we close the application
            if (this.tabControl2.TabPages.Count == 0)
            {
                Application.Exit();
            }
        }

        /// <summary>
        /// This event handler captures the Enter key when textbox is focused
        /// We retrieve the page which the URL is in the textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                Tab CurrentTab = (Tab)this.tabControl2.SelectedTab;
                Request.CreateRequest(history, CurrentTab);
                CurrentTab.localHistory.Add(CurrentTab.urlEntry.Text);
            }
        }

        /// <summary>
        /// We modify the default event handler used when we close a form 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            //We want to store our data into files
            history.WriteXML();
            favourites.WriteXML();
            //Then we want the behaviour of the default event handler
            base.OnFormClosing(e);
        }

        /// <summary>
        /// An event handler to close the app programatically
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public  void ExitApp(object sender, EventArgs e)
        {
            // We write our data (from history and Favourites) into the files to save it
            history.WriteXML();
            favourites.WriteXML();
            // Then we close the application
            Application.Exit();
        }

        /// <summary>
        /// This event links the MenuStrip created in Main.Designer to the tabs when selected
        /// </summary>
        /// <remaks>
        /// We have to do this because when we create a new tab the link is destroyed. Has to be fixed
        /// </remaks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void LinkMenuStrip(object sender, TabControlEventArgs e)
        {
            Tab currentTab = (Tab)e.TabPage;
            //To avoid an exception when there is no tab anymore
            if(currentTab != null)
            {
                //We link the menus
                currentTab.historyToolStripMenuItem.DropDownItems.AddRange(historyMenu.ToArray());
                currentTab.favouritesToolStripMenuItem.DropDownItems.AddRange(favouritesMenu.ToArray());
            }
        }

        /// <summary>
        /// This event handler is called when we click on an item in the history menu to retrieve the page
        /// </summary>
        /// <remarks>
        /// Needs to be refactored 
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OpenUrlFromHistory(object sender, EventArgs e)
        {
            // Suspend the layout to avoid graphical issues
            this.tabControl2.SuspendLayout();
            //We retrieve the URL of the item in the history
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            string url = history.history.First(eval => eval.name.Equals(item.Text)).url.ToString();
            //We create a new tab and get the page
            Tab temp = new Tab(this);
            temp.urlEntry.Text=url;
            this.tabControl2.Controls.Add(temp);
            this.tabControl2.ResumeLayout(false);
            this.tabControl2.PerformLayout();
            Request.CreateRequest(history, temp);
            //We change the selected tab with the new one to display the new page to the user
            this.tabControl2.SelectedIndex = (tabControl2.SelectedIndex + 1 < tabControl2.TabCount) ?
                             tabControl2.SelectedIndex + 1 : tabControl2.SelectedIndex;
        }

        /// <summary>
        /// This event handler is called when we click on an item in the favourites menu to retrieve the page
        /// </summary>
        /// <remarks>
        /// Needs to be refactored 
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OpenUrlFromFavourites(object sender, EventArgs e)
        {
            // Suspend the layout to avoid graphical issues
            this.tabControl2.SuspendLayout();
            //We retrieve the URL of the item in the history
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            string url = favourites.favourites.First(eval => eval.name.Equals(item.Text)).url.ToString();
            //We create a new tab and get the page
            Tab temp = new Tab(this);
            temp.urlEntry.Text = url;
            this.tabControl2.Controls.Add(temp);
            this.tabControl2.ResumeLayout(false);
            this.tabControl2.PerformLayout();
            Request.CreateRequest(history, temp);
            //We change the selected tab with the new one to display the new page to the user
            this.tabControl2.SelectedIndex = (tabControl2.SelectedIndex + 1 < tabControl2.TabCount) ?
                             tabControl2.SelectedIndex + 1 : tabControl2.SelectedIndex;
        }

        /// <summary>
        /// Handle key events on the form Main.
        /// </summary>
        /// <remarks>
        /// Needs to be fixed. Not working yet
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Shortcuts(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == (Keys.T | Keys.Control))
            {
                NewTab(this.newTabToolStripMenuItem, e);
            }

            if (e.KeyCode == (Keys.W | Keys.Alt))
            {
                CloseTab(this.newTabToolStripMenuItem, e);
            }

            if (e.KeyCode == (Keys.H | Keys.Alt))
            {
                CloseTab(this.newTabToolStripMenuItem, e);
            }
        }

        /// <summary>
        /// Defines the current displayed page as home page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SetHomePage(object sender, EventArgs e)
        {
            //We get the url of the selected tab
            Tab currentTab = (Tab)this.tabControl2.SelectedTab;
            Properties.Settings.Default.Homepage = currentTab.urlEntry.Text;
            //We save the changes
            Properties.Settings.Default.Save();
        }
    }
}
