using System;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    private static readonly int MAX_NUM_OF_ONESHOT_AUDIOSOURCES = 8;
    public static readonly string BG_MUSIC_TAG = "BGMUSIC";

    [Serializable]
    public class SoundInfo
    {
        public Sound Sound;
        public AudioClip Clip;

        [Range(0f,5f)]
        public float Volume = 1f;
        [Range(0f,3f)]
        public float Pitch = 1f;
        public bool IsRandomPitch;
        public float PitchDelta;
        public bool IsPlayOnAwake;
        public bool IsLoop;
        public string Tag;
    }


    // ENUM FOR SOUNDS USED IN GAME
    public enum Sound
    {
        None,
        Click,
        WinEffect,
        MoneyChange,
        AnswerCorrect,
        AnswerIncorrect,
        ClickSwitch,
        CantBuy,
        PlayButton

    }

    
    private static GameObject _oneShotGameObject;

    private static AudioSource[] _reservedOneShotAudioSources;
 


    public static void Initialize()
    {
        _reservedOneShotAudioSources = new AudioSource[MAX_NUM_OF_ONESHOT_AUDIOSOURCES - 1];

        SetPlayOnAwakeSounds();
        CreateSoundManagerGameObject();
    }

    private static void CreateSoundManagerGameObject()
    {
        _oneShotGameObject = new GameObject("SoundManagerGeneral");

        // Add AudioSouece components
        for (int i = 0; i < MAX_NUM_OF_ONESHOT_AUDIOSOURCES - 1; i++)
        {
            _reservedOneShotAudioSources[i] = _oneShotGameObject.AddComponent<AudioSource>();
        }
    }



    private static void SetPlayOnAwakeSounds()
    {
        foreach (var soundInfo in GameAssets.Instance.SoundInfoArray)
        {
            if (soundInfo.IsPlayOnAwake == true)
            {
                PlaySoundOnAudioSourceSeparateObject(soundInfo.Sound);
            }
        }
    }


    public static void PlaySound(Sound sound, bool doCreateSeparateObject = false)
    {

        if (doCreateSeparateObject)
        {
            PlaySoundOnAudioSourceSeparateObject(sound);
            
        }
        else
        {

            // Try to play sound on first non-occupied audiosource
            foreach (var reserevedAudioSource in _reservedOneShotAudioSources)
            {
                if (reserevedAudioSource.isPlaying)
                {
                    continue;
                }
                else
                {
                    PlaySoundOnAudioSourceOneShot(sound, reserevedAudioSource);
                    return;
                }
            }

            // if all are occupied: create temporary audiosource and delete after usage
            var tempAudioSource = _oneShotGameObject.AddComponent<AudioSource>();
            PlaySoundOnAudioSourceOneShot(sound, tempAudioSource, true);
        }

    }

    private static void PlaySoundOnAudioSourceSeparateObject(Sound sound)
    {
        GameObject newGO = new GameObject($"SoundManager GO ({sound})");
        AudioSource newGOAudioSource = newGO.AddComponent<AudioSource>();

        SoundInfo soundInfo = GetSoundInfoOfSound(sound);

        try
        {
            AssignSoundInfoToAudioSource(GetSoundInfoOfSound(sound), newGOAudioSource);

            newGOAudioSource.loop = soundInfo.IsLoop;

            newGOAudioSource.Play();

            //newGO.AddComponent<DoNotDestroyOnLoad>();

            if (!soundInfo.IsLoop)
            {
                GameObject.Destroy(newGO, newGOAudioSource.clip.length);
            }

            

            

        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }

    }


    private static void PlaySoundOnAudioSourceOneShot(Sound sound, AudioSource audioSource, bool destroyAfterEnd = false)
    {
        try
        {
            AssignSoundInfoToAudioSource(GetSoundInfoOfSound(sound), audioSource);
            

            audioSource.PlayOneShot(audioSource.clip);

            if (destroyAfterEnd)
            {
                GameObject.Destroy(audioSource, audioSource.clip.length);
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }


   

   
   


    private static SoundInfo GetSoundInfoOfSound(Sound sound)
    {
        foreach (var soundInfo in GameAssets.Instance.SoundInfoArray)
        {
            if (soundInfo.Sound == sound)
            {
                return soundInfo;
            }
        }

        throw new ArgumentException(nameof(sound));
    }

    private static void AssignSoundInfoToAudioSource(SoundInfo soundInfo, AudioSource audioSource)
    {
        audioSource.volume = soundInfo.Volume;

        if (soundInfo.IsRandomPitch)
        {
            audioSource.pitch = UnityEngine.Random.Range(soundInfo.Pitch - soundInfo.PitchDelta, soundInfo.Pitch + soundInfo.PitchDelta);
        }
        else
        {
            audioSource.pitch = soundInfo.Pitch;
        }
 
        audioSource.clip = soundInfo.Clip;

        audioSource.loop = soundInfo.IsLoop;
        audioSource.playOnAwake = soundInfo.IsPlayOnAwake;
    }

}
