using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

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
        public menuForm()
        {
            InitializeComponent();
            changingMenuBG();
        }

        private void menuForm_Load(object sender, EventArgs e)
        {
            label2.Font = _font;
            label2.ForeColor = Color.White;
            label2.BackColor = Color.Transparent;

            //playButton
            playButton.Font = _font;
            //settingsButton
            settingsButton.Font = _font;
            //leaderboardButton
            leaderBoardButton.Font = _font;

            
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
            
        }

        private void leaderBoardButton_Click(object sender, EventArgs e)
        {

        }

        private void settingsButton_Click(object sender, EventArgs e)
        {

        }

        private void exitButton_Click(object sender, EventArgs e)
        {

        }
    }
}
