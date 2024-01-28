using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager m_instance;

    public static AudioManager instance
    {
        get 
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<AudioManager>();
            }
            return m_instance; 
        }
    }

    public enum AvailableSFX
    {
        Slap,
        Slam,
        Fart,
        TOTAL
    }

    public AudioSource slapAS;
    public AudioSource slamAS;
    public AudioSource fartAS;

    public void PlaySFX(AvailableSFX sfx)
    {
        switch (sfx)
        {
            case AvailableSFX.Slap:
                if (!slapAS.isPlaying)
                {
                    slapAS.Play();
                }
                break;
            case AvailableSFX.Slam:
                if (!slamAS.isPlaying)
                {
                    slamAS.Play();
                }
                break;
            case AvailableSFX.Fart:
                fartAS.Play();
                break;
        }
    }
}
