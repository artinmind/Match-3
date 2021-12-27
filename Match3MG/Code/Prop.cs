using Microsoft.Xna.Framework;

namespace Match3MG
{
    class Prop
    {
        public Point point;
        public bool ver;
        public Prop(Point point, bool ver)
        {
            this.point = point;
            this.ver = ver;
        }
    }
}
