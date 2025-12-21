using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQProblems.ManageFilms
{
    
        public class Movie
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Genre { get; set; } // "Action", "Comedy", "Drama", "Sci-Fi"
            public int Year { get; set; }
            public int Duration { get; set; } // in minutes
            public DateTime? WatchedDate { get; set; }
            public string Status { get; set; } // "Watched", "To Watch"
        }

        public class Series
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Genre { get; set; }
            public int StartYear { get; set; }
            public int? EndYear { get; set; }
            public int TotalSeasons { get; set; }
            public int TotalEpisodes { get; set; }
            public int CurrentSeason { get; set; }
            public int CurrentEpisode { get; set; }
            public string Status { get; set; } // "Watching", "Completed", "To Watch", "Dropped"
        }

        public class WatchSession
        {
            public int Id { get; set; }
            public int? MovieId { get; set; }
            public int? SeriesId { get; set; }
            public DateTime StartTime { get; set; }
            public DateTime EndTime { get; set; }
            public string Type => MovieId.HasValue ? "Movie" : "Series";
        }

        public class Rating
        {
            public int Id { get; set; }
            public int? MovieId { get; set; }
            public int? SeriesId { get; set; }
            public int Score { get; set; } // 1-10
            public DateTime RatingDate { get; set; }
            public string Comment { get; set; }
        }

        public class ManageFilmsAndSeries
        {
            public static void Execute()
            {
            var movies = new List<Movie>
        {
            new Movie { Id = 1, Title = "Inception", Genre = "Sci-Fi", Year = 2010,
                       Duration = 148, WatchedDate = new DateTime(2024, 1, 15), Status = "Watched" },
            new Movie { Id = 2, Title = "The Dark Knight", Genre = "Action", Year = 2008,
                       Duration = 152, WatchedDate = new DateTime(2024, 1, 20), Status = "Watched" },
            new Movie { Id = 3, Title = "Parasite", Genre = "Drama", Year = 2019,
                       Duration = 132, WatchedDate = new DateTime(2024, 2, 5), Status = "Watched" },
            new Movie { Id = 4, Title = "Interstellar", Genre = "Sci-Fi", Year = 2014,
                       Duration = 169, WatchedDate = null, Status = "To Watch" },
            new Movie { Id = 5, Title = "The Godfather", Genre = "Drama", Year = 1972,
                       Duration = 175, WatchedDate = new DateTime(2024, 2, 10), Status = "Watched" }
        };

            var series = new List<Series>
        {
            new Series { Id = 1, Title = "Breaking Bad", Genre = "Drama", StartYear = 2008,
                        EndYear = 2013, TotalSeasons = 5, TotalEpisodes = 62,
                        CurrentSeason = 3, CurrentEpisode = 7, Status = "Watching" },
            new Series { Id = 2, Title = "Stranger Things", Genre = "Sci-Fi", StartYear = 2016,
                        EndYear = null, TotalSeasons = 4, TotalEpisodes = 34,
                        CurrentSeason = 4, CurrentEpisode = 9, Status = "Completed" },
            new Series { Id = 3, Title = "Friends", Genre = "Comedy", StartYear = 1994,
                        EndYear = 2004, TotalSeasons = 10, TotalEpisodes = 236,
                        CurrentSeason = 1, CurrentEpisode = 5, Status = "Watching" },
            new Series { Id = 4, Title = "The Crown", Genre = "Drama", StartYear = 2016,
                        EndYear = 2023, TotalSeasons = 6, TotalEpisodes = 60,
                        CurrentSeason = 0, CurrentEpisode = 0, Status = "To Watch" }
        };

            var watchSessions = new List<WatchSession>
        {
            // Movie sessions
            new WatchSession { Id = 1, MovieId = 1, SeriesId = null,
                             StartTime = new DateTime(2024, 1, 15, 20, 0, 0),
                             EndTime = new DateTime(2024, 1, 15, 22, 28, 0) },
            new WatchSession { Id = 2, MovieId = 2, SeriesId = null,
                             StartTime = new DateTime(2024, 1, 20, 18, 30, 0),
                             EndTime = new DateTime(2024, 1, 20, 21, 2, 0) },
            
            // Series sessions
            new WatchSession { Id = 3, MovieId = null, SeriesId = 1,
                             StartTime = new DateTime(2024, 2, 1, 19, 0, 0),
                             EndTime = new DateTime(2024, 2, 1, 20, 0, 0) },
            new WatchSession { Id = 4, MovieId = null, SeriesId = 1,
                             StartTime = new DateTime(2024, 2, 3, 21, 0, 0),
                             EndTime = new DateTime(2024, 2, 3, 22, 0, 0) },
            new WatchSession { Id = 5, MovieId = null, SeriesId = 3,
                             StartTime = new DateTime(2024, 2, 5, 18, 0, 0),
                             EndTime = new DateTime(2024, 2, 5, 18, 30, 0) },
            new WatchSession { Id = 6, MovieId = 3, SeriesId = null,
                             StartTime = new DateTime(2024, 2, 5, 20, 0, 0),
                             EndTime = new DateTime(2024, 2, 5, 22, 12, 0) },
            new WatchSession { Id = 7, MovieId = null, SeriesId = 3,
                             StartTime = new DateTime(2024, 2, 7, 17, 30, 0),
                             EndTime = new DateTime(2024, 2, 7, 18, 0, 0) }
        };

            var ratings = new List<Rating>
        {
            new Rating { Id = 1, MovieId = 1, SeriesId = null, Score = 9,
                        RatingDate = new DateTime(2024, 1, 16), Comment = "Mind-blowing!" },
            new Rating { Id = 2, MovieId = 2, SeriesId = null, Score = 10,
                        RatingDate = new DateTime(2024, 1, 21), Comment = "Masterpiece" },
            new Rating { Id = 3, MovieId = 3, SeriesId = null, Score = 8,
                        RatingDate = new DateTime(2024, 2, 6), Comment = "Great social commentary" },
            new Rating { Id = 4, MovieId = 5, SeriesId = null, Score = 10,
                        RatingDate = new DateTime(2024, 2, 11), Comment = "Cinematic perfection" },
            new Rating { Id = 5, SeriesId = 2, MovieId = null, Score = 9,
                        RatingDate = new DateTime(2024, 1, 30), Comment = "Nostalgic and exciting" }
        };

        var watchedMovies = movies
            .Where(m => m.Status == "Watched")
            .OrderByDescending(m => m.WatchedDate)
            .Select(m => new
            {
                m.Title,
                m.Genre,
                m.Year,
                m.Duration,
                WatchDate = m.WatchedDate?.ToString("yyyy-MM-dd") ?? "Not watched",
                Rating = ratings.FirstOrDefault(r => r.MovieId == m.Id)?.Score ?? 0,
                Comment = ratings.FirstOrDefault(r => r.MovieId == m.Id)?.Comment ?? "No rating"
            })
            .ToList();

        Console.WriteLine("=== Watched Movies ===");
        foreach (var movie in watchedMovies)
        {
            string stars = new string('★', movie.Rating / 2) + new string('☆', 5 - movie.Rating / 2);
            Console.WriteLine($"{movie.Title} ({movie.Year}) - {movie.Genre}");
            Console.WriteLine($"  Duration: {movie.Duration} min, Watched: {movie.WatchDate}");
            Console.WriteLine($"  Rating: {stars} ({movie.Rating}/10)");
            Console.WriteLine($"  Comment: {movie.Comment}");
        }

        var watchingSeries = series
            .Where(s => s.Status == "Watching")
            .Select(s => new
            {
                s.Title,
                s.Genre,
                Progress = Math.Round((double)((s.CurrentSeason - 1) * s.TotalEpisodes / s.TotalSeasons + s.CurrentEpisode) /
                    s.TotalEpisodes * 100, 1),
                Current = $"S{s.CurrentSeason}E{s.CurrentEpisode}",
                EpisodesWatched = (s.CurrentSeason - 1) * (s.TotalEpisodes / s.TotalSeasons) + s.CurrentEpisode,
                s.TotalEpisodes,
                EpisodesLeft = s.TotalEpisodes - ((s.CurrentSeason - 1) * (s.TotalEpisodes / s.TotalSeasons) + s.CurrentEpisode)
            })
            .ToList();

        Console.WriteLine("\n=== Currently Watching Series ===");
        foreach (var show in watchingSeries)
        {
            Console.WriteLine($"{show.Title} ({show.Genre})");
            Console.WriteLine($"  Progress: {show.Progress}% ({show.Current})");
            Console.WriteLine($"  Watched: {show.EpisodesWatched}/{show.TotalEpisodes} episodes");
            Console.WriteLine($"  Remaining: {show.EpisodesLeft} episodes");
        }

        var ratingAnalysis = ratings
            .GroupBy(r => r.Type)
            .Select(g => new
            {
                Type = g.Key,
                AverageRating = Math.Round(g.Average(r => r.Score), 1),
                Count = g.Count(),
                HighestRated = g.OrderByDescending(r => r.Score).FirstOrDefault(),
                LowestRated = g.OrderBy(r => r.Score).FirstOrDefault()
            })
            .ToList();

        Console.WriteLine("\n=== Rating Analysis ===");
        foreach (var analysis in ratingAnalysis)
        {
            Console.WriteLine($"{analysis.Type}s:");
            Console.WriteLine($"  Average Rating: {analysis.AverageRating}/10");
            Console.WriteLine($"  Total Ratings: {analysis.Count}");

            if (analysis.HighestRated != null)
            {
                string title = analysis.HighestRated.MovieId.HasValue ?
                    movies.First(m => m.Id == analysis.HighestRated.MovieId).Title :
                    series.First(s => s.Id == analysis.HighestRated.SeriesId).Title;
                Console.WriteLine($"  Highest: {title} ({analysis.HighestRated.Score}/10)");
            }

            var weeklyWatchTime = watchSessions
                .Where(ws => ws.StartTime >= DateTime.Now.AddDays(-7))
                .GroupBy(ws => ws.StartTime.Date)
                .Select(g => new
                {
                    Date = g.Key,
                    TotalMinutes = Math.Round(g.Sum(ws => (ws.EndTime - ws.StartTime).TotalMinutes), 0),
                    Sessions = g.Count(),
                    Content = g.Select(ws => ws.MovieId.HasValue ?
                            movies.First(m => m.Id == ws.MovieId).Title :
                            series.First(s => s.Id == ws.SeriesId).Title)
                        .Distinct()
                        .ToList()
                })
                .OrderByDescending(d => d.Date)
                .ToList();

            Console.WriteLine("\n=== Weekly Watch Time ===");
            foreach (var day in weeklyWatchTime)
            {
                Console.WriteLine($"{day.Date:ddd, MMM dd}:");
                Console.WriteLine($"  Time: {day.TotalMinutes} minutes ({Math.Round(day.TotalMinutes / 60.0, 1)} hours)");
                Console.WriteLine($"  Sessions: {day.Sessions}");
                Console.WriteLine($"  Content: {string.Join(", ", day.Content)}");
            }

            var favoriteGenre = movies
                .Where(m => m.Status == "Watched")
                .Concat(series.Where(s => s.Status == "Completed" || s.Status == "Watching")
                    .Select(s => new Movie
                    {
                        Title = s.Title,
                        Genre = s.Genre,
                        Status = s.Status
                    }))
                .GroupBy(x => x.Genre)
                .Select(g => new
                {
                    Genre = g.Key,
                    Count = g.Count(),
                    AverageRating = Math.Round(ratings
                        .Where(r => g.Any(item =>
                            (r.MovieId.HasValue && movies.Any(m => m.Id == r.MovieId && m.Genre == g.Key)) ||
                            (r.SeriesId.HasValue && series.Any(s => s.Id == r.SeriesId && s.Genre == g.Key))))
                        .Average(r => r.Score), 1)
                })
                .OrderByDescending(g => g.Count)
                .FirstOrDefault();

            if (favoriteGenre != null)
            {
                Console.WriteLine($"\n=== Recommendations Based on Your Favorite Genre: {favoriteGenre.Genre} ===");

                var recommendations = movies
                    .Where(m => m.Genre == favoriteGenre.Genre && m.Status == "To Watch")
                    .Take(3)
                    .Select(m => m.Title)
                    .ToList();

                if (recommendations.Any())
                {
                    Console.WriteLine($"Movies to watch: {string.Join(", ", recommendations)}");
                }

                var seriesRecs = series
                    .Where(s => s.Genre == favoriteGenre.Genre && s.Status == "To Watch")
                    .Take(2)
                    .Select(s => s.Title)
                    .ToList();

                if (seriesRecs.Any())
                {
                    Console.WriteLine($"Series to watch: {string.Join(", ", seriesRecs)}");
                }
            }

            var stats = new
            {
                TotalMovies = movies.Count,
                WatchedMovies = movies.Count(m => m.Status == "Watched"),
                TotalSeries = series.Count,
                WatchingSeries = series.Count(s => s.Status == "Watching"),
                CompletedSeries = series.Count(s => s.Status == "Completed"),
                TotalWatchTime = Math.Round(watchSessions.Sum(ws => (ws.EndTime - ws.StartTime).TotalHours), 1),
                AverageMovieRating = ratings.Where(r => r.MovieId.HasValue).Any() ?
                    Math.Round(ratings.Where(r => r.MovieId.HasValue).Average(r => r.Score), 1) : 0,
                AverageSeriesRating = ratings.Where(r => r.SeriesId.HasValue).Any() ?
                    Math.Round(ratings.Where(r => r.SeriesId.HasValue).Average(r => r.Score), 1) : 0
            };

            Console.WriteLine("\n=== Overall Statistics ===");
            Console.WriteLine($"Movies: {stats.WatchedMovies}/{stats.TotalMovies} watched");
            Console.WriteLine($"Series: {stats.WatchingSeries} watching, {stats.CompletedSeries} completed out of {stats.TotalSeries}");
            Console.WriteLine($"Total Watch Time: {stats.TotalWatchTime} hours");
            Console.WriteLine($"Average Movie Rating: {stats.AverageMovieRating}/10");
            Console.WriteLine($"Average Series Rating: {stats.AverageSeriesRating}/10");

            }

        var watchHistory = watchSessions
            .OrderByDescending(ws => ws.StartTime)
            .Take(5)
            .Select(ws => new
            {
                Content = ws.MovieId.HasValue ?
                    movies.First(m => m.Id == ws.MovieId).Title :
                    series.First(s => s.Id == ws.SeriesId).Title,
                Type = ws.Type,
                Date = ws.StartTime.ToString("MMM dd, HH:mm"),
                Duration = Math.Round((ws.EndTime - ws.StartTime).TotalMinutes, 0)
            })
            .ToList();

        Console.WriteLine("\n=== Recent Watch History ===");
        foreach (var item in watchHistory)
        {
            Console.WriteLine($"{item.Date}: {item.Content} ({item.Type}) - {item.Duration} min");
        }
            }
        }

        // Extension method to determine type of rating
        public static class RatingExtensions
        {
            public static string Type(this Rating rating)
            {
                return rating.MovieId.HasValue ? "Movie" : "Series";
            }
        }
}
        }
}
