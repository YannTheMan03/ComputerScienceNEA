using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iteration1
{
    internal class powerUp : Entity
    {
        public int level { get; private set; }
        public int boost { get; private set; }
        public int identifier { get; private set; }

        public powerUp(Bitmap spriteImage, int x, int y, int plevel, int pIndentifier)
             : base(spriteImage, x, y)
        {
            level = plevel;
            identifier = pIndentifier;
            boost = 1;  
        }

        public void Draw(Graphics g)
        {
            g.DrawImage(SpriteImage, PositionX, PositionY);
        }
    }
}
