namespace PathFinderGUI
{
    partial class Form1
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radio_start = new System.Windows.Forms.RadioButton();
            this.radio_target = new System.Windows.Forms.RadioButton();
            this.radio_obstacle = new System.Windows.Forms.RadioButton();
            this.btn_startSearch = new System.Windows.Forms.Button();
            this.board = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_deletePath = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radio_start);
            this.groupBox1.Controls.Add(this.radio_target);
            this.groupBox1.Controls.Add(this.radio_obstacle);
            this.groupBox1.Location = new System.Drawing.Point(518, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(171, 89);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Platzierungs Modi";
            // 
            // radio_start
            // 
            this.radio_start.AutoSize = true;
            this.radio_start.Location = new System.Drawing.Point(6, 42);
            this.radio_start.Name = "radio_start";
            this.radio_start.Size = new System.Drawing.Size(47, 17);
            this.radio_start.TabIndex = 2;
            this.radio_start.Text = "Start";
            this.radio_start.UseVisualStyleBackColor = true;
            // 
            // radio_target
            // 
            this.radio_target.AutoSize = true;
            this.radio_target.Location = new System.Drawing.Point(6, 65);
            this.radio_target.Name = "radio_target";
            this.radio_target.Size = new System.Drawing.Size(42, 17);
            this.radio_target.TabIndex = 1;
            this.radio_target.Text = "Ziel";
            this.radio_target.UseVisualStyleBackColor = true;
            // 
            // radio_obstacle
            // 
            this.radio_obstacle.AutoSize = true;
            this.radio_obstacle.Checked = true;
            this.radio_obstacle.Location = new System.Drawing.Point(6, 19);
            this.radio_obstacle.Name = "radio_obstacle";
            this.radio_obstacle.Size = new System.Drawing.Size(69, 17);
            this.radio_obstacle.TabIndex = 0;
            this.radio_obstacle.TabStop = true;
            this.radio_obstacle.Text = "Hindernis";
            this.radio_obstacle.UseVisualStyleBackColor = true;
            // 
            // btn_startSearch
            // 
            this.btn_startSearch.Location = new System.Drawing.Point(518, 107);
            this.btn_startSearch.Name = "btn_startSearch";
            this.btn_startSearch.Size = new System.Drawing.Size(75, 23);
            this.btn_startSearch.TabIndex = 3;
            this.btn_startSearch.Text = "Start";
            this.btn_startSearch.UseVisualStyleBackColor = true;
            this.btn_startSearch.Click += new System.EventHandler(this.OnButtonStartSearchClick);
            // 
            // board
            // 
            this.board.Location = new System.Drawing.Point(12, 12);
            this.board.Name = "board";
            this.board.Size = new System.Drawing.Size(500, 500);
            this.board.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(599, 107);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Reset";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.OnResetButtonClick);
            // 
            // btn_deletePath
            // 
            this.btn_deletePath.Location = new System.Drawing.Point(518, 136);
            this.btn_deletePath.Name = "btn_deletePath";
            this.btn_deletePath.Size = new System.Drawing.Size(156, 23);
            this.btn_deletePath.TabIndex = 7;
            this.btn_deletePath.Text = "Pfad löschen";
            this.btn_deletePath.UseVisualStyleBackColor = true;
            this.btn_deletePath.Click += new System.EventHandler(this.OnDeletePathButtonClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(699, 524);
            this.Controls.Add(this.btn_deletePath);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.board);
            this.Controls.Add(this.btn_startSearch);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_startSearch;
        private System.Windows.Forms.Panel board;
        private System.Windows.Forms.RadioButton radio_start;
        private System.Windows.Forms.RadioButton radio_target;
        private System.Windows.Forms.RadioButton radio_obstacle;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_deletePath;
    }
}

