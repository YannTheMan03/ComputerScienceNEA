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

        public settingsForm()
        {
            InitializeComponent();
        }

        private void settingsForm_Load(object sender, EventArgs e)
        {
            label1.Font = _font;
            trackBar1.Maximum = 100;
            trackBar1.Value = AudioManager.MusicPlayer.settings.volume;
        }

        private void volumeScroll(object sender, EventArgs e)
        {
            AudioManager.SetVolume(trackBar1.Value);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            muteFlag ^= true;
            AudioManager.SetMute(muteFlag);
        }

    }
}
