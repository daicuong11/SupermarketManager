namespace QLST_N7
{
    partial class frmInDonNhap
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
            this.crvHienThiDonNhap = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // crvHienThiDonNhap
            // 
            this.crvHienThiDonNhap.ActiveViewIndex = -1;
            this.crvHienThiDonNhap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crvHienThiDonNhap.Cursor = System.Windows.Forms.Cursors.Default;
            this.crvHienThiDonNhap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crvHienThiDonNhap.Location = new System.Drawing.Point(0, 0);
            this.crvHienThiDonNhap.Name = "crvHienThiDonNhap";
            this.crvHienThiDonNhap.Size = new System.Drawing.Size(1207, 753);
            this.crvHienThiDonNhap.TabIndex = 0;
            // 
            // frmInDonNhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1207, 753);
            this.Controls.Add(this.crvHienThiDonNhap);
            this.Name = "frmInDonNhap";
            this.Text = "In đơn nhập";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        public CrystalDecisions.Windows.Forms.CrystalReportViewer crvHienThiDonNhap;
    }
}