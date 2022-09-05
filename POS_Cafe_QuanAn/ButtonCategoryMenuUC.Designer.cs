
namespace POS_Cafe_QuanAn
{
    partial class ButtonCategoryMenuUC
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblCategoryMenu = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblCategoryMenu
            // 
            this.lblCategoryMenu.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCategoryMenu.ForeColor = System.Drawing.Color.Black;
            this.lblCategoryMenu.Location = new System.Drawing.Point(46, 23);
            this.lblCategoryMenu.Name = "lblCategoryMenu";
            this.lblCategoryMenu.Size = new System.Drawing.Size(106, 35);
            this.lblCategoryMenu.TabIndex = 0;
            this.lblCategoryMenu.Text = "TẤT CẢ";
            this.lblCategoryMenu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ButtonCategoryMenuUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Controls.Add(this.lblCategoryMenu);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Name = "ButtonCategoryMenuUC";
            this.Size = new System.Drawing.Size(203, 84);
            this.Load += new System.EventHandler(this.ButtonCategoryMenuUC_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label lblCategoryMenu;
    }
}
