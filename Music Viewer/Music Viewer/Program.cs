using Music_Viewer.Services;
using Music_Viewer.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<ISongService, SongService>();
builder.Services.AddScoped<ISongService, SongService>();

builder.Services.AddHttpClient<IArtistService, ArtistService>();
builder.Services.AddScoped<IArtistService, ArtistService>();

builder.Services.AddHttpClient<IAlbumService, AlbumService>();
builder.Services.AddScoped<IAlbumService, AlbumService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

/*
 * I did not create end-points for deleting, updating, or creating entries in the DB
 * The only supported actions are get all, and get by id
 * Not all tables in the DB have getters in the API
 * Only Songs, Albums, and Artists are retrievable
 */