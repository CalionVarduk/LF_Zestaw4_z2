﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LF_Zestaw4_z2.UI;

namespace LF_Zestaw4_z2.DicePokerGame.UI
{
    public partial class DicePokerForm : Form
    {
        private GraDwuosobowa game;
        private PagePanel display;

        public DicePokerForm()
        {
            InitializeComponent();

            display = new PagePanel();
            display.Parent = this;

            game = new DicePoker(display);
            game.Uruchom();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (game.Uruchomiona)
            {
                game.Przerwij();
                e.Cancel = true;
            }
        }
    }
}
