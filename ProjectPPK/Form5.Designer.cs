namespace ProjectPPK
{
    partial class formKonfirmasi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formKonfirmasi));
            this.btnYa = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnTidak = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnYa
            // 
            this.btnYa.Location = new System.Drawing.Point(68, 101);
            this.btnYa.Name = "btnYa";
            this.btnYa.Size = new System.Drawing.Size(75, 27);
            this.btnYa.TabIndex = 8;
            this.btnYa.Text = "Ya";
            this.btnYa.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(104, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(251, 24);
            this.label2.TabIndex = 7;
            this.label2.Text = "Apakah data ingin disimpan?";
            // 
            // label1
            // 
            this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
            this.label1.Location = new System.Drawing.Point(21, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 68);
            this.label1.TabIndex = 6;
            // 
            // btnTidak
            // 
            this.btnTidak.Location = new System.Drawing.Point(280, 101);
            this.btnTidak.Name = "btnTidak";
            this.btnTidak.Size = new System.Drawing.Size(75, 27);
            this.btnTidak.TabIndex = 9;
            this.btnTidak.Text = "Tidak";
            this.btnTidak.UseVisualStyleBackColor = true;
            // 
            // formKonfirmasi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(405, 170);
            this.Controls.Add(this.btnTidak);
            this.Controls.Add(this.btnYa);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "formKonfirmasi";
            this.Text = "Konfirmasi";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnYa;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnTidak;
    }
}