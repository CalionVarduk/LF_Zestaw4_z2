using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace LF_Zestaw4_z2.UI
{
    public class ClickableString : ClickableRectangle
    {
        private string text;
        public string Text
        {
            get { return text; }
            set
            {
                text = (value != null) ? value : string.Empty;
                MeasureText();
            }
        }

        public Brush TextBrush { get; set; }
        public Brush BackBrush { get; set; }

        private Font font;
        public Font Font
        {
            get { return font; }
            set
            {
                font = value;
                MeasureText();
            }
        }

        public ClickableString()
            : this(string.Empty)
        { }

        public ClickableString(string text)
        {
            this.text = (text != null) ? text : string.Empty;
            TextBrush = new SolidBrush(Color.Black);
            BackBrush = new SolidBrush(Color.White);
            Font = Control.DefaultFont;
        }

        public void MeasureText()
        {
            SizeF s = Graphics.FromHwnd(IntPtr.Zero).MeasureString(text, font);
            Width = (int)Math.Ceiling(s.Width);
            Height = (int)Math.Ceiling(s.Height);
        }

        public override void Draw(Graphics g)
        {
            if (Visible)
            {
                g.FillRectangle(BackBrush, Left, Top, Width, Height);
                g.DrawString(text, font, TextBrush, new RectangleF((PointF)Location, (SizeF)Size));
            }
        }
    }
}
