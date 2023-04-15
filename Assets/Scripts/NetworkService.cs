using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkService
{
    private const string jsonApi = "https://api.open-meteo.com/v1/forecast?latitude=52.52&longitude=13.41&hourly=cloudcover";
    private const string webImage = "https://upload.wikimedia.org/wikipedia/commons/c/c5/Moraine_Lake_17092005.jpg";
    private const string localApi = "http://localhost/uia/api.php";

    private IEnumerator CallAPI(string url, WWWForm form, Action<string> callback)
    {
        var request = form == null
            ? UnityWebRequest.Get(url)
            : UnityWebRequest.Post(url, form);

        using (request)
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
        return CallAPI(jsonApi, null, callback);
    }

    public IEnumerator LogWeather(string name, float cloudValue, Action<string> callback)
    {
        var form = new WWWForm();
        form.AddField("message", name);
        form.AddField("cloud_value", cloudValue.ToString());
        form.AddField("timestamp", DateTime.UtcNow.Ticks.ToString());

        return CallAPI(localApi, form, callback);

    }

    public IEnumerator DownloadImage(Action<Texture2D> callback)
    {
        var request = UnityWebRequestTexture.GetTexture(webImage);
        yield return request.SendWebRequest();
        callback(DownloadHandlerTexture.GetContent(request));
    }
}
