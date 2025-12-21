using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQProblems.ManageFilms
{
    public class ManageFilmsAndSeries
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
    }
}
