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
    [SerializeField] private SityManager _sityManager;
    [SerializeField] private TextAsset _sityTextAsset;
    [SerializeField] private Animator _animatorRefresh;
    private Texture2D _currentWeatherTexture;
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
        _sityManager.SetSity(WeatherData.name);
        Debug.Log(WeatherData.LastDataUpdate.Subtract(DateTime.Now));
        if (WeatherData.LastDataUpdate.Subtract(DateTime.Now) >= new TimeSpan(1, 0, 0))
        {
            Debug.Log("Данные устарели");
            WeatherData = await RESTApi.GetWeaher(_sityManager.GetSityNameList());
        }
        await UpdateWeather();
    }

    public async void UpdateWeatherButton()
    {
        _animatorRefresh.SetBool("Refresh",true);
        WeatherData = await RESTApi.GetWeaher(_sityManager.GetSityNameList());
        _animatorRefresh.SetBool("Refresh", false);
        await UpdateWeather();
        await DataManager.SaveWeather(WeatherData);
    }

    public async void UpdateWeatherButtonName(string sityname)
    {
        _animatorRefresh?.SetBool("Refresh", true);
        try
        {
            string name = _sityManager.GetSitiNameToInput(sityname);

            WeatherData = await RESTApi.GetWeaher(name);

            await UpdateWeather();
            _sityManager.SetSity(name);
            await DataManager.SaveWeather(WeatherData);
        }
        catch (NullReferenceException ex)
        {
            Debug.Log("Неправильно указан город");
        }
        _animatorRefresh?.SetBool("Refresh", false);
        
    }

    private async UniTask UpdateWeather()
    {
        try
        {
            _description.text = $"| {_weatherData.weather[0].description} |";
            _currentData.text = $"Погода {WeatherData.name}\nна: | {WeatherData.LastDataUpdate.Day}.{WeatherData.LastDataUpdate.Month}.{WeatherData.LastDataUpdate.Year} |";
            _currentVisibility.text = $"Видимость:| {_weatherData.visibility} |";
            _temperatureText.text = $"Температура:| {WeatherData.main.temp} | \nMin:| {WeatherData.main.temp_min} |\nMax:| {WeatherData.main.temp_max} |\nОщущается: | {WeatherData.main.feels_like} |";
            _currentClouds.text = $"Облачность:| {WeatherData.clouds.all} |";
            _windText.text = $"| {WeatherData.wind.speed}:m/s |\nПорывы до:| {WeatherData.wind.gust}m/s |";

            byte[] bytes = await RESTApi.GetIcon(WeatherData.weather[0].icon);
            _currentWeatherTexture = new Texture2D(500,500);
            _currentWeatherTexture.LoadImage(bytes);
            _currentWeatherTexture.Apply();
            _currentWeatherIcon.texture = _currentWeatherTexture;
        }
        catch (NullReferenceException ex)
        {
            Debug.Log("Неправильно указан город");
        }
    }
}
