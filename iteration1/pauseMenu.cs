using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iteration1
{
    public partial class pauseMenu : Form
    {
        private int score;
        private Font _font = new Font("Pixeloid Sans", 12);
        public pauseMenu(int pScore)
        {
            InitializeComponent();
            score = pScore;
        }

        private void pauseMenu_Load(object sender, EventArgs e)
        {
            label1.Font = _font;
            label2.Font = _font;
            label3.Font = _font;
            label3.Text = score.ToString();
        }



        private void button1_Click(object sender, EventArgs e)
        {


            this.Close();
        }

        private void exitPressed(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
