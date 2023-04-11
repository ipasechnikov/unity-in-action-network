using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkService
{
    private const string jsonApi = "https://api.open-meteo.com/v1/forecast?latitude=52.52&longitude=13.41&hourly=cloudcover";

    private IEnumerator CallAPI(string url, Action<string> callback)
    {
        using (var request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            switch (request.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                    Debug.LogError($"network problem: {request.error}");
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError($"response error: {request.error}");
                    break;
                default:
                    callback(request.downloadHandler.text);
                    break;
            }
        }
    }

    public IEnumerator GetWeatherJson(Action<string> callback)
    {
        return CallAPI(jsonApi, callback);
    }
}
