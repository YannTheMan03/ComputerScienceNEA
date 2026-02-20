using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;
using static System.Windows.Forms.DataFormats;

namespace iteration1
{
    public partial class menuForm : Form
    {
        private Image[] _background;
        private int _currentImageIndex = 0;
        private int BackgroundChangeCounter = 0;
        private const int BackgroundChangeDelay = 500;
        private DateTime _lastBackgroundChange = DateTime.Now;
        private Font _font = new Font("Pixeloid Sans", 12);
        public string Username { get; private set; }

        private Leaderboard _leaderboard = new();
        private static readonly string LeaderBoardPath
            = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "leaderboard.json");

        public menuForm()
        {
            InitializeComponent();
            changingMenuBG();
        }

        private void menuForm_Load(object sender, EventArgs e)
        {
             this.MaximizeBox = false; 
            label2.Font = _font;
            label2.ForeColor = Color.White;
            label2.BackColor = Color.Transparent;
            this.FormBorderStyle = FormBorderStyle.Fixed3D; 


            //playButton
            playButton.Font = _font;
            playButton.FlatStyle = FlatStyle.Flat;
            //settingsButton
            settingsButton.Font = _font;
            settingsButton.FlatStyle = FlatStyle.Flat;
            //leaderboardButton
            leaderBoardButton.Font = _font;
            leaderBoardButton.FlatStyle= FlatStyle.Flat;
            _leaderboard.Load(LeaderBoardPath);

            exitButton.Font = _font;
            exitButton.FlatStyle = FlatStyle.Flat;

            userNameBox.BorderStyle = BorderStyle.Fixed3D; 
            userNameBox.Font = _font;
            
        }
        void changingMenuBG()
        {
            _background = new Image[]
            {
                Properties.Resources.BG1,
                Properties.Resources.BG2,
                Properties.Resources.BG3,
                Properties.Resources.BG4,
                Properties.Resources.BG5,
                Properties.Resources.BG6,
                Properties.Resources.BG7,
                Properties.Resources.BG8

            };

            this.BackgroundImage = _background[0];
        }

        private void gameTimer(object sender, EventArgs e)
        {
            if ((DateTime.Now - _lastBackgroundChange).TotalMilliseconds >= BackgroundChangeDelay)
            {
                _currentImageIndex = (_currentImageIndex + 1) % _background.Length;
                this.BackgroundImage = _background[_currentImageIndex];
                _lastBackgroundChange = DateTime.Now;
            }
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(userNameBox.Text))
            {
                MessageBox.Show("Please Enter a username First");
                return;
            }

            Username = userNameBox.Text.Trim();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void leaderBoardButton_Click(object sender, EventArgs e)
        {
            GameOver();
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            settingsForm sForm = new settingsForm();
            sForm.ShowDialog();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void GameOver()
        {
            var topScores = _leaderboard.GetTopScores(5);
            string message = " Leaderboard: \n\n";

            int rank = 1;
            foreach (var entry in topScores)
            {
                string medal = rank switch
                {
                    1 => "🏆",
                    _ => "     "
                };
                message += $"{medal} {rank}. {entry.Key}: {entry.Value}\n";
                rank++;
            }

            MessageBox.Show(message, "Game Over");
        }
    }
}
