namespace HSServer {
    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.DebugBtn = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.againstBotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.againstPlayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.arenaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scriptsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblAccountInfoPacks = new System.Windows.Forms.Label();
            this.groupAccountInfo = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblAccountInfoRank = new System.Windows.Forms.Label();
            this.lblAccountInfoDust = new System.Windows.Forms.Label();
            this.lblAccountInfoGold = new System.Windows.Forms.Label();
            this.fileOpenScript = new System.Windows.Forms.OpenFileDialog();
            this.btnConnect = new System.Windows.Forms.Button();
            this.pnlInfo = new System.Windows.Forms.Panel();
            this.groupTask = new System.Windows.Forms.GroupBox();
            this.pnlConnect = new System.Windows.Forms.Panel();
            this.lblConnecting = new System.Windows.Forms.Label();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.menuStrip1.SuspendLayout();
            this.groupAccountInfo.SuspendLayout();
            this.pnlInfo.SuspendLayout();
            this.pnlConnect.SuspendLayout();
            this.SuspendLayout();
            // 
            // DebugBtn
            // 
            this.DebugBtn.Location = new System.Drawing.Point(62, 262);
            this.DebugBtn.Name = "DebugBtn";
            this.DebugBtn.Size = new System.Drawing.Size(75, 23);
            this.DebugBtn.TabIndex = 2;
            this.DebugBtn.Text = "Debug";
            this.DebugBtn.UseVisualStyleBackColor = true;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipText = "Nice";
            this.notifyIcon1.BalloonTipTitle = "Okay";
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Le Bot Doot Doot";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.scriptsToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(217, 24);
            this.menuStrip1.TabIndex = 13;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.againstBotToolStripMenuItem,
            this.againstPlayerToolStripMenuItem,
            this.arenaToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.fileToolStripMenuItem.Text = "Play";
            // 
            // againstBotToolStripMenuItem
            // 
            this.againstBotToolStripMenuItem.Name = "againstBotToolStripMenuItem";
            this.againstBotToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.againstBotToolStripMenuItem.Text = "Against Innkeeper";
            // 
            // againstPlayerToolStripMenuItem
            // 
            this.againstPlayerToolStripMenuItem.Name = "againstPlayerToolStripMenuItem";
            this.againstPlayerToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.againstPlayerToolStripMenuItem.Text = "Against Player";
            // 
            // arenaToolStripMenuItem
            // 
            this.arenaToolStripMenuItem.Name = "arenaToolStripMenuItem";
            this.arenaToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.arenaToolStripMenuItem.Text = "Arena";
            // 
            // scriptsToolStripMenuItem
            // 
            this.scriptsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addScriptToolStripMenuItem});
            this.scriptsToolStripMenuItem.Name = "scriptsToolStripMenuItem";
            this.scriptsToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.scriptsToolStripMenuItem.Text = "Scripts";
            // 
            // addScriptToolStripMenuItem
            // 
            this.addScriptToolStripMenuItem.Name = "addScriptToolStripMenuItem";
            this.addScriptToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.addScriptToolStripMenuItem.Text = "Run Script";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.propertiesToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // propertiesToolStripMenuItem
            // 
            this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.propertiesToolStripMenuItem.Text = "Properties";
            // 
            // lblAccountInfoPacks
            // 
            this.lblAccountInfoPacks.AutoSize = true;
            this.lblAccountInfoPacks.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccountInfoPacks.Location = new System.Drawing.Point(6, 16);
            this.lblAccountInfoPacks.Name = "lblAccountInfoPacks";
            this.lblAccountInfoPacks.Size = new System.Drawing.Size(58, 13);
            this.lblAccountInfoPacks.TabIndex = 15;
            this.lblAccountInfoPacks.Text = "Packs: (...)";
            // 
            // groupAccountInfo
            // 
            this.groupAccountInfo.BackColor = System.Drawing.SystemColors.Control;
            this.groupAccountInfo.Controls.Add(this.groupBox1);
            this.groupAccountInfo.Controls.Add(this.lblAccountInfoRank);
            this.groupAccountInfo.Controls.Add(this.lblAccountInfoDust);
            this.groupAccountInfo.Controls.Add(this.lblAccountInfoGold);
            this.groupAccountInfo.Controls.Add(this.lblAccountInfoPacks);
            this.groupAccountInfo.Cursor = System.Windows.Forms.Cursors.Default;
            this.groupAccountInfo.Location = new System.Drawing.Point(0, 2);
            this.groupAccountInfo.Name = "groupAccountInfo";
            this.groupAccountInfo.Size = new System.Drawing.Size(198, 91);
            this.groupAccountInfo.TabIndex = 17;
            this.groupAccountInfo.TabStop = false;
            this.groupAccountInfo.Text = "Account Info (...)";
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(0, 97);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 100);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // lblAccountInfoRank
            // 
            this.lblAccountInfoRank.AutoSize = true;
            this.lblAccountInfoRank.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccountInfoRank.Location = new System.Drawing.Point(6, 55);
            this.lblAccountInfoRank.Name = "lblAccountInfoRank";
            this.lblAccountInfoRank.Size = new System.Drawing.Size(54, 13);
            this.lblAccountInfoRank.TabIndex = 18;
            this.lblAccountInfoRank.Text = "Rank: (...)";
            // 
            // lblAccountInfoDust
            // 
            this.lblAccountInfoDust.AutoSize = true;
            this.lblAccountInfoDust.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccountInfoDust.Location = new System.Drawing.Point(6, 42);
            this.lblAccountInfoDust.Name = "lblAccountInfoDust";
            this.lblAccountInfoDust.Size = new System.Drawing.Size(50, 13);
            this.lblAccountInfoDust.TabIndex = 17;
            this.lblAccountInfoDust.Text = "Dust: (...)";
            // 
            // lblAccountInfoGold
            // 
            this.lblAccountInfoGold.AutoSize = true;
            this.lblAccountInfoGold.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccountInfoGold.Location = new System.Drawing.Point(6, 29);
            this.lblAccountInfoGold.Name = "lblAccountInfoGold";
            this.lblAccountInfoGold.Size = new System.Drawing.Size(50, 13);
            this.lblAccountInfoGold.TabIndex = 16;
            this.lblAccountInfoGold.Text = "Gold: (...)";
            // 
            // fileOpenScript
            // 
            this.fileOpenScript.FileName = "openFileDialog1";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(62, 23);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 19;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            // 
            // pnlInfo
            // 
            this.pnlInfo.Controls.Add(this.groupTask);
            this.pnlInfo.Controls.Add(this.DebugBtn);
            this.pnlInfo.Controls.Add(this.groupAccountInfo);
            this.pnlInfo.Location = new System.Drawing.Point(9, 30);
            this.pnlInfo.Margin = new System.Windows.Forms.Padding(2);
            this.pnlInfo.Name = "pnlInfo";
            this.pnlInfo.Size = new System.Drawing.Size(198, 294);
            this.pnlInfo.TabIndex = 19;
            this.pnlInfo.Visible = false;
            // 
            // groupTask
            // 
            this.groupTask.Location = new System.Drawing.Point(0, 99);
            this.groupTask.Name = "groupTask";
            this.groupTask.Size = new System.Drawing.Size(198, 156);
            this.groupTask.TabIndex = 18;
            this.groupTask.TabStop = false;
            this.groupTask.Text = "Task (None)";
            // 
            // pnlConnect
            // 
            this.pnlConnect.Controls.Add(this.btnConnect);
            this.pnlConnect.Controls.Add(this.lblConnecting);
            this.pnlConnect.Location = new System.Drawing.Point(9, 30);
            this.pnlConnect.Margin = new System.Windows.Forms.Padding(2);
            this.pnlConnect.Name = "pnlConnect";
            this.pnlConnect.Size = new System.Drawing.Size(198, 294);
            this.pnlConnect.TabIndex = 20;
            // 
            // lblConnecting
            // 
            this.lblConnecting.Location = new System.Drawing.Point(0, 49);
            this.lblConnecting.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblConnecting.Name = "lblConnecting";
            this.lblConnecting.Size = new System.Drawing.Size(198, 129);
            this.lblConnecting.TabIndex = 20;
            this.lblConnecting.Text = "connecting...";
            this.lblConnecting.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblConnecting.Visible = false;
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(217, 334);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.pnlInfo);
            this.Controls.Add(this.pnlConnect);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "HSInfo";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupAccountInfo.ResumeLayout(false);
            this.groupAccountInfo.PerformLayout();
            this.pnlInfo.ResumeLayout(false);
            this.pnlConnect.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Button DebugBtn;
        public System.Windows.Forms.NotifyIcon notifyIcon1;
        public System.Windows.Forms.MenuStrip menuStrip1;
        public System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem againstBotToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem againstPlayerToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem scriptsToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem addScriptToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem arenaToolStripMenuItem;
        public System.Windows.Forms.Label lblAccountInfoPacks;
        public System.Windows.Forms.GroupBox groupAccountInfo;
        public System.Windows.Forms.Label lblAccountInfoDust;
        public System.Windows.Forms.Label lblAccountInfoGold;
        public System.Windows.Forms.OpenFileDialog fileOpenScript;
        public System.Windows.Forms.Label lblAccountInfoRank;
        public System.Windows.Forms.Button btnConnect;
        public System.Windows.Forms.Panel pnlInfo;
        public System.Windows.Forms.Panel pnlConnect;
        public System.Windows.Forms.Label lblConnecting;
        public System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupTask;
    }
}