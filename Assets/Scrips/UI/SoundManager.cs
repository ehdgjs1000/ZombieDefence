using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    //오디오소스 풀링
    public AudioSource[] audioPool;

    public float soundsVolume;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        audioPool = new AudioSource[100];
    }

    public void ChangeVolume(float volume) //type = 1 Bgm / type = 0 SFX
    {
        soundsVolume = volume;
    }

    private IEnumerator DestroyAudio(GameObject audioGO, float clipLength)
    {
        yield return new WaitForSeconds(clipLength);
        Destroy(audioGO);
    }
    public void PlaySound(AudioClip _clip)
    {
        for (int i = 0; i < audioPool.Length; i++)
        {
            if (audioPool[i] == null || !audioPool[i].isPlaying)
            {
                GameObject go = new GameObject { name = _clip.name };
                audioPool[i] = go.AddComponent<AudioSource>();
                audioPool[i].transform.position = Camera.main.transform.position;
                audioPool[i].spatialBlend = 0.0f;
                audioPool[i].PlayOneShot(_clip, soundsVolume);

                return;
            }
        }
        return;
    }
   


}
