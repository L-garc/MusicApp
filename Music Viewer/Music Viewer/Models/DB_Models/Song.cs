namespace Music_Viewer.Models.DB_Models {
    public class Song {
        public int songID { get; set; }
        public string Name { get; set; }
        public int artistID { get; set; }
        public string? Genre { get; set; }
        public string? Length { get; set; }
        public int albumID { get; set; }

        public Artist Artist { get; set; }
        public Album Album { get; set; }
    }
}
