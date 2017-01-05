using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace LF_Zestaw4_z2.UI
{
    public class ClickableSpinBox : ClickableRectangle
    {
        public string Text { get; private set; }

        public Brush TextBrush { get; set; }
        public Brush BackBrush { get; set; }
        public Brush PlusBrush { get; set; }
        public Brush MinusBrush { get; set; }

        private Font font;
        public Font Font
        {
            get { return font; }
            set
            {
                font = value;
                MeasureTextHeight();
            }
        }

        private int minVal;
        public int MinValue
        {
            get { return minVal; }
            set
            {
                if (value > MaxValue)
                    throw new ArgumentException("Min value can't be greater than max value.");

                minVal = value;
                if (Value < minVal) Value = minVal;
            }
        }

        private int maxVal;
        public int MaxValue
        {
            get { return maxVal; }
            set
            {
                if (value < MinValue)
                    throw new ArgumentException("Max value can't be less than min value.");

                maxVal = value;
                if (Value > maxVal) Value = maxVal;
            }
        }

        private int val;
        public int Value
        {
            get { return val; }
            set
            {
                if (value < MinValue) value = MinValue;
                else if (value > MaxValue) value = MaxValue;

                if (val != value)
                {
                    bool incr = (value > val);
                    val = value;
                    Text = val.ToString();

                    if (ValueChanged != null)
                        ValueChanged(this, new GenericEventArgs<bool>(incr));
                }
            }
        }

        public int Step { get; set; }

        public event GenericEventHandler<bool> ValueChanged;

        public ClickableSpinBox()
            : this(0, 100, 0, 1)
        { }

        public ClickableSpinBox(int min, int max, int val, int step)
        {
            minVal = min;
            MaxValue = max;
            Step = step;
            if (this.val != val) Value = val;
            else Text = Value.ToString();

            TextBrush = new SolidBrush(Color.Black);
            BackBrush = new SolidBrush(Color.White);
            PlusBrush = new SolidBrush(Color.DarkGreen);
            MinusBrush = new SolidBrush(Color.OrangeRed);

            Font = Control.DefaultFont;
            MeasureTextHeight();
            Width = 55;
        }

        public void Incr()
        {
            Value += Step;
        }

        public void Decr()
        {
            Value -= Step;
        }

        public void MeasureTextHeight()
        {
            SizeF s = Graphics.FromHwnd(IntPtr.Zero).MeasureString(Text, font);
            Height = (int)Math.Ceiling(s.Height);
        }

        public override void Draw(Graphics g)
        {
            if (Visible)
            {
                g.FillRectangle(BackBrush, Left, Top, Width, Height);
                g.FillRectangle(MinusBrush, MinusBounds());
                g.FillRectangle(PlusBrush, PlusBoundsH());
                g.FillRectangle(PlusBrush, PlusBoundsV());
                g.DrawString(Text, font, TextBrush, TextBounds());
            }
        }

        protected override void OnClick(MouseEventArgs e)
        {
            if (MinusClickBounds().Contains(e.Location)) Decr();
            else if (PlusClickBounds().Contains(e.Location)) Incr();
            base.OnClick(e);
        }

        private Rectangle MinusClickBounds()
        {
            return new Rectangle(0, 0, Height, Height);
        }

        private Rectangle PlusClickBounds()
        {
            return new Rectangle(Width - Height, 0, Height, Height);
        }

        private RectangleF MinusBounds()
        {
            return new RectangleF(Left + 1.0f, Top + Height * 0.45f, Height - 2.0f, Height * 0.1f);
        }

        private RectangleF PlusBoundsH()
        {
            return new RectangleF(Left + Width - Height + 1.0f, Top + Height * 0.45f, Height - 2.0f, Height * 0.1f);
        }

        private RectangleF PlusBoundsV()
        {
            return new RectangleF(Left + Width - Height * 0.55f, Top + 1.0f, Height * 0.1f, Height - 2.0f);
        }

        private RectangleF TextBounds()
        {
            return new RectangleF(Left + Height + 1.0f, Top, Width - (Height << 1) - 2.0f, Height);
        }
    }
}
