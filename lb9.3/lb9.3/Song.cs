public class Song
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string Composer { get; set; }
    public int Year { get; set; }
    public string Lyrics { get; set; }
    public List<string> Performers { get; set; }

    public Song(string title, string author, string composer, int year, string lyrics, List<string> performers)
    {
        Title = title;
        Author = author;
        Composer = composer;
        Year = year;
        Lyrics = lyrics;
        Performers = performers;
    }
}