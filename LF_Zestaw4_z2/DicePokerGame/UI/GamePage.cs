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
    class GamePage : ClickablePage
    {
        public DicePokerLogic Logic { get; private set; }
        private bool gameFinished;

        public event MouseEventHandler ActionConfirmed
        {
            add { sAction.Click += value; }
            remove { sAction.Click -= value; }
        }

        public event MouseEventHandler ContinueClicked
        {
            add { sContinue.Click += value; }
            remove { sContinue.Click -= value; }
        }

        public GamePage(DicePokerLogic logic)
        {
            gameFinished = logic.GameFinished;
            Logic = logic;
            Initialize();
            Size = new Size(ComponentsRight + 10, ComponentsBottom + 10);
        }

        public void NewRound()
        {
            if (!gameFinished)
            {
                sInfo.Text = "Player 1 rolls:";
                sAction.Visible = true;
                sRank1.Visible = false;
                sRank2.Visible = false;
                dice1.Visible = false;
                dice2.Visible = false;
                sRoundWinner.Visible = false;
                sContinue.Visible = false;
            }
        }

        public void NextTurn()
        {
            if (!gameFinished)
            {
                switch (Logic.GameState)
                {
                    case GameState.Phase1Player2:
                        NextTurnPhase1Player2(); break;

                    case GameState.Phase2Player1:
                        NextTurnPhase2Player1(); break;

                    case GameState.Phase2Player2:
                        NextTurnPhase2Player2(); break;

                    case GameState.Phase3Player1:
                        NextTurnPhase3Player1(); break;

                    case GameState.Phase3Player2:
                        NextTurnPhase3Player2(); break;

                    case GameState.RoundEnded:
                    case GameState.Finished:
                        NextTurnRoundEnded(); break;
                }
            }
        }

        private void NextTurnPhase1Player2()
        {
            sInfo.Text = "Player 2 rolls:";
            sRank1.Text = "Player 1 Rank: " + dice1.Dice.RankString;
            sRank1.Visible = true;
            dice1.Visible = true;
        }

        private void NextTurnPhase2Player1()
        {
            sInfo.Text = "Player 1 locks:";
            sAction.Text = "Accept!";
            sRank2.Text = "Player 2 Rank: " + dice2.Dice.RankString;
            sRank2.Right = dice2.Right;
            sRank2.Visible = true;
            dice2.Visible = true;
            dice1.Locked = false;
        }

        private void NextTurnPhase2Player2()
        {
            sInfo.Text = "Player 2 locks:";
            dice1.Locked = true;
            dice2.Locked = false;
        }

        private void NextTurnPhase3Player1()
        {
            sInfo.Text = "Player 1 rolls:";
            sAction.Text = "Roll!";
            dice2.Locked = true;
        }

        private void NextTurnPhase3Player2()
        {
            sInfo.Text = "Player 2 rolls:";
            sRank1.Text = "Player 1 Rank: " + dice1.Dice.RankString;
        }

        private void NextTurnRoundEnded()
        {
            sRoundWinner.Text = (Logic.LastRoundWinner != 0) ? "Round Winner: Player " + Logic.LastRoundWinner.ToString() : "Draw!";
            sRoundWinner.CentreX = CentreX;

            if (Logic.LastRoundWinner == 1) sWins1.Text = "Player 1 Score: " + Logic.Wins1.ToString();
            else if (Logic.LastRoundWinner == 2) sWins2.Text = "Player 2 Score: " + Logic.Wins2.ToString();

            sInfo.Text = "Round has ended.";
            sRank2.Text = "Player 2 Rank: " + dice2.Dice.RankString;
            sRank2.Right = dice2.Right;
            sRoundWinner.Visible = true;
            sContinue.Visible = true;
            sAction.Visible = false;

            if (Logic.GameState == GameState.Finished)
                gameFinished = true;
        }

        #region UI Init

        private ClickableString sInfo;
        private ClickableString sAction;
        private ClickableString sRank1;
        private ClickableString sRank2;
        private ClickableString sWins1;
        private ClickableString sWins2;
        private ClickableString sRoundWinner;
        private ClickableString sContinue;
        private ClickableDice dice1;
        private ClickableDice dice2;

        private void Initialize()
        {
            sInfo = new ClickableString("Player 1 rolls:") { Locked = true };
            sAction = new ClickableString("Roll!");
            sRank1 = new ClickableString() { Visible = false, Locked = true };
            sRank2 = new ClickableString() { Visible = false, Locked = true };
            sWins1 = new ClickableString("Player 1 Score: 0") { Locked = true };
            sWins2 = new ClickableString("Player 2 Score: 0") { Locked = true };
            sRoundWinner = new ClickableString("Winner") { Visible = false, Locked = true };
            sContinue = new ClickableString("Continue!") { Visible = false };
            dice1 = new ClickableDice(Logic.Dice1) { Visible = false, Locked = true };
            dice2 = new ClickableDice(Logic.Dice2) { Visible = false, Locked = true };

            sInfo.Location = new Point(10, 10);
            sAction.Location = new Point(sInfo.Left, sInfo.Bottom + 10);
            sAction.TextBrush = new SolidBrush(Color.SteelBlue);
            sContinue.TextBrush = sAction.TextBrush;

            sRank1.Location = new Point(sInfo.Left, sAction.Bottom + 70);
            dice1.Location = new Point(sInfo.Left, sRank1.Bottom + 15);

            dice2.Location = new Point(dice1.Right + 40, dice1.Top);
            sRank2.Right = dice2.Right;
            sRank2.Top = sRank1.Top;

            sWins1.Top = sInfo.Top;
            sWins1.Right = dice2.Right;
            sWins2.Top = sWins1.Bottom + 10;
            sWins2.Right = dice2.Right;

            sRoundWinner.Bottom = dice2.Top >> 1;
            sRoundWinner.CentreX = (dice2.Right + 10) >> 1;
            sContinue.Top = sRoundWinner.Bottom + 5;
            sContinue.CentreX = sRoundWinner.CentreX;

            Components.Add(sInfo);
            Components.Add(sAction);
            Components.Add(sRank1);
            Components.Add(sRank2);
            Components.Add(sWins1);
            Components.Add(sWins2);
            Components.Add(sRoundWinner);
            Components.Add(sContinue);
            Components.Add(dice1);
            Components.Add(dice2);
        }

        #endregion
    }
}
