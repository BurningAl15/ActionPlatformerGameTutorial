using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance;

    public AudioClip audioClipShoot;
    public AudioClip audioClipJump;
    public AudioClip audioClipExplosion;

    public AudioClip audioClipShootD;
    public AudioClip audioClipExplosionD;

    public AudioSource audioSource;
    public AudioSource audioExplosion;
    private void Awake()
    {
        if (SoundManager.instance == null)
        {
            SoundManager.instance = this;
        }
        else if (SoundManager.instance != this)
        {
            Destroy(gameObject);
        }
    }

    //public void ChangeCharacter()
    //{
    //    PlayAudioClip(audioClipCoin);
    //}

    public void PlayJump()
    {
        PlayAudioClip(audioClipJump);
    }

    public void PlayShoot(int val)
    {
        audioSource.pitch = Random.Range(0.5f, 1.5f);
        if(val==0)
            PlayAudioClip(audioClipShoot);
        else if(val==1)
            PlayAudioClip(audioClipShootD);
    }

    public void PlayExplosion(int val)
    {
        if (val == 0)
            PlayExplosionClip(audioClipExplosion);
        else if (val == 1)
            PlayExplosionClip(audioClipExplosionD);
    }

    private void PlayAudioClip(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    private void PlayExplosionClip(AudioClip audioClip)
    {
        audioExplosion.clip = audioClip;
        audioExplosion.Play();
    }

    private void OnDestroy()
    {
        if (SoundManager.instance == this)
        {
            SoundManager.instance = null;
        }
    }
}
