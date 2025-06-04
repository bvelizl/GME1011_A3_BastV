using System.Collections.Generic;

namespace GME1011A3
{
    internal class Program
    {
        static void Main(string[] args)
        {

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


            //List that contain Goblins and Skellies as foes of the hero.
            List<Minion> baddies = new List<Minion>();


            //Now each baddie have 50% chances to be a Goblin, or a Skellie.
            for (int i = 0; i < numBaddies; i++)
            {
                int baddieRng = rng.Next(0,2);

                if(baddieRng == 0)
                    baddies.Add(new Goblin(rng.Next(30, 36), rng.Next(1, 5), rng.Next(1, 10)));
                else
                    baddies.Add(new Skellie(rng.Next(25, 31), 0));
            
            }

            //this should work even after you make the changes above
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

                //hero deals damage first. CHECK IF THIS IS WORKING. (where is the moment that the damage is asked again?)
                Console.WriteLine(hero.GetName() + " is attacking enemy #" + (indexOfEnemy+1) + " of " + numBaddies + ". Eek, it's a " + baddies[indexOfEnemy].GetType().Name);

                int specialAttack = rng.Next(0,3);
                int heroDamage = 0;

                if (hero.GetStrength() > 0 && specialAttack == 2)
                    heroDamage = hero.Berserk();
                else
                    heroDamage = hero.DealDamage();

                //How much damage?
                Console.WriteLine("Hero deals " + heroDamage + " heroic damage."); 
                baddies[indexOfEnemy].TakeDamage(heroDamage); //baddie takes the damage




                //TODO: The hero doesn't ever use their special attack - but they should. Change the above to 
                //have a 33% chance that the hero uses their special, and 67% that they use their regular attack.
                //If the hero doesn't have enough special power to use their special attack, they do their regular 
                //attack instead - but make a note of it in the output. There's no way for the hero to get more special
                //power points, but if you want to craft a way for that to happen, that's fine.




                //NOTE to coders - armour affects how much damage goblins take, and skellies take
                //half damage - remember that when reviewing the output

                //did we vanquish the baddie we were battling?
                if (baddies[indexOfEnemy].isDead())
                {
                    numAliveBaddies--; //one less baddie to worry about.
                    Console.WriteLine("Enemy #" + (indexOfEnemy+1) + " has been dispatched to void.");
                }
                else //baddie survived, now attacks the hero
                {
                    //Code to make the minion do their special attack. 33% of chances depending on the type of the enemy.
                    specialAttack = rng.Next(0,3);
                    int baddieDamage = 0;

                    if (specialAttack == 2 && baddies[indexOfEnemy] is Goblin)
                        baddieDamage = ((Goblin)baddies[indexOfEnemy]).GoblinBite();
                    else if (specialAttack == 2 && baddies[indexOfEnemy] is Skellie)
                        baddieDamage = ((Skellie)baddies[indexOfEnemy]).SkellieRattle();
                    else
                        baddieDamage = baddies[indexOfEnemy].DealDamage();  //how much damage?
                    
                    Console.WriteLine("Enemy #" + (indexOfEnemy+1) + " deals " + baddieDamage + " damage!");
                    hero.TakeDamage(baddieDamage); //hero takes damage




                    //TODO: The baddie doesn't ever use their special attack - but they should. Change the above to 
                    //have a 33% chance that the baddie uses their special, and 67% that they use their regular attack.
                    



                    //let's look in on our hero.
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