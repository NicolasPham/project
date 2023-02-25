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

    public async Task OnPostSearch(string searchInput) {
        await Fetch.GetSearch(searchInput);
        foreach(SearchResult poster in Fetch.searchResult.results) {
            movieTitles.Add(poster.title);
            if (poster.poster_path is not null && poster.overview is not null) {
                posterURLs.Add(path + poster.poster_path);

                if (poster.overview.Length > 50) {
                overview.Add(poster.overview.Substring(0, 50) + "...");
                } else {
                overview.Add(poster.overview.Substring(0, poster.overview.Length) + "...");
                }
            } else {
                overview.Add("...");
                posterURLs.Add("https://www.etsy.com/img/8515241/r/il/6f8376/519356130/il_570xN.519356130_dq2v.jpg");

            }
            movieIDs.Add(poster.id.ToString());
        }
    }

    // HOMEPAGE BUTTON
    public async Task OnPostTrending() {
        await OnGet();
    }

    public async Task OnPostNowPlaying() {
        await Fetch.GetNowPlaying();
        posterSet2 = Fetch.posterSet;
        foreach(Poster poster in posterSet2.results) {
            movieTitles.Add(poster.title);
            posterURLs.Add(path + poster.poster_path);
            overview.Add(poster.overview.Substring(0, 50) + "...");
            movieIDs.Add(poster.id.ToString());
        }

    }
    public async Task OnPostTopRated() {
        await Fetch.GetTopRated();
        posterSet2 = Fetch.posterSet;
        foreach(Poster poster in posterSet2.results) {
            movieTitles.Add(poster.title);
            posterURLs.Add(path + poster.poster_path);
            overview.Add(poster.overview.Substring(0, 50) + "...");
            movieIDs.Add(poster.id.ToString());
        }

    }
    public async Task OnPostUpComing() {
        await Fetch.GetUpComing();
        posterSet2 = Fetch.posterSet;
        foreach(Poster poster in posterSet2.results) {
            movieTitles.Add(poster.title);
            posterURLs.Add(path + poster.poster_path);
            overview.Add(poster.overview.Substring(0, 50) + "...");
            movieIDs.Add(poster.id.ToString());
        }

    }
    public async Task OnPostLastest() {
        await Fetch.GetLastest();
        posterSet2 = Fetch.posterSet;
        foreach(Poster poster in posterSet2.results) {
            movieTitles.Add(poster.title);
            posterURLs.Add(path + poster.poster_path);
            overview.Add(poster.overview.Substring(0, 50) + "...");
            movieIDs.Add(poster.id.ToString());
        }

    }

    public void OnPostDetails (string movieID) {
        Response.Redirect("./Movie?movieID=" + movieID);
    }
} //class
