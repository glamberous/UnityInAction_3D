using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherController : MonoBehaviour
{
    [SerializeField] private Material sky = null;
    [SerializeField] private Light sun = null;

    private float _fullIntensity;

    private void Awake() => Messenger.AddListener(GameEvent.WEATHER_UPDATED, OnWeatherUpdated);
    private void OnDestroy() => Messenger.RemoveListener(GameEvent.WEATHER_UPDATED, OnWeatherUpdated);
    private void Start() => _fullIntensity = sun.intensity;
    private void OnWeatherUpdated() => SetOvercast(Managers.Weather.cloudValue);

    private void SetOvercast(float value)
    {
        sky.SetFloat("_Blend", value);
        sun.intensity = _fullIntensity - (_fullIntensity * value);
    }
}