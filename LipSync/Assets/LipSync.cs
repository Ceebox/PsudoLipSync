using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Microphone;

public class LipSync : MonoBehaviour
{
    private AudioSource audio;
    public GameObject mouth;
    
    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.clip = Microphone.Start("Microphone (USB Audio Device)", true, 10, 44100);
        audio.loop = true;
        while (!(Microphone.GetPosition(null) > 0)){}
        audio.Play();
    }

    void Update()
    {
        //Debug.Log(audio.clip.frequency);

        float[] spectrum = new float[64];
        audio.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);

        // Getting a peak on the last 128 samples
        float levelMax = 0;
        for (int i = 0; i < 64; i++) {
            float wavePeak = spectrum[i] * spectrum[i];
            if (levelMax < wavePeak) {
                levelMax = wavePeak;
                levelMax *= 600;
                levelMax *= (20 ^ 1000);
            }
        }
                
        for (int i = 1; i < spectrum.Length - 1; i++)
        {
            var zee =  1.5f * (Mathf.SmoothStep(0, (spectrum[i] * (10 ^ 50000)), 0.1f) * 10) - 0.1f;

            if (levelMax > 0.5f)
            {
                if (zee > 0)
                {
                    if ((spectrum[i] * (10 ^ 50000)) <= 1 && (spectrum[i] * (10 ^ 50000)) > 0.25f)
                    {
                        mouth.transform.localScale = new Vector3(mouth.transform.localScale.x, mouth.transform.localScale.y, zee);
                    }
                    else{
                        mouth.transform.localScale = new Vector3(mouth.transform.localScale.x, mouth.transform.localScale.y, mouth.transform.localScale.z);
                    }
                }
            }
        }
    }
}
