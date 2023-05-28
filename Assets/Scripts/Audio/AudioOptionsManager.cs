using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore;
using UnityEngine.UI;

public class AudioOptionsManager : MonoBehaviour
{
    
    public static float musicVolume { get; private set; }
    public static float soundEffectsVolume { get; private set; }

    [SerializeField] private Text musicSliderText = null;
    [SerializeField] private Text soundEffectsSliderText = null;
    public void OnMusicSliderValueChange(float value)
    {
        musicVolume = value;
        //musicSliderText.text = Math.Floor((value * 100)).ToString();
        AudioManager.instance.updateMixerVolume();
    }

    public void OnSoundEffectSliderValueChange(float value)
    {
        soundEffectsVolume = value;
       // soundEffectsSliderText.text = Math.Floor((value * 100)).ToString();
        AudioManager.instance.updateMixerVolume();
    }
   
  



}// end of class
