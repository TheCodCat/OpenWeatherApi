using System;

namespace Assets.Scripts.Models
{
    [System.Serializable]
    public class WeatherData
    {
        public DateTime LastDataUpdate = DateTime.Now;
        public Coord coord;
        public Weather[] weather;
        public Main main;
        public int visibility;
        public Wind wind;
        public Clouds clouds;
        public string name;
    }
}

[System.Serializable]
public class Coord
{
    public float lon;
    public float lat;
}

[System.Serializable]
public class Weather
{
    public int id;
    public string main;
    public string description;
    public string icon;
}

[System.Serializable]
public class Main
{
    public float temp;
    public float feels_like;
    public float temp_min;
    public float temp_max;
    public int humidity;
    public int sea_level;
    public int grnd_level;
}

[System.Serializable]
public class Wind
{
    public float speed;
    public int deg;
    public float gust;
}

[System.Serializable]
public class Clouds
{
    public byte all;
}

