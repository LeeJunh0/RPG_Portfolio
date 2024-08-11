using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
    AudioSource[] audioSources = new AudioSource[(int)Define.ESound.MaxCount];
    Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();
    
    public void Init()
    {
        GameObject root = GameObject.Find("@ESound");
        if(root == null)
        {
            root = new GameObject { name = "@ESound" };
            Object.DontDestroyOnLoad(root);

            string[] soundNames = System.Enum.GetNames(typeof(Define.ESound));
            for (int i = 0; i < soundNames.Length - 1; i++)
            {
                GameObject go = new GameObject { name = soundNames[i] };
                audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }

            audioSources[(int)Define.ESound.Bgm].loop = true;
        }
    }

    public void Play(string path, Define.ESound type = Define.ESound.Effect, float pitch = 1.0f)
    {
        AudioClip audioClip = GetorAddAudioClip(path, type);
        Play(audioClip, type, pitch);
    }

    public void Play(AudioClip audioClip, Define.ESound type = Define.ESound.Effect, float pitch = 1.0f)
    {
        if (audioClip == null) return;

        if (type == Define.ESound.Bgm)
        {
            AudioSource audioSource = audioSources[(int)Define.ESound.Bgm];
            if (audioSource.isPlaying == true)
                audioSource.Stop();

            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            AudioSource audioSource = audioSources[(int)Define.ESound.Effect];
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(audioClip);
        }
    }

    public void Clear()
    {
        foreach(AudioSource audioSource in audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        audioClips.Clear();
    }

    AudioClip GetorAddAudioClip(string key, Define.ESound type = Define.ESound.Effect)
    {
        AudioClip audioclip = null;

        if (type == Define.ESound.Bgm)
        {
            audioclip = Managers.Resource.Load<AudioClip>(key);
            if (audioclip == null)            
                Debug.Log($"AudioClip Missing ! {key}");
        }
        else
        {
            if (audioClips.TryGetValue(key, out audioclip) == false)
            {
                audioclip = Managers.Resource.Load<AudioClip>(key);
                audioClips.Add(key, audioclip);
            }

            if (audioclip == null)           
                Debug.Log($"AudioClip Missing ! {key}");
        }  
        return audioclip;
    }

    public AudioSource GetAudioSource(Define.ESound type) { return audioSources[(int)type]; }
}
