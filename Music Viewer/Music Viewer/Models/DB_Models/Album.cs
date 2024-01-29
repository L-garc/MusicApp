namespace Music_Viewer.Models.DB_Models {
    public class Album {
        public int albumID { get; set; }
        public string Name { get; set; }
        public int artistID { get; set; }
        public string? AlbumArt { get; set; }
        public short? ReleaseYear { get; set; }

        public List<Song> Songs { get; set; }
        public Artist Artist { get; set; }
    }
}
