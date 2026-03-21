namespace snakeclassic
{
    partial class leadbordfrm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(leadbordfrm));
            this.picturetable = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.svernutbtn = new System.Windows.Forms.Button();
            this.closebtn = new System.Windows.Forms.Button();
            this.nazad_btn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picturetable)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // picturetable
            // 
            this.picturetable.BackColor = System.Drawing.Color.Transparent;
            this.picturetable.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picturetable.BackgroundImage")));
            this.picturetable.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picturetable.Location = new System.Drawing.Point(-45, -68);
            this.picturetable.Name = "picturetable";
            this.picturetable.Size = new System.Drawing.Size(394, 302);
            this.picturetable.TabIndex = 0;
            this.picturetable.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.svernutbtn);
            this.panel1.Controls.Add(this.closebtn);
            this.panel1.Location = new System.Drawing.Point(0, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(302, 45);
            this.panel1.TabIndex = 1;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // svernutbtn
            // 
            this.svernutbtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("svernutbtn.BackgroundImage")));
            this.svernutbtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.svernutbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.svernutbtn.Location = new System.Drawing.Point(236, 3);
            this.svernutbtn.Name = "svernutbtn";
            this.svernutbtn.Size = new System.Drawing.Size(30, 30);
            this.svernutbtn.TabIndex = 3;
            this.svernutbtn.UseVisualStyleBackColor = true;
            this.svernutbtn.Click += new System.EventHandler(this.svernutbtn_Click);
            // 
            // closebtn
            // 
            this.closebtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("closebtn.BackgroundImage")));
            this.closebtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.closebtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closebtn.Location = new System.Drawing.Point(272, 3);
            this.closebtn.Name = "closebtn";
            this.closebtn.Size = new System.Drawing.Size(30, 30);
            this.closebtn.TabIndex = 2;
            this.closebtn.UseVisualStyleBackColor = true;
            this.closebtn.Click += new System.EventHandler(this.closebtn_Click);
            // 
            // nazad_btn
            // 
            this.nazad_btn.BackColor = System.Drawing.Color.Transparent;
            this.nazad_btn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("nazad_btn.BackgroundImage")));
            this.nazad_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.nazad_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.nazad_btn.Location = new System.Drawing.Point(64, 389);
            this.nazad_btn.Name = "nazad_btn";
            this.nazad_btn.Size = new System.Drawing.Size(171, 49);
            this.nazad_btn.TabIndex = 14;
            this.nazad_btn.UseVisualStyleBackColor = false;
            this.nazad_btn.Click += new System.EventHandler(this.nazad_btn_Click);
            // 
            // leadbordfrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(303, 450);
            this.Controls.Add(this.nazad_btn);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.picturetable);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "leadbordfrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.leadbordfrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picturetable)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picturetable;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button svernutbtn;
        private System.Windows.Forms.Button closebtn;
        private System.Windows.Forms.Button nazad_btn;
    }
}