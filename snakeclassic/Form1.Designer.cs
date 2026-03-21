namespace snakeclassic
{
    // Панель без мерцания
    public class DoubleBufferedPanel : System.Windows.Forms.Panel
    {
        public DoubleBufferedPanel()
        {
            this.DoubleBuffered = true;
            this.SetStyle(
                System.Windows.Forms.ControlStyles.AllPaintingInWmPaint |
                System.Windows.Forms.ControlStyles.UserPaint |
                System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer,
                true);
            this.UpdateStyles();
        }
    }

    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            this.hudPanel = new System.Windows.Forms.Panel();
            this.lblNick = new System.Windows.Forms.Label();
            this.lblScore = new System.Windows.Forms.Label();
            this.lblLevel = new System.Windows.Forms.Label();
            this.gamePanel = new DoubleBufferedPanel();
            this.btnRestart = new System.Windows.Forms.Button();
            this.btnMenu = new System.Windows.Forms.Button();

            this.hudPanel.SuspendLayout();
            this.SuspendLayout();

            // ── gameTimer ─────────────────────────────────────────────
            this.gameTimer.Interval = 120;
            this.gameTimer.Tick += new System.EventHandler(this.GameTimer_Tick);

            // ── hudPanel ──────────────────────────────────────────────
            this.hudPanel.BackColor = System.Drawing.Color.FromArgb(50, 0, 80);
            this.hudPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.hudPanel.Height = 42;
            this.hudPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.hudPanel_Paint);

            // Порядок Controls.Add важен для Dock: Fill -> Right -> Left
            this.hudPanel.Controls.Add(this.lblScore);
            this.hudPanel.Controls.Add(this.lblLevel);
            this.hudPanel.Controls.Add(this.lblNick);

            // ── lblNick (слева) ───────────────────────────────────────
            this.lblNick.AutoSize = false;
            this.lblNick.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblNick.Width = 220;
            this.lblNick.ForeColor = System.Drawing.Color.FromArgb(220, 160, 255);
            this.lblNick.Font = new System.Drawing.Font("Segoe UI", 11,
                                         System.Drawing.FontStyle.Bold);
            this.lblNick.Text = "Игрок";
            this.lblNick.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblNick.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);

            // ── lblScore (Fill) — НЕВИДИМЫЙ, только держит место
            //    Счёт рисуется в hudPanel_Paint через Graphics
            this.lblScore.AutoSize = false;
            this.lblScore.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblScore.ForeColor = System.Drawing.Color.Transparent;
            this.lblScore.BackColor = System.Drawing.Color.Transparent;
            this.lblScore.Text = "";
            this.lblScore.Padding = new System.Windows.Forms.Padding(0);

            // ── lblLevel (справа) ─────────────────────────────────────
            this.lblLevel.AutoSize = false;
            this.lblLevel.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblLevel.Width = 110;
            this.lblLevel.ForeColor = System.Drawing.Color.FromArgb(0, 220, 255);
            this.lblLevel.Font = new System.Drawing.Font("Segoe UI", 11,
                                           System.Drawing.FontStyle.Bold);
            this.lblLevel.Text = "Ур.1";
            this.lblLevel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblLevel.Padding = new System.Windows.Forms.Padding(0, 0, 12, 0);

            // ── gamePanel ─────────────────────────────────────────────
            this.gamePanel.BackColor = System.Drawing.Color.FromArgb(10, 2, 20);
            this.gamePanel.Location = new System.Drawing.Point(0, 42);
            this.gamePanel.Size = new System.Drawing.Size(800, 460);
            this.gamePanel.Anchor =
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Bottom |
                System.Windows.Forms.AnchorStyles.Left |
                System.Windows.Forms.AnchorStyles.Right;
            this.gamePanel.Paint += new System.Windows.Forms.PaintEventHandler(
                                        this.gamePanel_Paint);

            // ── btnRestart ────────────────────────────────────────────
            this.btnRestart.Text = "▶  РЕСТАРТ";
            this.btnRestart.Size = new System.Drawing.Size(160, 44);
            this.btnRestart.Location = new System.Drawing.Point(200, 510);
            this.btnRestart.BackColor = System.Drawing.Color.FromArgb(50, 0, 100);
            this.btnRestart.ForeColor = System.Drawing.Color.White;
            this.btnRestart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestart.FlatAppearance.BorderColor =
                System.Drawing.Color.FromArgb(140, 0, 255);
            this.btnRestart.FlatAppearance.BorderSize = 2;
            this.btnRestart.Font = new System.Drawing.Font("Segoe UI", 11,
                                          System.Drawing.FontStyle.Bold);
            this.btnRestart.Visible = false;
            this.btnRestart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);

            // ── btnMenu ───────────────────────────────────────────────
            this.btnMenu.Text = "⬅  МЕНЮ";
            this.btnMenu.Size = new System.Drawing.Size(160, 44);
            this.btnMenu.Location = new System.Drawing.Point(440, 510);
            this.btnMenu.BackColor = System.Drawing.Color.FromArgb(100, 0, 30);
            this.btnMenu.ForeColor = System.Drawing.Color.White;
            this.btnMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMenu.FlatAppearance.BorderColor =
                System.Drawing.Color.FromArgb(255, 60, 100);
            this.btnMenu.FlatAppearance.BorderSize = 2;
            this.btnMenu.Font = new System.Drawing.Font("Segoe UI", 11,
                                       System.Drawing.FontStyle.Bold);
            this.btnMenu.Visible = false;
            this.btnMenu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMenu.Click += new System.EventHandler(this.btnMenu_Click);

            // ── Form1 ─────────────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(15, 5, 30);
            this.ClientSize = new System.Drawing.Size(800, 502);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Snake Classic";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);

            this.Controls.Add(this.gamePanel);
            this.Controls.Add(this.hudPanel);
            this.Controls.Add(this.btnRestart);
            this.Controls.Add(this.btnMenu);

            this.hudPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Timer gameTimer;
        private System.Windows.Forms.Panel hudPanel;
        private System.Windows.Forms.Label lblNick;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.Label lblLevel;
        private DoubleBufferedPanel gamePanel;
        private System.Windows.Forms.Button btnRestart;
        private System.Windows.Forms.Button btnMenu;
    }
}
