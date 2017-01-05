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
    public class WarriorCreatorPage : ClickablePage
    {
        private int ptsToSpend;

        private WarriorAttributes prototype;
        public WarriorAttributes Attributes
        {
            get
            {
                WarriorAttributes a = new WarriorAttributes(prototype);
                a.Reset();
                return a;
            }
        }

        public event MouseEventHandler ContinueClicked
        {
            add { sContinue.Click += value; }
            remove { sContinue.Click -= value; }
        }

        public WarriorCreatorPage()
        {
            ptsToSpend = ArenaDuelLogic.InitPointsToSpend;
            prototype = new WarriorAttributes();
            Initialize();
            Size = new Size(ComponentsRight + 30, ComponentsBottom + 30);
        }

        public void Reset(string header)
        {
            prototype = new WarriorAttributes();

            sHeader.Text = header;
            sHealth.Text = prototype.MaxHealth.ToString("F2");
            sDamage.Text = prototype.Damage.ToString("F2");
            sInitiative.Text = prototype.Initiative.ToString("F2");
            sActionPts.Text = prototype.MaxActionPoints.ToString();
            sDodge.Text = prototype.DodgeChance.ToString("F2") + " %";
            sRiposte.Text = prototype.RiposteDamagePerc.ToString("F2") + " %";

            bStrength.Value = prototype.Strength;
            bDexterity.Value = prototype.Dexterity;
            bOffence.Value = prototype.Offence;
            bDefence.Value = prototype.Defence;
            bArmor.Value = prototype.Armor;
            bBaseHealth.Value = prototype.MaxHealthBase;
            bBaseInitiative.Value = prototype.InitiativeBase;
            bBaseActionPts.Value = prototype.MaxActionPointsBase;

            ptsToSpend = ArenaDuelLogic.InitPointsToSpend;
            UpdatePointsToSpend();
        }

        public void RemoveSubscriptions()
        {
            bStrength.ValueChanged -= this.Event_StrengthChanged;
            bDexterity.ValueChanged -= this.Event_DexterityChanged;
            bOffence.ValueChanged -= this.Event_OffenceChanged;
            bDefence.ValueChanged -= this.Event_DefenceChanged;
            bArmor.ValueChanged -= this.Event_ArmorChanged;
            bBaseHealth.ValueChanged -= this.Event_HealthChanged;
            bBaseInitiative.ValueChanged -= this.Event_InitiativeChanged;
            bBaseActionPts.ValueChanged -= this.Event_ActionPtsChanged;
        }

        private void Event_StrengthChanged(object sender, GenericEventArgs<bool> e)
        {
            ptsToSpend += (e.Item ? -ArenaDuelLogic.InitStrengthCost : ArenaDuelLogic.InitStrengthCost);
            prototype.Strength = bStrength.Value;

            sHealth.Text = prototype.MaxHealth.ToString("F2");
            sDamage.Text = prototype.Damage.ToString("F2");
            sActionPts.Text = prototype.MaxActionPoints.ToString();
            UpdatePointsToSpend();
        }

        private void Event_DexterityChanged(object sender, GenericEventArgs<bool> e)
        {
            ptsToSpend += (e.Item ? -ArenaDuelLogic.InitDexterityCost : ArenaDuelLogic.InitDexterityCost);
            prototype.Dexterity = bDexterity.Value;

            sDamage.Text = prototype.Damage.ToString("F2");
            sInitiative.Text = prototype.Initiative.ToString("F2");
            sDodge.Text = prototype.DodgeChance.ToString("F2") + " %";
            sRiposte.Text = prototype.RiposteDamagePerc.ToString("F2") + " %";
            UpdatePointsToSpend();
        }

        private void Event_OffenceChanged(object sender, GenericEventArgs<bool> e)
        {
            ptsToSpend += (e.Item ? -ArenaDuelLogic.InitOffenceCost : ArenaDuelLogic.InitOffenceCost);
            prototype.Offence = bOffence.Value;
            UpdatePointsToSpend();
        }

        private void Event_DefenceChanged(object sender, GenericEventArgs<bool> e)
        {
            ptsToSpend += (e.Item ? -ArenaDuelLogic.InitDefenceCost : ArenaDuelLogic.InitDefenceCost);
            prototype.Defence = bDefence.Value;
            UpdatePointsToSpend();
        }

        private void Event_ArmorChanged(object sender, GenericEventArgs<bool> e)
        {
            ptsToSpend += (e.Item ? -ArenaDuelLogic.InitArmorCost : ArenaDuelLogic.InitArmorCost);
            prototype.Armor = bArmor.Value;
            UpdatePointsToSpend();
        }

        private void Event_HealthChanged(object sender, GenericEventArgs<bool> e)
        {
            ptsToSpend += (e.Item ? -ArenaDuelLogic.InitMaxHealthCost : ArenaDuelLogic.InitMaxHealthCost);
            prototype.MaxHealthBase = bBaseHealth.Value;

            sHealth.Text = prototype.MaxHealth.ToString("F2");
            UpdatePointsToSpend();
        }

        private void Event_InitiativeChanged(object sender, GenericEventArgs<bool> e)
        {
            ptsToSpend += (e.Item ? -ArenaDuelLogic.InitInitiativeCost : ArenaDuelLogic.InitInitiativeCost);
            prototype.InitiativeBase = bBaseInitiative.Value;

            sInitiative.Text = prototype.Initiative.ToString("F2");
            UpdatePointsToSpend();
        }

        private void Event_ActionPtsChanged(object sender, GenericEventArgs<bool> e)
        {
            ptsToSpend += (e.Item ? -ArenaDuelLogic.InitMaxActionPointsCost : ArenaDuelLogic.InitMaxActionPointsCost);
            prototype.MaxActionPointsBase = bBaseActionPts.Value;

            sActionPts.Text = prototype.MaxActionPoints.ToString();
            UpdatePointsToSpend();
        }

        private void UpdatePointsToSpend()
        {
            sPoints.Text = ptsToSpend.ToString();

            if (ptsToSpend < 0)
            {
                sContinue.Visible = false;
                (sPoints.TextBrush as SolidBrush).Color = Color.OrangeRed;
            }
            else
            {
                sContinue.Visible = true;
                (sPoints.TextBrush as SolidBrush).Color = Color.Black;
            }
        }

        #region UI Init

        private ClickableString sHeader;
        private ClickableString sPoints;
        private ClickableSpinBox bStrength;
        private ClickableSpinBox bDexterity;
        private ClickableSpinBox bOffence;
        private ClickableSpinBox bDefence;
        private ClickableSpinBox bArmor;
        private ClickableSpinBox bBaseHealth;
        private ClickableSpinBox bBaseInitiative;
        private ClickableSpinBox bBaseActionPts;
        private ClickableString sHealth;
        private ClickableString sDamage;
        private ClickableString sInitiative;
        private ClickableString sActionPts;
        private ClickableString sDodge;
        private ClickableString sRiposte;
        private ClickableString sContinue;

        private void Initialize()
        {
            sHeader = new ClickableString("Player 1 attributes:") { Locked = true };

            var sPointsTitle = new ClickableString("Points to Spend:") { Locked = true };
            var sStrengthTitle = new ClickableString("Strength:") { Locked = true };
            var sDexterityTitle = new ClickableString("Dexterity:") { Locked = true };
            var sOffenceTitle = new ClickableString("Offence:") { Locked = true };
            var sDefenceTitle = new ClickableString("Defence:") { Locked = true };
            var sArmorTitle = new ClickableString("Armor:") { Locked = true };
            var sBaseHealthTitle = new ClickableString("Base Max Health:") { Locked = true };
            var sBaseInitiativeTitle = new ClickableString("Base Initiative:") { Locked = true };
            var sBaseActionPtsTitle = new ClickableString("Base Max Action Pts:") { Locked = true };

            var sHealthTitle = new ClickableString("Max Health:") { Locked = true };
            var sDamageTitle = new ClickableString("Damage:") { Locked = true };
            var sInitiativeTitle = new ClickableString("Initiative:") { Locked = true };
            var sActionPtsTitle = new ClickableString("Max Action Pts:") { Locked = true };
            var sDodgeTitle = new ClickableString("Dodge Chance:") { Locked = true };
            var sRiposteTitle = new ClickableString("Riposte Dmg Mult:") { Locked = true };

            sContinue = new ClickableString("Continue");

            sPoints = new ClickableString(ptsToSpend.ToString()) { Locked = true };
            sHealth = new ClickableString(prototype.MaxHealth.ToString("F2")) { Locked = true };
            sDamage = new ClickableString(prototype.Damage.ToString("F2")) { Locked = true };
            sInitiative = new ClickableString(prototype.Initiative.ToString("F2")) { Locked = true };
            sActionPts = new ClickableString(prototype.MaxActionPoints.ToString()) { Locked = true };
            sDodge = new ClickableString(prototype.DodgeChance.ToString("F2") + " %") { Locked = true };
            sRiposte = new ClickableString(prototype.RiposteDamagePerc.ToString("F2") + " %") { Locked = true };

            bStrength = new ClickableSpinBox(1, 1000, prototype.Strength, ArenaDuelLogic.InitStrengthIncr);
            bDexterity = new ClickableSpinBox(1, 1000, prototype.Dexterity, ArenaDuelLogic.InitDexterityIncr);
            bOffence = new ClickableSpinBox(1, 1000, prototype.Offence, ArenaDuelLogic.InitOffenceIncr);
            bDefence = new ClickableSpinBox(1, 1000, prototype.Defence, ArenaDuelLogic.InitDefenceIncr);
            bArmor = new ClickableSpinBox(0, 1000, prototype.Armor, ArenaDuelLogic.InitArmorIncr);
            bBaseHealth = new ClickableSpinBox(10, 1000, prototype.MaxHealthBase, ArenaDuelLogic.InitMaxHealthIncr);
            bBaseInitiative = new ClickableSpinBox(1, 1000, prototype.InitiativeBase, ArenaDuelLogic.InitInitiativeIncr);
            bBaseActionPts = new ClickableSpinBox(0, 1000, prototype.MaxActionPointsBase, ArenaDuelLogic.InitMaxActionPointsIncr);

            sContinue.TextBrush = new SolidBrush(Color.SteelBlue);

            sHeader.Top = 30;

            sPointsTitle.Location = new Point(30, sHeader.Bottom + 15);
            sStrengthTitle.Location = new Point(sPointsTitle.Left, sPointsTitle.Bottom + 15);
            sDexterityTitle.Location = new Point(sPointsTitle.Left, sStrengthTitle.Bottom + 5);
            sOffenceTitle.Location = new Point(sPointsTitle.Left, sDexterityTitle.Bottom + 5);
            sDefenceTitle.Location = new Point(sPointsTitle.Left, sOffenceTitle.Bottom + 5);
            sArmorTitle.Location = new Point(sPointsTitle.Left, sDefenceTitle.Bottom + 5);
            sBaseHealthTitle.Location = new Point(sPointsTitle.Left, sArmorTitle.Bottom + 5);
            sBaseInitiativeTitle.Location = new Point(sPointsTitle.Left, sBaseHealthTitle.Bottom + 5);
            sBaseActionPtsTitle.Location = new Point(sPointsTitle.Left, sBaseInitiativeTitle.Bottom + 5);

            sHealthTitle.Location = new Point(sPointsTitle.Left, sBaseActionPtsTitle.Bottom + 15);
            sDamageTitle.Location = new Point(sPointsTitle.Left, sHealthTitle.Bottom + 5);
            sInitiativeTitle.Location = new Point(sPointsTitle.Left, sDamageTitle.Bottom + 5);
            sActionPtsTitle.Location = new Point(sPointsTitle.Left, sInitiativeTitle.Bottom + 5);
            sDodgeTitle.Location = new Point(sPointsTitle.Left, sActionPtsTitle.Bottom + 5);
            sRiposteTitle.Location = new Point(sPointsTitle.Left, sDodgeTitle.Bottom + 5);

            sPoints.Location = new Point(sPointsTitle.Right + 10, sPointsTitle.Top);
            bStrength.Location = new Point(sBaseActionPtsTitle.Right + 10, sStrengthTitle.Top);
            bDexterity.Location = new Point(bStrength.Left, sDexterityTitle.Top);
            bOffence.Location = new Point(bStrength.Left, sOffenceTitle.Top);
            bDefence.Location = new Point(bStrength.Left, sDefenceTitle.Top);
            bArmor.Location = new Point(bStrength.Left, sArmorTitle.Top);
            bBaseHealth.Location = new Point(bStrength.Left, sBaseHealthTitle.Top);
            bBaseInitiative.Location = new Point(bStrength.Left, sBaseInitiativeTitle.Top);
            bBaseActionPts.Location = new Point(bStrength.Left, sBaseActionPtsTitle.Top);

            sHealth.Location = new Point(sRiposteTitle.Right + 10, sHealthTitle.Top);
            sDamage.Location = new Point(sHealth.Left, sDamageTitle.Top);
            sInitiative.Location = new Point(sHealth.Left, sInitiativeTitle.Top);
            sActionPts.Location = new Point(sHealth.Left, sActionPtsTitle.Top);
            sDodge.Location = new Point(sHealth.Left, sDodgeTitle.Top);
            sRiposte.Location = new Point(sHealth.Left, sRiposteTitle.Top);

            sHeader.CentreX = (bStrength.Right + 30) >> 1;
            sContinue.Top = sRiposteTitle.Bottom + 15;
            sContinue.CentreX = sHeader.CentreX;

            bStrength.ValueChanged += this.Event_StrengthChanged;
            bDexterity.ValueChanged += this.Event_DexterityChanged;
            bOffence.ValueChanged += this.Event_OffenceChanged;
            bDefence.ValueChanged += this.Event_DefenceChanged;
            bArmor.ValueChanged += this.Event_ArmorChanged;
            bBaseHealth.ValueChanged += this.Event_HealthChanged;
            bBaseInitiative.ValueChanged += this.Event_InitiativeChanged;
            bBaseActionPts.ValueChanged += this.Event_ActionPtsChanged;

            Components.Add(sHeader);
            Components.Add(sPointsTitle);
            Components.Add(sStrengthTitle);
            Components.Add(sDexterityTitle);
            Components.Add(sOffenceTitle);
            Components.Add(sDefenceTitle);
            Components.Add(sArmorTitle);
            Components.Add(sBaseHealthTitle);
            Components.Add(sBaseInitiativeTitle);
            Components.Add(sBaseActionPtsTitle);
            Components.Add(sHealthTitle);
            Components.Add(sDamageTitle);
            Components.Add(sInitiativeTitle);
            Components.Add(sActionPtsTitle);
            Components.Add(sDodgeTitle);
            Components.Add(sRiposteTitle);
            Components.Add(sPoints);
            Components.Add(bStrength);
            Components.Add(bDexterity);
            Components.Add(bOffence);
            Components.Add(bDefence);
            Components.Add(bArmor);
            Components.Add(bBaseHealth);
            Components.Add(bBaseInitiative);
            Components.Add(bBaseActionPts);
            Components.Add(sHealth);
            Components.Add(sDamage);
            Components.Add(sInitiative);
            Components.Add(sActionPts);
            Components.Add(sDodge);
            Components.Add(sRiposte);
            Components.Add(sContinue);
        }

        #endregion
    }
}
