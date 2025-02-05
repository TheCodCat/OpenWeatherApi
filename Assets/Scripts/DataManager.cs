using Assets.Scripts.Models;
using System.IO;
using System;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;

namespace Assets.Scripts
{
    public static class DataManager
    {
        public static async UniTask SaveWeather(WeatherData weatherData)
        {
            string path = $"{Application.persistentDataPath}/data.data";

            using (FileStream fstream = new FileStream(path, FileMode.OpenOrCreate))
            {
                BinaryFormatter converter = new BinaryFormatter();
                converter.Serialize(fstream, weatherData);
                Console.WriteLine("Текст записан в файл");
            }
            await UniTask.Yield();
        }

        public static async UniTask<WeatherData> LoadWeather()
        {
            Debug.Log($"{Application.persistentDataPath}/data.data");
            string path = $"{Application.persistentDataPath}/data.data";

            if (!File.Exists(path))
            {
                WeatherData weatherData = new WeatherData();
                weatherData.name = "Абакан";
                return weatherData;
            }

            using (FileStream fstream = new FileStream(path, FileMode.Open))
            {
                BinaryFormatter converter = new BinaryFormatter();

                await UniTask.Yield();
                return (WeatherData)converter.Deserialize(fstream);
            }
        }
    }
}
