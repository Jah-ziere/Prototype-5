using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicSource;
    public Slider volumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        musicSource.Play();
        if (volumeSlider != null)
         {
            volumeSlider.onValueChanged.AddListener(SetVolume);
            volumeSlider.value = musicSource.volume; // sync with current volume
        }
    }

    public void SetVolume(float volume)
    {
        musicSource.volume = volume;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
