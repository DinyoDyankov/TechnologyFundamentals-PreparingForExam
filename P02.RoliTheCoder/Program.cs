namespace P02.RoliTheCoder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Events
    {
        public Events(int id, string eventName, List<string> participiants)
        {
            this.ID = id;
            this.EventName = eventName;
            this.Participiants = participiants;

        }

        public int ID { get; set; }
        public string EventName { get; set; }
        public List<string> Participiants { get; set; }
    }

    public class Program
    {
        public static void Main()
        {
            string eventInput = string.Empty;

            List<Events> allEvents = new List<Events>();

            while ((eventInput = Console.ReadLine()) != "Time for Code")
            {
                string[] currentEvent = eventInput.Split(new string[] { " ", "\t" }, StringSplitOptions.RemoveEmptyEntries).ToArray();

                if (currentEvent[1].StartsWith("#"))
                {
                    int id = int.Parse(currentEvent[0]);
                    string currentEventName = currentEvent[1].Remove(0,1);

                    List<string> currentParticipiants = new List<string>();

                    if (CheckIfIDExists(allEvents, id) == true)
                    {
                        int index = allEvents.FindIndex(e => e.ID == id);

                        if (allEvents[index].EventName == currentEventName)
                        {
                            for (int i = 2; i < currentEvent.Length; i++)
                            {
                                string participiantToCheck = currentEvent[i];

                                if (!allEvents[index].Participiants.Contains(participiantToCheck) && participiantToCheck.StartsWith("@"))
                                {
                                    allEvents[index].Participiants.Add(participiantToCheck);    
                                }
                            }
                        }
                        
                    }
                    else if (CheckIfIDExists(allEvents, id) == false && !allEvents.Any(e => e.EventName == currentEventName))
                    {
                        List<string> participiantsCurrentEvent = ParticipiantsToAdd(currentEvent);

                        Events eventToAdd = new Events(id, currentEventName, participiantsCurrentEvent);

                        allEvents.Add(eventToAdd);
                    }
                }
            }

            foreach (var events in allEvents.OrderByDescending(e => e.Participiants.Count).ThenBy(e => e.EventName))
            {
                Console.WriteLine($"{events.EventName} - {events.Participiants.Count}");
                foreach (var participiant in events.Participiants.OrderBy(p => p))
                {
                    Console.WriteLine(participiant);
                }
            }
        }

        public static List<string> ParticipiantsToAdd(string[] currentEvent)
        {
            List<string> participiantsToAdd = new List<string>();

            for (int i = 2; i < currentEvent.Length; i++)
            {
                string currentParticipiant = currentEvent[i];

                if (currentParticipiant.StartsWith("@") && !participiantsToAdd.Contains(currentParticipiant))
                {
                    participiantsToAdd.Add(currentParticipiant);
                }   
            }

            return participiantsToAdd;
        }

        public static bool CheckIfIDExists(List<Events> allEvents, int id)
        {
            foreach (var listedEvent in allEvents)
            {
                if (listedEvent.ID == id)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
