namespace Break_Bricks
{
    partial class BreakBricks
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
            this.endLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // endLabel
            // 
            this.endLabel.BackColor = System.Drawing.Color.Black;
            this.endLabel.Font = new System.Drawing.Font("BN Torrens", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.endLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.endLabel.Location = new System.Drawing.Point(179, 26);
            this.endLabel.Name = "endLabel";
            this.endLabel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.endLabel.Size = new System.Drawing.Size(529, 599);
            this.endLabel.TabIndex = 1;
            this.endLabel.Text = "יפה! \r\nסיימת/ה את כל השלבים!\r\nהניקוד שלך הוא: \r\n99999";
            this.endLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BreakBricks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Break_Bricks.Properties.Resources.Brick_Game;
            this.ClientSize = new System.Drawing.Size(1142, 650);
            this.Controls.Add(this.endLabel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.Name = "BreakBricks";
            this.Text = "שובר לבנים";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label endLabel;
    }
}

