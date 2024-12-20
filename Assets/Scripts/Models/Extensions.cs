using Assets.Scripts.Models;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;

public static class Extensions
{
    public static async UniTask<string> GetJson(this WeatherData data)
    {
        await UniTask.Yield();
        return JsonConvert.SerializeObject(data);
    }

}
