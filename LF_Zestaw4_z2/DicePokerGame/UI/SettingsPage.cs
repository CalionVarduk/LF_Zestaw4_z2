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
    public class SettingsPage : ClickablePage
    {
        public event MouseEventHandler ChoiceMade2
        {
            add { sTwo.Click += value; }
            remove { sTwo.Click -= value; }
        }

        public event MouseEventHandler ChoiceMade3
        {
            add { sThree.Click += value; }
            remove { sThree.Click -= value; }
        }

        public event MouseEventHandler ChoiceMade5
        {
            add { sFive.Click += value; }
            remove { sFive.Click -= value; }
        }

        public SettingsPage()
        {
            Initialize();
            Size = new Size(ComponentsRight + 30, ComponentsBottom + 30);
        }

        #region UI Init

        private ClickableString sTwo;
        private ClickableString sThree;
        private ClickableString sFive;

        private void Initialize()
        {
            var sMode = new ClickableString("First to X wins - choose X:") { Locked = true };
            sTwo = new ClickableString("2");
            sThree = new ClickableString("3");
            sFive = new ClickableString("5");

            sTwo.TextBrush = new SolidBrush(Color.SteelBlue);
            sThree.TextBrush = sTwo.TextBrush;
            sFive.TextBrush = sTwo.TextBrush;

            sMode.Location = new Point(30, 30);
            sTwo.Top = sMode.Bottom + 20;
            sThree.Top = sTwo.Top;
            sFive.Top = sTwo.Top;
            sThree.CentreX = sMode.CentreX;
            sTwo.Right = sThree.Left - 30;
            sFive.Left = sThree.Right + 30;

            Components.Add(sMode);
            Components.Add(sTwo);
            Components.Add(sThree);
            Components.Add(sFive);
        }

        #endregion
    }
}
