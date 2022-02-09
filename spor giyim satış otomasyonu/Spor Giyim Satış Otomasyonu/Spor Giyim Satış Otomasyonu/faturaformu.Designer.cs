namespace Spor_Giyim_Satış_Otomasyonu
{
    partial class faturaformu
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(3, 2);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(223, 420);
            this.listBox1.TabIndex = 2;
            // 
            // faturaformu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(230, 421);
            this.Controls.Add(this.listBox1);
            this.Location = new System.Drawing.Point(1050, 55);
            this.Name = "faturaformu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "SATIŞ FATURASI";
            this.Load += new System.EventHandler(this.faturaformu_Load);
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.ListBox listBox1;
    }
}