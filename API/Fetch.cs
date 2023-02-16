using System.Net.Http;
using System.Text.Json;

namespace project.API;

public static class Fetch {
    public static HttpClient client = new HttpClient();
    public static string Data { get; set; }
    public static PosterSet posterSet = new PosterSet();
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
    }


    private static void ClearHeader() {
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("applicationException/json"));
    }
}