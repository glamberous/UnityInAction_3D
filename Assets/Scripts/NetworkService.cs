using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class NetworkService
{
    private const string xmlApi = "http://api.openweathermap.org/data/2.5/weather?q=Seattle,us&mode=xml&APPID=38c03891715ccd0acef0a89fd8fc7874";
    private const string jsonApi = "http://api.openweathermap.org/data/2.5/weather?q=Seattle,us&APPID=38c03891715ccd0acef0a89fd8fc7874";
    private const string webImage = "https://i.kym-cdn.com/photos/images/facebook/001/053/890/c2d.jpg";
    private const string localApi = "http://localhost/uia/api.php";

    private IEnumerator CallAPI(string url, WWWForm form, Action<string> callback)
    {
        using (UnityWebRequest request = (form == null) ? UnityWebRequest.Get(url) : UnityWebRequest.Post(url, form))
        {
            yield return request.Send();

            if (request.isNetworkError)
                Debug.LogError("network problem: " + request.error);
            else if (request.responseCode != (long)System.Net.HttpStatusCode.OK)
                Debug.LogError("response error: " + request.responseCode);
            else
                callback(request.downloadHandler.text);
        }
    }

    private IEnumerator DownloadImage(string url, Action<Texture2D> callback)
    {
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(url))
        {
            yield return request.Send();

            if (request.isNetworkError)
                Debug.LogError("netowrk problem: " + request.error);
            else if (request.responseCode != (long)System.Net.HttpStatusCode.OK)
                Debug.LogError("response error: " + request.responseCode);
            else
                callback(DownloadHandlerTexture.GetContent(request));
        }
    }

    public IEnumerator GetImage(Action<Texture2D> callback) => DownloadImage(webImage, callback);
    public IEnumerator GetWeatherJSON(Action<string> callback) => CallAPI(jsonApi, null, callback);
    public IEnumerator GetWeatherXML(Action<string> callback) => CallAPI(xmlApi, null, callback);

    public IEnumerator LogWeather(string name, float cloudValue, Action<string> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("message", name);
        form.AddField("cloud_value", cloudValue.ToString());
        form.AddField("timestamp", DateTime.UtcNow.Ticks.ToString());

        return CallAPI(localApi, form, callback);
    }
}
