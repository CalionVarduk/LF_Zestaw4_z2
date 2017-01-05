using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace LF_Zestaw4_z2.UI
{
    public class PagePanel : Panel
    {
        private ClickablePage page;
        public ClickablePage Page
        {
            get { return page; }
            set
            {
                page = value;
                if (page != null) ClientSize = page.Size;
            }
        }

        public PagePanel()
        {
            DoubleBuffered = true;
            BackColor = Color.White;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (Parent != null) Parent.ClientSize = ClientSize;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (Page != null) Page.TryClick(e.X, e.Y);
            Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (Page != null)
            {
                e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bilinear;
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                Page.Draw(e.Graphics);
            }
        }
    }
}
