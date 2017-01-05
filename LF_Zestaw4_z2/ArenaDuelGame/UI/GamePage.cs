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
    public class GamePage : ClickablePage
    {
        public ArenaDuelLogic Logic { get; private set; }
        private bool player1PrevTurn;
        private bool gameFinished;

        public event MouseEventHandler AttackingNormal
        {
            add { sNormal.Click += value; }
            remove { sNormal.Click -= value; }
        }

        public event MouseEventHandler AttackingStrong
        {
            add { sStrongAttack.Click += value; }
            remove { sStrongAttack.Click -= value; }
        }

        public event MouseEventHandler PreparingStrongAttack
        {
            add { sStrong.Click += value; }
            remove { sStrong.Click -= value; }
        }

        public event MouseEventHandler Shielding
        {
            add { sShield.Click += value; }
            remove { sShield.Click -= value; }
        }

        public event MouseEventHandler Riposting
        {
            add { sRiposte.Click += value; }
            remove { sRiposte.Click -= value; }
        }

        public event MouseEventHandler ContinueClicked
        {
            add { sContinue.Click += value; }
            remove { sContinue.Click -= value; }
        }

        public GamePage(ArenaDuelLogic logic)
        {
            gameFinished = logic.GameFinished;
            player1PrevTurn = logic.Player1Turn;
            Logic = logic;
            Initialize();
            Size = new Size(ComponentsRight + 20, ComponentsBottom + 20);
        }

        public void NextTurn(TurnReport report)
        {
            if (!gameFinished)
            {
                if (Logic.Player1Turn) NextTurnPlayer1();
                else NextTurnPlayer2();

                UpdateViews();
                UpdateReport(report);
                player1PrevTurn = Logic.Player1Turn;
                if (Logic.GameFinished) GameIsFinished();
            }
        }

        private void UpdateViews()
        {
            view1.Update();
            view2.Update();
        }

        private void UpdateReport(TurnReport report)
        {
            sReport.Text = report.ToString();
            sReport.CentreX = CentreX;
            sReport.Visible = true;
        }

        private void NextTurnPlayer1()
        {
            sChoice.Text = "Player 1 - choose your action:";
            
            bool visible = (Logic.Warrior1.CanDodge && Logic.Warrior1.HasActionPoints);
            sStrong.Visible = visible;
            sShield.Visible = visible;
            sRiposte.Visible = visible;
            sNormal.Visible = !Logic.Warrior1.IsPreparing;
            sStrongAttack.Visible = Logic.Warrior1.IsPreparing;
        }

        private void NextTurnPlayer2()
        {
            sChoice.Text = "Player 2 - choose your action:";

            bool visible = (Logic.Warrior2.CanDodge && Logic.Warrior2.HasActionPoints);
            sStrong.Visible = visible;
            sShield.Visible = visible;
            sRiposte.Visible = visible;
            sNormal.Visible = !Logic.Warrior2.IsPreparing;
            sStrongAttack.Visible = Logic.Warrior2.IsPreparing;
        }

        private void GameIsFinished()
        {
            gameFinished = true;
            sChoice.Visible = false;
            sNormal.Visible = false;
            sStrong.Visible = false;
            sShield.Visible = false;
            sRiposte.Visible = false;
            sStrongAttack.Visible = false;
            sContinue.Visible = true;
        }

        #region UI Init

        private WarriorView view1;
        private WarriorView view2;

        private ClickableString sChoice;
        private ClickableString sStrongAttack;
        private ClickableString sNormal;
        private ClickableString sStrong;
        private ClickableString sShield;
        private ClickableString sRiposte;
        private ClickableString sReport;
        private ClickableString sContinue;

        private void Initialize()
        {
            view1 = new WarriorView("Player 1", Logic.Warrior1, Logic.Warrior2);
            view2 = new WarriorView("Player 2", Logic.Warrior2, Logic.Warrior1);

            sChoice = new ClickableString(Logic.Player1Turn ? "Player 1 - choose your action:" : "Player 2 - choose your action:") { Locked = true };
            sStrongAttack = new ClickableString("Attack!") { Visible = false };
            sNormal = new ClickableString("Normal attack");
            sStrong = new ClickableString("Strong attack");
            sShield = new ClickableString("Shield stance");
            sRiposte = new ClickableString("Riposte stance");
            sReport = new ClickableString("report") { Visible = false, Locked = true };
            sContinue = new ClickableString("Continue!") { Visible = false };

            sNormal.TextBrush = new SolidBrush(Color.SteelBlue);
            sStrongAttack.TextBrush = sNormal.TextBrush;
            sStrong.TextBrush = sNormal.TextBrush;
            sShield.TextBrush = sNormal.TextBrush;
            sRiposte.TextBrush = sNormal.TextBrush;
            sContinue.TextBrush = sNormal.TextBrush;

            view1.Location = new Point(20, 20);
            sChoice.Location = new Point(view1.Right + 30, view1.Top + 100);
            view2.Location = new Point(sChoice.Right + 30, view1.Top);
            sReport.Top = view1.Bottom + 25;

            sNormal.Top = sChoice.Bottom + 25;
            sStrongAttack.Top = sNormal.Top;
            sContinue.Top = sNormal.Top;
            sStrong.Top = sNormal.Bottom + 5;
            sShield.Top = sStrong.Bottom + 5;
            sRiposte.Top = sShield.Bottom + 5;

            sNormal.CentreX = sChoice.CentreX;
            sStrongAttack.CentreX = sNormal.CentreX;
            sContinue.CentreX = sNormal.CentreX;
            sStrong.CentreX = sChoice.CentreX;
            sShield.CentreX = sChoice.CentreX;
            sRiposte.CentreX = sChoice.CentreX;

            Components.Add(view1);
            Components.Add(view2);
            Components.Add(sChoice);
            Components.Add(sStrongAttack);
            Components.Add(sContinue);
            Components.Add(sNormal);
            Components.Add(sStrong);
            Components.Add(sShield);
            Components.Add(sRiposte);
            Components.Add(sReport);
        }

        #endregion
    }
}
