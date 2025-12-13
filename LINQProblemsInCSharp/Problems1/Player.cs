using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQProblemsInCSharp.Problems1
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TeamId { get; set; }
        public string Position { get; set; } // "Forward", "Midfielder", "Defender", "Goalkeeper"
        public int JerseyNumber { get; set; }
        public DateTime JoinDate { get; set; }
        public decimal Salary { get; set; }
    }

    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Coach { get; set; }
        public string City { get; set; }
        public DateTime FoundedYear { get; set; }
    }

    public class Match
    {
        public int Id { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public DateTime MatchDate { get; set; }
        public string Stadium { get; set; }
        public int? HomeTeamScore { get; set; }
        public int? AwayTeamScore { get; set; }
        public string Status { get; set; } // "Scheduled", "Completed", "Cancelled"
    }

    public class PlayerStatistic
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int MatchId { get; set; }
        public int Goals { get; set; }
        public int Assists { get; set; }
        public int YellowCards { get; set; }
        public int RedCards { get; set; }
        public int MinutesPlayed { get; set; }
    }

    public class RunProblem
    {
        public static void Run()
        {
            var teams = new List<Team>
        {
            new Team { Id = 1, Name = "Shahrvand FC", Coach = "Ali Rezaei", City = "Tehran", FoundedYear = new DateTime(2000, 1, 1) },
            new Team { Id = 2, Name = "Golestan United", Coach = "Mohammad Karimi", City = "Mashhad", FoundedYear = new DateTime(1995, 1, 1) },
            new Team { Id = 3, Name = "Daryaye Noor", Coach = "Hassan Ahmadi", City = "Isfahan", FoundedYear = new DateTime(2005, 1, 1) },
            new Team { Id = 4, Name = "Sabzevaran", Coach = "Reza Mohammadi", City = "Shiraz", FoundedYear = new DateTime(1998, 1, 1) }
        };

            var players = new List<Player>
        {
            // Team 1
            new Player { Id = 1, Name = "Saeed Hosseini", TeamId = 1, Position = "Forward", JerseyNumber = 9,
                        JoinDate = DateTime.Now.AddYears(-2), Salary = 50000 },
            new Player { Id = 2, Name = "Amir Naderi", TeamId = 1, Position = "Midfielder", JerseyNumber = 10,
                        JoinDate = DateTime.Now.AddYears(-1), Salary = 45000 },
            new Player { Id = 3, Name = "Hadi Karimi", TeamId = 1, Position = "Defender", JerseyNumber = 4,
                        JoinDate = DateTime.Now.AddYears(-3), Salary = 40000 },
            new Player { Id = 4, Name = "Mojtaba Rezaei", TeamId = 1, Position = "Goalkeeper", JerseyNumber = 1,
                        JoinDate = DateTime.Now.AddYears(-4), Salary = 35000 },
            
            // Team 2
            new Player { Id = 5, Name = "Reza Alavi", TeamId = 2, Position = "Forward", JerseyNumber = 7,
                        JoinDate = DateTime.Now.AddYears(-2), Salary = 48000 },
            new Player { Id = 6, Name = "Mohsen Ahmadi", TeamId = 2, Position = "Midfielder", JerseyNumber = 8,
                        JoinDate = DateTime.Now.AddYears(-1), Salary = 42000 },
            new Player { Id = 7, Name = "Ali Moradi", TeamId = 2, Position = "Defender", JerseyNumber = 3,
                        JoinDate = DateTime.Now.AddYears(-3), Salary = 38000 },
            
            // Team 3
            new Player { Id = 8, Name = "Hamid Nabavi", TeamId = 3, Position = "Forward", JerseyNumber = 11,
                        JoinDate = DateTime.Now.AddYears(-1), Salary = 46000 },
            new Player { Id = 9, Name = "Javad Rahimi", TeamId = 3, Position = "Midfielder", JerseyNumber = 6,
                        JoinDate = DateTime.Now.AddYears(-2), Salary = 41000 },
            
            // Team 4
            new Player { Id = 10, Name = "Mostafa Jafari", TeamId = 4, Position = "Forward", JerseyNumber = 10,
                        JoinDate = DateTime.Now.AddYears(-2), Salary = 49000 },
            new Player { Id = 11, Name = "Hossein Taheri", TeamId = 4, Position = "Midfielder", JerseyNumber = 5,
                        JoinDate = DateTime.Now.AddYears(-1), Salary = 43000 }
        };

            var matches = new List<Match>
        {
            new Match { Id = 1, HomeTeamId = 1, AwayTeamId = 2, MatchDate = DateTime.Now.AddDays(-30),
                       Stadium = "Azadi Stadium", HomeTeamScore = 2, AwayTeamScore = 1, Status = "Completed" },
            new Match { Id = 2, HomeTeamId = 3, AwayTeamId = 4, MatchDate = DateTime.Now.AddDays(-25),
                       Stadium = "Nagsh-e Jahan Stadium", HomeTeamScore = 1, AwayTeamScore = 1, Status = "Completed" },
            new Match { Id = 3, HomeTeamId = 2, AwayTeamId = 3, MatchDate = DateTime.Now.AddDays(-20),
                       Stadium = "Samen Stadium", HomeTeamScore = 0, AwayTeamScore = 2, Status = "Completed" },
            new Match { Id = 4, HomeTeamId = 4, AwayTeamId = 1, MatchDate = DateTime.Now.AddDays(-15),
                       Stadium = "Hafezieh Stadium", HomeTeamScore = 1, AwayTeamScore = 3, Status = "Completed" },
            new Match { Id = 5, HomeTeamId = 1, AwayTeamId = 3, MatchDate = DateTime.Now.AddDays(5),
                       Stadium = "Azadi Stadium", HomeTeamScore = null, AwayTeamScore = null, Status = "Scheduled" },
            new Match { Id = 6, HomeTeamId = 2, AwayTeamId = 4, MatchDate = DateTime.Now.AddDays(10),
                       Stadium = "Saman Stadium", HomeTeamScore = null, AwayTeamScore = null, Status = "Scheduled" }
        };

            var playerStats = new List<PlayerStatistic>
        {
            // Match 1
            new PlayerStatistic { Id = 1, PlayerId = 1, MatchId = 1, Goals = 1, Assists = 1,
                                 YellowCards = 0, RedCards = 0, MinutesPlayed = 90 },
            new PlayerStatistic { Id = 2, PlayerId = 2, MatchId = 1, Goals = 1, Assists = 0,
                                 YellowCards = 1, RedCards = 0, MinutesPlayed = 85 },
            new PlayerStatistic { Id = 3, PlayerId = 5, MatchId = 1, Goals = 1, Assists = 0,
                                 YellowCards = 0, RedCards = 0, MinutesPlayed = 90 },
            
            // Match 2
            new PlayerStatistic { Id = 4, PlayerId = 8, MatchId = 2, Goals = 1, Assists = 0,
                                 YellowCards = 0, RedCards = 0, MinutesPlayed = 90 },
            new PlayerStatistic { Id = 5, PlayerId = 10, MatchId = 2, Goals = 1, Assists = 0,
                                 YellowCards = 1, RedCards = 0, MinutesPlayed = 88 },
            
            // Match 3
            new PlayerStatistic { Id = 6, PlayerId = 8, MatchId = 3, Goals = 2, Assists = 0,
                                 YellowCards = 0, RedCards = 0, MinutesPlayed = 90 },
            new PlayerStatistic { Id = 7, PlayerId = 6, MatchId = 3, Goals = 0, Assists = 0,
                                 YellowCards = 1, RedCards = 0, MinutesPlayed = 90 },
            
            // Match 4
            new PlayerStatistic { Id = 8, PlayerId = 1, MatchId = 4, Goals = 2, Assists = 1,
                                 YellowCards = 0, RedCards = 0, MinutesPlayed = 90 },
            new PlayerStatistic { Id = 9, PlayerId = 10, MatchId = 4, Goals = 1, Assists = 0,
                                 YellowCards = 0, RedCards = 0, MinutesPlayed = 90 },
            new PlayerStatistic { Id = 10, PlayerId = 2, MatchId = 4, Goals = 1, Assists = 1,
                                 YellowCards = 0, RedCards = 0, MinutesPlayed = 90 }
        };

            var topPlayers = playerStats
            .GroupBy(ps => ps.PlayerId)
            .Select(g => new
            {
                PlayerId = g.Key,
                TotalGoals = g.Sum(ps => ps.Goals),
                TotalAssists = g.Sum(ps => ps.Assists),
                TotalPoints = g.Sum(ps => ps.Goals + ps.Assists),
                MatchesPlayed = g.Select(ps => ps.MatchId).Distinct().Count(),
                TotalMinutes = g.Sum(ps => ps.MinutesPlayed),
                YellowCards = g.Sum(ps => ps.YellowCards),
                RedCards = g.Sum(ps => ps.RedCards)
            })
            .Join(players,
                  stats => stats.PlayerId,
                  player => player.Id,
                  (stats, player) => new
                  {
                      player.Name,
                      player.Position,
                      player.JerseyNumber,
                      stats.TotalGoals,
                      stats.TotalAssists,
                      stats.TotalPoints,
                      stats.MatchesPlayed,
                      stats.TotalMinutes,
                      stats.YellowCards,
                      stats.RedCards,
                      GoalsPerMatch = Math.Round((double)stats.TotalGoals / stats.MatchesPlayed, 2),
                      PointsPerMatch = Math.Round((double)stats.TotalPoints / stats.MatchesPlayed, 2)
                  })
            .OrderByDescending(p => p.TotalPoints)
            .Take(5)
            .ToList();

            Console.WriteLine("=== Top Players (Goals + Assists) ===");
            foreach (var player in topPlayers)
            {
                Console.WriteLine($"{player.Name} (#{player.JerseyNumber}, {player.Position}):");
                Console.WriteLine($"  Goals: {player.TotalGoals}, Assists: {player.TotalAssists}, Points: {player.TotalPoints}");
                Console.WriteLine($"  Matches: {player.MatchesPlayed}, Minutes: {player.TotalMinutes}");
                Console.WriteLine($"  Avg: {player.GoalsPerMatch} goals/match, {player.PointsPerMatch} points/match");
            }

        }
    }
}
