using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LF_Zestaw4_z2
{
    public class GenericEventArgs<T> : EventArgs
    {
        public T Item { get; private set; }

        public GenericEventArgs(T item)
        {
            Item = item;
        }
    }

    public delegate void GenericEventHandler<T>(object sender, GenericEventArgs<T> e);
}
