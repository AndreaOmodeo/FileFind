using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace FileFind
{
    /// <summary>
    /// Summary description for Form1.
    /// </summary>
    public class Form1 : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Button btnGO;
        private System.Windows.Forms.TextBox m_txtPattern;
        private System.Windows.Forms.TextBox m_txtStartPath;
        private System.Windows.Forms.ListView m_listView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader Date;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BrowseDir;
        private System.Windows.Forms.StatusBar statusBar1;
        private System.Windows.Forms.StatusBarPanel statusBarPanel1;
        private System.Windows.Forms.ImageList icons;
        private ListViewColumnSorter lvwColumnSorter;
        private BackgroundWorker m_bgWorker;
        private Hashtable ht = new Hashtable();
        private string m_Pattern;
        static private readonly int basecounter = 32;
        private int counter = basecounter;

        [DllImport("shell32.dll")]
        public static extern IntPtr ExtractAssociatedIcon(int a, string b, int c);

        public Form1()
        {
            InitializeComponent();
            lvwColumnSorter = new ListViewColumnSorter();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnGO = new System.Windows.Forms.Button();
            this.m_txtPattern = new System.Windows.Forms.TextBox();
            this.m_txtStartPath = new System.Windows.Forms.TextBox();
            this.m_listView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Date = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.icons = new System.Windows.Forms.ImageList();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.BrowseDir = new System.Windows.Forms.Button();
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.statusBarPanel1 = new System.Windows.Forms.StatusBarPanel();
            this.m_bgWorker = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGO
            // 
            this.btnGO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGO.Location = new System.Drawing.Point(456, 8);
            this.btnGO.Name = "btnGO";
            this.btnGO.Size = new System.Drawing.Size(96, 32);
            this.btnGO.TabIndex = 0;
            this.btnGO.Text = "GO!";
            this.btnGO.Click += new System.EventHandler(this.button1_Click);
            // 
            // m_txtPattern
            // 
            this.m_txtPattern.Location = new System.Drawing.Point(120, 8);
            this.m_txtPattern.Name = "m_txtPattern";
            this.m_txtPattern.Size = new System.Drawing.Size(152, 20);
            this.m_txtPattern.TabIndex = 1;
            this.m_txtPattern.Text = ".ico";
            // 
            // m_txtStartPath
            // 
            this.m_txtStartPath.Location = new System.Drawing.Point(120, 32);
            this.m_txtStartPath.Name = "m_txtStartPath";
            this.m_txtStartPath.Size = new System.Drawing.Size(152, 20);
            this.m_txtStartPath.TabIndex = 2;
            this.m_txtStartPath.Text = "C:\\";
            // 
            // m_listView
            // 
            this.m_listView.AllowDrop = true;
            this.m_listView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.Date,
            this.columnHeader3,
            this.columnHeader4});
            this.m_listView.HideSelection = false;
            this.m_listView.LargeImageList = this.icons;
            this.m_listView.Location = new System.Drawing.Point(0, 64);
            this.m_listView.Name = "m_listView";
            this.m_listView.Size = new System.Drawing.Size(560, 223);
            this.m_listView.SmallImageList = this.icons;
            this.m_listView.TabIndex = 4;
            this.m_listView.UseCompatibleStateImageBehavior = false;
            this.m_listView.View = System.Windows.Forms.View.Details;
            this.m_listView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.MylistView_ColumnClick);
            this.m_listView.ItemActivate += new System.EventHandler(this.MylistView_DoubleClick);
            this.m_listView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.MylistView_ItemDrag);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "FIlename";
            this.columnHeader1.Width = 125;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Dir";
            this.columnHeader2.Width = 250;
            // 
            // Date
            // 
            this.Date.Text = "Last Modified";
            this.Date.Width = 120;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Size";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Attr";
            // 
            // icons
            // 
            this.icons.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.icons.ImageSize = new System.Drawing.Size(16, 16);
            this.icons.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Sottostringa";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Directory";
            // 
            // BrowseDir
            // 
            this.BrowseDir.AutoSize = true;
            this.BrowseDir.Location = new System.Drawing.Point(278, 28);
            this.BrowseDir.Name = "BrowseDir";
            this.BrowseDir.Size = new System.Drawing.Size(68, 30);
            this.BrowseDir.TabIndex = 7;
            this.BrowseDir.Text = "Change dir";
            this.BrowseDir.Click += new System.EventHandler(this.BrowseDir_Click);
            // 
            // statusBar1
            // 
            this.statusBar1.Location = new System.Drawing.Point(0, 285);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.statusBarPanel1});
            this.statusBar1.ShowPanels = true;
            this.statusBar1.Size = new System.Drawing.Size(560, 24);
            this.statusBar1.TabIndex = 8;
            // 
            // statusBarPanel1
            // 
            this.statusBarPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
            this.statusBarPanel1.Name = "statusBarPanel1";
            this.statusBarPanel1.Text = "Press \"GO\"";
            this.statusBarPanel1.Width = 543;
            // 
            // m_bgWorker
            // 
            this.m_bgWorker.WorkerReportsProgress = true;
            this.m_bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.m_bgWorker_DoWork);
            this.m_bgWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.m_bgWorker_ProgressChanged);
            this.m_bgWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.m_bgWorker_RunWorkerCompleted);
            // 
            // Form1
            // 
            this.AcceptButton = this.btnGO;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(560, 309);
            this.Controls.Add(this.statusBar1);
            this.Controls.Add(this.BrowseDir);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_listView);
            this.Controls.Add(this.m_txtStartPath);
            this.Controls.Add(this.m_txtPattern);
            this.Controls.Add(this.btnGO);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "File Find";
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new Form1());
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            btnGO.Enabled = false;
            m_listView.Items.Clear();
            m_listView.ListViewItemSorter = null;
            Cursor = System.Windows.Forms.Cursors.WaitCursor;
            m_Pattern = m_txtPattern.Text.ToUpper();
            m_bgWorker.RunWorkerAsync();
        }

        private void VisitaRicorsiva(string d)
        {
            try
            {
                if (!Directory.Exists(d))
                    return;
                foreach (string s in Directory.GetDirectories(d))
                    VisitaRicorsiva(s);

                foreach (string s in Directory.GetFiles(d))
                {
                    System.IO.FileInfo fi = new FileInfo(s);
                    string q = s.ToUpper();
                    if (q.IndexOf(m_Pattern) > 0)
                        m_bgWorker.ReportProgress(10, fi);
                }
            }
            catch
            { }
            m_bgWorker.ReportProgress(0, d);
        }

        private void MylistView_DoubleClick(object sender, System.EventArgs e)
        {
            foreach (System.Windows.Forms.ListViewItem x in m_listView.SelectedItems)
            {
                try
                {
                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo(System.IO.Path.Combine(x.SubItems[1].Text, x.Text));
                    startInfo.UseShellExecute = true;
                    System.Diagnostics.Process.Start(startInfo);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Errore");
                }
                System.Windows.Forms.Application.DoEvents();
            }
        }

        private void MylistView_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            m_listView.Sort();
        }

        private void BrowseDir_Click(object sender, System.EventArgs e)
        {
            FolderBrowser folderBrowser1 = new FolderBrowser();
            folderBrowser1.OnlyFilesystem = true;
            if (DialogResult.OK == folderBrowser1.ShowDialog())
                m_txtStartPath.Text = folderBrowser1.DirectoryPath;
        }

        private void MylistView_ItemDrag(object sender, System.Windows.Forms.ItemDragEventArgs e)
        {
            int count = m_listView.SelectedItems.Count;
            string[] s = new string[count];
            int i = 0;

            foreach (ListViewItem x in m_listView.SelectedItems)
                s[i++] = System.IO.Path.Combine(x.SubItems[1].Text, x.Text);

            DataObject d = new DataObject();
            d.SetData(DataFormats.FileDrop, s);
            m_listView.DoDragDrop(d, DragDropEffects.Copy);
        }
 
        int BestIconFor(string name)
        {
            string ext = System.IO.Path.GetExtension(name.ToUpper());
            switch (ext)
            {
                case ".EXE":
                case ".ICO":
                case ".CUR":
                case ".LNK":
                    icons.Images.Add(ExtractIconClass.GetIcon(name, true));
                    return icons.Images.Count - 1;
                default:
                    if (ht.ContainsKey(ext))
                        return (int)ht[ext];
                    else
                    {
                        icons.Images.Add(ExtractIconClass.GetIcon(name, true));
                        ht.Add(ext, icons.Images.Count - 1);
                        return icons.Images.Count - 1;
                    }
            }
        }

        private void m_bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            VisitaRicorsiva(m_txtStartPath.Text);
        }

        private void m_bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            FileInfo fileinfo = e.UserState as FileInfo;
            if (e.ProgressPercentage > 0)
            {
                if (fileinfo != null)
                {
                    int icon = BestIconFor(fileinfo.FullName);
                    System.Windows.Forms.ListViewItem x = new ListViewItem(System.IO.Path.GetFileName(fileinfo.FullName), icon);
                    x.SubItems.Add(fileinfo.DirectoryName);
                    x.SubItems.Add(fileinfo.LastWriteTime.ToString());
                    x.SubItems.Add(fileinfo.Length.ToString("N0"));
                    string strAttr = "";

                    if ((fileinfo.Attributes & FileAttributes.Archive) != 0)
                        strAttr += "A";

                    if ((fileinfo.Attributes & FileAttributes.Hidden) != 0)
                        strAttr += "H";

                    if ((fileinfo.Attributes & FileAttributes.ReadOnly) != 0)
                        strAttr += "R";

                    if ((fileinfo.Attributes & FileAttributes.System) != 0)
                        strAttr += "S";

                    x.SubItems.Add(strAttr);
                    m_listView.Items.Add(x);
                }
            }
            string s = e.UserState as string;
            if (s != null)
            {
                counter--;
                if (counter <= 0)
                {
                    counter = basecounter;
                    statusBar1.Panels[0].Text = s;
                }
            }
        }

        private void m_bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Cursor = System.Windows.Forms.Cursors.Default;
            foreach (ColumnHeader ch in m_listView.Columns)
            {
                ch.Width = -2;
            }
            m_listView.ListViewItemSorter = lvwColumnSorter;
            statusBar1.Panels[0].Text = m_listView.Items.Count.ToString() + " items found.";
            btnGO.Enabled = true;
        }
    }
}