using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF_Zestaw4_z2.ArenaDuelGame
{
    public class ArenaDuelLogic
    {
        public const int InitPointsToSpend = 50;
        public const int InitStrengthCost = 1;
        public const int InitDexterityCost = 1;
        public const int InitOffenceCost = 1;
        public const int InitDefenceCost = 1;
        public const int InitMaxHealthCost = 1;
        public const int InitInitiativeCost = 1;
        public const int InitMaxActionPointsCost = 5;
        public const int InitArmorCost = 1;

        public const int InitStrengthIncr = 1;
        public const int InitDexterityIncr = 1;
        public const int InitOffenceIncr = 1;
        public const int InitDefenceIncr = 1;
        public const int InitMaxHealthIncr = 5;
        public const int InitInitiativeIncr = 1;
        public const int InitMaxActionPointsIncr = 1;
        public const int InitArmorIncr = 1;

        public Warrior Warrior1 { get; private set; }
        public Warrior Warrior2 { get; private set; }

        public bool Player1Won { get { return (Warrior1.IsAlive && Warrior2.IsDead); } }
        public bool Player2Won { get { return (Warrior1.IsDead && Warrior2.IsAlive); } }
        public bool GameFinished { get { return (Warrior1.IsDead || Warrior2.IsDead); } }

        private double initiativeCtrl;
        public bool Player1Turn { get { return (initiativeCtrl > 0); } }
        public bool Player2Turn { get { return !Player1Turn; } }

        public ArenaDuelLogic()
        {
            Warrior1 = new Warrior();
            Warrior2 = new Warrior();
            initiativeCtrl = 0;
        }

        public void Start()
        {
            if (!GameFinished)
            {
                initiativeCtrl = (Warrior1.Attributes.Initiative < Warrior2.Attributes.Initiative) ?
                    -Warrior2.Attributes.Initiative + 1 : Warrior1.Attributes.Initiative;
            }
        }

        public TurnReport NextTurn()
        {
            TurnReport report = null;

            if (!GameFinished)
            {
                if (Player1Turn)
                {
                    report = Warrior1.TakeActionAgainst(Warrior2);
                    initiativeCtrl -= Warrior2.Attributes.Initiative;
                    report.Player1Active = true;
                    return report;
                }

                report = Warrior2.TakeActionAgainst(Warrior1);
                initiativeCtrl += Warrior1.Attributes.Initiative;
                report.Player1Active = false;
                return report;
            }
            return report;
        }
    }
}
