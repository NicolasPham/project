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

    //similar Movies
    public PosterSet posterSet = new PosterSet();
    public List<string> movieTitles = new List<string>();
    public List<string> posterURLs = new List<string>();
    public List<string> movieIDs = new List<string>();
    public string path = "https://image.tmdb.org/t/p/w500";


    public async Task OnGet(string movieID) {
        
        await Fetch.GetDetails(movieID);
        title = Fetch.movie.title;
        poster_path = "https://image.tmdb.org/t/p/w500" + Fetch.movie.poster_path;
        backdrop_path = "https://image.tmdb.org/t/p/original" + Fetch.movie.backdrop_path;
        overview = Fetch.movie.overview;
        release_date = Fetch.movie.release_date;

        if (Fetch.movie.budget != 0) {
        budget = Fetch.movie.budget.ToString("C0");
        } else {
            budget = "Unknown";
        }
        
        if (Fetch.movie.revenue != 0) {
        revenue = Fetch.movie.revenue.ToString("C0");
        } else {
            revenue = "Unknown";
        }

        //Credits:
        if (Fetch.credits.cast.Count >= 10) {
            CAST_COUNT = 10;
        } else {
            CAST_COUNT = Fetch.credits.cast.Count;
        }

        for (int i = 0; i < CAST_COUNT; i++) {
            castIDs.Add(Fetch.credits.cast[i].id);  
            castNames.Add(Fetch.credits.cast[i].name);

            if (Fetch.credits.cast[i].profile_path is null) {
                castProfiles.Add("https://www.shutterstock.com/image-illustration/male-profile-picture-silhouette-avatar-260nw-148961501.jpg");
            } else {
                castProfiles.Add("https://image.tmdb.org/t/p/w500" + Fetch.credits.cast[i].profile_path);
            }
        }

        //Videos
        for (int i =0; i < Fetch.videos.results.Count; i++) {
            videoURLs.Add("https://www.youtube.com/embed/" + Fetch.videos.results[i].key);
            videoNames.Add(Fetch.videos.results[i].name);
        }


        //Similar Videos
        await Fetch.GetSimilar(movieID);
        posterSet = Fetch.posterSet;
        foreach(Poster poster in posterSet.results) {
            movieTitles.Add(poster.title);
            posterURLs.Add(path + poster.poster_path);
            movieIDs.Add(poster.id.ToString());
        }

    } //OnGet()

    public void OnPostCastDetail(int castID) {
        Response.Redirect("./Cast?castID="+castID);
    }

    public void OnPostSimilar (string movieID) {
        Response.Redirect("./Movie?movieID=" + movieID);
    }


}//class