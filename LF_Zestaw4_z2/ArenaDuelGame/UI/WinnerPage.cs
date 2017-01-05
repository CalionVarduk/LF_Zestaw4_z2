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
    public class WinnerPage : ClickablePage
    {
        public event MouseEventHandler CloseClicked
        {
            add { sClose.Click += value; }
            remove { sClose.Click -= value; }
        }

        public WinnerPage(ArenaDuelLogic logic)
        {
            Initialize(logic);
            Size = new Size(ComponentsRight + 50, ComponentsBottom + 50);
        }

        #region UI Init

        private ClickableString sClose;

        private void Initialize(ArenaDuelLogic logic)
        {
            WarriorAttributes a = (logic.Player1Won ? logic.Warrior1.Attributes : logic.Warrior2.Attributes);

            var sWinner = new ClickableString("Player " + (logic.Player1Won ? "1" : "2") + " Wins!") { Locked = true };
            var sHealth = new ClickableString("With " + a.Health.ToString("F2") + "/" + a.MaxHealth.ToString("F2") + " health left!") { Locked = true };
            sClose = new ClickableString("Close");

            sClose.TextBrush = new SolidBrush(Color.SteelBlue);

            sWinner.Top = 50;
            sHealth.Top = sWinner.Bottom;
            sClose.Top = sHealth.Bottom + 15;
            sHealth.CentreX = (sHealth.Width + 100) >> 1;
            sWinner.CentreX = sHealth.CentreX;
            sClose.CentreX = sHealth.CentreX;

            Components.Add(sWinner);
            Components.Add(sHealth);
            Components.Add(sClose);
        }

        #endregion
    }
}
