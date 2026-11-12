using D20Tek.Blazor.BrowserStorage;
using DemoWasm1.Models;
using System.Net.Http.Json;

namespace DemoWasm1.Services;
public class PostService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;

    public PostService(HttpClient httpClient, ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
    }

    public async Task<List<Post>> GetPostsAsync()
    {
        try
        {
            var posts = await _httpClient.GetFromJsonAsync<List<Post>>("https://jsonplaceholder.typicode.com/posts");

            await _localStorage.SetAsync("posts", posts);
            return posts;
        }
        catch (Exception)
        {

            var posts = await _localStorage.GetAsync<List<Post>>("posts");
            if(posts.IsSuccess)
            {
                return posts.Value;
            }
            else
            {
                return new List<Post>();
            }
        }

    }
}
