namespace ArchiveExams
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class Program
    {
        public static void Main()
        {
            string messageInput = string.Empty;

            var regex = @"^(?<dLeft>[0-9]+)(?<word>[A-Za-z]+)(?<dRight>[^A-Za-z]*)$";

            while ((messageInput = Console.ReadLine()) != "Over!")
            {
                int decryptingNumber = int.Parse(Console.ReadLine());

                if (Regex.IsMatch(messageInput, regex))
                {
                    var matchedMessage = Regex.Match(messageInput, regex);

                    if (matchedMessage.Groups["word"].Length == decryptingNumber)
                    {
                        string allDigits = matchedMessage.Groups["dLeft"].ToString() + matchedMessage.Groups["dRight"].ToString();
                        string currentWordToDecrypt = matchedMessage.Groups["word"].ToString();

                        string decryptedMessage = FindDecryptedMesasge(currentWordToDecrypt, allDigits);

                        Console.WriteLine($"{currentWordToDecrypt} == {decryptedMessage}");
                    }
                }
            }
        }

        public static string FindDecryptedMesasge(string currentWordToDecrypt, string allDigits)
        {
            string decryptedMessage = string.Empty;

            for (int i = 0; i < allDigits.Length; i++)
            {
                if (char.IsDigit(allDigits[i]))
                {
                    string number = allDigits[i].ToString();
                    int currentNumber = int.Parse(number);

                    if (currentNumber > currentWordToDecrypt.Length - 1 || currentNumber < 0)
                    {
                        decryptedMessage += (char)32;
                    }
                    else
                    {
                        decryptedMessage += currentWordToDecrypt[currentNumber];
                    }
                }
            }

            return decryptedMessage;
        }
    }
}
