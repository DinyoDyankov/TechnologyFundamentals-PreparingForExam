namespace P01.ChoreWars
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public class Program
    {
        public static void Main()
        {
            string choresInput = string.Empty;

            Dictionary<string, int> allChoresAndTimes = new Dictionary<string, int>
            {
                { "Doing the dishes", 0 },
                { "Cleaning the house", 0 },
                { "Doing the laundry", 0 }
            };


            while ((choresInput = Console.ReadLine()) != "wife is happy")
            {
                var regex = @"<[a-z0-9]+>|\[[A-Z0-9]+\]|{.*}";

                if (Regex.IsMatch(choresInput, regex))
                {
                    var matchedChores = Regex.Match(choresInput, regex);

                    string currentMatch = matchedChores.Value;

                    if (currentMatch[0] == '<')
                    {
                        int workingTime = FindWorkingTime(currentMatch);
                        allChoresAndTimes["Doing the dishes"] += workingTime;
                    }
                    else if (currentMatch[0] == '[')
                    {
                        int workingTime = FindWorkingTime(currentMatch);
                        allChoresAndTimes["Cleaning the house"] += workingTime;

                    }
                    else if (currentMatch[0] == '{')
                    {
                        int workingTime = FindWorkingTime(currentMatch);
                        allChoresAndTimes["Doing the laundry"] += workingTime;

                    }
                }
            }

            int totalTime = 0;

            foreach (var chore in allChoresAndTimes)
            {
                Console.WriteLine($"{chore.Key} - {chore.Value} min.");
                totalTime += chore.Value;
            }

            Console.WriteLine($"Total - {totalTime} min.");
        }

        public static int FindWorkingTime(string currentMatch)
        {
            int totalTime = 0;

            for (int i = 1; i < currentMatch.Length - 1; i++)
            {
                if (char.IsDigit(currentMatch[i]))
                {
                    string currentNumber = currentMatch[i].ToString();
                    totalTime += int.Parse(currentNumber);
                }
            }

            return totalTime;
        }
    }
}
