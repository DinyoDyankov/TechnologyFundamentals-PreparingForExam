namespace ConsoleApp1
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public class Program
    {
        public static void Main()
        {
            var participantsEntry = Console.ReadLine()
                .Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .OrderBy(d => d)
                .ToList();

            var regexHealth = @"[^\d\-*\/.]"; //@"[^\d\.\+\-\*\/\s\,]"; 

            var regexBaseDamage = @"[\-\+]?[\d]+(?:[\.]*[\d]+|[\d]*)";//@"[\d-+.]*";

            for (int i = 0; i < participantsEntry.Count; i++)
            {
                var matchedDemonHealth = Regex.Matches(participantsEntry[i], regexHealth);
                var matchedDemonBaseDamage = Regex.Matches(participantsEntry[i], regexBaseDamage);

                double currentDemonHealth = FindDemonHealth(matchedDemonHealth);

                double currentDemonTotalDamage = FindDemonTotalDamage(matchedDemonBaseDamage, participantsEntry[i]);

                Console.WriteLine($"{participantsEntry[i]} - {currentDemonHealth} health, {currentDemonTotalDamage:f2} damage");
            }
        }

        public static double FindDemonTotalDamage(MatchCollection matchedDemonBaseDamage, string demonDetails)
        {
            double currentTotalDamage = 0;

            for (int i = 0; i < matchedDemonBaseDamage.Count; i++)
            {
                if (matchedDemonBaseDamage[i].Value != "")
                {
                    currentTotalDamage += double.Parse(matchedDemonBaseDamage[i].Value);

                }
            }

            for (int i = 0; i < demonDetails.Length; i++)
            {
                if (demonDetails[i] == '*')
                {
                    currentTotalDamage *= 2;
                }
                else if (demonDetails[i] == '/')
                {
                    currentTotalDamage /= 2;

                }
            }

            return currentTotalDamage;
        }

        public static double FindDemonHealth(MatchCollection matchedDemonHealth)
        {
            double currentHealth = 0;

            for (int i = 0; i < matchedDemonHealth.Count; i++)
            {
                char currentHealthSymbol = char.Parse(matchedDemonHealth[i].Value);

                currentHealth += currentHealthSymbol;
            }

            return currentHealth;
        }
    }
}
