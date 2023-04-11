using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherController : MonoBehaviour
{
    [SerializeField] Material sky;
    [SerializeField] Light sun;

    private float fullIntensity;
    private float cloudValue = 0f;

    void OnEnable()
    {
        Messenger.AddListener(GameEvent.WEATHER_UPDATED, OnWeatherUpdated);
    }

    void OnDisable()
    {
        Messenger.RemoveListener(GameEvent.WEATHER_UPDATED, OnWeatherUpdated);
    }

    // Start is called before the first frame update
    void Start()
    {
        fullIntensity = sun.intensity;
    }

    private void SetOvercast(float value)
    {
        sky.SetFloat("_Blend", value);
        var newIntensity = fullIntensity - (fullIntensity * value);
        newIntensity = Mathf.Clamp(newIntensity, 0, fullIntensity);
        sun.intensity = newIntensity;
    }

    private void OnWeatherUpdated()
    {
        SetOvercast(Managers.Weather.CloudValue);
    }
}
