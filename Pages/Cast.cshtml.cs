using Microsoft.AspNetCore.Mvc.RazorPages;
using project.API;

namespace project.Pages;

public class CastModel : PageModel
{

    public string castName;
    public string castProfile;
    public string castBiography;
    public string castBirth;
    public int age;
    public string castPlaceOfBirth;

    public List<string> castPhotos = new List<string>();

    public async Task OnGet(int castID) {
        await Fetch.GetCastDetails(castID);
        castName = Fetch.castDetail.name;
        castProfile = "https://image.tmdb.org/t/p/w500" + Fetch.castDetail.profile_path;
        castBiography = Fetch.castDetail.biography;
        castBirth = Fetch.castDetail.birthday;
        castPlaceOfBirth = Fetch.castDetail.place_of_birth;

        age = DateTime.Now.Year - Int32.Parse(castBirth.Split("-")[0]);

        for (int i = 0; i < Fetch.castPhoto.profiles.Count; i++) {
            castPhotos.Add("https://image.tmdb.org/t/p/w500" + Fetch.castPhoto.profiles[i].file_path);
        }
    }

}