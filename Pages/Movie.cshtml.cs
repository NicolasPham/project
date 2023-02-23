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
    public string budget;
    public string revenue;
    public string genres;

    //Credits
    public int CAST_COUNT;
    public List<string> castNames = new List<string>();
    public List<string> castProfiles = new List<string>();
    public List<int>  castIDs = new List<int>();
    public List<string> videoURLs = new List<string>();
    public List<string> videoNames = new List<string>();


    public async Task OnGet(string movieID) {
        
        await Fetch.GetDetails(movieID);
        title = Fetch.movie.title;
        poster_path = "https://image.tmdb.org/t/p/w500" + Fetch.movie.poster_path;
        backdrop_path = "https://image.tmdb.org/t/p/original" + Fetch.movie.backdrop_path;
        overview = Fetch.movie.overview;
        release_date = Fetch.movie.release_date;
        budget = Fetch.movie.budget.ToString("C0");
        revenue = Fetch.movie.revenue.ToString("C0");

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

        //Videos
        for (int i =0; i < Fetch.videos.results.Count; i++) {
            videoURLs.Add("https://www.youtube.com/embed/" + Fetch.videos.results[i].key);
            videoNames.Add(Fetch.videos.results[i].name);
        }
    } //OnGet()

    public void OnPostCastDetail(int castID) {
        Response.Redirect("./Cast?castID="+castID);
    }


}//class