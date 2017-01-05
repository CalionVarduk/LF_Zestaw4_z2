using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using LF_Zestaw4_z2.UI;

namespace LF_Zestaw4_z2.ArenaDuelGame.UI
{
    public class GameFinishedPage : ClickablePage
    {
        public event MouseEventHandler ShowWinnerClicked
        {
            add { sShowWinner.Click += value; }
            remove { sShowWinner.Click -= value; }
        }

        public GameFinishedPage()
        {
            Initialize();
            Size = new Size(ComponentsRight + 50, ComponentsBottom + 50);
        }

        #region UI Init

        private ClickableString sShowWinner;

        private void Initialize()
        {
            var sEnded = new ClickableString("Game has ended.") { Locked = true };
            sShowWinner = new ClickableString("Show the winner!");

            sShowWinner.TextBrush = new SolidBrush(Color.SteelBlue);

            sEnded.Top = 50;
            sShowWinner.Top = sEnded.Bottom + 10;
            sShowWinner.CentreX = (sShowWinner.Width + 100) >> 1;
            sEnded.CentreX = sShowWinner.CentreX;

            Components.Add(sEnded);
            Components.Add(sShowWinner);
        }

        #endregion
    }
}
