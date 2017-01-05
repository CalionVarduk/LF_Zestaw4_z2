using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF_Zestaw4_z2.ArenaDuelGame
{
    public class TurnReport
    {
        public bool Player1Active { get; set; }
        public TurnAction ActivePlayerAction { get; private set; }

        private byte activeStance;
        private byte inactiveStance;
        public bool ActivePlayerShielding { get { return (activeStance == 0); } }
        public bool ActivePlayerRiposting { get { return (activeStance == 1); } }
        public bool ActivePlayerPreparing { get { return (activeStance == 2); } }
        public bool InactivePlayerShielding { get { return (inactiveStance == 0); } }
        public bool InactivePlayerRiposting { get { return (inactiveStance == 1); } }
        public bool InactivePlayerPreparing { get { return (inactiveStance == 2); } }

        public double ActiveDamage { get; set; }
        public double InactiveDamage { get; set; }

        public bool AttackDodged { get; set; }
        public bool AttackRiposted { get; set; }
        public bool AttackFullHit { get; set; }
        public bool AttackCrit { get; set; }

        public TurnReport(Warrior active, Warrior inactive)
        {
            ActivePlayerAction = active.NextAction;
            activeStance = (byte)(active.NextAction == TurnAction.Shield ? 0 : active.NextAction == TurnAction.Riposte ? 1 : active.IsPreparing ? 2 : 3);
            inactiveStance = (byte)(inactive.IsShielding ? 0 : inactive.IsRiposting ? 1 : inactive.IsPreparing ? 2 : 3);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(100);

            sb.Append(Player1Active ? "Player 1 " : "Player 2 ");

            if (ActivePlayerShielding) sb.Append("is shielding!");
            else if (ActivePlayerRiposting) sb.Append("is riposting!");
            else if (ActivePlayerPreparing) sb.Append("is preparing a strong attack!");
            else
            {
                sb.Append("attacked (").Append(AttackFullHit ? "hit" : "glanced").Append(AttackCrit ? ", crit) " : ") ");
                sb.Append(Player1Active ? "Player 2 " : "Player 1 ");

                if (AttackDodged) sb.Append("but the attack has been dodged!");
                else
                {
                    sb.Append("for ").Append(ActiveDamage.ToString("F2")).Append(" damage!");

                    if (InactivePlayerShielding) sb.Append(" (shielded)");
                    else if (AttackRiposted) sb.Append(" (riposted for ").Append(InactiveDamage.ToString("F2")).Append(" damage!)");
                }
            }

            return sb.ToString();
        }
    }
}
