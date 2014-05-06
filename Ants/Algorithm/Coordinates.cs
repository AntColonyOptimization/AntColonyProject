using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ants
{
    public class Coordinates
    {
        private int height;
        private int width;
        public int Height
        {
            get
            {
                return this.height;
            }
            set
            {
                this.height = value;
            }
        }
        public int Width
        {
            get
            {
                return this.width;
            }
            set
            {
                this.width = value;
            }
        }

        public Coordinates(int h, int w)
        {
            height = h;
            width = w;
        }

        public void SetOffset(Coordinates offset)
        {
            width += offset.Width;
            height += offset.Height;
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
