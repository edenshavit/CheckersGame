namespace CheckersWindowsUI
{
    public partial class SettingsForm
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
            this.labelBoardSize = new System.Windows.Forms.Label();
            this.radioButton6X6 = new System.Windows.Forms.RadioButton();
            this.radioButton8X8 = new System.Windows.Forms.RadioButton();
            this.radioButton10X10 = new System.Windows.Forms.RadioButton();
            this.LabelPlayers = new System.Windows.Forms.Label();
            this.textBoxPlayer1 = new System.Windows.Forms.TextBox();
            this.LabelPlayer1 = new System.Windows.Forms.Label();
            this.textBoxPlayer2 = new System.Windows.Forms.TextBox();
            this.checkBoxIsTwoPlayers = new System.Windows.Forms.CheckBox();
            this.buttonDone = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelBoardSize
            // 
            this.labelBoardSize.AutoSize = true;
            this.labelBoardSize.Location = new System.Drawing.Point(14, 11);
            this.labelBoardSize.Name = "labelBoardSize";
            this.labelBoardSize.Size = new System.Drawing.Size(87, 20);
            this.labelBoardSize.TabIndex = 0;
            this.labelBoardSize.Text = "BoardSize:";
            // 
            // radioButton6X6
            // 
            this.radioButton6X6.AutoSize = true;
            this.radioButton6X6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.radioButton6X6.Checked = true;
            this.radioButton6X6.Location = new System.Drawing.Point(39, 49);
            this.radioButton6X6.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radioButton6X6.Name = "radioButton6X6";
            this.radioButton6X6.Size = new System.Drawing.Size(63, 24);
            this.radioButton6X6.TabIndex = 1;
            this.radioButton6X6.TabStop = true;
            this.radioButton6X6.Text = "6X6";
            this.radioButton6X6.UseVisualStyleBackColor = true;
            this.radioButton6X6.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton8X8
            // 
            this.radioButton8X8.AutoSize = true;
            this.radioButton8X8.Location = new System.Drawing.Point(140, 49);
            this.radioButton8X8.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radioButton8X8.Name = "radioButton8X8";
            this.radioButton8X8.Size = new System.Drawing.Size(63, 24);
            this.radioButton8X8.TabIndex = 2;
            this.radioButton8X8.Text = "8X8";
            this.radioButton8X8.UseVisualStyleBackColor = true;
            this.radioButton8X8.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton10X10
            // 
            this.radioButton10X10.AutoSize = true;
            this.radioButton10X10.Location = new System.Drawing.Point(243, 49);
            this.radioButton10X10.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radioButton10X10.Name = "radioButton10X10";
            this.radioButton10X10.Size = new System.Drawing.Size(81, 24);
            this.radioButton10X10.TabIndex = 3;
            this.radioButton10X10.Text = "10X10";
            this.radioButton10X10.UseVisualStyleBackColor = true;
            this.radioButton10X10.Click += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // LabelPlayers
            // 
            this.LabelPlayers.AutoSize = true;
            this.LabelPlayers.Location = new System.Drawing.Point(14, 98);
            this.LabelPlayers.Name = "LabelPlayers";
            this.LabelPlayers.Size = new System.Drawing.Size(64, 20);
            this.LabelPlayers.TabIndex = 4;
            this.LabelPlayers.Text = "Players:";
            // 
            // textBoxPlayer1
            // 
            this.textBoxPlayer1.Location = new System.Drawing.Point(186, 141);
            this.textBoxPlayer1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxPlayer1.Name = "textBoxPlayer1";
            this.textBoxPlayer1.Size = new System.Drawing.Size(142, 26);
            this.textBoxPlayer1.TabIndex = 5;
            this.textBoxPlayer1.TextChanged += new System.EventHandler(this.textBoxPlayer1_TextChanged);
            // 
            // LabelPlayer1
            // 
            this.LabelPlayer1.AutoSize = true;
            this.LabelPlayer1.Location = new System.Drawing.Point(56, 145);
            this.LabelPlayer1.Name = "LabelPlayer1";
            this.LabelPlayer1.Size = new System.Drawing.Size(69, 20);
            this.LabelPlayer1.TabIndex = 6;
            this.LabelPlayer1.Text = "Player 1:";
            // 
            // textBoxPlayer2
            // 
            this.textBoxPlayer2.AccessibleName = string.Empty;
            this.textBoxPlayer2.Cursor = System.Windows.Forms.Cursors.Default;
            this.textBoxPlayer2.Enabled = false;
            this.textBoxPlayer2.Location = new System.Drawing.Point(186, 191);
            this.textBoxPlayer2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxPlayer2.Name = "textBoxPlayer2";
            this.textBoxPlayer2.Size = new System.Drawing.Size(142, 26);
            this.textBoxPlayer2.TabIndex = 7;
            this.textBoxPlayer2.Text = "[Computer]";
            this.textBoxPlayer2.TextChanged += new System.EventHandler(this.textBoxPlayer2_TextChanged);
            // 
            // checkBoxIsTwoPlayers
            // 
            this.checkBoxIsTwoPlayers.AutoSize = true;
            this.checkBoxIsTwoPlayers.Location = new System.Drawing.Point(60, 194);
            this.checkBoxIsTwoPlayers.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.checkBoxIsTwoPlayers.Name = "checkBoxIsTwoPlayers";
            this.checkBoxIsTwoPlayers.Size = new System.Drawing.Size(95, 24);
            this.checkBoxIsTwoPlayers.TabIndex = 9;
            this.checkBoxIsTwoPlayers.Text = "Player 2:";
            this.checkBoxIsTwoPlayers.UseVisualStyleBackColor = true;
            this.checkBoxIsTwoPlayers.CheckedChanged += new System.EventHandler(this.checkBoxIsTwoPlayers_CheckedChanged);
            // 
            // buttonDone
            // 
            this.buttonDone.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonDone.Location = new System.Drawing.Point(244, 265);
            this.buttonDone.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonDone.Name = "buttonDone";
            this.buttonDone.Size = new System.Drawing.Size(84, 29);
            this.buttonDone.TabIndex = 10;
            this.buttonDone.Text = "Done";
            this.buttonDone.UseVisualStyleBackColor = true;
            this.buttonDone.Click += new System.EventHandler(this.buttonDone_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 324);
            this.Controls.Add(this.buttonDone);
            this.Controls.Add(this.checkBoxIsTwoPlayers);
            this.Controls.Add(this.textBoxPlayer2);
            this.Controls.Add(this.LabelPlayer1);
            this.Controls.Add(this.textBoxPlayer1);
            this.Controls.Add(this.LabelPlayers);
            this.Controls.Add(this.radioButton10X10);
            this.Controls.Add(this.radioButton8X8);
            this.Controls.Add(this.radioButton6X6);
            this.Controls.Add(this.labelBoardSize);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "SettingsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GameSettings";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelBoardSize;
        private System.Windows.Forms.RadioButton radioButton6X6;
        private System.Windows.Forms.RadioButton radioButton8X8;
        private System.Windows.Forms.RadioButton radioButton10X10;
        private System.Windows.Forms.Label LabelPlayers;
        private System.Windows.Forms.TextBox textBoxPlayer1;
        private System.Windows.Forms.Label LabelPlayer1;
        private System.Windows.Forms.TextBox textBoxPlayer2;
        private System.Windows.Forms.CheckBox checkBoxIsTwoPlayers;
        private System.Windows.Forms.Button buttonDone;
    }
}