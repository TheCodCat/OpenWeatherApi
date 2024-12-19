using Assets.Scripts.Models;
using Cysharp.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System;

public static class RESTApi
{
    private static HttpClient HttpClient = new HttpClient();
    private const string API_KEY = "25dc40ef46d017a1eb9826fcb17b9590";

    public static async UniTask<WeatherData> GetWeaher(string sity)
    {
        using HttpResponseMessage httpRequestMessage = await HttpClient.GetAsync($"https://api.openweathermap.org/data/2.5/weather?q={sity}&units=metric&lang=ru&appid={API_KEY}").AsUniTask();

        string json = await httpRequestMessage.Content.ReadAsStringAsync().AsUniTask();

        return JsonConvert.DeserializeObject<WeatherData>(json);
    }

    public static async UniTask<byte[]> GetIcon(string numIcon)
    {
        using HttpResponseMessage httpRequestMessage = await HttpClient.GetAsync($"http://openweathermap.org/img/wn/{numIcon}@4x.png").AsUniTask();

        var result = await httpRequestMessage.Content.ReadAsByteArrayAsync().AsUniTask();

        return result;
    }
}
