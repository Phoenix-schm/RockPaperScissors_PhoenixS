using System.Security;

namespace RockPaperScissors_PhoenixS
{
    internal class Program
    {
        public enum Game
        {
            Rock,
            Paper,
            Scissors,
            Lizard,
            Spock
        }
        static void Main(string[] args)
        {
            // userInput;
            string stringerUserInput = "";
            int result = 0;

            int userWins = 0;
            int cpuWins = 0;
            int roundNum = 1;                       // Rounds start at 1 not 0
            int num = 0;


            Write("Welcome to the Rock, Paper, Scissors 5000");
            Write("Now with Spock and Lizard.");
            Write("");

            // Lists out each Game enum and their associated value
            foreach(Enum i in Enum.GetValues(typeof(Game)))
            {
                Write(i + " = " + num);
                num++;
            }
            Write("--------------------------");

            while (cpuWins < 5 && userWins < 5)
            {

                int cpuInput = CPUinput();                              //Gives random number between 0 - 4, must occur within while loop 

                Write("");

                Write("Round " + roundNum);
                Write("Press 0 - 4 or type your response.");

                CheckUserInput(stringerUserInput, result);

                Write("");

                Enum input = (Game)result;

                roundNum++;

                if (GameLogic(input, cpuInput, roundNum) == 0)          // Lose round
                {
                    cpuWins++;
                    Console.WriteLine("You lose this round.");
                }
                else if (GameLogic(input, cpuInput, roundNum) == 1)     // win round
                {
                    userWins++;
                    Console.WriteLine("You win this round.");

                }
                else if (GameLogic(input, cpuInput, roundNum) == 2)     // tie
                {
                    Write("It was a tie.");
                }
                Write("Player input: " + input + "     CPU input: " + (Game)cpuInput + ".");
                Write("Current Score: Player: " + userWins + "    CPU: " + cpuWins + ".");
                Write("--------------------------");
            }

            if (cpuWins == 5)
            {
                Write("You lose! CPU is the champion.");
            }
            else
            {
                Write("You win!");
            }
        }
        static int CPUinput()
        {
            int cpu;
            Random rand = new Random();

            cpu = rand.Next(0, 5);          // .Next() exclusive of maxValue

            return cpu;
        }

        static int Conditions(int cpu, int lose1, int lose2, int win1, int win2)
        {
            if (cpu == lose1 || cpu == lose2)
            {
                return 0;
                // you lose
            }
            else if (cpu == win1 || cpu == win2)
            {
                return 1;
                // you win
            }
            else
            {
                return 2;
                // it's a tie
            }
        }
        static int GameLogic(Enum input, int cpu, int round)
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
        static void Write(string sentence)
        {
            Console.WriteLine(sentence);
        }

        static void CheckUserInput(string? stringUserInput, int userInput)
        {
            bool boolean = true;

            while (boolean)
            {
                stringUserInput = Console.ReadLine();               // Take user input
                int.TryParse(stringUserInput, out userInput);       // Parses stringUserInput for integers and stores it in 'integer',
                                                                    //        TryParse() allows for null strings

                bool stringCheck = stringUserInput.All(char.IsDigit); // returns true if stringUserInput contains a number

                if (stringUserInput == null || stringUserInput == "")
                {
                    Write("Cannot input nothing. Try again.");
                }
                else if (stringCheck == true)                                           // If there is a number
                {
                    foreach (int i in Enum.GetValues(typeof(Game)))                     // Go through list of int in Game enums
                    {
                        if (userInput == i)                                             // if that number is equal to a Game enum
                        {
                            boolean = false;
                            break;
                        }
                    }
                    if (boolean == true)
                    {
                        Write("Must input valid number from list. Try again.");
                    }
                }
                
                else
                {
                    foreach (Enum i in Enum.GetValues(typeof(Game)))                    // going through list of Game enums
                    {
                        if (stringUserInput.ToLower() == i.ToString().ToLower())        // if stringUserinput is equal to one of the Game enums
                        {
                            boolean = false;
                            break;
                        }
                    }
                    if (boolean == true)
                    {
                        Write("Must input valid word from list. Try again.");
                    }
                }

            }
        }
    }
}
