namespace P07.ForceBook
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Program
    {
        public static void Main()
        {
            string input = string.Empty;

            Dictionary<string, List<string>> forceBook = new Dictionary<string, List<string>>();

            while ((input = Console.ReadLine()) != "Lumpawaroo")
            {
                if (input.Contains('|'))
                {
                    string[] forceSideAndUser = input.Split(new [] { " | " }, StringSplitOptions.RemoveEmptyEntries);
                    string forceSide = forceSideAndUser[0];
                    string user = forceSideAndUser[1];

                    if (!forceBook.ContainsKey(forceSide))
                    {
                        forceBook[forceSide] = new List<string>
                        {
                            user
                        };
                    }
                    else
                    {
                        if (!forceBook[forceSide].Contains(user))
                        {
                            forceBook[forceSide].Add(user);
                        }
                    }

                }
                else
                {
                    string[] userAndForceSide = input.Split(new [] { " -> " }, StringSplitOptions.RemoveEmptyEntries);
                    string user  = userAndForceSide[0];
                    string forceSide = userAndForceSide[1];
                    bool userFound = false;

                    foreach (var side in forceBook)
                    {
                        if (side.Value.Contains(user))
                        {
                            side.Value.Remove(user);

                            if (!forceBook.ContainsKey(forceSide))
                            {
                                forceBook[forceSide] = new List<string>
                                {
                                    user
                                };
                            }
                            else
                            {
                                if (!forceBook[forceSide].Contains(user))
                                {
                                    forceBook[forceSide].Add(user);
                                }
                            }

                            Console.WriteLine($"{user} joins the {forceSide} side!");

                            userFound = true;
                            break;
                        }
                    }
                    if (userFound == false)
                    {
                        if (!forceBook.ContainsKey(forceSide))
                        {
                            forceBook[forceSide] = new List<string>();
                            forceBook[forceSide].Add(user);
                        }
                        else
                        {
                            if (!forceBook[forceSide].Contains(user))
                            {
                                forceBook[forceSide].Add(user);
                            }
                        }

                        Console.WriteLine($"{user} joins the {forceSide} side!");
                    }
                }
            }

            foreach (var side in forceBook.OrderByDescending(s => s.Value.Count).ThenBy(s => s.Key))
            {
                if (side.Value.Count != 0)
                {
                    Console.WriteLine($"Side: {side.Key}, Members: {side.Value.Count}");

                    foreach (var user in side.Value.OrderBy(u => u))
                    {
                        Console.WriteLine($"! {user}");
                    }
                }
            }
        }
    }
}
