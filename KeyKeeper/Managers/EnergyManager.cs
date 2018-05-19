using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Managers
{
    public class EnergyManager
    {
        public static readonly int MIN_SPEED = 0;
        public static readonly int NORMAL_SPEED = 3;
        public static readonly int MAX_SPEED = 5;

        public static readonly int ACTION_COST = 12;

        public static readonly int[] GAINS = { 2, 3, 4, 6, 9, 12 };

        public int Energy { get; private set; }

        public bool CanTakeTurn => Energy >= ACTION_COST;

        public void Spend()
        {
            if (Energy >= ACTION_COST) Energy = Energy % ACTION_COST;
        }

        public bool GainEnergy(int speed)
        {
            Energy += GAINS[speed];
            return CanTakeTurn;
        }
                
    }
}
