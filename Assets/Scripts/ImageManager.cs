using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageManager : MonoBehaviour, IGameManager
{
    private NetworkService network;
    private Texture2D webImage;

    public ManagerStatus Status
    {
        get; private set;
    }

    public void Startup(NetworkService service)
    {
        Debug.Log("Image manager starting...");

        network = service;

        Status = ManagerStatus.Started;
    }

    public void GetWebImage(Action<Texture2D> callback)
    {
        if (webImage == null)
            StartCoroutine(network.DownloadImage(
                image =>
                {
                    webImage = image;
                    callback(webImage);
                }
            ));
        else
            callback(webImage);
    }
}
