using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiLow
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Name A : ");
            Player player1 = new Player(Console.ReadLine());
            Console.Write("Name B : ");
            Player player2 = new Player(Console.ReadLine());

            List<Cards> allCard = new List<Cards>();

            for (int i = 1; i <= 13; i++)
            {
                for (int j = 1; j <= 4; j++)
                {
                    allCard.Add(new Cards(i, j));
                } 
            }
            allCard = ShuffleList(allCard);

            for (int i = 0; i < 52; i += 2)
            {
                player1.addCard(allCard[i]);
                player2.addCard(allCard[i + 1]);
            }
            do
            {
                if (player1.getNumdeck() == 26)
                {
                    Console.Write("\nStart Game, Each player has " + player1.getNumdeck() + " Cards.\n");
                }
                else if (player1.getNumdeck() == 1)
                {
                    Console.Write("\nNext Round, Each player has " + player1.getNumdeck() + " Card.\n");
                }
                else Console.Write("\nNext Round, Each player has " + player1.getNumdeck() + " Cards.\n");

                Cards c1 = player1.Draw();
                Console.Write(player1.getName() + " Draw : " + c1.nameRank() + c1.nameSuit() + "\n");
                Cards c2 = player2.Draw();
                Console.Write(player2.getName() + " Draw : " + c2.nameRank() + c2.nameSuit() + "\n");
                if (c1.getRank() < c2.getRank())
                {
                    Console.WriteLine(player1.getName() + " Win! and get 2 cards.");
                    player1.addPile(c1);
                    player1.addPile(c2);
                }
                else if (c1.getRank() > c2.getRank())
                {
                    Console.WriteLine(player2.getName() + " Win! and get 2 cards.");
                    player2.addPile(c1);
                    player2.addPile(c2);
                }
                else
                {

                    int i, round;
                    List<Cards> tempc1 = new List<Cards>();
                    List<Cards> tempc2 = new List<Cards>();

                    tempc1.Add(c1);
                    tempc2.Add(c2);

                    if (player1.getNumdeck() < c1.getRank())
                    {
                        round = player1.getNumdeck();
                    }
                    else round = c1.getRank();
                    Console.WriteLine("Card is equal at rank " + Convert.ToString(c1.getRank()) + "\nEach player draw " + Convert.ToString(round) + " Cards");
                    for (i = 0; i < round; i++)
                    {
                        tempc1.Add(player1.Draw());
                        tempc2.Add(player2.Draw());
                    }

                    Console.WriteLine(player1.getName() + " draw last card is " + tempc1[i - 1].nameRank() + tempc1[i - 1].nameSuit());
                    Console.WriteLine(player2.getName() + " draw last card is " + tempc2[i - 1].nameRank() + tempc2[i - 1].nameSuit());

                    if (tempc1[i - 1].getRank() < tempc2[i - 1].getRank())
                    {
                        Console.WriteLine(player1.getName() + " Win! and get " + Convert.ToString(round * 2) + "+2 Cards");
                        for (i = 0; i < round + 1; i++)
                        {
                            player1.addPile(tempc1[i]);
                            player1.addPile(tempc2[i]);
                        }
                    }
                    else if (tempc1[i - 1].getRank() > tempc2[i - 1].getRank())
                    {
                        Console.WriteLine(player2.getName() + " Win! and get " + Convert.ToString(round * 2) + "+2 Cards");
                        for (i = 0; i < round + 1; i++)
                        {
                            player2.addPile(tempc1[i]);
                            player2.addPile(tempc2[i]);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Equal again! Return all cards to your deck, then shuffle it.");
                        for (i = 0; i < round + 1; i++)
                        {
                            player1.addCard(tempc1[i]);
                            player2.addCard(tempc2[i]);
                        }
                        player1.deckShuff();
                        player2.deckShuff();
                    }
                }
                Console.WriteLine(player1.getName() + " has " + player1.getNumPile() + " Cards in Pile");
                Console.WriteLine(player2.getName() + " has " + player2.getNumPile() + " Cards in Pile");
                Console.ReadKey();
            } while (player1.getNumdeck() > 0);
            if (player1.getNumPile() > player2.getNumPile())
            {
                Console.WriteLine("\nThe Winner is " + player1.getName());
            }
            else if (player1.getNumPile() < player2.getNumPile())
            {
                Console.WriteLine("\nThe Winner is " + player2.getName());
            }
            else
            {
                Console.WriteLine("\nPlease New Game Again.");
            }
            Console.ReadKey();
        }



        static List<Cards> ShuffleList(List<Cards> inputList)
        {
            List<Cards> randomList = new List<Cards>();

            Random r = new Random();
            int randomIndex = 0;
            while (inputList.Count > 0)
            {
                randomIndex = r.Next(0, inputList.Count);
                randomList.Add(inputList[randomIndex]);
                inputList.RemoveAt(randomIndex);
            }

            return randomList;
        }
    }
}
