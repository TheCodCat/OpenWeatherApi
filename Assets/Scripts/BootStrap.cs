using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class BootStrap : MonoBehaviour
{
    [field: SerializeField] WeatherManager _weatherManager;
    [field: SerializeField] SityManager _sityManager;

    private async void Start()
    {
        Debug.Log(DateTime.Now);
        await UniTask.Delay(1000);
        await _sityManager.BootInit();
        await _weatherManager.BootInit();
        Debug.Log($"Опен везер подключен {DateTime.Now}");
    }
}
