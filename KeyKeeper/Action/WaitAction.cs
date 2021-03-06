﻿using fNbt;
using KeyKeeper.Entities;
using KeyKeeper.Helpers.Game;
using KeyKeeper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Action
{
    public sealed class WaitAction : IAction
    {
        public WaitAction()
        {
        }

        public ActionResult Perform(Creature creature, GameResult result)
        {
            return ActionResult.SUCCESS;
        }

        public void SetDataFromTag(NbtCompound tag)
        {
            // nothing to set
        }

        public NbtTag WriteDataToTag(string name)
        {
            return new NbtByte(name);
        }
    }
}
