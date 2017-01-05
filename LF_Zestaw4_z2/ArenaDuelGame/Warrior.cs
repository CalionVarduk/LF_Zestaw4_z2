using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF_Zestaw4_z2.ArenaDuelGame
{
    public class Warrior
    {
        public const double MultShielded = 0.1;
        public const double MultCritHit = 1.75;
        public const double MultDefendedHit = 0.3;
        public const double MultStrongAttack = 2.0;
        public const double MultStrongAttackRiposte = 2.5;
        public const double MultRipostedHit = 0.0;
        public const double MultPreparingHit = 1.1;

        public WarriorAttributes Attributes { get; private set; }

        public bool IsAlive { get { return (Attributes.Health > 0); } }
        public bool IsDead { get { return !IsAlive; } }
        public bool HasActionPoints { get { return (Attributes.ActionPoints > 0); } }

        public bool IsShielding { get; private set; }
        public bool IsRiposting { get; private set; }
        public bool IsPreparing { get { return (PrepTime > 0); } }
        public bool CanDodge { get { return (!IsShielding && !IsRiposting && !IsPreparing); } }

        public int PrepTime { get; private set; }

        private TurnAction nextAction;
        public TurnAction NextAction
        {
            get { return nextAction; }
            set
            {
                if  (
                    (nextAction == TurnAction.Waiting) &&
                    (value != TurnAction.Waiting) &&
                    (CanDodge || value <= TurnAction.NormalAttack)
                    )
                {
                    if (value == TurnAction.NormalAttack)
                    {
                        nextAction = value;
                        PrepTime = 1;
                    }
                    else if (HasActionPoints)
                    {
                        nextAction = value;
                        PrepTime = (nextAction == TurnAction.StrongAttack) ? 2 : 1;
                    }
                }
            }
        }

        public Warrior()
        {
            Attributes = new WarriorAttributes();
            nextAction = TurnAction.Waiting;
            PrepTime = 0;
        }

        public TurnReport TakeActionAgainst(Warrior enemy)
        {
            if (nextAction == TurnAction.Waiting)
                throw new InvalidOperationException("A warrior must have his next action specified before taking said action.");

            if (PrepTime == 0 || --PrepTime == 0)
            {
                TurnReport report = new TurnReport(this, enemy);

                switch (nextAction)
                {
                    case TurnAction.NormalAttack:
                        NormalAttack(enemy, report); break;

                    case TurnAction.StrongAttack:
                        StrongAttack(enemy, report); break;

                    case TurnAction.Shield:
                        ShieldOn(); break;

                    case TurnAction.Riposte:
                        RiposteOn(); break;
                }

                nextAction = TurnAction.Waiting;
                return report;
            }
            return new TurnReport(this, enemy);
        }

        private void NormalAttack(Warrior enemy, TurnReport report)
        {
            if (!enemy.DodgeSucceeded())
            {
                double preMult = EvalPreArmorDamageMult(enemy, report);
                double postMult = EvalPostArmorDamageMult(enemy);
                double dmg = DamageAgainst(preMult, postMult, enemy);
                report.ActiveDamage = dmg;

                if (enemy.IsRiposting)
                {
                    enemy.ReceiveDamage(dmg);
                    dmg = DamageAgainst(1.0, enemy.Attributes.RiposteDamagePerc / 100, this);
                    report.InactiveDamage = dmg;
                    report.AttackRiposted = true;
                    ReceiveDamage(dmg);
                }
                else enemy.ReceiveDamage(dmg);
            }
            else report.AttackDodged = true;

            if(!IsShielding && !IsRiposting) ++Attributes.ActionPoints;
        }

        private void StrongAttack(Warrior enemy, TurnReport report)
        {
            if (!enemy.DodgeSucceeded())
            {
                double preMult = EvalPreArmorDamageMult(enemy, report) * (enemy.IsRiposting ? MultStrongAttackRiposte : MultStrongAttack);
                double postMult = EvalPostArmorDamageMult(enemy);
                double dmg = DamageAgainst(preMult, postMult, enemy);
                report.ActiveDamage = dmg;
                enemy.ReceiveDamage(dmg);
            }
            else report.AttackDodged = true;

            --Attributes.ActionPoints;
        }

        private void ShieldOn()
        {
            IsShielding = true;
            --Attributes.ActionPoints;
        }

        private void RiposteOn()
        {
            IsRiposting = true;
            --Attributes.ActionPoints;
        }

        private void ReceiveDamage(double dmg)
        {
            Attributes.Health -= dmg;
            IsShielding = false;
            IsRiposting = false;
        }

        private double EvalPreArmorDamageMult(Warrior enemy, TurnReport report)
        {
            double hitChance, critChance, mult = 1.0;
            Attributes.HitCritChanceAgainst(enemy.Attributes, out hitChance, out critChance);

            if (!AttemptEvaluator.Succeeded(hitChance))
                mult *= MultDefendedHit;
            else report.AttackFullHit = true;

            if (AttemptEvaluator.Succeeded(critChance))
            {
                report.AttackCrit = true;
                mult *= MultCritHit;
            }

            return mult;
        }

        private double EvalPostArmorDamageMult(Warrior enemy)
        {
            double mult = 1.0;

            if (enemy.IsShielding) mult *= MultShielded;
            else if (enemy.IsPreparing) mult *= MultPreparingHit;
            else if (enemy.IsRiposting && nextAction != TurnAction.StrongAttack) mult *= MultRipostedHit;

            return mult;
        }

        private double DamageAgainst(double preArmorMult, double postArmorMult, Warrior other)
        {
            return Limiter.AtLeast(1, (Limiter.AtLeast(0, (Attributes.Damage * preArmorMult) - other.Attributes.Armor) * postArmorMult));
        }

        private bool DodgeSucceeded()
        {
            return (CanDodge && AttemptEvaluator.Succeeded(Attributes.DodgeChance));
        }
    }
}
