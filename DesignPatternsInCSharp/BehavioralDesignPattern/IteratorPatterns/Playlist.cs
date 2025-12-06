using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsInCSharp.BehavioralDesignPattern.IteratorPatterns
{
    public class Playlist
    {
        private string[] _songs = { "Song1", "Song2", "Song3", "Song4" };

        public string GetSong(int index) => _songs[index];
        public int SongCount => _songs.Length;
    }
}
