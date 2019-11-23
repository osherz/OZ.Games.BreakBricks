namespace Game.Controls
{
    partial class PlayerTable
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
            this.label_PlayerDetail = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label_PlayerDetail
            // 
            this.label_PlayerDetail.AutoSize = true;
            this.label_PlayerDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label_PlayerDetail.Location = new System.Drawing.Point(3, 3);
            this.label_PlayerDetail.Margin = new System.Windows.Forms.Padding(3);
            this.label_PlayerDetail.Name = "label_PlayerDetail";
            this.label_PlayerDetail.Size = new System.Drawing.Size(118, 24);
            this.label_PlayerDetail.TabIndex = 0;
            this.label_PlayerDetail.Text = "שם השחקן - 0";
            this.label_PlayerDetail.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // PlayerTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label_PlayerDetail);
            this.Name = "PlayerTable";
            this.Size = new System.Drawing.Size(155, 30);
            this.Load += new System.EventHandler(this.PlayerTable_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_PlayerDetail;
    }
}
