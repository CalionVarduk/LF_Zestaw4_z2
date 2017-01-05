using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using LF_Zestaw4_z2.UI;

namespace LF_Zestaw4_z2.ArenaDuelGame.UI
{
    public class WarriorView : ClickablePage
    {
        public Warrior Warrior { get; private set; }

        public WarriorView(string name, Warrior warrior, Warrior enemy)
        {
            Locked = true;
            Warrior = warrior;
            Initialize(name, enemy.Attributes);
            Size = new Size(ComponentsRight, ComponentsBottom);
        }

        public void Update()
        {
            UpdateHealth();
            UpdateActionPts();
            UpdateStatus();
        }

        public void UpdateHealth()
        {
            bHealth.Percentage = Warrior.Attributes.Health / Warrior.Attributes.MaxHealth;
            sHealth.Text = Warrior.Attributes.Health.ToString("F2") + "/" + Warrior.Attributes.MaxHealth.ToString("F2");
        }

        public void UpdateActionPts()
        {
            bActionPts.Percentage = Warrior.Attributes.ActionPoints / (double)Warrior.Attributes.MaxActionPoints;
            sActionPts.Text = Warrior.Attributes.ActionPoints.ToString() + "/" + Warrior.Attributes.MaxActionPoints.ToString();
        }

        public void UpdateStatus()
        {
            if (Warrior.IsShielding)
            {
                sStatus.Text = "[ Is shielding! ]";
                sStatus.Visible = true;
            }
            else if (Warrior.IsRiposting)
            {
                sStatus.Text = "[ Is riposting! ]";
                sStatus.Visible = true;
            }
            else if (Warrior.IsPreparing)
            {
                sStatus.Text = "[ Is preparing a strong attack! ]";
                sStatus.Visible = true;
            }
            else sStatus.Visible = false;
        }

        #region UI Init

        private ClickableBar bHealth;
        private ClickableBar bActionPts;
        private ClickableString sHealth;
        private ClickableString sActionPts;
        private ClickableString sStatus;

        private void Initialize(string name, WarriorAttributes enemy)
        {
            var sName = new ClickableString(name) { Locked = true };

            bHealth = new ClickableBar() { Locked = true };
            bActionPts = new ClickableBar() { Locked = true };

            var sHealthTitle = new ClickableString("Health:") { Locked = true };
            var sActionPtsTitle = new ClickableString("Action Points:") { Locked = true };
            var sHitChanceTitle = new ClickableString("Full Hit Chance:") { Locked = true };
            var sCritChanceTitle = new ClickableString("Crit Chance:") { Locked = true };
            var sInitiativeTitle = new ClickableString("Initiative:") { Locked = true };
            var sStrengthTitle = new ClickableString("Strength:") { Locked = true };
            var sDexterityTitle = new ClickableString("Dexterity:") { Locked = true };
            var sOffenceTitle = new ClickableString("Offence:") { Locked = true };
            var sDefenceTitle = new ClickableString("Defence:") { Locked = true };
            var sDamageTitle = new ClickableString("Damage:") { Locked = true };
            var sArmorTitle = new ClickableString("Armor:") { Locked = true };
            var sDodgeTitle = new ClickableString("Dodge Chance:") { Locked = true };
            var sRiposteTitle = new ClickableString("Riposte Dmg Mult:") { Locked = true };

            sHealth = new ClickableString(Warrior.Attributes.Health.ToString("F2") + "/" + Warrior.Attributes.MaxHealth.ToString("F2")) { Locked = true };
            sActionPts = new ClickableString(Warrior.Attributes.ActionPoints.ToString() + "/" + Warrior.Attributes.MaxActionPoints.ToString()) { Locked = true };

            var sHitChance = new ClickableString(Warrior.Attributes.HitChanceAgainst(enemy).ToString("F2") + " %") { Locked = true };
            var sCritChance = new ClickableString(Warrior.Attributes.CritChanceAgainst(enemy).ToString("F2") + " %") { Locked = true };
            var sInitiative = new ClickableString(Warrior.Attributes.Initiative.ToString("F2")) { Locked = true };
            var sStrength = new ClickableString(Warrior.Attributes.Strength.ToString()) { Locked = true };
            var sDexterity = new ClickableString(Warrior.Attributes.Dexterity.ToString()) { Locked = true };
            var sOffence = new ClickableString(Warrior.Attributes.Offence.ToString()) { Locked = true };
            var sDefence = new ClickableString(Warrior.Attributes.Defence.ToString()) { Locked = true };
            var sDamage = new ClickableString(Warrior.Attributes.Damage.ToString("F2")) { Locked = true };
            var sArmor = new ClickableString(Warrior.Attributes.Armor.ToString()) { Locked = true };
            var sDodge = new ClickableString(Warrior.Attributes.DodgeChance.ToString("F2") + " %") { Locked = true };
            var sRiposte = new ClickableString(Warrior.Attributes.RiposteDamagePerc.ToString("F2") + " %") { Locked = true };
            sStatus = new ClickableString("status") { Visible = false, Locked = true };

            (bHealth.ForeBrush as SolidBrush).Color = Color.OrangeRed;

            int width = sRiposteTitle.Width + sRiposte.Width + 5;

            bHealth.Margin = 2;
            bActionPts.Margin = bHealth.Margin;
            bHealth.Width = width + 100;
            bActionPts.Width = bHealth.Width;

            sName.Top = 0;
            bHealth.Location = new Point(0, sName.Bottom + 5);
            bActionPts.Location = new Point(bHealth.Left, bHealth.Bottom - 3);
            sName.CentreX = bHealth.CentreX;

            sHealthTitle.Location = new Point(25, bActionPts.Bottom + 5);
            sActionPtsTitle.Location = new Point(sHealthTitle.Left, sHealthTitle.Bottom + 5);
            sHitChanceTitle.Location = new Point(sHealthTitle.Left, sActionPtsTitle.Bottom + 5);
            sCritChanceTitle.Location = new Point(sHealthTitle.Left, sHitChanceTitle.Bottom + 5);
            sInitiativeTitle.Location = new Point(sHealthTitle.Left, sCritChanceTitle.Bottom + 5);
            sStrengthTitle.Location = new Point(sHealthTitle.Left, sInitiativeTitle.Bottom + 5);
            sDexterityTitle.Location = new Point(sHealthTitle.Left, sStrengthTitle.Bottom + 5);
            sOffenceTitle.Location = new Point(sHealthTitle.Left, sDexterityTitle.Bottom + 5);
            sDefenceTitle.Location = new Point(sHealthTitle.Left, sOffenceTitle.Bottom + 5);
            sDamageTitle.Location = new Point(sHealthTitle.Left, sDefenceTitle.Bottom + 5);
            sArmorTitle.Location = new Point(sHealthTitle.Left, sDamageTitle.Bottom + 5);
            sDodgeTitle.Location = new Point(sHealthTitle.Left, sArmorTitle.Bottom + 5);
            sRiposteTitle.Location = new Point(sHealthTitle.Left, sDodgeTitle.Bottom + 5);
            sStatus.Location = new Point(sHealthTitle.Left, sRiposteTitle.Bottom + 10);

            sHealth.Location = new Point(sRiposteTitle.Right + 5, sHealthTitle.Top);
            sActionPts.Location = new Point(sHealth.Left, sActionPtsTitle.Top);
            sHitChance.Location = new Point(sHealth.Left, sHitChanceTitle.Top);
            sCritChance.Location = new Point(sHealth.Left, sCritChanceTitle.Top);
            sInitiative.Location = new Point(sHealth.Left, sInitiativeTitle.Top);
            sStrength.Location = new Point(sHealth.Left, sStrengthTitle.Top);
            sDexterity.Location = new Point(sHealth.Left, sDexterityTitle.Top);
            sOffence.Location = new Point(sHealth.Left, sOffenceTitle.Top);
            sDefence.Location = new Point(sHealth.Left, sDefenceTitle.Top);
            sDamage.Location = new Point(sHealth.Left, sDamageTitle.Top);
            sArmor.Location = new Point(sHealth.Left, sArmorTitle.Top);
            sDodge.Location = new Point(sHealth.Left, sDodgeTitle.Top);
            sRiposte.Location = new Point(sHealth.Left, sRiposteTitle.Top);

            Components.Add(sName);
            Components.Add(bHealth);
            Components.Add(bActionPts);
            Components.Add(sHealthTitle);
            Components.Add(sActionPtsTitle);
            Components.Add(sHitChanceTitle);
            Components.Add(sCritChanceTitle);
            Components.Add(sInitiativeTitle);
            Components.Add(sStrengthTitle);
            Components.Add(sDexterityTitle);
            Components.Add(sOffenceTitle);
            Components.Add(sDefenceTitle);
            Components.Add(sDamageTitle);
            Components.Add(sArmorTitle);
            Components.Add(sDodgeTitle);
            Components.Add(sRiposteTitle);
            Components.Add(sHealth);
            Components.Add(sActionPts);
            Components.Add(sHitChance);
            Components.Add(sCritChance);
            Components.Add(sInitiative);
            Components.Add(sStrength);
            Components.Add(sDexterity);
            Components.Add(sOffence);
            Components.Add(sDefence);
            Components.Add(sDamage);
            Components.Add(sArmor);
            Components.Add(sDodge);
            Components.Add(sRiposte);
            Components.Add(sStatus);
        }

        #endregion
    }
}
