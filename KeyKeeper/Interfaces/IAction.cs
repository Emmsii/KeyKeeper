using KeyKeeper.Action;
using KeyKeeper.Entities;
using KeyKeeper.Helpers.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Interfaces
{
    public interface IAction : INbtSerializable
    {
        ActionResult Perform(Creature creature, GameResult result);
    }
}
