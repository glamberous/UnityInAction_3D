using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ImageManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }

    private NetworkService _network;

    private Texture2D _webImage = null;

    public void Startup(NetworkService service)
    {
        Debug.Log("Images manager starting...");

        _network = service;

        status = ManagerStatus.Started;
    }

    public void GetWebImage(Action<Texture2D> callback)
    {
        if (_webImage == null)
            StartCoroutine(_network.GetImage((Texture2D image) => { _webImage = image; callback(_webImage); Debug.Log("image cached."); }));
        else
            callback(_webImage);
    }
}
