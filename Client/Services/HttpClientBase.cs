using System.Net.Http;

namespace Client.Services;

public static class HttpClientBase
{
    private const string _baseUrl = "http://localhost:5036";
    public static HttpClient HttpClient = new HttpClient() {BaseAddress = new Uri(_baseUrl)};
}