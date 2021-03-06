﻿namespace P06.FootballStandings
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.Linq;

    public class LeagueGroup
    {
        public LeagueGroup(string teamName)
        {
            this.TeamName = teamName;
        }
        public string TeamName { get; set; }
        public int Points { get; set; }
        public int ScoredGoals { get; set; }
    }

    public class Program
    {
        public static void Main()
        {

            string splitInput = Console.ReadLine();

            string encrytedGameStats = string.Empty;

            var regexToMatchTeams = Regex.Escape(splitInput) + @"[A-Za-z]*" + Regex.Escape(splitInput);

            var regexToMatchResults = @"[0-9]+:[0-9]+";

            List<LeagueGroup> leagueGroupStats = new List<LeagueGroup>();

            while ((encrytedGameStats = Console.ReadLine()) != "final")
            {
                encrytedGameStats = encrytedGameStats.Trim();

                var matchedTeams = Regex.Matches(encrytedGameStats, regexToMatchTeams);
                var matchedResults = Regex.Match(encrytedGameStats, regexToMatchResults);

                List<string> currentMatch = new List<string>();
                List<int> currentMatchFinalScores = new List<int>();

                string [] score = matchedResults.Value.Split(':');

                for (int i = 0; i < score.Length; i++)
                {
                    currentMatchFinalScores.Add(int.Parse(score[i]));
                }

                foreach (Match team in matchedTeams)
                {
                    var currentTeam = team.Value.Replace(splitInput, "");
                    currentTeam = currentTeam.ToUpper();
                    var currentTeamDecrypted = DecryptingTeamName(currentTeam);
                    currentMatch.Add(currentTeamDecrypted);

                    if (!leagueGroupStats.Any(t => t.TeamName.Contains(currentTeamDecrypted) && t.TeamName.Length == currentTeamDecrypted.Length))
                    {
                        LeagueGroup newTeamToAdd = new LeagueGroup(currentTeamDecrypted);
                        leagueGroupStats.Add(newTeamToAdd);
                    }
                }

                string homeTeam = currentMatch[0];
                int indexOfHomeTeam = leagueGroupStats.FindIndex(t => t.TeamName == homeTeam);
                string awayTeam = currentMatch[1];
                int indexOfAwayTeam = leagueGroupStats.FindIndex(t => t.TeamName == awayTeam);


                if (currentMatchFinalScores[0] > currentMatchFinalScores[1])
                {
                    leagueGroupStats[indexOfHomeTeam].Points += 3;
                    leagueGroupStats[indexOfHomeTeam].ScoredGoals += currentMatchFinalScores[0];

                    leagueGroupStats[indexOfAwayTeam].ScoredGoals += currentMatchFinalScores[1];
                }
                else if (currentMatchFinalScores[0] < currentMatchFinalScores[1])
                {

                    leagueGroupStats[indexOfHomeTeam].ScoredGoals += currentMatchFinalScores[0];

                    leagueGroupStats[indexOfAwayTeam].Points += 3;
                    leagueGroupStats[indexOfAwayTeam].ScoredGoals += currentMatchFinalScores[1];
                }
                else if (currentMatchFinalScores[0] == currentMatchFinalScores[1])
                {
                    leagueGroupStats[indexOfHomeTeam].Points += 1;
                    leagueGroupStats[indexOfHomeTeam].ScoredGoals += currentMatchFinalScores[0];

                    leagueGroupStats[indexOfAwayTeam].Points += 1;
                    leagueGroupStats[indexOfAwayTeam].ScoredGoals += currentMatchFinalScores[1];
                }
            }

            Console.WriteLine("League standings:");
            int groupPosition = 1;
            foreach (var team in leagueGroupStats.OrderByDescending(t => t.Points).ThenBy(t => t.TeamName))
            {
                Console.WriteLine($"{groupPosition}. {team.TeamName} {team.Points}");
                groupPosition++;
            }

            Console.WriteLine("Top 3 scored goals:");
            foreach (var team in leagueGroupStats.OrderByDescending(t => t.ScoredGoals).ThenBy(t => t.TeamName).Take(3))
            {
                Console.WriteLine($"- {team.TeamName} -> {team.ScoredGoals}");
            }
        }

        public static string DecryptingTeamName(string currentTeam)
        {
            char[] splitOfTeamName = currentTeam.ToCharArray();

            Array.Reverse(splitOfTeamName);

            return new string(splitOfTeamName);
        }
    }
}
