using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF_Zestaw4_z2.DicePokerGame
{
    public class DicePokerLogic
    {
        public Random Rng { get; private set; }
        public GameState GameState { get; private set; }
        public int WinAt { get; private set; }
        public int Wins1 { get; private set; }
        public int Wins2 { get; private set; }
        public Dice Dice1 { get; private set; }
        public Dice Dice2 { get; private set; }

        public bool Player1Won { get { return (Wins1 == WinAt); } }
        public bool Player2Won { get { return (Wins2 == WinAt); } }
        public bool GameFinished { get { return (Player1Won || Player2Won); } }
        public int LastRoundWinner { get; private set; }

        public DicePokerLogic(int winAt)
        {
            Rng = new Random();
            GameState = GameState.Phase1Player1;
            WinAt = Limiter.AtLeast(1, winAt);
            Wins1 = 0;
            Wins2 = 0;
            LastRoundWinner = 0;
            Dice1 = new Dice();
            Dice2 = new Dice();
        }

        public void NewRound()
        {
            if (GameState != GameState.Finished)
            {
                ResetDice(Dice1);
                ResetDice(Dice2);
                GameState = GameState.Phase1Player1;
            }
        }

        public void NextTurn()
        {
            if (GameState < GameState.RoundEnded)
            {
                switch (GameState)
                {
                    case GameState.Phase1Player1:
                        Dice1.Roll(Rng);
                        break;

                    case GameState.Phase1Player2:
                        Dice2.Roll(Rng);
                        Dice1.CanLock = true;
                        break;

                    case GameState.Phase2Player1:
                        Dice1.CanLock = false;
                        Dice2.CanLock = true;
                        break;

                    case GameState.Phase2Player2:
                        Dice2.CanLock = false;
                        break;

                    case GameState.Phase3Player1:
                        Dice1.Roll(Rng);
                        ResetDice(Dice1);
                        break;

                    case GameState.Phase3Player2:
                        Dice2.Roll(Rng);
                        ResetDice(Dice2);
                        break;
                }

                if (++GameState == GameState.RoundEnded)
                    EndOfRound();
            }
        }

        private void EndOfRound()
        {
            Rank r1 = Dice1.Rank;
            Rank r2 = Dice2.Rank;

            if (r1 > r2)
            {
                LastRoundWinner = 1;
                ++Wins1;
            }
            else if (r1 < r2)
            {
                LastRoundWinner = 2;
                ++Wins2;
            }
            else LastRoundWinner = 0;

            if (GameFinished)
                GameState = GameState.Finished;
        }

        private void ResetDice(Dice d)
        {
            d.CanLock = true;
            d.UnlockAll();
            d.CanLock = false;
        }
    }
}
