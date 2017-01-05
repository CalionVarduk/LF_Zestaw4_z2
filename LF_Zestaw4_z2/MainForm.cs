using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LF_Zestaw4_z2
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void buttonDice_Click(object sender, EventArgs e)
        {
            Form form = new DicePokerGame.UI.DicePokerForm();
            form.Show();
        }

        private void buttonDuel_Click(object sender, EventArgs e)
        {
            Form form = new ArenaDuelGame.UI.ArenaDuelForm();
            form.Show();
        }

        private void buttonLog_Click(object sender, EventArgs e)
        {
            Form form = new GameLogsForm();
            form.Show();
        }
    }
}
