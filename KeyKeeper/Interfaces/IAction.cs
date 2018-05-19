using KeyKeeper.Action;
using KeyKeeper.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Interfaces
{
    public interface IAction
    {
        ActionResult Perform(Creature creature);
    }
}
