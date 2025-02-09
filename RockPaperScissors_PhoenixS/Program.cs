using System.ComponentModel;
using System.Security;

namespace RockPaperScissors_PhoenixS
{
    internal class Program
    {
        public enum Game
        {
            Rock = 1,
            Paper,
            Scissors,
            Lizard,
            Spock
        }
        static void Main(string[] args)
        {
            // variable declarations
            string? stringUserInput = "";
            int result = 0;

            int userWins = 0;
            int cpuWins = 0;
            int roundNum = 1;                       // Rounds start at 1 not 0
            
            // when Enum Game i changed, change these variables as well
            int iterator = 1;                       // labels each input for player with int
            int minRange = 1;                       // min range of Game (for CPUinput())
            int maxRange = 5;                       // max range of Game (for CPUinput())

            Write("Welcome to the Rock, Paper, Scissors 5000");
            Write("Now with Spock and Lizard!");
            Write("");

            // Lists out each Game enum and their associated value
            foreach(Enum i in Enum.GetValues(typeof(Game)))
            {
                Write(i + " = " + iterator);
                iterator++;
            }
            Write("--------------------------");

            while (cpuWins < 5 && userWins < 5)                         // exits while loop when someone wins
            {
                int cpuInput = CPUinput(minRange, maxRange);                              // Gives random number between 0 - 4, must occur within while loop 

                Write("");

                Write("Round " + roundNum);
                Write("Press " + minRange + " - " + maxRange + " or type your choice of weapon then press Enter.");

                Enum userInput = CheckUserInput(stringUserInput, result);

                Write("");

                //Enum userInput = (Game)result;
                int gameLogic = GameLogic(userInput, cpuInput);
                roundNum++;

                switch (gameLogic)
                {
                    case 0:
                        cpuWins++;
                        Write("You lose this round.");
                        break;
                    case 1:
                        userWins++;
                        Write("You win this round.");
                        break;
                    case 2:
                        Write("It was a tie.");
                        break;
                    default:
                        break;
                }
                Write("Player input: " + userInput + "   |   CPU input: " + (Game)cpuInput);
                Write("Current Score: Player: " + userWins + "   |   CPU: " + cpuWins);
                Write("--------------------------");
            }

            if (cpuWins == 5)
            {
                Write("You lose! CPU is the champion.");
            }
            else
            {
                Write("You win! CPU wasn't up to the challenge.");
            }
        }
        static int CPUinput(int min, int max)
        {
            // outputs random number for CPU output
            int cpu;
            Random rand = new Random();
            cpu = rand.Next(min, max + 1);          // .Next() exclusive of maxValue
            return cpu;
        }
        static int Conditions(int cpu, int lose1, int lose2, int win1, int win2)
        {
            if (cpu == lose1 || cpu == lose2)
            {
                return 0;                       // you lose
            }
            else if (cpu == win1 || cpu == win2)
            {
                return 1;                       // you win
            }
            else
            {
                return 2;                       // it's a tie
            }
        }
        static int GameLogic(Enum input, int cpu)
        {
            // Every outcome of rock, paper, scissors, lizard, spock
            // returns either 0, 1, 2
            switch (input)
            {
                case Game.Rock:
                    return Conditions(cpu, (int)Game.Paper, (int)Game.Spock, (int)Game.Scissors, (int)Game.Lizard);
                case Game.Paper:
                    return Conditions(cpu, (int)Game.Scissors, (int)Game.Lizard, (int)Game.Rock, (int)Game.Spock);
                case Game.Scissors:
                    return Conditions(cpu, (int)Game.Rock, (int)Game.Spock, (int)Game.Paper, (int)Game.Lizard);
                case Game.Lizard:
                    return Conditions(cpu, (int)Game.Scissors, (int)Game.Rock, (int)Game.Spock, (int)Game.Paper);
                case Game.Spock:
                    return Conditions(cpu, (int)Game.Paper, (int)Game.Lizard, (int)Game.Scissors, (int)Game.Rock);
                default:
                    Write("How did you mess up this badly?");
                    return 3;
            }
        }

        static Enum CheckUserInput(string? stringUserInput, int intUserInput)
        {
            bool boolean = true;

            // Returns either a (string)enum or (int)enum based on whether player inputed a number or words
            // enum declarations per case for clarity
            // Known issues held strictly in using intUserInput. TryParse extremely temperamental with large stringUserInput's.  
            int y = 5;
            Enum defaultCase = (Game)y;

            while (boolean)
            {
                // Must contain the ReadLine() here! Do NOT move (If still using intUserInput)
                stringUserInput = Console.ReadLine();                   // Take user input
                int.TryParse(stringUserInput, out intUserInput);        // Parses stringUserInput for integers and stores it in 'integer',
                                                                        //        TryParse() allows for null strings
                                                                        
                bool stringCheck = stringUserInput.All(char.IsDigit);   // returns true if stringUserInput contains a number

                if (stringUserInput == null || stringUserInput == "")
                {
                    Write("Cannot input nothing. Try again.");
                    boolean = true;
                }
                
                else if (stringCheck == true)                                  // If there is a number
                {
                    foreach (int i in Enum.GetValues(typeof(Game)))            // Go through list of int in Game enums
                    {
                        if (intUserInput == i)                                 // if that number is equal to a Game enum
                        {
                            Enum intOutput = (Game)i;
                            boolean = false;
                            return intOutput;
                        }   
                    }    
                    if (boolean == true)                                        // contained within if because it will write otherwise
                    {
                        Write("Invalid number input. Try again.");
                    }
                }
                
                else
                {
                    foreach (Enum stringOutput in Enum.GetValues(typeof(Game)))                    // going through list of Game enums
                    {
                        if (stringUserInput.ToLower() == stringOutput.ToString().ToLower())        // if stringUserinput is equal to one of the Game enums
                        {
                            boolean = false;
                            return stringOutput;
                        }
                    }
                    if (boolean == true)
                    {
                        Write("Invalid word input. Try again.");
                    }
                }
            }
            return defaultCase;
        }
        static void Write(string sentence)
        {
            Console.WriteLine(sentence);
        }
    }
}
