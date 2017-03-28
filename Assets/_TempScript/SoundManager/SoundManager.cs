using DateDeclare;
using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager gSingleton;
    public bool m_bPlay = true;
    public AudioClip mAwardCoinClip;
    public AudioClip mButtonClip;
    public AudioClip mChangeGameClip;
    public AudioClip mGameTipClip;
    public AudioClip mGetExpClip;
    public AudioClip mInterfaceClip;
    public AudioClip mInterfaceSuccClip;
    public AudioClip mPageTurning;
    public AudioClip mUnOpenClip;
    public AudioSource MusicSource;
    private float MusicVolume = 0.5f;
    public AudioSource SoundSource;
    private float SoundVolume = 0.5f;

    private void Awake()
    {
        if (gSingleton == null)
        {
            gSingleton = this;
        }
        MusicSource = gameObject.GetComponent<AudioSource>();
        SoundSource = gameObject.GetComponent<AudioSource>();
        this.SoundSource.volume = this.SoundVolume;
        this.MusicSource.volume = this.MusicVolume;
    }

    public static SoundManager GetSingleton()
    {
        return gSingleton;
    }

    public void playButtonSound(MusicType soundtype /*= 1*/)
    {
 
            AudioClip mButtonClip = null;
            switch (soundtype)
            {
                case MusicType.Type_Button:
                    mButtonClip = this.mButtonClip;
                    break;

                case MusicType.Type_AwardCoin:
                    mButtonClip = this.mAwardCoinClip;
                    break;

                case MusicType.Type_GetExp:
                    mButtonClip = this.mGetExpClip;
                    break;

                case MusicType.Type_Interface:
                    mButtonClip = this.mInterfaceClip;
                    break;

                case MusicType.Type_InterfaceSucc:
                    mButtonClip = this.mInterfaceSuccClip;
                    break;

                case MusicType.Type_ChangeGame:
                    mButtonClip = this.mChangeGameClip;
                    break;

                case MusicType.Type_UnOpen:
                    mButtonClip = this.mUnOpenClip;
                    break;

                case MusicType.Type_GameTip:
                    mButtonClip = this.mGameTipClip;
                    break;

                case MusicType.Type_PageTurning:
                    mButtonClip = this.mPageTurning;
                    break;
            }
            if ((this.SoundSource != null) && (mButtonClip != null))
            {
                if (this.SoundSource.isPlaying)
                {
                    this.SoundSource.Stop();
                }
                Debug.Log(mButtonClip.name);
                this.SoundSource.enabled = true;
                this.SoundSource.clip = mButtonClip;
                this.SoundSource.volume = this.SoundVolume;
                this.SoundSource.Play();
                //关闭了背景音乐的循环播放
                this.SoundSource.loop = false;
            }
            else if (this.SoundSource == null)
            {
            }
        
    }

    public void PlayMusicSource()
    {
        this.MusicSource.enabled = true;
        if (this.MusicSource != null)
        {
            if (this.MusicSource.isPlaying)
            {
                this.MusicSource.Stop();
            }
            this.MusicSource.loop = true;
            this.MusicSource.Play();
        }
        else if (this.MusicSource == null)
        {
        }
    }

    public void SetMusicVolume(float volume)
    {
        if ((volume > 1f) || (volume < 0f))
        {
            volume = 0.5f;
        }
        this.MusicVolume = volume;
        this.MusicSource.volume = volume;
    }

    public void SetSoundVolume(float volume)
    {
        if ((volume > 1f) || (volume < 0f))
        {
            volume = 0.5f;
        }
        this.SoundVolume = volume;
        this.SoundSource.volume = volume;
    }

    public void StopAudioSource(AudioSource audioSource)
    {
        if (audioSource != null)
        {
            audioSource.Stop();
            audioSource.enabled = false;
        }
    }
}

