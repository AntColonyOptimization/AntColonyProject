using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ants
{
    public class Coordinates
    {
        public int Height { get; set; }
        public int Width { get; set; }

        public Coordinates(int h, int w)
        {
            Height = h;
            Width = w;
        }

        public void SetOffset(Coordinates offset)
        {
            Width += offset.Width;
            Height += offset.Height;
        }

        public override bool Equals(object o)
        {
            if (o == null || o.GetType() != typeof(Coordinates))
                return false;
            Coordinates ca = (Coordinates)o;
            return ca.Width == this.Width && ca.Height == this.Height;
            //return ca._Path.Equals(this._Path);
        }


    }
}
