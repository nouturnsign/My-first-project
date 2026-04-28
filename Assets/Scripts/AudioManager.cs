using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private string exposedMusicParam;
    [SerializeField] private Slider audioSlider;

    public void SetMusicVolume()
    {
        // DB is logarithmic
        float volumeInDB = Mathf.Log10(Mathf.Max(audioSlider.value, 0.0001f)) * 20f;
        audioMixer.SetFloat(exposedMusicParam, volumeInDB);
    }
}
