using fNbt;
using KeyKeeper.Content;
using KeyKeeper.Entities;
using KeyKeeper.Graphics;
using KeyKeeper.Helpers.Game;
using KeyKeeper.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Action
{
    public class FireAction : IAction
    {
        private int _x;
        private int _y;

        public FireAction(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public ActionResult Perform(Creature creature, GameResult result)
        {
            GameEffect fireEffect = new FireEffect(_x, _y, _x + 5, _y + 5, Assets.RootBeer, 1f, 2);
            GameEvent newEvent = new GameEvent(fireEffect, _x, _y);
            result.AddEvent(newEvent);
            return ActionResult.SUCCESS;            
        }

        public void SetDataFromTag(NbtCompound tag)
        {
            throw new NotImplementedException();
        }

        public NbtTag WriteDataToTag(string name)
        {
            throw new NotImplementedException();
        }
    }
}
