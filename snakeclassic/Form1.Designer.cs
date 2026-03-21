namespace snakeclassic
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            this.hudPanel = new System.Windows.Forms.Panel();
            this.lblNick = new System.Windows.Forms.Label();
            this.lblScore = new System.Windows.Forms.Label();
            this.lblLevel = new System.Windows.Forms.Label();
            this.gamePanel = new System.Windows.Forms.Panel();
            this.btnRestart = new System.Windows.Forms.Button();
            this.btnMenu = new System.Windows.Forms.Button();
            this.hudPanel.SuspendLayout();
            this.SuspendLayout();

            // ── gameTimer ─────────────────────────────────────────────
            this.gameTimer.Interval = 120;

            // ── hudPanel ──────────────────────────────────────────────
            this.hudPanel.BackColor = System.Drawing.Color.FromArgb(30, 10, 60);
            this.hudPanel.Controls.Add(this.lblNick);
            this.hudPanel.Controls.Add(this.lblScore);
            this.hudPanel.Controls.Add(this.lblLevel);
            this.hudPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.hudPanel.Height = 40;

            // ── lblNick ───────────────────────────────────────────────
            this.lblNick.AutoSize = false;
            this.lblNick.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblNick.Width = 200;
            this.lblNick.ForeColor = System.Drawing.Color.Plum;
            this.lblNick.Font = new System.Drawing.Font("Arial", 11, System.Drawing.FontStyle.Bold);
            this.lblNick.Text = "Игрок";
            this.lblNick.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblNick.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);

            // ── lblScore ──────────────────────────────────────────────
            this.lblScore.AutoSize = false;
            this.lblScore.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblScore.ForeColor = System.Drawing.Color.Yellow;
            this.lblScore.Font = new System.Drawing.Font("Arial", 13, System.Drawing.FontStyle.Bold);
            this.lblScore.Text = "Счёт: 0";
            this.lblScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // ── lblLevel ──────────────────────────────────────────────
            this.lblLevel.AutoSize = false;
            this.lblLevel.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblLevel.Width = 100;
            this.lblLevel.ForeColor = System.Drawing.Color.LightCyan;
            this.lblLevel.Font = new System.Drawing.Font("Arial", 11, System.Drawing.FontStyle.Bold);
            this.lblLevel.Text = "Ур. 1";
            this.lblLevel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblLevel.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);

            // ── gamePanel ─────────────────────────────────────────────
            this.gamePanel.BackColor = System.Drawing.Color.FromArgb(15, 5, 30);
            this.gamePanel.Location = new System.Drawing.Point(0, 40);
            this.gamePanel.Size = new System.Drawing.Size(640, 440);
            this.gamePanel.Anchor = System.Windows.Forms.AnchorStyles.Top
                                  | System.Windows.Forms.AnchorStyles.Bottom
                                  | System.Windows.Forms.AnchorStyles.Left
                                  | System.Windows.Forms.AnchorStyles.Right;
            this.gamePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.gamePanel_Paint);

            // ── btnRestart ────────────────────────────────────────────
            this.btnRestart.Text = "▶ РЕСТАРТ";
            this.btnRestart.Size = new System.Drawing.Size(150, 40);
            this.btnRestart.Location = new System.Drawing.Point(175, 490);
            this.btnRestart.BackColor = System.Drawing.Color.FromArgb(60, 20, 120);
            this.btnRestart.ForeColor = System.Drawing.Color.White;
            this.btnRestart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestart.Font = new System.Drawing.Font("Arial", 11, System.Drawing.FontStyle.Bold);
            this.btnRestart.Visible = false;
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);

            // ── btnMenu ───────────────────────────────────────────────
            this.btnMenu.Text = "⬅ МЕНЮ";
            this.btnMenu.Size = new System.Drawing.Size(150, 40);
            this.btnMenu.Location = new System.Drawing.Point(345, 490);
            this.btnMenu.BackColor = System.Drawing.Color.FromArgb(120, 20, 20);
            this.btnMenu.ForeColor = System.Drawing.Color.White;
            this.btnMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMenu.Font = new System.Drawing.Font("Arial", 11, System.Drawing.FontStyle.Bold);
            this.btnMenu.Visible = false;
            this.btnMenu.Click += new System.EventHandler(this.btnMenu_Click);

            // ── Form1 ─────────────────────────────────────────────────
            this.ClientSize = new System.Drawing.Size(640, 540);
            this.Controls.Add(this.gamePanel);
            this.Controls.Add(this.hudPanel);
            this.Controls.Add(this.btnRestart);
            this.Controls.Add(this.btnMenu);
            this.Text = "Snake Classic";
            this.BackColor = System.Drawing.Color.FromArgb(15, 5, 30);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.KeyPreview = true;
            this.Load += new System.EventHandler(this.Form1_Load);

            this.hudPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Timer gameTimer;
        private System.Windows.Forms.Panel hudPanel;
        private System.Windows.Forms.Label lblNick;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.Label lblLevel;
        private System.Windows.Forms.Panel gamePanel;
        private System.Windows.Forms.Button btnRestart;
        private System.Windows.Forms.Button btnMenu;
    }
}
