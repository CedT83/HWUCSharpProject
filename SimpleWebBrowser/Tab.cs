/// <summary>
/// We do some graphical design here so we just need <c>System.Windows.Forms</c>
/// to derives from TabPage and create our custom Tab
/// </summary>
using System.Windows.Forms;

namespace FireDogeWebBrowser
{
    /// <summary>
    /// This class is used to define a Tab of our web browser. 
    /// So we can use it like a unique entity composed of many various controls
    /// </summary>
    /// <remarks>
    /// This class derives from TabPage which defines an empry Tab. We want the same behaviour for our custom Tab
    /// </remarks>
    class Tab : TabPage
    {
        /// <summary>
        /// This property is used to have a reference of Main (initially Form1)
        /// So we can perform actions on it and call methods
        /// </summary>
        /// <remarks>
        /// This makes the Designer crash beacause Main instancies some Tab objetcs which references themselves Main
        /// But so far it compiles and works
        /// </remarks>
        private Main obj;


        /// <summary>
        /// This attribute is used to define the color in the ribbon
        /// </summary>
        /// <remarks>
        /// It possibly can have all the valid values for a RGB color. 
        /// </remarks>
        public System.Drawing.Color color = System.Drawing.Color.FromArgb(203, 168, 87);


        // These attributes are all the objects used for designing one tab
        // This LayoutPanel is used to split the form in to parts
        // one for the ribbon, the other to display the HTML
        private TableLayoutPanel tableLayoutPanel1;

        // This LayoutPanel is used to split the ribbon into 3 parts
        // one for the buttons on the left, another for the textbox used for the URL entry
        // the last one for the StripMenu on the right
        private TableLayoutPanel tableLayoutPanel2;

        // This LayoutPanel divides the second column of the Panel above 
        // in order to center the textbox in the previous panel'row
        private TableLayoutPanel tableLayoutPanel3;


        /// <summary>
        /// This button is used to go to the previous URL 
        /// </summary>
        /// <remarks>
        /// The accessibility level is protected internal 
        /// so we can access the button within the assembly and perform actions on it
        /// Also internal, in case we need a derived type of this class
        /// </remarks>
        protected internal Button goPreviousBtn { get; set; }


        /// <summary>
        /// This button is used to go to the next URL we can visit
        /// </summary>
        /// <remarks>
        /// The accessibility level is protected internal 
        /// so we can access the button within the assembly and perform actions on it
        /// Also internal, in case we need a derived type of this class
        /// </remarks>
        protected internal Button goNextBtn { get; set; }


        /// <summary>
        /// This button is used to perform a GET request at the specified URL 
        /// </summary>
        /// <remarks>
        /// The accessibility level is protected internal 
        /// so we can access the button within the assembly and perform actions on it
        /// Also internal, in case we need a derived type of this class
        /// </remarks>
        protected internal Button reloadBtn { get; set; }


        /// <summary>
        /// This textox is used to enter a URL
        /// </summary>
        /// <remarks>
        /// The accessibility level is protected internal 
        /// so we can access the button within the assembly and perform actions on it
        /// Also internal, in case we need a derived type of this class
        /// </remarks>
        protected internal TextBox urlEntry { get; set; }

        //Here we have some objects used for the design of the StripMenu
        // Names are explicit
        private MenuStrip menuStrip1;
        protected internal ToolStripMenuItem favouritesToolStripMenuItem;
        private ToolStripMenuItem customizeToolStripMenuItem;
        private ToolStripMenuItem newWindowToolStripMenuItem;
        protected internal ToolStripMenuItem historyToolStripMenuItem;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private ToolStripMenuItem homePageToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;

        /// <summary>
        /// This button is used to display the HTML we received from the GET request  
        /// </summary>
        /// <remarks>
        /// The accessibility level is protected internal 
        /// so we can access the button within the assembly and perform actions on it
        /// Also internal, in case we need a derived type of this class
        /// </remarks>
        protected internal TextBox htmlDisplay { get; set; }

        /// <summary>
        /// This list is used to have logs of the pages visited with the current tab
        /// <remarks>
        /// It is useful for the Previous and Next buttons
        /// <see cref="goPreviousBtn"/>
        /// <see cref="goNextBtn"/>
        /// </remarks>
        /// </summary>
        protected internal NavigationList<string> localHistory { get; set; }

        //Class variable, in case we need to know how many instances of this class he have
        static private uint counter = 1;


        public Tab(Form obj)
        {
            //This looks savage but we just initialise the objects used for the Tab
            //Nothing particular to say
            this.tableLayoutPanel1 = new TableLayoutPanel();
            this.tableLayoutPanel2 = new TableLayoutPanel();
            this.goPreviousBtn = new Button();
            this.goNextBtn = new Button();
            this.reloadBtn = new Button();
            this.tableLayoutPanel3 = new TableLayoutPanel();
            this.urlEntry = new TextBox();
            this.menuStrip1 = new MenuStrip();
            this.favouritesToolStripMenuItem = new ToolStripMenuItem();
            this.customizeToolStripMenuItem = new ToolStripMenuItem();
            this.newWindowToolStripMenuItem = new ToolStripMenuItem();
            this.historyToolStripMenuItem = new ToolStripMenuItem();
            this.settingsToolStripMenuItem = new ToolStripMenuItem();
            this.exitToolStripMenuItem = new ToolStripMenuItem();
            this.homePageToolStripMenuItem = new ToolStripMenuItem();
            this.htmlDisplay = new TextBox();
            localHistory = new NavigationList<string>();

            //Suspend Layout to keep errors away and prevent from layout interferences 
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.menuStrip1.SuspendLayout();

            /// <summary>
            /// <see cref="obj">See the declaration of the attribute</see>
            /// So we can have a reference to Main and apply methods on it
            /// </summary>
            this.obj = (Main)obj;


            //At this point we now customise the objects to create the Tab

            // 
            // Current tabPage
            // 
            this.Controls.Add(this.tableLayoutPanel1);
            this.BackColor = System.Drawing.Color.FromArgb(242, 242, 242);
            this.Location = new System.Drawing.Point(8, 39);
            this.Name = "tabPage" + " " + counter++;
            this.Padding = new Padding(0);
            this.Margin = new Padding(0);
            this.Size = new System.Drawing.Size(1878, 962);
            this.TabIndex = 0;
            this.Text = Properties.Resources.NewTab;
            this.UseVisualStyleBackColor = true;


            // 
            // tableLayoutPanel1
            // Used to separate the sealed ribbon on the top of a tab and the display Control
            // 
            this.tableLayoutPanel1.AutoScroll = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.htmlDisplay, 0,1);
            this.tableLayoutPanel1.Margin = new Padding(0);
            this.tableLayoutPanel1.Padding = new Padding(0);
            this.tableLayoutPanel1.Dock = DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 64F));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1872, 956);
            this.tableLayoutPanel1.TabIndex = 4;


            // 
            // tableLayoutPanel2
            // Used to create the top ribbon, saparate controls and place them
            // 
            this.tableLayoutPanel2.BackColor = color;
            this.tableLayoutPanel2.ColumnCount = 5;
            this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 64F));
            this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 64F));
            this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 64F));
            this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 280F));
            this.tableLayoutPanel2.Controls.Add(this.goPreviousBtn, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.goNextBtn, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.reloadBtn, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.menuStrip1, 4, 0);
            this.tableLayoutPanel2.Dock = DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1872, 64);
            this.tableLayoutPanel2.TabIndex = 0;


            // 
            // goPreviousBtn design
            // 
            this.goPreviousBtn.BackColor = color;
            this.goPreviousBtn.Dock = DockStyle.Fill;
            this.goPreviousBtn.FlatAppearance.BorderSize = 0;
            this.goPreviousBtn.FlatStyle = FlatStyle.Flat;
            this.goPreviousBtn.Location = new System.Drawing.Point(3, 3);
            this.goPreviousBtn.Name = "goPreviousBtn";
            this.goPreviousBtn.Size = new System.Drawing.Size(58, 58);
            this.goPreviousBtn.TabIndex = 0;
            this.goPreviousBtn.Text = Properties.Resources.PreviousPage;
            this.goPreviousBtn.UseVisualStyleBackColor = false;


            // 
            // goNextBtn design
            // 
            this.goNextBtn.Dock = DockStyle.Fill;
            this.goNextBtn.FlatAppearance.BorderSize = 0;
            this.goNextBtn.FlatStyle = FlatStyle.Flat;
            this.goNextBtn.Location = new System.Drawing.Point(67, 3);
            this.goNextBtn.Name = "goNextBtn";
            this.goNextBtn.Size = new System.Drawing.Size(58, 58);
            this.goNextBtn.TabIndex = 1;
            this.goNextBtn.Text = Properties.Resources.NextPage;
            this.goNextBtn.UseVisualStyleBackColor = true;


            // 
            // reloadBtn design
            // 
            this.reloadBtn.Dock = DockStyle.Fill;
            this.reloadBtn.FlatAppearance.BorderSize = 0;
            this.reloadBtn.FlatStyle = FlatStyle.Flat;
            this.reloadBtn.Location = new System.Drawing.Point(131, 3);
            this.reloadBtn.Name = "reloadBtn";
            this.reloadBtn.Size = new System.Drawing.Size(58, 58);
            this.reloadBtn.TabIndex = 2;
            this.reloadBtn.Text = Properties.Resources.ReloadPage;
            this.reloadBtn.UseVisualStyleBackColor = true;

            // 
            // tableLayoutPanel3
            // Used to center the textBox to enter the URL
            // 
            this.tableLayoutPanel3.BackColor = color;
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.urlEntry, 0, 1);
            this.tableLayoutPanel3.Dock = DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(192, 0);
            this.tableLayoutPanel3.Margin = new Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1400, 64);
            this.tableLayoutPanel3.TabIndex = 5;


            // 
            // urlEntry (Textbox) design
            // 
            this.urlEntry.Dock = DockStyle.Fill;
            this.urlEntry.Location = new System.Drawing.Point(3, 13);
            this.urlEntry.Name = "urlEntry";
            this.urlEntry.Size = new System.Drawing.Size(1394, 31);
            this.urlEntry.TabIndex = 0;


            // 
            // menuStrip1 design
            // Used to display favourites and options
            // 
            this.menuStrip1.BackColor = color; ;
            this.menuStrip1.Dock = DockStyle.Fill;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new ToolStripItem[] {
            this.favouritesToolStripMenuItem,
            this.customizeToolStripMenuItem
            });
            this.menuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.menuStrip1.Location = new System.Drawing.Point(1592, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(280, 64);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";


            // 
            // favouritesToolStripMenuItem
            // To display favourites by clincking on it
            // 
            this.favouritesToolStripMenuItem.BackColor = color;
            this.favouritesToolStripMenuItem.DropDownItems.AddRange(this.obj.favouritesMenu.ToArray());
            this.favouritesToolStripMenuItem.Name = "favouritesToolStripMenuItem";
            this.favouritesToolStripMenuItem.Size = new System.Drawing.Size(135, 60);
            this.favouritesToolStripMenuItem.Text = Properties.Resources.Favourites;



            // 
            // customizeToolStripMenuItem
            // Access the parameters of the Window that are editable
            // 
            this.customizeToolStripMenuItem.BackColor = color;
            this.customizeToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
            this.newWindowToolStripMenuItem,
            this.historyToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.homePageToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.customizeToolStripMenuItem.Name = "customizeToolStripMenuItem";
            this.customizeToolStripMenuItem.Size = new System.Drawing.Size(113, 60);
            this.customizeToolStripMenuItem.Text = Properties.Resources.Options;


            // 
            // newWindowwToolStripMenuItem
            // In case we want to execute another instance of the app. In a separated processus
            // 
            this.newWindowToolStripMenuItem.Name = "newWindowToolStripMenuItem";
            this.newWindowToolStripMenuItem.Size = new System.Drawing.Size(274, 38);
            this.newWindowToolStripMenuItem.Text = Properties.Resources.NewWindow;

            // 
            // newWindowwToolStripMenuItem
            // In case we want to execute another instance of the app. In a separated processus
            // 
            this.homePageToolStripMenuItem.Name = "homePageToolStripMenuItem";
            this.homePageToolStripMenuItem.Size = new System.Drawing.Size(274, 38);
            this.homePageToolStripMenuItem.Text = Properties.Resources.HomePage;


            // 
            // historyToolStripMenuItem
            // To display history
            // 
            this.historyToolStripMenuItem.Name = "historyToolStripMenuItem";
            this.historyToolStripMenuItem.Size = new System.Drawing.Size(274, 38);
            this.historyToolStripMenuItem.Text = Properties.Resources.Log;
            this.historyToolStripMenuItem.DropDownItems.AddRange(this.obj.historyMenu.ToArray());


            // 
            // settingsToolStripMenuItem
            // To access the settings
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(274, 38);
            this.settingsToolStripMenuItem.Text = Properties.Resources.Settings;


            // 
            // exitToolStripMenuItem
            // To exit the current instance of the application
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(274, 38);
            this.exitToolStripMenuItem.Text = Properties.Resources.CloseAppli;


            // 
            // HtmlDisplay (TextBox) design
            // 
            this.htmlDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom)
            | AnchorStyles.Left)
            | AnchorStyles.Right)));
            this.htmlDisplay.BackColor = System.Drawing.Color.White;
            this.htmlDisplay.BorderStyle = BorderStyle.FixedSingle;
            this.htmlDisplay.Enabled = false;
            this.htmlDisplay.Location = new System.Drawing.Point(0, 64);
            this.htmlDisplay.Margin = new Padding(0);
            this.htmlDisplay.Multiline = true;
            this.htmlDisplay.Name = "HtmlDisplay";
            //Used such as the user can only read the HTML and not modify it
            this.htmlDisplay.ReadOnly = true;
            this.htmlDisplay.ScrollBars = ScrollBars.Vertical;
            this.htmlDisplay.Size = new System.Drawing.Size(1872, 892);
            this.htmlDisplay.TabIndex = 1;
            this.htmlDisplay.Hide();


            //Here are the differents event we want our controls to handle
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.obj.ExitApp);
            this.newWindowToolStripMenuItem.Click += new System.EventHandler(this.obj.NewWindow);
            this.homePageToolStripMenuItem.Click += new System.EventHandler(this.obj.SetHomePage);
            this.reloadBtn.Click += new System.EventHandler(this.obj.reloadBtn);
            this.goNextBtn.Click += new System.EventHandler(this.obj.goNext);
            this.urlEntry.KeyPress += new KeyPressEventHandler(this.obj.textBox_KeyPress);
            this.goPreviousBtn.Click += new System.EventHandler(this.obj.goPrevious);

            //Here we define the differents shortcut keys we want our StripMenu to handle
            this.exitToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.Q;
            this.settingsToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.S;
            this.newWindowToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.N;
            this.homePageToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.H;


            //We call the mothds to perform the global layout on the tab
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

