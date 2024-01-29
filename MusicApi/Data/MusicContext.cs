using MusicApi.Models.DB_Models;
using Microsoft.EntityFrameworkCore;

namespace MusicApi.Data {
    public class MusicContext : DbContext {
        public MusicContext(DbContextOptions<MusicContext> options) : base(options) { }
        public DbSet<Song> Song { get; set; }
        public DbSet<Artist> Artist { get; set; }
        public DbSet<Album> Album { get; set; }
    }
}
