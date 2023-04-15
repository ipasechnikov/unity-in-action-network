using System.Collections;
using System.Collections.Generic;

using Newtonsoft.Json;

using UnityEngine;

public class WeatherManager : MonoBehaviour, IGameManager
{
    public ManagerStatus Status
    {
        get; private set;
    }

    public float CloudValue
    {
        get; private set;
    }

    private NetworkService network;

    public void Startup(NetworkService service)
    {
        Debug.Log("Weather manager starting...");

        network = service;
        StartCoroutine(network.GetWeatherJson(OnJsonDataLoaded));

        Status = ManagerStatus.Initializing;
    }

    public void OnJsonDataLoaded(string data)
    {
        var weatherInfo = JsonConvert.DeserializeObject<WeatherInfo>(data);
        CloudValue = weatherInfo.Hourly.CloudCover[0] / 100f;
        Debug.Log($"Cloud value: {CloudValue}");

        Messenger.Broadcast(GameEvent.WEATHER_UPDATED);

        Status = ManagerStatus.Started;
    }

    public void LogWeather(string name)
    {
        StartCoroutine(network.LogWeather(name, CloudValue, OnLogged));
    }

    public void OnLogged(string response)
    {
        Debug.Log(response);
    }
}
