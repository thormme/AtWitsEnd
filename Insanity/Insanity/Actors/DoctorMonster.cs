﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Insanity.Actors.InputBots;

namespace Insanity.Actors
{
    public class DoctorMonster : Enemy
    {
        public DoctorMonster(Vector2 position)
            : base(position, new Vector2(40, 180), "spriteSheets/doctor sane spritesheet", "spriteSheets/doctor insane spritesheet", 
            new DoctorInput(), Player.perfectlySane, Player.humanEnemyThreshold)
        {
        }
        
        public DoctorMonster(List<string> args)
            : this(new Vector2(float.Parse(args[0]), float.Parse(args[1])))
        {
        }
    }
}
