using Microsoft.AspNetCore.Mvc.RazorPages;
using project.API;

namespace project.Pages;

public class CastModel : PageModel
{

    public string castName;
    public string castProfile;
    public string castBiography;
    public string castBirth;
    public string castPlaceOfBirth;
    public async Task OnGet(int castID) {
        await Fetch.GetCastDetails(castID);
        castName = Fetch.castDetail.name;
    }

}