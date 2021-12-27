using Microsoft.Xna.Framework;

namespace Match3MG
{
    class Bomb
    {
        public bool IsReplaced { get; set; }
        public Point point { get; set; }
        public Bomb()
        {
            IsReplaced = false;
            point = new Point();
        }
    }
}
