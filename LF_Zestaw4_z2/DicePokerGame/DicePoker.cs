using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LF_Zestaw4_z2.UI;
using LF_Zestaw4_z2.DicePokerGame.UI;

namespace LF_Zestaw4_z2.DicePokerGame
{
    public class DicePoker : GraDwuosobowa
    {
        public override string Nazwa { get { return "Dice Poker"; } }

        private PagePanel display;

        private DicePokerLogic logic;
        private GameState state;

        private SettingsPage uiSettings;
        private GamePage uiGame;
        private GameFinishedPage uiFinished;
        private WinnerPage uiWinner;

        public DicePoker(PagePanel display)
        {
            this.display = display;

            state = GameState.SettingUp;
            uiSettings = new SettingsPage();
            uiSettings.ChoiceMade2 += this.Event_WinAtChosen;
            uiSettings.ChoiceMade3 += this.Event_WinAtChosen;
            uiSettings.ChoiceMade5 += this.Event_WinAtChosen;

            display.Page = uiSettings;
        }

        public override void Przerwij()
        {
            base.Przerwij();
            state = GameState.FinallyDone;
        }

        protected override void UstawGre()
        {
            WaitWhile(GameState.SettingUp);
        }

        protected override void RozegrajGre()
        {
            while (state < GameState.Finished)
            {
                WaitWhile(GameState.Phase1Player1);
                WaitWhile(GameState.Phase1Player2);
                WaitWhile(GameState.Phase2Player1);
                WaitWhile(GameState.Phase2Player2);
                WaitWhile(GameState.Phase3Player1);
                WaitWhile(GameState.Phase3Player2);
                WaitWhile(GameState.RoundEnded);
                if (!JestPrzerywana) DodajLog("Koniec rundy.");
            }
        }

        protected override void GraJestSkonczona()
        {
            WaitWhile(GameState.Finished);
        }

        protected override void WyswietlZwyciezce()
        {
            DodajLog("Zwyciezca jest: " + (logic.Player1Won ? "Gracz 1!" : "Gracz 2!"));
            WaitWhile(GameState.ShowingWinner);
        }

        private void WaitWhile(GameState state)
        {
            while (this.state == state) Uspij(10);
        }

        private void Event_ActionConfirmed(object sender, MouseEventArgs e)
        {
            logic.NextTurn();
            uiGame.NextTurn();
            ++state;
        }

        private void Event_NextRound(object sender, MouseEventArgs e)
        {
            if (!logic.GameFinished)
            {
                logic.NewRound();
                uiGame.NewRound();
                state = GameState.Phase1Player1;
            }
            else
            {
                SwitchToFinishedUI();
                ++state;
            }
        }

        private void Event_WinAtChosen(object sender, MouseEventArgs e)
        {
            logic = new DicePokerLogic(int.Parse((sender as ClickableString).Text));
            SwitchToGameUI();
            ++state;
        }

        private void Event_ToWinnerDisplay(object sender, MouseEventArgs e)
        {
            SwitchToWinnerUI();
            ++state;
        }

        private void Event_Close(object sender, MouseEventArgs e)
        {
            uiWinner.CloseClicked -= this.Event_Close;
            uiWinner = null;
            logic = null;
            display.Page = null;
            ++state;
        }

        private void SwitchToGameUI()
        {
            uiSettings.ChoiceMade2 -= this.Event_WinAtChosen;
            uiSettings.ChoiceMade3 -= this.Event_WinAtChosen;
            uiSettings.ChoiceMade5 -= this.Event_WinAtChosen;
            uiSettings = null;

            uiGame = new GamePage(logic);
            uiGame.ActionConfirmed += this.Event_ActionConfirmed;
            uiGame.ContinueClicked += this.Event_NextRound;

            display.Page = uiGame;
        }

        private void SwitchToFinishedUI()
        {
            uiGame.ActionConfirmed -= this.Event_ActionConfirmed;
            uiGame.ContinueClicked -= this.Event_NextRound;
            uiGame = null;

            uiFinished = new GameFinishedPage();
            uiFinished.ShowWinnerClicked += this.Event_ToWinnerDisplay;

            display.Page = uiFinished;
        }

        private void SwitchToWinnerUI()
        {
            uiFinished.ShowWinnerClicked -= this.Event_ToWinnerDisplay;
            uiFinished = null;

            uiWinner = new WinnerPage(logic);
            uiWinner.CloseClicked += this.Event_Close;

            display.Page = uiWinner;
        }

        protected override void KoniecPracy()
        {
            display.FindForm().Close();
        }
    }
}
