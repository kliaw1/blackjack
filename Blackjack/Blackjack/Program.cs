/*NAME: Karen Liaw
 * Course Number: CMIS 1301 CRN: 22851 SEMESTER: Spring 2020
 * REQUIREMENTS MET:
 * 
 * Provides the same console interface as shown in the HW4 demo video
 * Uses System.Enviornment.Exit() when player inputs q
 * Random is used to shuffle the standard deck and the first randomly shuffled card is added to the dealer and player's hands as necessary
 * Uses the shuffling and printing of a deck previously made in Homework 3
 * Created a class called Hand, keeps a list of all cards dealt to the player and dealer during a game
 * A maximum of 11 deals is possible before the game automatically ends and the player is asked to try again or to quit.
 * 
 * The game iteratively asks the player the course of action they would like to take:
 * This being to either hit (draw a card), pass (hold/end their turn), restart the game, or quit. 
 * 
 * Strives to follow the commenting template provided
 *  
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    public enum Suit
    {
        Clubs,
        Diamonds,
        Hearts,
        Spades
    }
    public enum Rank
    {
        Ace = 1, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King
    }
    public class Card
    {
        public Rank rank;
        public Suit suit;
        public bool isUsed = false;

        //constructor for the Card class
        public Card(Suit suit, Rank rank)
        {
            this.suit = suit;
            this.rank = rank;
        }
    }
    public class Deck
    {
        /* FUNCTION: Shuffle()
         * PARAMS: None
         * RETURNS: None
         * CLASS SCOPE EFFECTS: isUsed
         * CALLED FUNCTIONS: PrintDeck()
         * 
         * DESCRIPTION: Initializes a complete, ascending ordered deck of cards of every suit and rank called masterOrderedDeck. Shuffling is done with a nested for loop (outer loop handles the rows, inner loop handles the columns) where in each iteration, a random row between 0 and 3 and random column between 0 and 12 is chosen.
         * The random row and random column are used to pick a random card from the array masterOrderedDeck. The card value isUsed is checked, and if isUsed is false, the card is inserted into the array shuffledDeck. 
         * Checks if the card value isUsed is true, the current column value is decremented so that the for loop does not move to the next index of shuffledDeck and instead, repeats until a card whose isUsed value returns false (aka has not been inserted into the deck yet).
         * Shuffle() calls the PrintDeck() method once shuffling is complete
         */
        //Initialize cards into ascending order by suit then rank in the array masterOrderedDeck
        public static Card[,] masterOrderedDeck = new Card[4, 13] { {new Card(Suit.Clubs, Rank.Ace), new Card(Suit.Clubs, Rank.Two), new Card(Suit.Clubs, Rank.Three), new Card(Suit.Clubs, Rank.Four), new Card(Suit.Clubs, Rank.Five), new Card(Suit.Clubs, Rank.Six), new Card (Suit.Clubs, Rank.Seven), new Card(Suit.Clubs, Rank.Eight), new Card(Suit.Clubs, Rank.Nine), new Card(Suit.Clubs, Rank.Ten), new Card(Suit.Clubs, Rank.Jack), new Card(Suit.Clubs, Rank.Queen), new Card(Suit.Clubs, Rank.King)},
                                                          {new Card(Suit.Spades, Rank.Ace), new Card(Suit.Spades, Rank.Two), new Card(Suit.Spades, Rank.Three), new Card(Suit.Spades, Rank.Four), new Card(Suit.Spades, Rank.Five), new Card(Suit.Spades, Rank.Six), new Card (Suit.Spades, Rank.Seven), new Card(Suit.Spades, Rank.Eight), new Card(Suit.Spades, Rank.Nine), new Card(Suit.Spades, Rank.Ten), new Card(Suit.Spades, Rank.Jack), new Card(Suit.Spades, Rank.Queen), new Card(Suit.Spades, Rank.King)},
                                                          {new Card(Suit.Hearts, Rank.Ace), new Card(Suit.Hearts, Rank.Two), new Card(Suit.Hearts, Rank.Three), new Card(Suit.Hearts, Rank.Four), new Card(Suit.Hearts, Rank.Five), new Card(Suit.Hearts, Rank.Six), new Card (Suit.Hearts, Rank.Seven), new Card(Suit.Hearts, Rank.Eight), new Card(Suit.Hearts, Rank.Nine), new Card(Suit.Hearts, Rank.Ten), new Card(Suit.Hearts, Rank.Jack), new Card(Suit.Hearts, Rank.Queen), new Card(Suit.Hearts, Rank.King)},
                                                          {new Card(Suit.Diamonds, Rank.Ace), new Card(Suit.Diamonds, Rank.Two), new Card(Suit.Diamonds, Rank.Three), new Card(Suit.Diamonds, Rank.Four), new Card(Suit.Diamonds, Rank.Five), new Card(Suit.Diamonds, Rank.Six), new Card (Suit.Diamonds, Rank.Seven), new Card(Suit.Diamonds, Rank.Eight), new Card(Suit.Diamonds, Rank.Nine), new Card(Suit.Diamonds, Rank.Ten), new Card(Suit.Diamonds, Rank.Jack), new Card(Suit.Diamonds, Rank.Queen), new Card(Suit.Diamonds, Rank.King)},
                                                         };
        //Declaration for the array shuffledDeck
        public static Card[,] shuffledDeck = new Card[4, 13];

        //Declaration of the shuffledCards array. This is a one dimensional array, unlike the shuffledDeck/masterOrderedDeck and is used as an easier access to the shuffled deck during actual game play
        public static Card[] shuffledCards = new Card[52];

        public static void Shuffle()
        {
            

            //Declaration for the random class variable
            Random rand = new Random();

            //declaration of deckRow and deckColumn, these two will be used to hold the indeces for the random card being looked at in masterOrderedDeck
            int deckRow;
            int deckColumn;

            //Iterate through the shuffledDeck indexes to insert cards from the masterOrderedDeck randomly
            for (int row = 0; row < 4; row++)
            {
                for (int column = 0; column < 13; column++)
                {
                    deckRow = rand.Next(4); //get a random row number between 0 and 3, this will represent Suit
                    deckColumn = rand.Next(13); //get a random column number between 0 and 12, this will represent Rank

                    //check if card randomly chosen was not already put into deck
                    if ((masterOrderedDeck[deckRow, deckColumn]).isUsed == false)
                    {
                        //insert the card into the shuffled deck and change the isUsed value of that card in the master deck to true to indicate it has already been inserted
                        shuffledDeck[row, column] = masterOrderedDeck[deckRow, deckColumn];
                        masterOrderedDeck[deckRow, deckColumn].isUsed = true;
                    }
                    else
                    {
                        //subtract the column number so that when the loop increments, the loop just repeats the current index until a card that hasn't been used already is found
                        column--;
                    }
                }
            }
            //print the newly shuffled deck
            PrintDeck(shuffledDeck);
        }
        /*FUNCTION: PrintDeck()
         * PARAMS: Card[,]deck (a 2D array of values from the class Card)
         * RETURNS: None
         * CLASS SCOPE EFFECT: None (doesn't change any values of other variables)
         * CALLED FUNCTION: None
         * 
         * Description: Print the shuffled cards in the shuffledDeck array from Shuffle() using a for loop. On printing, the masterOrderedDeck Card elements isUsed value is reset to false to prepare them for any more possible shuffles.
         * The shuffledDeck Card elements are also copied over to the shuffledCards one dimensional array (again, this is done so that it is easier to access the shuffled cards during gameplay, by using a single index instead of having to pass two indexes)
         */
        public static void PrintDeck(Card[,] deck)
        {
            //declare and initialize a count variable to track the card number
            int cardCount = 0;
            Console.WriteLine("The current shuffled deck.");
            Console.WriteLine("-------------------------------------");
            for (int row = 0; row < 4; row++)
            {
                for (int column = 0; column < 13; column++)
                {
                    masterOrderedDeck[row, column].isUsed = false; //reset the masterOrderedDeck Card elements back to false

                    //if the card rank is an Ace, Jack, Queen, or King, display the string name for them
                    if( (deck[row, column].rank == Rank.Queen) || (deck[row, column].rank == Rank.King) || (deck[row, column].rank == Rank.Jack) )
                    {
                        Console.WriteLine(cardCount + "| [" + row + "]" + "[" + column + "]: \tSuit: " + deck[row, column].suit + ",\tRank: " + deck[row, column].rank + " \tValue: 10");
                    }
                    else if((deck[row, column].rank == Rank.Ace))
                    {
                        Console.WriteLine(cardCount + "| [" + row + "]" + "[" + column + "]: \tSuit: " + deck[row, column].suit + ",\tRank: " + deck[row, column].rank + " \tValue: 1");
                    }
                    //otherwise, display the numberic value of the card
                    else
                    {
                        Console.WriteLine(cardCount + "| [" + row + "]" + "[" + column + "]: \tSuit: " + deck[row, column].suit + ",\tRank: " + deck[row, column].rank  + " \tValue: " + (int)deck[row, column].rank);
                    }
                    shuffledCards[cardCount] = deck[row, column]; //copy the shuffled cards to the shuffledCards one dimensional array
                    cardCount++;
                }
            }
            Console.WriteLine("-------------------------------------");
        }
    }

    public class Hand
    {
        string ownerType;
        int score;
        public List<Card> dealtHand; //used a list to handle dealt cards to make it easier to add/remove cards from the List as necessary without a need to match the first available null index. the deal variable ensures the List shouldn't exceed 11 total elements

        //Constructor for the Hand class, when called, initialize the owner type and dealtHaand List and set the score to 0
        public Hand(string owner) 
        {
            ownerType = owner;
            score = 0;
            dealtHand = new List <Card>();
        }

        /*FUNCTION: DrawnCard()
         * PARAMS: Card drawnCard (from Card class)
         * RETURNS: None
         * CLASS SCOPE EFFECT: List<Card> dealtHand
         * CALLED FUNCTION: 
         * 
         * Description: Handles adding the passed Card drawnCard to the dealtHand List
         */
        public void DrawCard(Card drawnCard)
        {
            dealtHand.Add(drawnCard);
        }

        /*FUNCTION: CheckHand()
         * PARAMS: None
         * RETURNS: None
         * CLASS SCOPE EFFECT: None (doesn't change any variables)
         * CALLED FUNCTION: None
         * 
         * Description: Checks the current elements in the dealtHand List
         */
        public void CheckHand()
        {
            Console.WriteLine("Checking " + ownerType);
            foreach(Card card in dealtHand)
            {
                Console.WriteLine(card.suit + ",  " + card.rank);

            }
        }
        /*FUNCTION: TallyHand()
         * PARAMS: None
         * RETURNS: int score
         * CLASS SCOPE EFFECT: int score
         * CALLED FUNCTION: None
         * 
         * Description: Tallies up the score of the current dealtHand List and sets the found value to the score variable and returns the value
         */
        public int TallyHand()
        {
            score = 0; //score is set to 0; this is in order to prevent the previously set score from being added as new elements are introduced and checked
            foreach(Card card in dealtHand)
            {
                if( (card.rank==Rank.Jack) || (card.rank == Rank.Queen) || (card.rank == Rank.King)) //if the card in the List is a Jack, Queen, or King, add 10
                {
                    score = score + 10;
                }
                else if( card.rank == Rank.Ace) //if the card in the List found is an Ace, add 1
                {
                    score++;
                }
                else //otherwise, add the respective integer value of the rank to the score
                {
                    score = score + (int)card.rank;
                }
            }
            return score;
        }
    }
    
    class Program
    {
        /*Declaring static global values for the deal counter, card counter values, and player/dealer holding boolean checks
         * The deal variable will track the number of deals so that the number doesn't exceed 11, which is the maximum possible number of deals for Blackjack
         * The cardCount variable will track the card of deck to be dealt to the player
         * playerHolds/dealerHolds is set depending if the player or dealer holds and will help in determining the winner. When both the dealer and player hold, then the winner can be determined.
         */
        static int deal = 1;
        static int cardCount = 0;
        static bool playerHolds = false;
        static bool dealerHolds = false;
        static void Main()
        {
            Console.WriteLine("Preparing for a game of Blackjack...please press y to begin.");
            Console.WriteLine("Blackjack rules:");
            Console.WriteLine("Player hits or passes to score closest to 21 points without exceeding.");
            Console.WriteLine("Dealer hits until score >= 17. Then Dealer holds. You are the Player.");

            Deck.Shuffle();

            Console.WriteLine("The deck has been shuffled!");
            Console.WriteLine();

            //Declaring empty hands for the dealer and player
            Hand dealerHand = new Hand("dealer");
            Hand playerHand = new Hand("player");

            //While loop is set to only loop while the numeber of deals is less than or equal to 11 (as stated before, this is the maximum possible deals in Blackjack)
            while ((deal <= 11) )
            {
                //Passing the empty hands to the method
                HitOrPass(dealerHand, playerHand);
            }
            Console.WriteLine("Game ends in a draw, Press (r)estart or (q)uit then hit ENTER!");
            string userInput = Console.ReadLine(); //get user input
            if (userInput == "r")
            {
                //reset the deal and cardCount counters to their original values to restart the game correctly and call the main method
                deal = 1;
                cardCount = 0;
                Console.WriteLine("Game RESTART!");
                Program.Main();

            }
            else if (userInput == "q")
            {
                System.Environment.Exit(0); //exit the game console
            }

            Console.ReadLine();
        }

        /*FUNCTION: HitOrPass()
         * PARAMS: Hand dealerHand, Hand playerHand (the empty hands declared in the main method for the start of each new game)
         * RETURNS: None
         * CLASS SCOPE EFFECT: int deal, int cardCount, bool playerHolds, bool dealerHolds
         * CALLED FUNCTION: Hit(), Pass(), Program.Main(), System.Enviornment.Exit(), CheckForWinner()
         * 
         * Description: Handles the primary gameplay of Blackjack, where player input is taken for each move
         * Player can choose h to hit, where a card is drawn for them, p to pass where they will declare their hold, or r to restart the game, or q to quit the game.
         * If the player inputs something that is not an option listed above, system advises the player to try again
         */
        public static void HitOrPass(Hand dealerHand, Hand playerHand)
        {

            Console.WriteLine("Press (h)it, (p)ass to play, (r)estart or (q)uit then hit ENTER!");
            Console.WriteLine();
            string userInput = Console.ReadLine(); //get user input

            if (userInput == "h")
            {
                Hit(dealerHand, playerHand);
                deal++; //increment the deal count by one to indicate one completed deal

            }
            else if (userInput == "p")
            {
                Pass(dealerHand);
                deal++; //increment the deal count by one to indicate one completed deal


            }
            else if (userInput == "r")
            {
                //reset the deal and cardCount counters to their original values to restart the game correctly and call the main method
                deal = 1; 
                cardCount = 0;
                Console.WriteLine("Game RESTART!");
                Program.Main();
               
            }
            else if(userInput == "q")
            {
                System.Environment.Exit(0); //exit the game console
            }
            else
            {
                Console.WriteLine("Invalid entry!");
            }

            Console.WriteLine("-------------------------------------");
            Console.WriteLine("Dealer Score: " + dealerHand.TallyHand());
            Console.WriteLine("Player Score: " + playerHand.TallyHand());
            Console.WriteLine();
            //call the CheckForWinner method to determine the winner if there is one
            CheckForWinner(dealerHand, playerHand);
            Console.WriteLine();

        }

        /*FUNCTION: Hit()
         * PARAMS: Hand dealerHand, Hand playerHand (in order to add to the current cards the player/dealer currently have)
         * RETURNS: None
         * CLASS SCOPE EFFECT: Class Hand List<Card> dealtHand, int cardCounter, bool dealerHolds, bool playerHolds
         * CALLED FUNCTION: Class Hand DrawCard(), TallHand()
         * 
         * Description: Access the Deck class's shuffledCards deck and add the first available card of the deck to the dealer and player's Hands, respectively. This is tracked with the cardCount variable. 
         * Display the drawn card for the dealer/player. The drawn card is added to the dealer/player's list of cards in the Hand class. 
         * The Hit()  method also checks the dealer's current score; if the dealer's score is equal to or greater than 17, the dealer will not have anymore cards added to their List.
         * The cardCount is increased as needed. 
         */
        public static void Hit(Hand dealerHand, Hand playerHand)
        {
            Console.WriteLine("You decided to HIT!");
            Console.WriteLine();
            Console.WriteLine("Deal count: " + deal);
            
            if (dealerHand.TallyHand() < 17) //if the dealer's hand is less than 17 points, they must keep hitting themselves/adding to their current List of cards
            {
                //The dealer shall hit themselves first before the player, so when adding the cards to each of their hands, the dealer shall take the first card (represented as cardCount) and the player shall take the next card after that (cardCount + 1)
                Console.WriteLine("Dealer draws: Suit: " + Deck.shuffledCards[cardCount].suit + ", Rank: " + Deck.shuffledCards[cardCount].rank);
                dealerHand.DrawCard(Deck.shuffledCards[cardCount]); //call the DrawCard function in the Hand class to add the card to the dealer's hand

                //using cardCount+1 so that the player gets the card after the dealer hits
                Console.WriteLine("Player draws: Suit: " + Deck.shuffledCards[cardCount+1].suit + ", Rank: " + Deck.shuffledCards[cardCount+1].rank);
                playerHand.DrawCard(Deck.shuffledCards[cardCount + 1]);
                
                playerHolds = false; //indicate the playerHolds is false in the event that the player passed the previous turn
                cardCount = cardCount + 2; //increment the cardCount by 2, to account for both initial cards having been 'dealt' to each player and are no longer available to draw
                

            }
            else //Only if the dealer has a score of 17 or more will they no longer add to their hand
            {
                dealerHolds = true; //set the dealerHolds variable to true to indicate the dealer is no longer dealing
                Console.WriteLine("Dealer holds.");

                //because the dealer is no longer drawing a card, the player will have the first available card added to their hand and the cardCount variable is only incremented once
                Console.WriteLine("Player draws: Suit: " + Deck.shuffledCards[cardCount].suit + ", Rank: " + Deck.shuffledCards[cardCount].rank);
                playerHand.DrawCard(Deck.shuffledCards[cardCount]);
                playerHolds = false;
                cardCount++;
            }
        }

        /*FUNCTION: Pass()
         * PARAMS: Hand dealerHand (in order to add to the current cards the dealer currently have)
         * RETURNS: None
         * CLASS SCOPE EFFECT: Class Hand List<Card> dealtHand, int cardCounter, bool dealerHolds, bool playerHolds
         * CALLED FUNCTION: Class Hand DrawCard(), TallyHand()
         * 
         * Description: Access the Deck class's shuffledCards deck and add the first available card of the deck to the dealer's Hands. This is tracked with the cardCount variable. 
         * Display the drawn card for the dealer. The drawn card is added to the dealer's list of cards in the Hand class. 
         * The Pass() method also checks the dealer's current score; if the dealer's score is equal to or greater than 17, the dealer will not have anymore cards added to their List.
         * The cardCount is increased as needed. 
         */
        public static void Pass(Hand dealerHand)
        {
            Console.WriteLine("You decided to PASS!");
            Console.WriteLine("");
            if (dealerHand.TallyHand() < 17) //if the dealer's hand is less than 17 points, they must keep hitting themselves/adding to their current List of cards
            {
                Console.WriteLine("Dealer draws: Suit: " + Deck.shuffledCards[cardCount].suit + ", Rank: " + Deck.shuffledCards[cardCount].rank);
                dealerHand.DrawCard(Deck.shuffledCards[cardCount]);
                Console.WriteLine("Player holds.");
                playerHolds = true; //playerHolds is set to true to indicate no card was added to the playerHand List dealtHand
                cardCount++; //increment cardCount in order to draw the next available card
            }
            else //Only if the dealer has a score of 17 or more will they no longer add to their hand. Thus, neither player or dealer has a card added to their hand and both dealerHolds and playerHolds are set to true
            {
                dealerHolds = true;
                playerHolds = true;
                Console.WriteLine("Dealer holds.");
                Console.WriteLine("Player holds.");
            }            
        }

        /*FUNCTION: CheckForWinner()
         * PARAMS: Hand dealerHand, Hand playerHand (in order to add to the current cards the player/dealer currently have)
         * RETURNS: None
         * CLASS SCOPE EFFECT: None (doesn't change any values of other variables)
         * CALLED FUNCTION: Class Hand TallyHand()
         * 
         * Description: Check the player and dealer's hands to get their current score to determine a winner
         * If the player's score exceeds 21, the dealer wins; if the dealer's score exceeds 21, the player wins
         * The player and dealer must hold before the scores can be accounted for and a winner can be determined if neither player or dealer busts prior to holding
         */
        public static void CheckForWinner(Hand dealerHand, Hand playerHand)
        {
            //if both player and dealer hold (and neither have busted), check both scores
            if ((playerHolds == true) && (dealerHolds == true))
            {
                //if the dealer's score is greater than the player's and the dealer didn't exceed a score of 21, the dealer wins
                if ((dealerHand.TallyHand() > playerHand.TallyHand()) && (dealerHand.TallyHand() <= 21))
                {
                    Console.WriteLine("Winner is Dealer.");

                }
                //if the player's score exceeds the dealers and doesn't exceed 21, the player wins
                else if (((playerHand.TallyHand() > dealerHand.TallyHand()) && (playerHand.TallyHand() <= 21)))
                {
                    Console.WriteLine("Winner is Player");

                }
            }
            //otherwise, if the player's score exceeds 21 (they bust) or the dealer gets a score of 21, the dealer wins
            else if (playerHand.TallyHand() > 21 || dealerHand.TallyHand()==21)
            {
                Console.WriteLine("Winner is Dealer.");
            }
            //otherwise, if the dealer busts or the player gets a score of 21, the player wins
            else if (dealerHand.TallyHand() > 21 || playerHand.TallyHand() == 21)
            {
                Console.WriteLine("Winner is Player");
            }
            //otherwise the player is undetermined. this also covers cases of ties.
            else
            {
                Console.WriteLine("Winner is Undetermined.");
            }
        }
    }
}
