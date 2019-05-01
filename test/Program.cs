namespace _02.Roli_The_Coder
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    class Program
    {
        static void Main()
        {
            var events = new Dictionary<string, string>();
            var answer = new Dictionary<string, List<string>>();

            string line = string.Empty;

            while ((line = Console.ReadLine()) != "Time for Code")
            {
                string[] cmd = line.Split();

                char[] chrArray = cmd[0].ToCharArray();

                string id = cmd[0];

                string eventName = cmd[1].Substring(1, cmd[1].Length - 1);

                if (!events.ContainsKey(eventName) && !events.ContainsValue(id))
                {
                    answer[eventName] = new List<string>();

                    events[eventName] = id;
                }
                if ((events.ContainsKey(eventName) && !events.ContainsValue(id)) ||
                    (!events.ContainsKey(eventName) && events.ContainsValue(id)))
                {
                    continue;
                }

                for (int i = 2; i < cmd.Length; i++)
                {
                    string user = cmd[i];

                    if (!answer[eventName].Contains(user) && user.Contains("@"))
                    {
                        answer[eventName].Add(user);
                    }
                }
            }

            foreach (var i in answer.OrderByDescending(x => x.Value.Distinct().Count()).ThenBy(x => x.Key))
            {
                Console.WriteLine($"{i.Key} - {i.Value.Count}");
                foreach (var j in i.Value.OrderBy(k => k).Distinct())
                {
                    Console.WriteLine(j);
                }
            }


            //foreach (var i in answer.OrderByDescending(x => x.Value.Count).ThenBy(k => k.Key.First()))
            //{
            //    foreach (var item in answer[i.Key])
            //    {
            //        Console.WriteLine($"{item.Key} - {item.Value.Count}");

            //        Console.WriteLine(string.Join("\n", item.Value.OrderBy(v => v)));
            //    }
            //}
        }
    }
}