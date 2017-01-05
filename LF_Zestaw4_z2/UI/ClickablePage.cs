using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace LF_Zestaw4_z2.UI
{
    public class ClickablePage : ClickableRectangle
    {
        public List<ClickableRectangle> Components { get; private set; }

        public int ComponentsLeft
        {
            get
            {
                int l = 0;
                foreach (var c in Components)
                    if (c.Left < l) l = c.Left;
                return l;
            }
        }

        public int ComponentsRight
        {
            get
            {
                int r = 0;
                foreach (var c in Components)
                    if (c.Right > r) r = c.Right;
                return r;
            }
        }

        public int ComponentsTop
        {
            get
            {
                int t = 0;
                foreach (var c in Components)
                    if (c.Top < t) t = c.Top;
                return t;
            }
        }

        public int ComponentsBottom
        {
            get
            {
                int b = 0;
                foreach (var c in Components)
                    if (c.Bottom > b) b = c.Bottom;
                return b;
            }
        }

        public override int Left
        {
            get { return base.Left; }
            set
            {
                int dx = value - Left;
                base.Left += dx;

                int count = Components.Count;
                for (int i = 0; i < count; ++i)
                    Components[i].Left += dx;
            }
        }

        public override int Top
        {
            get { return base.Top; }
            set
            {
                int dy = value - Top;
                base.Top += dy;

                int count = Components.Count;
                for (int i = 0; i < count; ++i)
                    Components[i].Top += dy;
            }
        }

        public ClickablePage()
        {
            Components = new List<ClickableRectangle>();
        }

        public override void Draw(Graphics g)
        {
            if (Visible)
                foreach (var c in Components)
                    c.Draw(g);
        }

        protected override void OnClick(MouseEventArgs e)
        {
            int count = Components.Count;
            for(int i = 0; i < count; ++i)
                if (Components[i].TryClick(e.X, e.Y)) break;

            base.OnClick(e);
        }
    }
}
