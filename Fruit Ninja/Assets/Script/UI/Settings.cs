using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using TMPro;

public class Settings : MonoBehaviour
{
    #region FloatVaribles
    [HideInInspector] public float BGMMixerVolume;
    [HideInInspector] public float SFXMixerVolume;
    #endregion

    #region BoolVariables
    private bool isBGMMuted;
    private bool isSFXMuted;
    #endregion

    #region OtherVariables
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider BGMMixerVolumeSlider;
    [SerializeField] private Slider SFXMixerVolumeSlider;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button muteUnmuteBGMButton;
    [SerializeField] private Button muteUnmuteSFXButton;
    [SerializeField] private TextMeshProUGUI BGMMuteUnmuteText;
    [SerializeField] private TextMeshProUGUI SFXMuteUnmuteText;
    GameManager gm;
    #endregion

    void Start()
    {
        gm = GameManager.Instance;
        
        BGMMixerVolume = PlayerPrefs.GetFloat("BGMMixerVolume",0);
        SFXMixerVolume = PlayerPrefs.GetFloat("SFXMixerVolume",0);

        audioMixer.SetFloat("BGM_Volume",BGMMixerVolume);
        audioMixer.SetFloat("SFX_Volume",SFXMixerVolume);

        BGMMixerVolumeSlider.value = BGMMixerVolume;
        SFXMixerVolumeSlider.value = SFXMixerVolume;

        closeButton.onClick.AddListener(() => {
            gameObject.SetActive(false);
        });

        muteUnmuteBGMButton.onClick.AddListener(() => {
            MuteUnmuteBGM();
        });

        muteUnmuteSFXButton.onClick.AddListener(() => {
            MuteUnmuteSFX();
        });
        
        gameObject.SetActive(false);
    }

    public void ChangeBGMVolume(float value)
    {
        audioMixer.SetFloat("BGM_Volume",value);
        PlayerPrefs.SetFloat("BGMMixerVolume",value);
    }

    public void ChangeSFXVolume(float value)
    {
        audioMixer.SetFloat("SFX_Volume",value);
        PlayerPrefs.SetFloat("SFXMixerVolume",value);
    }

    private void MuteUnmuteBGM()
    {
        if(!isBGMMuted) 
        {
            BGMMuteUnmuteText.text = "Unmute";
            audioMixer.SetFloat("BGM_Volume",-80.0f);
            isBGMMuted = true;
        }
        else if(isBGMMuted)
        {
            BGMMuteUnmuteText.text = "Mute";
            audioMixer.SetFloat("BGM_Volume",PlayerPrefs.GetFloat("BGMMixerVolume"));
            isBGMMuted = false;
        }
    }

    private void MuteUnmuteSFX()
    {
        if(!isSFXMuted) 
        {
            SFXMuteUnmuteText.text = "Unmute";
            audioMixer.SetFloat("SFX_Volume",-80.0f);
            isSFXMuted = true;
        }
        else if(isSFXMuted)
        {
            SFXMuteUnmuteText.text = "Mute";
            audioMixer.SetFloat("SFX_Volume",PlayerPrefs.GetFloat("SFXMixerVolume"));
            isSFXMuted = false;
        }
    }
}
