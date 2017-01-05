using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace LF_Zestaw4_z2.UI
{
    public abstract class ClickableRectangle
    {
        public bool Visible { get; set; }
        public bool Locked { get; set; }

        private Size size;
        public Size Size
        {
            get { return size; }
            set { size = value; }
        }

        public int Width
        {
            get { return size.Width; }
            set { size.Width = value; }
        }

        public int Height
        {
            get { return size.Height; }
            set { size.Height = value; }
        }

        private Point location;
        public Point Location
        {
            get { return location; }
            set
            {
                Left = value.X;
                Top = value.Y;
            }
        }

        public virtual int Left
        {
            get { return location.X; }
            set { location.X = value; }
        }

        public int Right
        {
            get { return Left + Width; }
            set { Left = value - Width; }
        }

        public int CentreX
        {
            get { return Left + (Width >> 1); }
            set { Left = value - (Width >> 1); }
        }

        public virtual int Top
        {
            get { return location.Y; }
            set { location.Y = value; }
        }

        public int Bottom
        {
            get { return Top + Height; }
            set { Top = value - Height; }
        }

        public int CentreY
        {
            get { return Top + (Height >> 1); }
            set { Top = value - (Height >> 1); }
        }

        public event MouseEventHandler Click;

        public bool TryClick(int x, int y)
        {
            if (Visible && !Locked && x >= Left && x <= Right && y >= Top && y <= Bottom)
            {
                OnClick(new MouseEventArgs(MouseButtons.None, 0, x - Left, y - Top, 0));
                return true;
            }
            return false;
        }

        public abstract void Draw(Graphics g);

        protected ClickableRectangle()
        {
            Locked = false;
            Visible = true;
            location = Point.Empty;
            size = Size.Empty;
        }

        protected virtual void OnClick(MouseEventArgs e)
        {
            if (Click != null) Click(this, e);
        }
    }
}
