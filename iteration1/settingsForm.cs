using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;


namespace iteration1
{
    public partial class settingsForm : Form
    {
        private Font _font = new Font("Pixeloid Sans", 12);
        public WindowsMediaPlayer player = new WindowsMediaPlayer();
        public bool muteFlag = false;
        private static readonly string LeaderBoardPath
            = "C:\\Users\\yb.2415248\\OneDrive - Hereford Sixth Form College\\Computer Science\\C03 - Project\\Assets\\leaderboard.json";



        public settingsForm()
        {
            InitializeComponent();
        }

        private void settingsForm_Load(object sender, EventArgs e)
        {
            label1.Font = _font;
            label2.Font = _font;
            label3.Font = _font;
            label4.Font = _font;
            button1.Font = new Font("Pixeloid Sans", 9);

            button2.Font = new Font("Pixeloid Sans", 9);
            trackBar1.Maximum = 100;
            trackBar1.Value = AudioManager.MusicPlayer.settings.volume;
            pictureBox1.BackgroundImage = Properties.Resources.x_mark_xxl;
        }

        private void volumeScroll(object sender, EventArgs e)
        {
            AudioManager.SetVolume(trackBar1.Value);
        }

        private void button1_Click(object sender, EventArgs e)
        {

            muteFlag ^= true;
            if (muteFlag)
            {
                pictureBox1.BackgroundImage = Properties.Resources._458595;
            }
            else pictureBox1.BackgroundImage = Properties.Resources.x_mark_xxl;
            AudioManager.SetMute(muteFlag);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            File.WriteAllText(LeaderBoardPath, "{}");
        }
    }
}
