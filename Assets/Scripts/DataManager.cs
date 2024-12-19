using Assets.Scripts.Models;
using System.IO;
using System.Text;
using System;
using UnityEngine;
using Newtonsoft.Json;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts
{
    public static class DataManager
    {
        public static async UniTask SaveWeather(WeatherData weatherData)
        {
            string path = $"{Application.persistentDataPath}/data.data";

            using (FileStream fstream = new FileStream(path, FileMode.OpenOrCreate))
            {
                // преобразуем строку в байты
                string json = JsonConvert.SerializeObject(weatherData);
                byte[] buffer = Encoding.Default.GetBytes(json);
                // запись массива байтов в файл
                await fstream.WriteAsync(buffer, 0, buffer.Length);
                Console.WriteLine("Текст записан в файл");
            }
        }

        public static async UniTask<WeatherData> LoadWeather()
        {
            string path = $"{Application.persistentDataPath}/data.data";
            if (!File.Exists(path)) return new WeatherData();

            using (FileStream fstream = File.OpenRead(path))
            {
                // выделяем массив для считывания данных из файла
                byte[] buffer = new byte[fstream.Length];
                // считываем данные
                await fstream.ReadAsync(buffer, 0, buffer.Length);
                // декодируем байты в строку
                string textFromFile = Encoding.Default.GetString(buffer);

                return JsonConvert.DeserializeObject<WeatherData>(textFromFile);
            }
        }
    }
}
