using System;
using System.Collections;
using System.Collections.Generic;

using Newtonsoft.Json;

using UnityEngine;

public class WeatherHourly
{
    [JsonProperty("time")]
    public string[] Time
    {
        get; set;
    }

    [JsonProperty("cloudcover")]
    public int[] CloudCover
    {
        get; set;
    }
}


public class WeatherInfo
{
    [JsonProperty("hourly")]
    public WeatherHourly Hourly
    {
        get; set;
    }
}
