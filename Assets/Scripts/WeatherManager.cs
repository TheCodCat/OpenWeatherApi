using Assets.Scripts.Models;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using Cysharp.Threading.Tasks;
using Assets.Scripts;
using System.IO;

public class WeatherManager : MonoBehaviour
{

    [Header("Мусор")]
    [SerializeField] private RawImage _currentWeatherIcon;
    [SerializeField] private Texture2D _currentWeatherTexture;
    [SerializeField] private TMP_InputField _sityInputField;
    [Header("Text")]
    [SerializeField] private TMP_Text _currentClouds;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private TMP_Text _currentVisibility;
    [SerializeField] private TMP_Text _currentData;
    [SerializeField] private TMP_Text _temperatureText;
    [SerializeField] private TMP_Text _windText;
    [Header("Данные о погоде")]
    [SerializeField] private WeatherData _weatherData = new();
    public WeatherData WeatherData
    {
        get { return _weatherData; }
        set { _weatherData = value; }
    }

    private async void Start()
    {
        WeatherData = await DataManager.LoadWeather();
        Debug.Log(WeatherData.GetJson());
        Debug.Log(WeatherData.LastDataUpdate.Subtract(DateTime.Now));
        if (WeatherData.LastDataUpdate.Subtract(DateTime.Now) >= new TimeSpan(0, 1, 0))
        {
            Debug.Log("Данные устарели");
            WeatherData = await RESTApi.GetWeaher(_sityInputField.text);
            Debug.Log(WeatherData.GetJson());
        }
        await UpdateWeather();
    }
    public async void UpdateWeatherButton()
    {
        WeatherData = await RESTApi.GetWeaher(_sityInputField.text);
        await UpdateWeather();
        await DataManager.SaveWeather(WeatherData);
    }
    private async UniTask UpdateWeather()
    {
        try
        {
            _sityInputField.text = WeatherData.name;
            _description.text = $"| {_weatherData.weather[0].description} |";
            _currentData.text = $"Погода {WeatherData.name} на:| {WeatherData.LastDataUpdate.Day}.{WeatherData.LastDataUpdate.Month}.{WeatherData.LastDataUpdate.Year} |";
            _currentVisibility.text = $"Видимость:| {_weatherData.visibility} |";
            _temperatureText.text = $"Температура:| {WeatherData.main.temp} | \nMin:| {WeatherData.main.temp_min} |\nMax:| {WeatherData.main.temp_max} |\nОщущается: | {WeatherData.main.feels_like} |";
            _currentClouds.text = $"Облачность:| {WeatherData.clouds.all} |";
            _windText.text = $"| {WeatherData.wind.speed}:m/s |\nПорывы до:| {WeatherData.wind.gust}m/s |";

            byte[] bytes = await RESTApi.GetIcon(WeatherData.weather[0].icon);
            _currentWeatherTexture = new Texture2D(500,500);
            _currentWeatherTexture.LoadImage(bytes);
            _currentWeatherTexture.Apply();
            Debug.Log(_currentWeatherTexture.width);
            _currentWeatherIcon.texture = _currentWeatherTexture;
        }
        catch (NullReferenceException ex)
        {
            Debug.Log("Неправильно указан город");
        }
    }
}
