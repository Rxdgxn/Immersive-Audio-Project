using UnityEngine;
using UnityEngine.UI;
using FMOD.Studio;
using FMODUnity;
using System;

public class AudioMixerController : MonoBehaviour
{
    [Header("UI Sliders")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider sonificationSlider;

    private VCA masterVCA;
    private VCA musicVCA;
    private VCA sfxVCA;
    private VCA sonificationVCA;

    private void Awake()
    {
        masterVCA = RuntimeManager.GetVCA("vca:/Master");
        musicVCA = RuntimeManager.GetVCA("vca:/Music");
        sfxVCA = RuntimeManager.GetVCA("vca:/SFX");
        sonificationVCA = RuntimeManager.GetVCA("vca:/Sonification");
    }

    private void OnEnable()
    {
        if (masterSlider) masterSlider.onValueChanged.AddListener(OnMasterVolumeChanged);
        if (musicSlider) musicSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        if (sfxSlider) sfxSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
        if (sonificationSlider) sonificationSlider.onValueChanged.AddListener(OnSonificationVolumeChanged);
    }

    private void OnDisable()
    {
        if (masterSlider) masterSlider.onValueChanged.RemoveListener(OnMasterVolumeChanged);
        if (musicSlider) musicSlider.onValueChanged.RemoveListener(OnMusicVolumeChanged);
        if (sfxSlider) sfxSlider.onValueChanged.RemoveListener(OnSFXVolumeChanged);
        if (sonificationSlider) sonificationSlider.onValueChanged.RemoveListener(OnSonificationVolumeChanged);
    }

    public void OnMasterVolumeChanged(float value)
    {
        masterVCA.setVolume(value);
    }

    public void OnMusicVolumeChanged(float value)
    {
        musicVCA.setVolume(value);
    }

    public void OnSFXVolumeChanged(float value)
    {
        sfxVCA.setVolume(value);
    }

    private void OnSonificationVolumeChanged(float value)
    {
        sonificationVCA.setVolume(value);
    }
}