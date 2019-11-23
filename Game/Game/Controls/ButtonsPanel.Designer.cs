namespace Game.Controls
{
    partial class ButtonsPanel
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
            this.button_NewGame = new System.Windows.Forms.Button();
            this.button_Continue = new System.Windows.Forms.Button();
            this.button_Back = new System.Windows.Forms.Button();
            this.button_Refresh = new System.Windows.Forms.Button();
            this.button_Exit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_NewGame
            // 
            this.button_NewGame.Location = new System.Drawing.Point(3, 3);
            this.button_NewGame.Name = "button_NewGame";
            this.button_NewGame.Size = new System.Drawing.Size(75, 23);
            this.button_NewGame.TabIndex = 0;
            this.button_NewGame.Text = "משחק חדש";
            this.button_NewGame.UseVisualStyleBackColor = true;
            // 
            // button_Continue
            // 
            this.button_Continue.Location = new System.Drawing.Point(3, 32);
            this.button_Continue.Name = "button_Continue";
            this.button_Continue.Size = new System.Drawing.Size(75, 23);
            this.button_Continue.TabIndex = 1;
            this.button_Continue.Text = "המשך";
            this.button_Continue.UseVisualStyleBackColor = true;
            // 
            // button_Back
            // 
            this.button_Back.Location = new System.Drawing.Point(3, 60);
            this.button_Back.Name = "button_Back";
            this.button_Back.Size = new System.Drawing.Size(75, 23);
            this.button_Back.TabIndex = 2;
            this.button_Back.Text = "חזור";
            this.button_Back.UseVisualStyleBackColor = true;
            // 
            // button_Refresh
            // 
            this.button_Refresh.Location = new System.Drawing.Point(3, 89);
            this.button_Refresh.Name = "button_Refresh";
            this.button_Refresh.Size = new System.Drawing.Size(75, 23);
            this.button_Refresh.TabIndex = 3;
            this.button_Refresh.Text = "רענן משחק";
            this.button_Refresh.UseVisualStyleBackColor = true;
            // 
            // button_Exit
            // 
            this.button_Exit.Location = new System.Drawing.Point(3, 118);
            this.button_Exit.Name = "button_Exit";
            this.button_Exit.Size = new System.Drawing.Size(75, 23);
            this.button_Exit.TabIndex = 4;
            this.button_Exit.Text = "יציאה";
            this.button_Exit.UseVisualStyleBackColor = true;
            // 
            // ButtonsPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button_NewGame);
            this.Controls.Add(this.button_Continue);
            this.Controls.Add(this.button_Refresh);
            this.Controls.Add(this.button_Back);
            this.Controls.Add(this.button_Exit);
            this.Name = "ButtonsPanel";
            this.Size = new System.Drawing.Size(81, 144);
            this.Load += new System.EventHandler(this.ButtonsPanel_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_NewGame;
        private System.Windows.Forms.Button button_Continue;
        private System.Windows.Forms.Button button_Back;
        private System.Windows.Forms.Button button_Refresh;
        private System.Windows.Forms.Button button_Exit;
    }
}
