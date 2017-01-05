using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace LF_Zestaw4_z2.UI
{
    public class ClickableBar : ClickableRectangle
    {
        public Brush BackBrush { get; set; }
        public Brush ForeBrush { get; set; }

        private int margin;
        public int Margin
        {
            get { return margin; }
            set { margin = Limiter.AtLeast(0, value); }
        }

        private double percentage;
        public double Percentage
        {
            get { return percentage; }
            set { percentage = Limiter.Between(0, 1, value); }
        }

        public ClickableBar()
        {
            Height = 20;
            percentage = 1;
            BackBrush = new SolidBrush(Color.Black);
            ForeBrush = new SolidBrush(Color.DodgerBlue);
        }

        public override void Draw(Graphics g)
        {
            if (Visible)
            {
                g.FillRectangle(BackBrush, Left, Top, Width, Height);
                g.FillRectangle(ForeBrush, ForeRect());
            }
        }

        private Rectangle ForeRect()
        {
            int width = (int)(percentage * Width + 0.5);
            return new Rectangle(Left + Margin, Top + Margin, width - (Margin << 1), Height - (Margin << 1));
        }
    }
}
