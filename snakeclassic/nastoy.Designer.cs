namespace snakeclassic
{
    partial class nastoy
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(nastoy));
            this.panel1 = new System.Windows.Forms.Panel();
            this.svernutbtn = new System.Windows.Forms.Button();
            this.closebtn = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSkins = new System.Windows.Forms.Label();
            this.lblFood = new System.Windows.Forms.Label();
            this.skinPanel0 = new System.Windows.Forms.Panel();
            this.skinPic0 = new System.Windows.Forms.PictureBox();
            this.skinLbl0 = new System.Windows.Forms.Label();
            this.skinPanel1 = new System.Windows.Forms.Panel();
            this.skinPic1 = new System.Windows.Forms.PictureBox();
            this.skinLbl1 = new System.Windows.Forms.Label();
            this.skinPanel2 = new System.Windows.Forms.Panel();
            this.skinPic2 = new System.Windows.Forms.PictureBox();
            this.skinLbl2 = new System.Windows.Forms.Label();
            this.skinPanel3 = new System.Windows.Forms.Panel();
            this.skinPic3 = new System.Windows.Forms.PictureBox();
            this.skinLbl3 = new System.Windows.Forms.Label();
            this.foodPanel0 = new System.Windows.Forms.Panel();
            this.foodPic0 = new System.Windows.Forms.PictureBox();
            this.foodLbl0 = new System.Windows.Forms.Label();
            this.foodPanel1 = new System.Windows.Forms.Panel();
            this.foodPic1 = new System.Windows.Forms.PictureBox();
            this.foodLbl1 = new System.Windows.Forms.Label();
            this.btn_gotov = new System.Windows.Forms.Button();
            this.nazad_btn = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.skinPanel0.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.skinPic0)).BeginInit();
            this.skinPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.skinPic1)).BeginInit();
            this.skinPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.skinPic2)).BeginInit();
            this.skinPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.skinPic3)).BeginInit();
            this.foodPanel0.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.foodPic0)).BeginInit();
            this.foodPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.foodPic1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.svernutbtn);
            this.panel1.Controls.Add(this.closebtn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(640, 38);
            this.panel1.TabIndex = 0;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // svernutbtn
            // 
            this.svernutbtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("svernutbtn.BackgroundImage")));
            this.svernutbtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.svernutbtn.FlatAppearance.BorderSize = 0;
            this.svernutbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.svernutbtn.Location = new System.Drawing.Point(575, 4);
            this.svernutbtn.Name = "svernutbtn";
            this.svernutbtn.Size = new System.Drawing.Size(28, 28);
            this.svernutbtn.TabIndex = 1;
            this.svernutbtn.UseVisualStyleBackColor = true;
            this.svernutbtn.Click += new System.EventHandler(this.svernutbtn_Click);
            // 
            // closebtn
            // 
            this.closebtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("closebtn.BackgroundImage")));
            this.closebtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.closebtn.FlatAppearance.BorderSize = 0;
            this.closebtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closebtn.Location = new System.Drawing.Point(608, 4);
            this.closebtn.Name = "closebtn";
            this.closebtn.Size = new System.Drawing.Size(28, 28);
            this.closebtn.TabIndex = 0;
            this.closebtn.UseVisualStyleBackColor = true;
            this.closebtn.Click += new System.EventHandler(this.closebtn_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Arial Black", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(80)))), ((int)(((byte)(220)))));
            this.lblTitle.Location = new System.Drawing.Point(0, 42);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(640, 68);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "НАСТРОЙКИ";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSkins
            // 
            this.lblSkins.BackColor = System.Drawing.Color.Transparent;
            this.lblSkins.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.lblSkins.ForeColor = System.Drawing.Color.White;
            this.lblSkins.Location = new System.Drawing.Point(0, 118);
            this.lblSkins.Name = "lblSkins";
            this.lblSkins.Size = new System.Drawing.Size(640, 26);
            this.lblSkins.TabIndex = 2;
            this.lblSkins.Text = "СКИН ЗМЕЙКИ";
            this.lblSkins.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFood
            // 
            this.lblFood.BackColor = System.Drawing.Color.Transparent;
            this.lblFood.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.lblFood.ForeColor = System.Drawing.Color.White;
            this.lblFood.Location = new System.Drawing.Point(0, 320);
            this.lblFood.Name = "lblFood";
            this.lblFood.Size = new System.Drawing.Size(640, 26);
            this.lblFood.TabIndex = 7;
            this.lblFood.Text = "ТИП ЕДЫ";
            this.lblFood.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // skinPanel0
            // 
            this.skinPanel0.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(20)))), ((int)(((byte)(100)))));
            this.skinPanel0.Controls.Add(this.skinPic0);
            this.skinPanel0.Controls.Add(this.skinLbl0);
            this.skinPanel0.Cursor = System.Windows.Forms.Cursors.Hand;
            this.skinPanel0.Location = new System.Drawing.Point(37, 150);
            this.skinPanel0.Name = "skinPanel0";
            this.skinPanel0.Size = new System.Drawing.Size(130, 155);
            this.skinPanel0.TabIndex = 3;
            this.skinPanel0.Click += new System.EventHandler(this.skinPanel_Click);
            // 
            // skinPic0
            // 
            this.skinPic0.BackColor = System.Drawing.Color.Transparent;
            this.skinPic0.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("skinPic0.BackgroundImage")));
            this.skinPic0.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.skinPic0.Cursor = System.Windows.Forms.Cursors.Hand;
            this.skinPic0.Location = new System.Drawing.Point(10, 10);
            this.skinPic0.Name = "skinPic0";
            this.skinPic0.Size = new System.Drawing.Size(110, 110);
            this.skinPic0.TabIndex = 0;
            this.skinPic0.TabStop = false;
            this.skinPic0.Click += new System.EventHandler(this.skinPanel_Click);
            // 
            // skinLbl0
            // 
            this.skinLbl0.BackColor = System.Drawing.Color.Transparent;
            this.skinLbl0.Cursor = System.Windows.Forms.Cursors.Hand;
            this.skinLbl0.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.skinLbl0.ForeColor = System.Drawing.Color.White;
            this.skinLbl0.Location = new System.Drawing.Point(0, 127);
            this.skinLbl0.Name = "skinLbl0";
            this.skinLbl0.Size = new System.Drawing.Size(130, 24);
            this.skinLbl0.TabIndex = 1;
            this.skinLbl0.Text = "Зелёная";
            this.skinLbl0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.skinLbl0.Click += new System.EventHandler(this.skinPanel_Click);
            // 
            // skinPanel1
            // 
            this.skinPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(20)))), ((int)(((byte)(100)))));
            this.skinPanel1.Controls.Add(this.skinPic1);
            this.skinPanel1.Controls.Add(this.skinLbl1);
            this.skinPanel1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.skinPanel1.Location = new System.Drawing.Point(182, 150);
            this.skinPanel1.Name = "skinPanel1";
            this.skinPanel1.Size = new System.Drawing.Size(130, 155);
            this.skinPanel1.TabIndex = 4;
            this.skinPanel1.Click += new System.EventHandler(this.skinPanel_Click);
            // 
            // skinPic1
            // 
            this.skinPic1.BackColor = System.Drawing.Color.Transparent;
            this.skinPic1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("skinPic1.BackgroundImage")));
            this.skinPic1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.skinPic1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.skinPic1.Location = new System.Drawing.Point(10, 10);
            this.skinPic1.Name = "skinPic1";
            this.skinPic1.Size = new System.Drawing.Size(110, 110);
            this.skinPic1.TabIndex = 0;
            this.skinPic1.TabStop = false;
            this.skinPic1.Click += new System.EventHandler(this.skinPanel_Click);
            // 
            // skinLbl1
            // 
            this.skinLbl1.BackColor = System.Drawing.Color.Transparent;
            this.skinLbl1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.skinLbl1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.skinLbl1.ForeColor = System.Drawing.Color.White;
            this.skinLbl1.Location = new System.Drawing.Point(0, 127);
            this.skinLbl1.Name = "skinLbl1";
            this.skinLbl1.Size = new System.Drawing.Size(130, 24);
            this.skinLbl1.TabIndex = 1;
            this.skinLbl1.Text = "Синяя";
            this.skinLbl1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.skinLbl1.Click += new System.EventHandler(this.skinPanel_Click);
            // 
            // skinPanel2
            // 
            this.skinPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(20)))), ((int)(((byte)(100)))));
            this.skinPanel2.Controls.Add(this.skinPic2);
            this.skinPanel2.Controls.Add(this.skinLbl2);
            this.skinPanel2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.skinPanel2.Location = new System.Drawing.Point(327, 150);
            this.skinPanel2.Name = "skinPanel2";
            this.skinPanel2.Size = new System.Drawing.Size(130, 155);
            this.skinPanel2.TabIndex = 5;
            this.skinPanel2.Click += new System.EventHandler(this.skinPanel_Click);
            // 
            // skinPic2
            // 
            this.skinPic2.BackColor = System.Drawing.Color.Transparent;
            this.skinPic2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("skinPic2.BackgroundImage")));
            this.skinPic2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.skinPic2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.skinPic2.Location = new System.Drawing.Point(10, 10);
            this.skinPic2.Name = "skinPic2";
            this.skinPic2.Size = new System.Drawing.Size(110, 110);
            this.skinPic2.TabIndex = 0;
            this.skinPic2.TabStop = false;
            this.skinPic2.Click += new System.EventHandler(this.skinPanel_Click);
            // 
            // skinLbl2
            // 
            this.skinLbl2.BackColor = System.Drawing.Color.Transparent;
            this.skinLbl2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.skinLbl2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.skinLbl2.ForeColor = System.Drawing.Color.White;
            this.skinLbl2.Location = new System.Drawing.Point(0, 127);
            this.skinLbl2.Name = "skinLbl2";
            this.skinLbl2.Size = new System.Drawing.Size(130, 24);
            this.skinLbl2.TabIndex = 1;
            this.skinLbl2.Text = "Оранжевая";
            this.skinLbl2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.skinLbl2.Click += new System.EventHandler(this.skinPanel_Click);
            // 
            // skinPanel3
            // 
            this.skinPanel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(20)))), ((int)(((byte)(100)))));
            this.skinPanel3.Controls.Add(this.skinPic3);
            this.skinPanel3.Controls.Add(this.skinLbl3);
            this.skinPanel3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.skinPanel3.Location = new System.Drawing.Point(472, 150);
            this.skinPanel3.Name = "skinPanel3";
            this.skinPanel3.Size = new System.Drawing.Size(130, 155);
            this.skinPanel3.TabIndex = 6;
            this.skinPanel3.Click += new System.EventHandler(this.skinPanel_Click);
            // 
            // skinPic3
            // 
            this.skinPic3.BackColor = System.Drawing.Color.Transparent;
            this.skinPic3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("skinPic3.BackgroundImage")));
            this.skinPic3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.skinPic3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.skinPic3.Location = new System.Drawing.Point(10, 10);
            this.skinPic3.Name = "skinPic3";
            this.skinPic3.Size = new System.Drawing.Size(110, 110);
            this.skinPic3.TabIndex = 0;
            this.skinPic3.TabStop = false;
            this.skinPic3.Click += new System.EventHandler(this.skinPanel_Click);
            // 
            // skinLbl3
            // 
            this.skinLbl3.BackColor = System.Drawing.Color.Transparent;
            this.skinLbl3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.skinLbl3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.skinLbl3.ForeColor = System.Drawing.Color.White;
            this.skinLbl3.Location = new System.Drawing.Point(0, 127);
            this.skinLbl3.Name = "skinLbl3";
            this.skinLbl3.Size = new System.Drawing.Size(130, 24);
            this.skinLbl3.TabIndex = 1;
            this.skinLbl3.Text = "Красная";
            this.skinLbl3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.skinLbl3.Click += new System.EventHandler(this.skinPanel_Click);
            // 
            // foodPanel0
            // 
            this.foodPanel0.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(20)))), ((int)(((byte)(100)))));
            this.foodPanel0.Controls.Add(this.foodPic0);
            this.foodPanel0.Controls.Add(this.foodLbl0);
            this.foodPanel0.Cursor = System.Windows.Forms.Cursors.Hand;
            this.foodPanel0.Location = new System.Drawing.Point(190, 352);
            this.foodPanel0.Name = "foodPanel0";
            this.foodPanel0.Size = new System.Drawing.Size(130, 155);
            this.foodPanel0.TabIndex = 8;
            this.foodPanel0.Click += new System.EventHandler(this.foodPanel_Click);
            // 
            // foodPic0
            // 
            this.foodPic0.BackColor = System.Drawing.Color.Transparent;
            this.foodPic0.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("foodPic0.BackgroundImage")));
            this.foodPic0.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.foodPic0.Cursor = System.Windows.Forms.Cursors.Hand;
            this.foodPic0.Location = new System.Drawing.Point(10, 10);
            this.foodPic0.Name = "foodPic0";
            this.foodPic0.Size = new System.Drawing.Size(110, 110);
            this.foodPic0.TabIndex = 0;
            this.foodPic0.TabStop = false;
            this.foodPic0.Click += new System.EventHandler(this.foodPanel_Click);
            // 
            // foodLbl0
            // 
            this.foodLbl0.BackColor = System.Drawing.Color.Transparent;
            this.foodLbl0.Cursor = System.Windows.Forms.Cursors.Hand;
            this.foodLbl0.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.foodLbl0.ForeColor = System.Drawing.Color.White;
            this.foodLbl0.Location = new System.Drawing.Point(0, 127);
            this.foodLbl0.Name = "foodLbl0";
            this.foodLbl0.Size = new System.Drawing.Size(130, 24);
            this.foodLbl0.TabIndex = 1;
            this.foodLbl0.Text = "Яблоко";
            this.foodLbl0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.foodLbl0.Click += new System.EventHandler(this.foodPanel_Click);
            // 
            // foodPanel1
            // 
            this.foodPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(20)))), ((int)(((byte)(100)))));
            this.foodPanel1.Controls.Add(this.foodPic1);
            this.foodPanel1.Controls.Add(this.foodLbl1);
            this.foodPanel1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.foodPanel1.Location = new System.Drawing.Point(335, 352);
            this.foodPanel1.Name = "foodPanel1";
            this.foodPanel1.Size = new System.Drawing.Size(130, 155);
            this.foodPanel1.TabIndex = 9;
            this.foodPanel1.Click += new System.EventHandler(this.foodPanel_Click);
            // 
            // foodPic1
            // 
            this.foodPic1.BackColor = System.Drawing.Color.Transparent;
            this.foodPic1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("foodPic1.BackgroundImage")));
            this.foodPic1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.foodPic1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.foodPic1.Location = new System.Drawing.Point(10, 10);
            this.foodPic1.Name = "foodPic1";
            this.foodPic1.Size = new System.Drawing.Size(110, 110);
            this.foodPic1.TabIndex = 0;
            this.foodPic1.TabStop = false;
            this.foodPic1.Click += new System.EventHandler(this.foodPanel_Click);
            // 
            // foodLbl1
            // 
            this.foodLbl1.BackColor = System.Drawing.Color.Transparent;
            this.foodLbl1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.foodLbl1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.foodLbl1.ForeColor = System.Drawing.Color.White;
            this.foodLbl1.Location = new System.Drawing.Point(0, 127);
            this.foodLbl1.Name = "foodLbl1";
            this.foodLbl1.Size = new System.Drawing.Size(130, 24);
            this.foodLbl1.TabIndex = 1;
            this.foodLbl1.Text = "Банан";
            this.foodLbl1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.foodLbl1.Click += new System.EventHandler(this.foodPanel_Click);
            // 
            // btn_gotov
            // 
            this.btn_gotov.BackColor = System.Drawing.Color.Transparent;
            this.btn_gotov.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_gotov.BackgroundImage")));
            this.btn_gotov.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_gotov.FlatAppearance.BorderSize = 0;
            this.btn_gotov.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_gotov.Location = new System.Drawing.Point(80, 534);
            this.btn_gotov.Name = "btn_gotov";
            this.btn_gotov.Size = new System.Drawing.Size(220, 62);
            this.btn_gotov.TabIndex = 10;
            this.btn_gotov.UseVisualStyleBackColor = false;
            this.btn_gotov.Click += new System.EventHandler(this.btn_gotov_Click);
            this.btn_gotov.MouseEnter += new System.EventHandler(this.btn_gotov_MouseEnter);
            this.btn_gotov.MouseLeave += new System.EventHandler(this.btn_gotov_MouseLeave);
            // 
            // nazad_btn
            // 
            this.nazad_btn.BackColor = System.Drawing.Color.Transparent;
            this.nazad_btn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("nazad_btn.BackgroundImage")));
            this.nazad_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.nazad_btn.FlatAppearance.BorderSize = 0;
            this.nazad_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.nazad_btn.Location = new System.Drawing.Point(340, 534);
            this.nazad_btn.Name = "nazad_btn";
            this.nazad_btn.Size = new System.Drawing.Size(220, 62);
            this.nazad_btn.TabIndex = 11;
            this.nazad_btn.UseVisualStyleBackColor = false;
            this.nazad_btn.Click += new System.EventHandler(this.nazad_btn_Click);
            this.nazad_btn.MouseEnter += new System.EventHandler(this.nazad_btn_MouseEnter);
            this.nazad_btn.MouseLeave += new System.EventHandler(this.nazad_btn_MouseLeave);
            // 
            // nastoy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::snakeclassic.Properties.Resources.bg_stars;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(640, 620);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblSkins);
            this.Controls.Add(this.skinPanel0);
            this.Controls.Add(this.skinPanel1);
            this.Controls.Add(this.skinPanel2);
            this.Controls.Add(this.skinPanel3);
            this.Controls.Add(this.lblFood);
            this.Controls.Add(this.foodPanel0);
            this.Controls.Add(this.foodPanel1);
            this.Controls.Add(this.btn_gotov);
            this.Controls.Add(this.nazad_btn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "nastoy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Настройки";
            this.Load += new System.EventHandler(this.nastoy_Load);
            this.panel1.ResumeLayout(false);
            this.skinPanel0.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.skinPic0)).EndInit();
            this.skinPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.skinPic1)).EndInit();
            this.skinPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.skinPic2)).EndInit();
            this.skinPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.skinPic3)).EndInit();
            this.foodPanel0.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.foodPic0)).EndInit();
            this.foodPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.foodPic1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        // ── Поля ──────────────────────────────────────────────────────
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button svernutbtn;
        private System.Windows.Forms.Button closebtn;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSkins;
        private System.Windows.Forms.Label lblFood;

        private System.Windows.Forms.Panel skinPanel0, skinPanel1, skinPanel2, skinPanel3;
        private System.Windows.Forms.PictureBox skinPic0, skinPic1, skinPic2, skinPic3;
        private System.Windows.Forms.Label skinLbl0, skinLbl1, skinLbl2, skinLbl3;

        private System.Windows.Forms.Panel foodPanel0, foodPanel1;
        private System.Windows.Forms.PictureBox foodPic0, foodPic1;
        private System.Windows.Forms.Label foodLbl0, foodLbl1;

        private System.Windows.Forms.Button btn_gotov;
        private System.Windows.Forms.Button nazad_btn;
    }
}
