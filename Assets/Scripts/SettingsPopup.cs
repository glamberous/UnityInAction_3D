using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPopup : MonoBehaviour
{
    [SerializeField] private Slider speedSlider = null;
    [SerializeField] private AudioClip sound = null;

    public void OnSoundToggle()
    {
        Managers.Audio.soundMute = !Managers.Audio.soundMute;
        Managers.Audio.PlaySound(sound);
    }

    public void OnSoundValue(float volume) => Managers.Audio.soundVolume = volume;
    public void Start() => speedSlider.value = PlayerPrefs.GetFloat("speed", 1); //Gets the last save speed value, sets 1 if it doesn't exist
    public void Open() => gameObject.SetActive(true);
    public void Close() => gameObject.SetActive(false);
    public void OnSubmitName(string name) => Debug.Log(name);

    public void OnSpeedValue(float speed)
    {
        PlayerPrefs.SetFloat("speed", speed);
        Messenger<float>.Broadcast(GameEvent.SPEED_CHANGED, speed);
    }

    public void OnPlayMusic(int selector)
    {
        Managers.Audio.PlaySound(sound);

        switch (selector)
        {
            case 1:
                Managers.Audio.PlayLevelMusic();
                break;
            case 2:
                Managers.Audio.PlayIntroMusic();
                break;
            default:
                Managers.Audio.StopMusic();
                break;
        }
    }

    public void OnMusicToggle()
    {
        Managers.Audio.musicMute = !Managers.Audio.musicMute;
        Managers.Audio.PlaySound(sound);
    }

    public void OnMusicValue(float volume)
    {
        Managers.Audio.musicVolume = volume;
    }
}
