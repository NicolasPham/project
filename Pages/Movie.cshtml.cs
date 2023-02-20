using Microsoft.AspNetCore.Mvc.RazorPages;
using project.API;

namespace project.Pages;

public class MovieModel : PageModel
{ 
    public string title;
    public string poster_path;
    public string backdrop_path;
    public string overview;
    public string release_date;


    public async Task OnGet(string movieID) {
        
        await Fetch.GetDetails(movieID);
        title = Fetch.movie.title;
        poster_path = "https://image.tmdb.org/t/p/w500" + Fetch.movie.poster_path;
        backdrop_path = "https://image.tmdb.org/t/p/original" + Fetch.movie.backdrop_path;
        overview = Fetch.movie.overview;
        release_date = Fetch.movie.release_date;
    }


}