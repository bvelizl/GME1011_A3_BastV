using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GME1011A3
{
    internal class Jack_Frost : Minion
    {

        //Jack Frost use magic for his special attack.
        private int _magic;

        //Constructor for the mighty Jack Frost.
        public Jack_Frost(int health, int armour, int magic) : base(health, armour)
        {
            if (magic < 0 || magic > 12)
                magic = 12;
            _magic = magic;
        }

        //Jack Frost's basic attacks are low.
        public override int DealDamage()
        {
            Random rng = new Random();
            return rng.Next(1, 4);
        }

        //Jack Frost special. The idea is to use its magic to massively
        //increase his attack. There is a chance to do a special of 36 points of damage.
        public int JackBufula()
        {
            Console.WriteLine("BUFULA!");
            Random rng = new Random();
            return _magic * rng.Next(1,4);
        }

        public override string ToString()
        {
            return "Jack Frost[" + base.ToString() + "]";
        }
    }
}
