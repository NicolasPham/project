using Microsoft.AspNetCore.Mvc.RazorPages;
using project.API;

namespace project.Pages;

public class IndexModel : PageModel
{
    public List<string> movieTitles = new List<string>();
    public PosterSet posterSet2 = new PosterSet();
    public async Task OnGet()
    {
        await Fetch.GetTrends();
        posterSet2 = Fetch.posterSet;
        foreach(Poster poster in posterSet2.results) {
            movieTitles.Add(poster.title);
        }
    } //OnGet();
} //class
