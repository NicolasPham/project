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

    //Credits
    public int CAST_COUNT;
    public List<string> castNames = new List<string>();
    public List<string> castProfiles = new List<string>();
    public List<int>  castIDs = new List<int>();


    public async Task OnGet(string movieID) {
        
        await Fetch.GetDetails(movieID);
        title = Fetch.movie.title;
        poster_path = "https://image.tmdb.org/t/p/w500" + Fetch.movie.poster_path;
        backdrop_path = "https://image.tmdb.org/t/p/original" + Fetch.movie.backdrop_path;
        overview = Fetch.movie.overview;
        release_date = Fetch.movie.release_date;

        //Credits:
        if (Fetch.credits.cast.Count >= 10) {
            CAST_COUNT = 10;
        } else {
            CAST_COUNT = Fetch.credits.cast.Count;
        }

        for (int i = 0; i < CAST_COUNT; i++) {
            castIDs.Add(Fetch.credits.cast[i].id);
            castNames.Add(Fetch.credits.cast[i].name);
            castProfiles.Add("https://image.tmdb.org/t/p/w500" + Fetch.credits.cast[i].profile_path);
        }
    } //OnGet()

    public void OnPostCastDetail(int castID) {
        Response.Redirect("./Cast?castID="+castID);
    }


}//class