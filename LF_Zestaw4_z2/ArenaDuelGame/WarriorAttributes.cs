using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF_Zestaw4_z2.ArenaDuelGame
{
    public class WarriorAttributes
    {
        public const int DefaultStrength = 10;
        public const int DefaultDexterity = 10;
        public const int DefaultOffence = 10;
        public const int DefaultDefence = 10;
        public const int DefaultArmor = 0;
        public const int DefaultMaxHealthBase = 80;
        public const int DefaultInitiativeBase = 8;
        public const int DefaultMaxActionPointsBase = 0;
        public const double DodgeChanceBase = -2.5;
        public const double RiposteDamagePercBase = 20;
        public const double HitChanceBase = 60;
        public const double CritChanceBase = 65;

        public const double MultStrengthMaxHealth = 2;
        public const double MultStrengthMaxActionPoints = 0.2;
        public const double MultDexterityMaxActionPoints = 0.1;
        public const double MultDexterityInitiative = 0.25;
        public const double MultDexterityDodgeChance = 0.75;
        public const double MultDexterityRiposteDamagePerc = 4;
        public const double MultStrengthDamage = 1;
        public const double MultDexterityDamage = 0.25;

        private int strength;
        public int Strength
        {
            get { return strength; }
            set { strength = Limiter.AtLeast(1, value); }
        }

        private int dexterity;
        public int Dexterity
        {
            get { return dexterity; }
            set { dexterity = Limiter.AtLeast(1, value); }
        }

        private int offence;
        public int Offence
        {
            get { return offence; }
            set { offence = Limiter.AtLeast(1, value); }
        }

        private int defence;
        public int Defence
        {
            get { return defence; }
            set { defence = Limiter.AtLeast(1, value); }
        }

        private int armor;
        public int Armor
        {
            get { return armor; }
            set { armor = Limiter.AtLeast(0, value); }
        }

        private int maxHpBase;
        public int MaxHealthBase
        {
            get { return maxHpBase; }
            set { maxHpBase = Limiter.AtLeast(5, value); }
        }

        private double health;
        public double Health
        {
            get { return health; }
            set { health = Limiter.Between(0, MaxHealth, value); }
        }

        private int initiativeBase;
        public int InitiativeBase
        {
            get { return initiativeBase; }
            set { initiativeBase = Limiter.AtLeast(1, value); }
        }

        private int maxActionPtsBase;
        public int MaxActionPointsBase
        {
            get { return maxActionPtsBase; }
            set { maxActionPtsBase = Limiter.AtLeast(0, value); }
        }

        private int actionPts;
        public int ActionPoints
        {
            get { return actionPts; }
            set { actionPts = Limiter.Between(0, MaxActionPoints, value); }
        }

        public double MaxHealth { get { return MaxHealthBase + (Strength * MultStrengthMaxHealth); } }
        public double Initiative { get { return InitiativeBase + (Dexterity * MultDexterityInitiative); } }
        public int MaxActionPoints { get { return MaxActionPointsBase + (int)(Strength * MultStrengthMaxActionPoints + Dexterity * MultDexterityMaxActionPoints); } }

        public double DodgeChance { get { return Limiter.Between(0, 100, DodgeChanceBase + (Dexterity * MultDexterityDodgeChance)); } }
        public double RiposteDamagePerc { get { return Limiter.AtLeast(20, RiposteDamagePercBase + (Dexterity * MultDexterityRiposteDamagePerc)); } }

        public double Damage { get { return (Strength * MultStrengthDamage) + (Dexterity * MultDexterityDamage); } }

        public WarriorAttributes()
        {
            strength = DefaultStrength;
            dexterity = DefaultDexterity;
            offence = DefaultOffence;
            defence = DefaultDefence;
            armor = DefaultArmor;
            maxHpBase = DefaultMaxHealthBase;
            initiativeBase = DefaultInitiativeBase;
            maxActionPtsBase = DefaultMaxActionPointsBase;
            actionPts = MaxActionPoints;
            health = MaxHealth;
        }

        public WarriorAttributes(WarriorAttributes other)
        {
            Set(other);
        }

        public void Set(WarriorAttributes other)
        {
            strength = other.Strength;
            dexterity = other.Dexterity;
            offence = other.Offence;
            defence = other.Defence;
            armor = other.Armor;
            maxHpBase = other.MaxHealthBase;
            initiativeBase = other.InitiativeBase;
            maxActionPtsBase = other.MaxActionPointsBase;
            actionPts = other.ActionPoints;
            health = other.Health;
        }

        public void Reset()
        {
            health = MaxHealth;
            actionPts = MaxActionPoints;
        }

        public double HitChanceAgainst(WarriorAttributes other)
        {
            double chance = CalcHitChance(other.Defence);
            return Limiter.Between(0, 100, chance);
        }

        public double CritChanceAgainst(WarriorAttributes other)
        {
            double chance = CalcHitChance(other.Defence) - CritChanceBase;
            return Limiter.Between(0, 100, chance);
        }

        public void HitCritChanceAgainst(WarriorAttributes other, out double hitChance, out double critChance)
        {
            double chance = CalcHitChance(other.Defence);
            hitChance = Limiter.Between(0, 100, chance);
            critChance = Limiter.Between(0, 100, chance - CritChanceBase);
        }

        private double CalcHitChance(int defence)
        {
            return HitChanceBase + ((Offence - defence) * 3);
        }
    }
}
