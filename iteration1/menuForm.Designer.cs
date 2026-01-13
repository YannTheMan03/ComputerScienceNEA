namespace iteration1
{
    partial class menuForm
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
            components = new System.ComponentModel.Container();
            timer1 = new System.Windows.Forms.Timer(components);
            label1 = new Label();
            pictureBox1 = new PictureBox();
            playButton = new Button();
            leaderBoardButton = new Button();
            settingsButton = new Button();
            exitButton = new Button();
            userNameBox = new TextBox();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 20;
            timer1.Tick += gameTimer;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(152, 45);
            label1.Name = "label1";
            label1.Size = new Size(0, 15);
            label1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Image = Properties.Resources.Galaga_logo_svg;
            pictureBox1.Location = new Point(40, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(314, 164);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // playButton
            // 
            playButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            playButton.Font = new Font("Segoe UI", 15F);
            playButton.Location = new Point(117, 276);
            playButton.Name = "playButton";
            playButton.Size = new Size(150, 50);
            playButton.TabIndex = 2;
            playButton.Text = "Play";
            playButton.UseVisualStyleBackColor = true;
            playButton.Click += playButton_Click;
            // 
            // leaderBoardButton
            // 
            leaderBoardButton.Font = new Font("Segoe UI", 15F);
            leaderBoardButton.Location = new Point(117, 332);
            leaderBoardButton.Name = "leaderBoardButton";
            leaderBoardButton.Size = new Size(150, 50);
            leaderBoardButton.TabIndex = 3;
            leaderBoardButton.Text = "Leaderboard";
            leaderBoardButton.UseVisualStyleBackColor = true;
            leaderBoardButton.Click += leaderBoardButton_Click;
            // 
            // settingsButton
            // 
            settingsButton.Font = new Font("Segoe UI", 15F);
            settingsButton.Location = new Point(117, 388);
            settingsButton.Name = "settingsButton";
            settingsButton.Size = new Size(150, 50);
            settingsButton.TabIndex = 4;
            settingsButton.Text = "Settings";
            settingsButton.UseVisualStyleBackColor = true;
            settingsButton.Click += settingsButton_Click;
            // 
            // exitButton
            // 
            exitButton.BackColor = Color.Red;
            exitButton.Location = new Point(322, 499);
            exitButton.Name = "exitButton";
            exitButton.Size = new Size(50, 50);
            exitButton.TabIndex = 5;
            exitButton.Text = "X";
            exitButton.UseVisualStyleBackColor = false;
            exitButton.Click += exitButton_Click;
            // 
            // userNameBox
            // 
            userNameBox.Location = new Point(167, 247);
            userNameBox.Name = "userNameBox";
            userNameBox.Size = new Size(100, 23);
            userNameBox.TabIndex = 6;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.Location = new Point(116, 247);
            label2.Name = "label2";
            label2.Size = new Size(45, 21);
            label2.TabIndex = 7;
            label2.Text = "User:";
            // 
            // menuForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(384, 561);
            Controls.Add(label2);
            Controls.Add(userNameBox);
            Controls.Add(exitButton);
            Controls.Add(settingsButton);
            Controls.Add(leaderBoardButton);
            Controls.Add(playButton);
            Controls.Add(pictureBox1);
            Controls.Add(label1);
            Name = "menuForm";
            Text = "menuForm";
            Load += menuForm_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private Label label1;
        private PictureBox pictureBox1;
        private Button playButton;
        private Button leaderBoardButton;
        private Button settingsButton;
        private Button exitButton;
        private TextBox userNameBox;
        private Label label2;
    }
}