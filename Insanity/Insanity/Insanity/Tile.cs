using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Insanity
{
    public class Tile
    {
        public bool Solid;
        public Sprite Sprite;
        public int X;
        public int Y;
        public int Index;

        public Tile(bool solid, Sprite sprite, int x, int y, int index)
        {
            Solid = solid;
            Sprite = sprite;
            X = x;
            Y = y;
            Index = index;
        }
    }
}
