using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using LF_Zestaw4_z2.UI;

namespace LF_Zestaw4_z2.DicePokerGame.UI
{
    public class WinnerPage : ClickablePage
    {
        public event MouseEventHandler CloseClicked
        {
            add { sClose.Click += value; }
            remove { sClose.Click -= value; }
        }

        public WinnerPage(DicePokerLogic logic)
        {
            Initialize(logic);
            Size = new Size(ComponentsRight + 50, ComponentsBottom + 50);
        }

        #region UI Init

        private ClickableString sClose;

        private void Initialize(DicePokerLogic logic)
        {
            var sScoreTitle = new ClickableString("Final Score:") { Locked = true };
            var sScore = new ClickableString("Player 1     " + logic.Wins1.ToString() + " : " + logic.Wins2.ToString() + "     Player 2") { Locked = true };
            var sWinner = new ClickableString("Player " + (logic.Player1Won ? "1" : "2") + " Wins!") { Locked = true };
            sClose = new ClickableString("Close");

            sClose.TextBrush = new SolidBrush(Color.SteelBlue);

            sScoreTitle.Top = 50;
            sScore.Top = sScoreTitle.Bottom;
            sWinner.Top = sScore.Bottom + 10;
            sClose.Top = sWinner.Bottom + 15;
            sScore.CentreX = (sScore.Width + 100) >> 1;
            sScoreTitle.CentreX = sScore.CentreX;
            sWinner.CentreX = sScore.CentreX;
            sClose.CentreX = sScore.CentreX;

            Components.Add(sScoreTitle);
            Components.Add(sScore);
            Components.Add(sWinner);
            Components.Add(sClose);
        }

        #endregion
    }
}
