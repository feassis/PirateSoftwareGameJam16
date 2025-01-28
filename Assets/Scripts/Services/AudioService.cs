using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioService : MonoBehaviour
{
    [SerializeField] private AudioSource BGMAudioSource;
    [SerializeField] private List<AudioSource> SFXAudioSourceList;
    [SerializeField] private List<BGMTracks> BGMTracksList;
    [SerializeField] private List<SFXTrack> SFXTracksList;

    private int index = 0;

    [Serializable]
    private class BGMTracks
    {
        public BGMType BGMType;
        public AudioClip AudioClip;
    }

    [Serializable]
    private class SFXTrack
    {
        public SFXType SFXType;
        public AudioClip AudioClip;
    }

    public void PlayBGM(BGMType bgmType)
    {
        BGMTracks bgmTrack = BGMTracksList.Find(x => x.BGMType == bgmType);

        if (bgmTrack != null)
        {
            BGMAudioSource.clip = bgmTrack.AudioClip;
            BGMAudioSource.Play();
        }
        else
        {
            Debug.LogWarning("BGM not found.");
        }
    }

    public void PlaySFXAtPosition(SFXType sFXType, Vector3 position)
    {
        SFXTrack sfxTrack = SFXTracksList.Find(x => x.SFXType == sFXType);

        if (sfxTrack != null)
        {
            AudioSource audioSource = SFXAudioSourceList.Find(x => !x.isPlaying);

            if (audioSource != null)
            {
                audioSource.transform.position = position;
                audioSource.clip = sfxTrack.AudioClip;
                audioSource.Play();
            }
            else
            {
                Debug.LogWarning("No available audio source.");
            }
        }
        else
        {
            Debug.LogWarning("SFX not found.");
        }
    }

    public void PlaySFX(SFXType sfxType)
    {
        SFXTrack sfxTrack = SFXTracksList.Find(x => x.SFXType == sfxType);

        if (sfxTrack != null)
        {
            AudioSource audioSource = SFXAudioSourceList.Find(x => !x.isPlaying);

            if (audioSource != null)
            {
                audioSource.clip = sfxTrack.AudioClip;
                audioSource.Play();
            }
            else
            {
                Debug.LogWarning("No available audio source.");
            }
        }
        else
        {
            Debug.LogWarning("SFX not found.");
        }
    }
}

public enum BGMType
{
    Main
}

public enum SFXType
{
    PlayerShoot,
    PlayerHit,
    EnemyShoot,
    EnemyGranade
}
