namespace GuessNumber
{
    internal class Program
    {
        static void Main(string[] args)
        {
         Random rnd = new Random();
          int random =  rnd.Next(1,100);
          Console.WriteLine("Welcome To The Game Guess Number(1-100)");
            
            int attemps = 0;
            bool iscorrect = false;
            while (!iscorrect) 
            {
                Console.WriteLine("Enter Your Guess");
                string user = Console.ReadLine();
                
                if (!int.TryParse(user, out int userGuess))
                {
                    Console.WriteLine("Error: Please enter a valid number.");
                    continue;
                }

                if (userGuess > random)
                {
                    attemps++;
                    Console.WriteLine("Too High");
                }
                else if( userGuess < random )
                {
                    attemps++;
                    Console.WriteLine("Too Low");
                }
                else 
                {
                    Console.WriteLine($"Congratulation You Guess Number in {attemps} Attemps Correct: {random}");
                    iscorrect = true;
                }
                if (attemps == 5)  
                {
                    Console.WriteLine($"Game Over! The Number Was {random}");
                    iscorrect = true;
                }
            }
        }
    }
}
