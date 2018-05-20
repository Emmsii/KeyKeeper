using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyKeeper.Interfaces;

namespace KeyKeeper.Entities
{
    public class Monster : Creature
    {
        public Monster(Species species) : base(species)
        {

        }

        protected override IAction OnGetAction()
        {
            throw new NotImplementedException();
        }
    }
}
