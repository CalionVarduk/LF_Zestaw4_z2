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
    public class ClickableDice : ClickableRectangle
    {
        private static readonly PointF[][] dotLocations;
        static ClickableDice()
        {
            dotLocations = new PointF[6][];
            dotLocations[0] = new PointF[] { new PointF(0.5f, 0.5f) };
            dotLocations[1] = new PointF[] { new PointF(0.2f, 0.2f), new PointF(0.8f, 0.8f) };
            dotLocations[2] = new PointF[] { new PointF(0.2f, 0.2f), new PointF(0.5f, 0.5f), new PointF(0.8f, 0.8f) };
            dotLocations[3] = new PointF[] { new PointF(0.2f, 0.2f), new PointF(0.8f, 0.2f), new PointF(0.2f, 0.8f), new PointF(0.8f, 0.8f) };
            dotLocations[4] = new PointF[] { new PointF(0.2f, 0.2f), new PointF(0.8f, 0.2f), new PointF(0.2f, 0.8f), new PointF(0.8f, 0.8f), new PointF(0.5f, 0.5f) };
            dotLocations[5] = new PointF[] { new PointF(0.2f, 0.2f), new PointF(0.8f, 0.2f), new PointF(0.2f, 0.5f), new PointF(0.8f, 0.5f), new PointF(0.2f, 0.8f), new PointF(0.8f, 0.8f) };
        }

        private Dice dice;
        public Dice Dice
        {
            get { return dice; }
            set
            {
                dice = value;
                MeasureDice();
            }
        }

        private int diceSize;
        public int DiceSize
        {
            get { return diceSize; }
            set
            {
                diceSize = value;
                MeasureDice();
            }
        }

        private int diceOffset;
        public int DiceOffset
        {
            get { return diceOffset; }
            set
            {
                diceOffset = value;
                MeasureDice();
            }
        }

        public int DotSize { get; set; }

        public Brush DiceBrush { get; set; }
        public Brush LockedDiceBrush { get; set; }
        public Brush DotBrush { get; set; }

        public ClickableDice(Dice dice)
        {
            this.dice = dice;
            diceSize = 30;
            DiceOffset = 5;
            DotSize = 7;
            DiceBrush = new SolidBrush(Color.NavajoWhite);
            LockedDiceBrush = new SolidBrush(Color.Gray);
            DotBrush = new SolidBrush(Color.DodgerBlue);
        }

        public void MeasureDice()
        {
            Height = DiceSize;
            Width = Dice.Count * DiceSize + (Dice.Count - 1) * DiceOffset;
        }
        public override void Draw(Graphics g)
        {
            if (Visible)
            {
                Rectangle rDice = new Rectangle(Left, Top, DiceSize, DiceSize);

                for (int i = 0; i < Dice.Count; ++i)
                {
                    g.FillRectangle(Dice.IsLocked(i) ? LockedDiceBrush : DiceBrush, rDice);
                    DrawDots(g, rDice, Dice.Value(i));
                    rDice.X += DiceSize + DiceOffset;
                }
            }
        }

        protected override void OnClick(MouseEventArgs e)
        {
            if (e.Y >= 0 && e.Y <= DiceSize)
            {
                int index = e.X / (DiceSize + DiceOffset);
                if (e.X <= (index + 1) * (DiceSize + DiceOffset) - DiceOffset)
                {
                    if (dice.IsLocked(index)) dice.Unlock(index);
                    else dice.Lock(index);
                }
            }
            base.OnClick(e);
        }

        private void DrawDots(Graphics g, Rectangle r, int count)
        {
            PointF[] dots = dotLocations[count - 1];

            for (int i = 0; i < dots.Length; ++i)
            {
                PointF loc = new PointF(r.X + dots[i].X * DiceSize, r.Y + dots[i].Y * DiceSize);
                loc.X -= (DotSize >> 1);
                loc.Y -= (DotSize >> 1);
                g.FillEllipse(DotBrush, loc.X, loc.Y, (float)DotSize, (float)DotSize);
            }
        }
    }
}
