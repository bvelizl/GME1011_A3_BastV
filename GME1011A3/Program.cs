using System.Collections.Generic;

namespace GME1011A3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
             * GME1011_A3_BastV
             */


            //Epic battle goes here :)
            Random rng = new Random();

            //Prompting the user for the hero's values.
            Console.Write("How much health do you want for your hero? (1 - 100): ");
            int health = int.Parse(Console.ReadLine());

            Console.Write("What is the name of your hero?: ");
            string name = Console.ReadLine();

            Console.Write("What about the strength of your hero? (1 - 10): ");
            int strength = int.Parse(Console.ReadLine());

            //Empty line just to make it easier to read the values of our hero.
            Console.WriteLine();

            //Printing the hero that the user created.
            Fighter hero = new Fighter(health, name, strength);
            Console.WriteLine("Here is our heroic hero: " + hero + "\n\n");

            //Prompting the user to decide how many enemies will fight the hero.
            Console.Write("How many enemies you want your hero to fight against? (1 - 5): ");

            int numBaddies = int.Parse(Console.ReadLine());
            int numAliveBaddies = numBaddies;


            //List that contain Goblins, Skellies, and Jack Frosts as foes of the hero.
            List<Minion> baddies = new List<Minion>();


            //Now each baddie have the same chances to be a Goblin, a Skellie, or Jack Frost.
            for (int i = 0; i < numBaddies; i++)
            {
                int baddieRng = rng.Next(0,3);

                if(baddieRng == 0)
                    baddies.Add(new Goblin(rng.Next(30, 36), rng.Next(1, 5), rng.Next(1, 10)));
                if (baddieRng == 1)
                    baddies.Add(new Skellie(rng.Next(25, 31), 0));
                if (baddieRng == 2)
                    baddies.Add(new Jack_Frost(rng.Next(30, 41), rng.Next(5, 10), 12));
            
            }

            //Enemies printed to see its class, and values.
            Console.WriteLine("Here are the baddies!!!");
            for(int i = 0; i < baddies.Count; i++)
            {
                Console.WriteLine(baddies[i]);
            }
            Console.WriteLine("\n\n");
            Console.WriteLine("Let the EPIC battle begin!!!");
            Console.WriteLine("----------------------------");


            //loop runs as long as there are baddies still alive and the hero is still alive!!
            while (numAliveBaddies > 0 && !hero.isDead())
            {
                //figure out which enemy we are going to battle - the first one that isn't dead
                int indexOfEnemy = 0;
                while (baddies[indexOfEnemy].isDead())
                {
                    indexOfEnemy++;
                }

                //hero deals damage first. 33% of chances to do the special attack (Berserk).
                Console.WriteLine(hero.GetName() + " is attacking enemy #" + (indexOfEnemy+1) + " of " + numBaddies + ". Eek, it's a " + baddies[indexOfEnemy].GetType().Name);

                int specialAttack = rng.Next(0,3);
                int heroDamage = 0;

                if (hero.GetStrength() > 0 && specialAttack == 2)
                    heroDamage = hero.Berserk();
                else
                    heroDamage = hero.DealDamage();

                //How much damage?
                //Now we can also see the actual health of the current enemy (it depends on its armour to see how much damage the hero causes).
                Console.WriteLine("Hero deals " + heroDamage + " heroic damage.");
                baddies[indexOfEnemy].TakeDamage(heroDamage); //baddie takes the damage
                Console.WriteLine("Enemy #" + (indexOfEnemy + 1) + " current health is: " + baddies[indexOfEnemy].GetHealth() + "\n");


                //did we vanquish the baddie we were battling?
                if (baddies[indexOfEnemy].isDead())
                {
                    numAliveBaddies--; //one less baddie to worry about.
                    Console.WriteLine("Enemy #" + (indexOfEnemy+1) + " has been dispatched to void.");
                }
                else //baddie survived, now attacks the hero
                {
                    //Code to make the enemy do their special attack. 33% of chances to do it.
                    specialAttack = rng.Next(0,3);
                    int baddieDamage = 0;

                    if (specialAttack == 2 && baddies[indexOfEnemy] is Goblin)
                    {
                        baddieDamage = ((Goblin)baddies[indexOfEnemy]).GoblinBite();
                        hero.TakeDamage(baddieDamage);
                    }
                    else if (specialAttack == 2 && baddies[indexOfEnemy] is Skellie)
                    {
                        baddieDamage = ((Skellie)baddies[indexOfEnemy]).SkellieRattle();
                        hero.TakeDamage(baddieDamage);
                    }
                    else if (specialAttack == 2 && baddies[indexOfEnemy] is Jack_Frost)
                    {
                        baddieDamage = ((Jack_Frost)baddies[indexOfEnemy]).JackBufula();
                        hero.TakeDamage(baddieDamage);
                    }
                    //if not, 67% of chances that the enemy do a regular attack.    
                    else
                    {
                        baddieDamage = baddies[indexOfEnemy].DealDamage();
                        hero.TakeDamage(baddieDamage);
                    }
                    
                    Console.WriteLine("Enemy #" + (indexOfEnemy+1) + " deals " + baddieDamage + " damage!");



                    //let's check our hero.
                    Console.WriteLine(hero.GetName() + " has " + hero.GetHealth() + " health remaining.");
                    if (hero.isDead()) //did the hero die
                    {
                        Console.WriteLine(hero.GetName() + " has died. All hope is lost.");
                    }

                }
                Console.WriteLine("-----------------------------------------");
            }
            //if we made it this far, the hero is victorious! (that's what the message says.
            if(!hero.isDead())
                Console.WriteLine("\nAll enemies have been dispatched!! " + hero.GetName() + " is victorious!");
        }

    }
}