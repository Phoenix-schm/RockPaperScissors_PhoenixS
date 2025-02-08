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
            string stringerUserInput;

            int userWins = 0;
            int cpuWins = 0;
            int roundNum = 1;
            int num = 0;

            bool winExit = false;


            Write("Welcome to the Rock, Paper, Scissors 5000");
            Write("Now with Spock and Lizard.");
            Write("");

            // Lists out each Game enum and their associated value
            foreach(int i in Enum.GetValues(typeof(Game)))
            {
                Console.WriteLine((Game)i + " = " + num);
                num++;
            }
            while (cpuWins <= 5 || userWins <= 5)
            {
                int cpuInput = CPUinput();                              //Gives random number between 0 - 4, must occur within while loop 



                Write("Round " + roundNum);
                Write("Press 0 - 4 to begin");
                Write("");
                
                // Take user input
                ConsoleKeyInfo userInput = Console.ReadKey();
                int.TryParse(userInput.KeyChar.ToString(), out int result);

                Console.WriteLine();

                Enum input = (Game)result;

                roundNum++;
                Write("Player input: " + input + "     CPU input: " + (Game)cpuInput);
                Write("Current Score: Player: " + userWins + "    CPU: " + cpuWins);

                if (GameLogic(input, cpuInput, roundNum) == 0)          // Lose round
                {
                    cpuWins++;
                    Console.WriteLine("You lose this round");
                }
                else if (GameLogic(input, cpuInput, roundNum) == 1)     // win round
                {
                    userWins++;
                    Console.WriteLine("You win this round");

                }
                else if (GameLogic(input, cpuInput, roundNum) == 2)     // tie
                {
                    Write("It was a tie");
                }

                if (cpuWins == 5)
                {

                }
                Console.WriteLine();
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
                    return 3;
            }
        }
        static void Write(string sentence)
        {
            Console.WriteLine(sentence);
        }
    }
}
