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
        }
        }
}
