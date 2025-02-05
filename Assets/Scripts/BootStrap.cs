using System.Collections.Generic;
using UnityEngine;

public class BootStrap : MonoBehaviour
{
    [field: SerializeField] WeatherManager _weatherManager;
    [field: SerializeField] SityManager _sityManager;

    private void Start()
    {
        _weatherManager.BootInit();
        _sityManager.BootInit();
    }
}
