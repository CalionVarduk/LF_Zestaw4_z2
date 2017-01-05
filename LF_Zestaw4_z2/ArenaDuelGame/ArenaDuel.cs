using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LF_Zestaw4_z2.UI;
using LF_Zestaw4_z2.ArenaDuelGame.UI;

namespace LF_Zestaw4_z2.ArenaDuelGame
{
    public class ArenaDuel : GraDwuosobowa
    {
        public override string Nazwa { get { return "Arena Duel"; } }

        private PagePanel display;

        private ArenaDuelLogic logic;
        private GameState state;

        private WarriorCreatorPage uiCreator;
        private GamePage uiGame;
        private GameFinishedPage uiFinished;
        private WinnerPage uiWinner;

        public ArenaDuel(PagePanel display)
        {
            this.display = display;

            state = GameState.CreatingWarrior1;
            uiCreator = new WarriorCreatorPage();
            uiCreator.ContinueClicked += this.Event_WarriorCreated;

            display.Page = uiCreator;
        }

        public override void Przerwij()
        {
            base.Przerwij();
            state = GameState.FinallyDone;
        }

        protected override void UstawGre()
        {
            WaitWhile(GameState.CreatingWarrior1);
            WaitWhile(GameState.CreatingWarrior2);
        }

        protected override void RozegrajGre()
        {
            WaitWhile(GameState.Running);
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

        private void Event_WarriorCreated(object sender, MouseEventArgs e)
        {
            if (state == GameState.CreatingWarrior1)
                SetFirstWarrior();
            else SetSecondWarrior();
            ++state;
        }

        private void Event_AttackingNormal(object sender, MouseEventArgs e)
        {
            ChoiceMade(TurnAction.NormalAttack);
        }

        private void Event_AttackingStrong(object sender, MouseEventArgs e)
        {
            ChoiceMade(TurnAction.Waiting);
        }

        private void Event_PreparingStrongAttack(object sender, MouseEventArgs e)
        {
            ChoiceMade(TurnAction.StrongAttack);
        }

        private void Event_Shielding(object sender, MouseEventArgs e)
        {
            ChoiceMade(TurnAction.Shield);
        }

        private void Event_Riposting(object sender, MouseEventArgs e)
        {
            ChoiceMade(TurnAction.Riposte);
        }

        private void Event_ToGameFinished(object sender, MouseEventArgs e)
        {
            SwitchToFinishedUI();
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

        private void SetFirstWarrior()
        {
            logic = new ArenaDuelLogic();
            logic.Warrior1.Attributes.Set(uiCreator.Attributes);
            uiCreator.Reset("Player 2 attributes:");
        }

        private void SetSecondWarrior()
        {
            logic.Warrior2.Attributes.Set(uiCreator.Attributes);
            logic.Start();
            SwitchToGameUI();
        }

        private void ChoiceMade(TurnAction action)
        {
            if (logic.Player1Turn) logic.Warrior1.NextAction = action;
            else logic.Warrior2.NextAction = action;

            TurnReport report = logic.NextTurn();
            uiGame.NextTurn(report);
        }

        private void SwitchToGameUI()
        {
            uiCreator.RemoveSubscriptions();
            uiCreator.ContinueClicked -= this.Event_WarriorCreated;
            uiCreator = null;

            uiGame = new GamePage(logic);
            uiGame.AttackingNormal += this.Event_AttackingNormal;
            uiGame.AttackingStrong += this.Event_AttackingStrong;
            uiGame.PreparingStrongAttack += this.Event_PreparingStrongAttack;
            uiGame.Shielding += this.Event_Shielding;
            uiGame.Riposting += this.Event_Riposting;
            uiGame.ContinueClicked += this.Event_ToGameFinished;

            display.Page = uiGame;
        }

        private void SwitchToFinishedUI()
        {
            uiGame.AttackingNormal -= this.Event_AttackingNormal;
            uiGame.AttackingStrong -= this.Event_AttackingStrong;
            uiGame.PreparingStrongAttack -= this.Event_PreparingStrongAttack;
            uiGame.Shielding -= this.Event_Shielding;
            uiGame.Riposting -= this.Event_Riposting;
            uiGame.ContinueClicked -= this.Event_ToGameFinished;
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
