namespace snakeclassic
{
    partial class leadbordfrm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources =
                new System.ComponentModel.ComponentResourceManager(typeof(leadbordfrm));

            this.panel1 = new System.Windows.Forms.Panel();
            this.svernutbtn = new System.Windows.Forms.Button();
            this.closebtn = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.headerRow = new System.Windows.Forms.Panel();
            this.lblHPlace = new System.Windows.Forms.Label();
            this.lblHNick = new System.Windows.Forms.Label();
            this.lblHScore = new System.Windows.Forms.Label();
            this.tablePanel = new System.Windows.Forms.FlowLayoutPanel();
            this.nazad_btn = new System.Windows.Forms.Button();

            this.panel1.SuspendLayout();
            this.headerRow.SuspendLayout();
            this.SuspendLayout();

            // ── panel1 (шапка, drag) ──────────────────────────────────────
            this.panel1.BackColor = System.Drawing.Color.FromArgb(40, 20, 70);
            this.panel1.Controls.Add(this.svernutbtn);
            this.panel1.Controls.Add(this.closebtn);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(460, 38);
            this.panel1.TabIndex = 0;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);

            // ── svernutbtn ────────────────────────────────────────────────
            this.svernutbtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("svernutbtn.BackgroundImage")));
            this.svernutbtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.svernutbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.svernutbtn.FlatAppearance.BorderSize = 0;
            this.svernutbtn.Location = new System.Drawing.Point(396, 4);
            this.svernutbtn.Name = "svernutbtn";
            this.svernutbtn.Size = new System.Drawing.Size(30, 30);
            this.svernutbtn.TabIndex = 1;
            this.svernutbtn.UseVisualStyleBackColor = true;
            this.svernutbtn.Click += new System.EventHandler(this.svernutbtn_Click);

            // ── closebtn ──────────────────────────────────────────────────
            this.closebtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("closebtn.BackgroundImage")));
            this.closebtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.closebtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closebtn.FlatAppearance.BorderSize = 0;
            this.closebtn.Location = new System.Drawing.Point(426, 4);
            this.closebtn.Name = "closebtn";
            this.closebtn.Size = new System.Drawing.Size(30, 30);
            this.closebtn.TabIndex = 2;
            this.closebtn.UseVisualStyleBackColor = true;
            this.closebtn.Click += new System.EventHandler(this.closebtn_Click);

            // ── lblTitle (заголовок «ТАБЛИЦА ЛИДЕРОВ») ────────────────────
            this.lblTitle.Text = "ТАБЛИЦА ЛИДЕРОВ";
            this.lblTitle.Font = new System.Drawing.Font("Impact", 22f, System.Drawing.FontStyle.Regular);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(255, 80, 220);
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTitle.Location = new System.Drawing.Point(0, 48);
            this.lblTitle.Size = new System.Drawing.Size(460, 50);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.TabIndex = 3;

            // Свечение через OutlineLabel — делаем через Paint
            this.lblTitle.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                // Тень / свечение
                using (var shadowBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(120, 180, 0, 255)))
                {
                    for (int dx = -2; dx <= 2; dx++)
                        for (int dy = -2; dy <= 2; dy++)
                            if (dx != 0 || dy != 0)
                                e.Graphics.DrawString(this.lblTitle.Text,
                                    this.lblTitle.Font, shadowBrush,
                                    new System.Drawing.RectangleF(dx, dy, this.lblTitle.Width, this.lblTitle.Height),
                                    new System.Drawing.StringFormat { Alignment = System.Drawing.StringAlignment.Center, LineAlignment = System.Drawing.StringAlignment.Center });
                }
                using (var mainBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 80, 220)))
                {
                    e.Graphics.DrawString(this.lblTitle.Text,
                        this.lblTitle.Font, mainBrush,
                        new System.Drawing.RectangleF(0, 0, this.lblTitle.Width, this.lblTitle.Height),
                        new System.Drawing.StringFormat { Alignment = System.Drawing.StringAlignment.Center, LineAlignment = System.Drawing.StringAlignment.Center });
                }
            };

            // ── headerRow (шапка таблицы) ─────────────────────────────────
            this.headerRow.BackColor = System.Drawing.Color.FromArgb(80, 120, 60, 200);
            this.headerRow.Controls.Add(this.lblHPlace);
            this.headerRow.Controls.Add(this.lblHNick);
            this.headerRow.Controls.Add(this.lblHScore);
            this.headerRow.Location = new System.Drawing.Point(20, 108);
            this.headerRow.Name = "headerRow";
            this.headerRow.Size = new System.Drawing.Size(420, 30);
            this.headerRow.TabIndex = 4;

            // ── lblHPlace ─────────────────────────────────────────────────
            this.lblHPlace.Text = "МЕСТО";
            this.lblHPlace.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            this.lblHPlace.ForeColor = System.Drawing.Color.FromArgb(255, 215, 0);
            this.lblHPlace.Location = new System.Drawing.Point(6, 0);
            this.lblHPlace.Size = new System.Drawing.Size(56, 30);
            this.lblHPlace.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblHPlace.Name = "lblHPlace";

            // ── lblHNick ──────────────────────────────────────────────────
            this.lblHNick.Text = "ИГРОК";
            this.lblHNick.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            this.lblHNick.ForeColor = System.Drawing.Color.FromArgb(255, 215, 0);
            this.lblHNick.Location = new System.Drawing.Point(62, 0);
            this.lblHNick.Size = new System.Drawing.Size(180, 30);
            this.lblHNick.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblHNick.Name = "lblHNick";

            // ── lblHScore ─────────────────────────────────────────────────
            this.lblHScore.Text = "ОЧКИ";
            this.lblHScore.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            this.lblHScore.ForeColor = System.Drawing.Color.FromArgb(255, 215, 0);
            this.lblHScore.Location = new System.Drawing.Point(248, 0);
            this.lblHScore.Size = new System.Drawing.Size(110, 30);
            this.lblHScore.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblHScore.Name = "lblHScore";

            // ── tablePanel (FlowLayoutPanel со строками) ──────────────────
            this.tablePanel.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.tablePanel.WrapContents = false;
            this.tablePanel.AutoScroll = false;
            this.tablePanel.Location = new System.Drawing.Point(20, 144);
            this.tablePanel.Name = "tablePanel";
            this.tablePanel.Size = new System.Drawing.Size(420, 380);
            this.tablePanel.TabIndex = 5;

            // ── nazad_btn ─────────────────────────────────────────────────
            this.nazad_btn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("nazad_btn.BackgroundImage")));
            this.nazad_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.nazad_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.nazad_btn.FlatAppearance.BorderSize = 0;
            this.nazad_btn.BackColor = System.Drawing.Color.Transparent;
            this.nazad_btn.Location = new System.Drawing.Point(145, 540);
            this.nazad_btn.Name = "nazad_btn";
            this.nazad_btn.Size = new System.Drawing.Size(171, 49);
            this.nazad_btn.TabIndex = 6;
            this.nazad_btn.UseVisualStyleBackColor = false;
            this.nazad_btn.Click += new System.EventHandler(this.nazad_btn_Click);

            // ── Form ──────────────────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(460, 610);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Name = "leadbordfrm";
            this.Text = "Таблица лидеров";
            this.Load += new System.EventHandler(this.leadbordfrm_Load);

            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.headerRow);
            this.Controls.Add(this.tablePanel);
            this.Controls.Add(this.nazad_btn);

            this.panel1.ResumeLayout(false);
            this.headerRow.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button svernutbtn;
        private System.Windows.Forms.Button closebtn;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel headerRow;
        private System.Windows.Forms.Label lblHPlace;
        private System.Windows.Forms.Label lblHNick;
        private System.Windows.Forms.Label lblHScore;
        private System.Windows.Forms.FlowLayoutPanel tablePanel;
        private System.Windows.Forms.Button nazad_btn;
    }
}
