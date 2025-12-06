using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsInCSharp.BehavioralDesignPattern.IteratorPatterns
{
    public class Playlist : IMusicPlayer
    {
        private string[] _songs = { "Shape of You", "Blinding Lights", "Dance Monkey", "Levitating" };
        private int _currentIndex = 0;

        // اینترفیس IMusicPlayer را پیاده‌سازی می‌کنیم
        public bool HasNextSong()
        {
            return _currentIndex < _songs.Length;
        }

        public string PlayNext()
        {
            if (!HasNextSong())
            {
                Reset();  // به ابتدا برگردان
                return "Playlist restarted!";
            }

            string song = _songs[_currentIndex];
            _currentIndex++;
            return $"Now playing: {song}";
        }

        public string CurrentSong()
        {
            if (_currentIndex == 0 || _currentIndex > _songs.Length)
                return "No song playing";

            return _songs[_currentIndex - 1];
        }

        public void Reset()
        {
            _currentIndex = 0;
        }
    }


    public interface IMusicPlayer
    {
        bool HasNextSong();    // آیا آهنگ بعدی وجود دارد؟
        string PlayNext();     // آهنگ بعدی را پخش کن
        string CurrentSong();  // آهنگ جاری
        void Reset();         // از اول شروع کن
    }

    public class MusicApp
    {
        public static void Main()
        {
            IMusicPlayer player = new Playlist();

            Console.WriteLine("🎵 Starting Music Player...\n");

            // پخش ۶ آهنگ (بیشتر از تعداد واقعی)
            for (int i = 0; i < 6; i++)
            {
                Console.WriteLine(player.PlayNext());
            }

            Console.WriteLine($"\nCurrent Song: {player.CurrentSong()}");
        }
    }
}
