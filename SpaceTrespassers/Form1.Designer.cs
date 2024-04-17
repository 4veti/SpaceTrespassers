namespace SpaceTrespassers
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            gameTimer = new System.Windows.Forms.Timer(components);
            scoreText = new Label();
            gameOverLabel = new Label();
            scoreboard = new Label();
            startButton = new Button();
            pausedText = new Label();
            mainMenuTimer = new System.Windows.Forms.Timer(components);
            pauseInstructions = new Label();
            pictureBox16 = new PictureBox();
            controlsInfo = new Label();
            playerNameBox = new TextBox();
            benchmark = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox16).BeginInit();
            SuspendLayout();
            // 
            // gameTimer
            // 
            gameTimer.Interval = 20;
            gameTimer.Tick += MainGameTimerEvent;
            // 
            // scoreText
            // 
            scoreText.AutoSize = true;
            scoreText.BackColor = Color.Black;
            scoreText.Font = new Font("Comic Sans MS", 18F, FontStyle.Regular, GraphicsUnit.Point);
            scoreText.ForeColor = Color.Aqua;
            scoreText.Location = new Point(29, 20);
            scoreText.Name = "scoreText";
            scoreText.Size = new Size(79, 33);
            scoreText.TabIndex = 23;
            scoreText.Text = "label1";
            // 
            // gameOverLabel
            // 
            gameOverLabel.AutoSize = true;
            gameOverLabel.BackColor = Color.Transparent;
            gameOverLabel.Font = new Font("Comic Sans MS", 32F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            gameOverLabel.ForeColor = Color.OrangeRed;
            gameOverLabel.Location = new Point(1384, 574);
            gameOverLabel.Name = "gameOverLabel";
            gameOverLabel.Size = new Size(239, 61);
            gameOverLabel.TabIndex = 24;
            gameOverLabel.Text = "game over";
            // 
            // scoreboard
            // 
            scoreboard.AutoSize = true;
            scoreboard.Font = new Font("Complex", 14F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            scoreboard.ForeColor = Color.Gold;
            scoreboard.Location = new Point(12, 641);
            scoreboard.Name = "scoreboard";
            scoreboard.Size = new Size(154, 28);
            scoreboard.TabIndex = 25;
            scoreboard.Text = "Scoreboard";
            // 
            // startButton
            // 
            startButton.Font = new Font("Vineta BT", 26.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            startButton.Location = new Point(1084, 493);
            startButton.Name = "startButton";
            startButton.Size = new Size(235, 91);
            startButton.TabIndex = 26;
            startButton.Text = "Start!";
            startButton.UseVisualStyleBackColor = true;
            startButton.MouseClick += StartButtonClicked;
            // 
            // pausedText
            // 
            pausedText.AutoSize = true;
            pausedText.BackColor = Color.Transparent;
            pausedText.Font = new Font("Comic Sans MS", 72F, FontStyle.Regular, GraphicsUnit.Point);
            pausedText.ForeColor = Color.OrangeRed;
            pausedText.Location = new Point(1435, 416);
            pausedText.Name = "pausedText";
            pausedText.Size = new Size(362, 135);
            pausedText.TabIndex = 27;
            pausedText.Text = "Paused";
            // 
            // mainMenuTimer
            // 
            mainMenuTimer.Interval = 20;
            mainMenuTimer.Tick += MainMenuTimerEvent;
            // 
            // pauseInstructions
            // 
            pauseInstructions.AutoSize = true;
            pauseInstructions.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point);
            pauseInstructions.ForeColor = Color.IndianRed;
            pauseInstructions.Location = new Point(1703, 23);
            pauseInstructions.Name = "pauseInstructions";
            pauseInstructions.Size = new Size(188, 30);
            pauseInstructions.TabIndex = 29;
            pauseInstructions.Text = "Press \"P\" to pause";
            // 
            // pictureBox16
            // 
            pictureBox16.Image = Properties.Resources.enemyProjectile1;
            pictureBox16.Location = new Point(254, 655);
            pictureBox16.Name = "pictureBox16";
            pictureBox16.Size = new Size(23, 27);
            pictureBox16.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox16.TabIndex = 30;
            pictureBox16.TabStop = false;
            pictureBox16.Visible = false;
            // 
            // controlsInfo
            // 
            controlsInfo.AutoSize = true;
            controlsInfo.Font = new Font("Kristen ITC", 21.75F, FontStyle.Regular, GraphicsUnit.Point);
            controlsInfo.ForeColor = Color.Chartreuse;
            controlsInfo.Location = new Point(803, 554);
            controlsInfo.Name = "controlsInfo";
            controlsInfo.Size = new Size(302, 40);
            controlsInfo.TabIndex = 34;
            controlsInfo.Text = "controlsDescription";
            // 
            // playerNameBox
            // 
            playerNameBox.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point);
            playerNameBox.Location = new Point(1473, 326);
            playerNameBox.Name = "playerNameBox";
            playerNameBox.Size = new Size(150, 36);
            playerNameBox.TabIndex = 35;
            playerNameBox.KeyDown += playerNameBox_KeyDown;
            // 
            // benchmark
            // 
            benchmark.AutoSize = true;
            benchmark.Font = new Font("Segoe UI", 20F, FontStyle.Regular, GraphicsUnit.Point);
            benchmark.ForeColor = SystemColors.MenuHighlight;
            benchmark.Location = new Point(245, 825);
            benchmark.Name = "benchmark";
            benchmark.Size = new Size(90, 37);
            benchmark.TabIndex = 37;
            benchmark.Text = "label2";
            benchmark.Visible = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(1904, 1041);
            Controls.Add(benchmark);
            Controls.Add(playerNameBox);
            Controls.Add(controlsInfo);
            Controls.Add(pictureBox16);
            Controls.Add(pauseInstructions);
            Controls.Add(pausedText);
            Controls.Add(startButton);
            Controls.Add(scoreboard);
            Controls.Add(gameOverLabel);
            Controls.Add(scoreText);
            Name = "Form1";
            Text = "SpaceTrespassers";
            Load += Form1_Load;
            KeyDown += KeyIsDown;
            KeyUp += KeyIsUp;
            ((System.ComponentModel.ISupportInitialize)pictureBox16).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Timer gameTimer;
        private Label scoreText;
        private Label gameOverLabel;
        private Label scoreboard;
        private Button startButton;
        private Label pausedText;
        private System.Windows.Forms.Timer mainMenuTimer;
        private Label pauseInstructions;
        private PictureBox pictureBox16;
        private Label controlsInfo;
        private TextBox playerNameBox;
        private Label benchmark;
    }
}