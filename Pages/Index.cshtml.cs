using Microsoft.AspNetCore.Mvc.RazorPages;
using project.API;

namespace project.Pages;

public class IndexModel : PageModel
{
    public List<string> movieTitles = new List<string>();
    public List<string> posterURLs = new List<string>();
    public string path = "https://image.tmdb.org/t/p/w500";
    public List<string> overview = new List<string>();
    public List<string> movieIDs = new List<string>();
    public PosterSet posterSet2 = new PosterSet();

    public async Task OnGet()
    {
        await Fetch.GetTrends();
        posterSet2 = Fetch.posterSet;
        foreach(Poster poster in posterSet2.results) {
            movieTitles.Add(poster.title);
            posterURLs.Add(path + poster.poster_path);
            overview.Add(poster.overview.Substring(0, 50) + "...");
            movieIDs.Add(poster.id.ToString());

        }
    } //OnGet();

    public void OnPostDetails (string movieID) {
        Response.Redirect("./Movie?movieID=" + movieID);
    }
} //class
