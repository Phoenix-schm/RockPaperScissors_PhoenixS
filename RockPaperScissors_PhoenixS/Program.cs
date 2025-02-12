using System.Collections;
using System.Security.Cryptography.X509Certificates;

namespace RockPaperScissors_PhoenixS
{
    internal class Program
    {
        public enum Game
        {
            Invalid,
            Rock,
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
            int roundNum = 1;                               // Rounds start at 1 not 0

            // when Enum Game i changed, change these variables as well
            int minRange = 1;                               // min range of Game 
            int maxRange = minRange + 4;                    // max range of Game, change int when adding to Game

            Write("Welcome to the Rock, Paper, Scissors 5000");
            Write("Now with Spock and Lizard!");
            Write("First to five rounds wins.");
            Write("");

            // Lists out each Game enum and their associated value
            foreach(Game i in Enum.GetValues(typeof(Game)))
            {
                if (i == Game.Invalid)
                {
                    continue;
                }
                Write(i + " = " + (int)i);
            }
            Write("--------------------------");

            while (cpuWins < 5 && userWins < 5)                         // exits while loop when someone wins
            {
                Write("");
                Write("Round " + roundNum);
                Write("Press " + minRange + " - " + maxRange + " or type your choice of weapon, then press Enter.");

                // All variables must occur within while loop!
                int cpuInput = CPUinput(minRange, maxRange);
                Enum userInput = CheckUserInput(stringUserInput, result);
                int gameLogic = GameLogic(userInput, cpuInput);
                roundNum++;

                Write("");

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

        /// <summary>
        /// Outputs a random number for CPU choice
        /// </summary>
        /// <param name="min"> the minimum range value that the cpu can choose</param>
        /// <param name="max"> the maximum range value that the cpu can choose</param>
        /// <returns> cpuInput </returns>
        static int CPUinput(int min, int max)
        {
            int cpuInput;
            Random rand = new Random();
            cpuInput = rand.Next(min, max + 1);          // .Next() exclusive of maxValue
            return cpuInput;
        }

        /// <summary>
        /// Every outcome of rock, paper, scissors, lizard, spock
        /// </summary>
        /// <param name="_userInput"></param>
        /// <param name="_cpuInput"></param>
        /// <returns> 0, 1, 2 </returns>
        static int GameLogic(Enum _userInput, int _cpuInput)
        {
            switch (_userInput)
            {
                case Game.Rock:
                    return GameConditions(_cpuInput, (int)Game.Paper, (int)Game.Spock, (int)Game.Scissors, (int)Game.Lizard);
                case Game.Paper:
                    return GameConditions(_cpuInput, (int)Game.Scissors, (int)Game.Lizard, (int)Game.Rock, (int)Game.Spock);
                case Game.Scissors:
                    return GameConditions(_cpuInput, (int)Game.Rock, (int)Game.Spock, (int)Game.Paper, (int)Game.Lizard);
                case Game.Lizard:
                    return GameConditions(_cpuInput, (int)Game.Scissors, (int)Game.Rock, (int)Game.Spock, (int)Game.Paper);
                case Game.Spock:
                    return GameConditions(_cpuInput, (int)Game.Paper, (int)Game.Lizard, (int)Game.Scissors, (int)Game.Rock);
                default:
                    Write("How did you mess up this badly?");
                    return 3;
            }
        }

        /// <summary>
        /// The win and lose conditions for the game based on what the cpu inputs
        /// </summary>
        /// <param name="cpu"> CPU choice </param>
        /// <param name="loseCase1"> possible lose scenario for player </param>
        /// <param name="loseCase2"> possible lose scenario for player </param>
        /// <param name="winCase1">  possible win scenario for player </param>
        /// <param name="winCase2">  possible win scenario for player </param>
        /// <returns> 0, 1, 2 </returns>
        static int GameConditions(int cpu, int loseCase1, int loseCase2, int winCase1, int winCase2)
        {
            if (cpu == loseCase1 || cpu == loseCase2)
            {
                return 0;                       // you lose
            }
            else if (cpu == winCase1 || cpu == winCase2)
            {
                return 1;                       // you win
            }
            else
            {
                return 2;                       // it's a tie
            }
        }

        /// <summary>
        /// Checks if the userInput is valid and returns the valid input, otherwise repeat
        /// </summary>
        /// <param name="_stringUserInput"> whatever the player inputs is taken as a string </param>
        /// <param name="_intUserInput">    player input converted into a string </param>
        /// <param name="_minRange">        minRange of Game enum, in case of catastrophic failure </param>
        /// <returns></returns>
        static Enum CheckUserInput(string? _stringUserInput, int _intUserInput)
        {
            // variable declarations
            bool isValid = false;                       // Used for while loop, check if user input is valid
            bool stringCheck;                           // Used to check if string contains a number
            Enum validInput = Game.Invalid;             // for the return case

            while (!isValid)                            // Returns either a (enum)string or (enum)int
            {
                // Must contain the ReadLine() here! Do NOT move!
                _stringUserInput = Console.ReadLine();                          // Take user input
                int.TryParse(_stringUserInput, out _intUserInput);              // Parses stringUserInput for integers and stores it in 'integer',
                                                                                //        TryParse() allows for null strings, defaults 0 if only string

                stringCheck = _stringUserInput.All(char.IsDigit);

                if (_stringUserInput != null)
                {
                    if (_stringUserInput == "")
                    {
                        Write("Cannot input nothing. Try again.");
                    }
                    else if (stringCheck)                       // if there is a number
                    {
                        validInput = CheckGameList(0, _intUserInput, _stringUserInput, ref isValid);
                    }
                    else                                        // if input is a string
                    {
                        validInput = CheckGameList(1, _intUserInput, _stringUserInput, ref isValid);
                    }
                }

                if (isValid) // is true
                { 
                    switch(validInput)
                    {
                        case Game.Invalid:
                            isValid = false;
                            Write("Invalid number input. Try again.");
                            break;
                        default:
                            return validInput;
                    }
                }
            }
            return Game.Invalid;                                // should never happen unless something dramatically went wrong
                                                                // will output GameLogic() default case
        }

        /// <summary>
        /// checks if player input is valid based on if player input is a string or an int
        /// runs through list of Game enums to check for valid inputs
        /// </summary>
        /// <param name="ifCheck"> determines if Game enums are checked through integers or enum names </param>
        /// <param name="_intUserInput"> integer player input </param>
        /// <param name="_stringUserInput">string player input </param>
        /// <param name="boolean"> isValid boolean of CheckUserInput()</param>
        /// <returns></returns>
        static Enum CheckGameList(int ifCheck, int _intUserInput, string _stringUserInput, ref bool boolean)
        {
            switch (ifCheck)
            {
                case 0:                                                         // if case is an integer
                    foreach (Game i in Enum.GetValues(typeof(Game)))
                    {
                        if (_intUserInput == (int)i)
                        {
                            boolean = true;
                            return i;
                        }
                    }
                    if (boolean == false)
                    {
                        Write("Invalid number input. Try again.");
                    }
                    break;

                case 1:                                                         // if case is a string
                    foreach (Game i in Enum.GetValues(typeof(Game)))
                    {
                        if (_stringUserInput.ToLower() == i.ToString().ToLower())
                        {
                            boolean = true;
                            return i;
                        }
                    }
                    if (boolean == false)
                    {
                        Write("Invalid word input. Try again.");
                    }
                    break;
            }
            return Game.Invalid;
        }

        // because I am too lazy to write Console.WriteLine a dozen times over
        static void Write(string sentence)
        {
            Console.WriteLine(sentence);
        }
    }

}
