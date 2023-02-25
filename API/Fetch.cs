using System.Net.Http;
using System.Text.Json;

namespace project.API;

public static class Fetch {
    public static HttpClient client = new HttpClient();
    public static string Data { get; set; }
    public static string CreditData {get; set;}

    public static PosterSet posterSet = new PosterSet();
    public static Movie movie = new Movie();
    public static Credits credits = new Credits();
    public static CastDetail castDetail = new CastDetail();
    public static Video videos = new Video();

    public static CastPhoto castPhoto = new CastPhoto();
    public static Search searchResult = new Search();
    public const string API_KEY = "d194eb72915bc79fac2eb1a70a71ddd3";




    public static async Task GetTrends() {
        ClearHeader();
        HttpResponseMessage response = await client.GetAsync(
            "https://api.themoviedb.org/3/trending/movie/week?api_key=" + API_KEY
        );
 
        if (response.IsSuccessStatusCode) {
            Data = await response.Content.ReadAsStringAsync();
            posterSet = JsonSerializer.Deserialize<PosterSet>(Data);
        } else {
            Data = null;
        }
    } //Get trends

    public static async Task GetDetails(string movieID) {
        ClearHeader();
        HttpResponseMessage response = await client.GetAsync(
        // https://api.themoviedb.org/3/movie/{movie_id}?api_key=<<api_key>>&language=en-US
        "https://api.themoviedb.org/3/movie/" + movieID + "?api_key=" + API_KEY + "&language=en-US"
        );

        if (response.IsSuccessStatusCode) {
            Data = await response.Content.ReadAsStringAsync();
            movie = JsonSerializer.Deserialize<Movie>(Data);
            //budget, genres[], revenue
        } else {
            Data = null;
        }

        // Get Casts and Crews
        HttpResponseMessage creditResponse = await client.GetAsync(
            //https://api.themoviedb.org/3/movie/{movie_id}/credits?api_key=<<api_key>>&language=en-US
            "https://api.themoviedb.org/3/movie/" + movieID + "/credits?api_key=" + API_KEY + "&language=en-US"
        );

        if (creditResponse.IsSuccessStatusCode) {
            Data = await creditResponse.Content.ReadAsStringAsync();
            // credits = JsonSerializer.Deserialize<>
            credits = JsonSerializer.Deserialize<Credits>(Data);
        } else {
            Data = null;
        }

        //Get Videos
        HttpResponseMessage VideoResponse = await client.GetAsync(
            // /https://api.themoviedb.org/3/movie/{movie_id}/videos?api_key=<<api_key>>&language=en-US
            "https://api.themoviedb.org/3/movie/"+ movieID +"/videos?api_key=" + API_KEY + "&language=en-US"
        );
        
        if (VideoResponse.IsSuccessStatusCode) {
            Data = await VideoResponse.Content.ReadAsStringAsync();
            videos = JsonSerializer.Deserialize<Video>(Data);
        } else {
            Data = null;
        }
    }//get Movie Details

    public static async Task GetCastDetails(int castID) {
        ClearHeader();
        HttpResponseMessage castResponse = await client.GetAsync(
            //https://api.themoviedb.org/3/person/{person_id}?api_key=<<api_key>>&language=en-US
            "https://api.themoviedb.org/3/person/" + castID + "?api_key=" + API_KEY + "&language=en-US"
        );
        if (castResponse.IsSuccessStatusCode) {
            Data = await castResponse.Content.ReadAsStringAsync();
            castDetail = JsonSerializer.Deserialize<CastDetail>(Data);
        }//Get cast info

        HttpResponseMessage castPhotoResponse = await client.GetAsync(
            "https://api.themoviedb.org/3/person/"+ castID + "/images?api_key=" + API_KEY
        );
        if (castPhotoResponse.IsSuccessStatusCode) {
            Data = await castPhotoResponse.Content.ReadAsStringAsync();
            castPhoto = JsonSerializer.Deserialize<CastPhoto>(Data);
        }

    }//getCastDetail

    public static async Task GetSearch(string search) {
        ClearHeader();
        HttpResponseMessage searchMovie = await client.GetAsync(
          //https://api.themoviedb.org/3/search/movie?api_key=<<api_key>>&language=en-US&page=1&include_adult=false
        "https://api.themoviedb.org/3/search/movie?api_key=" + API_KEY + "&language=en-US&page=1&include_adult=false&query=" + search
        );

        if (searchMovie.IsSuccessStatusCode) {
            Data = await searchMovie.Content.ReadAsStringAsync();
            searchResult = JsonSerializer.Deserialize<Search>(Data);
        }
    }


    private static void ClearHeader() {
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("applicationException/json"));
    }
}