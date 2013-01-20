using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Insanity.Actors.InputBots;
using Microsoft.Xna.Framework;

namespace Insanity.Actors
{
    public class TrashCan : Enemy
    {
        public TrashCan(Vector2 position)
            : base(position, new Vector2(60, 240), "spriteSheets/trashcan visible spritesheet", "spriteSheets/trashcan harmful spritesheet", 
            new TrashInput(), Player.perfectlySane, Player.inanimateEnemyThreshold)
        {
        }
        
        public TrashCan(List<string> args)
            : this(new Vector2(float.Parse(args[0]), float.Parse(args[1])))
        {
        }
    }
}
