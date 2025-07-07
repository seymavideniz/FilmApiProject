using System.Net.Http;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Linq;
using FilmProject.Models;          
using FilmProject.Database;             

namespace FilmProject.Services
{
    public class FilmUpdateService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;

        public FilmUpdateService(HttpClient httpClient, IConfiguration configuration, AppDbContext context)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _context = context;
        }

        public async Task UpdateFilmsAsync()
        {

            string apiKey = _configuration["TMDB:ApiKey"];
            string baseUrl = _configuration["TMDB:BaseUrl"];
            int maxFilms = int.Parse(_configuration["TMDB:MaxFilms"]);


            string url = $"{baseUrl}/movie/popular?api_key={apiKey}&language=en-US&page=1";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return;

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonDocument.Parse(json);

            var films = result.RootElement.GetProperty("results").EnumerateArray().Take(maxFilms);

            foreach (var film in films)
            {
                int tmdbId = film.GetProperty("id").GetInt32();
                string title = film.GetProperty("title").GetString();
                string overview = film.GetProperty("overview").GetString();

                var existingFilm = _context.Films.FirstOrDefault(f => f.TmdbId == tmdbId);
                if (existingFilm == null)
                {
                    _context.Films.Add(new Film
                    {
                        TmdbId = tmdbId,
                        Title = title,
                        Description = overview
                    });
                }
                else
                {
                    existingFilm.Title = title;
                    existingFilm.Description = overview;
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
