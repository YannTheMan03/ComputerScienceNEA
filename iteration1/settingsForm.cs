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
    public partial class settingsForm : Form
    {
        private Font _font = new Font("Pixeloid Sans", 12);

        public settingsForm()
        {
            InitializeComponent();
        }

        private void settingsForm_Load(object sender, EventArgs e)
        {
            label1.Font = _font;
            trackBar1.Maximum = 100;
        }
    }
}
