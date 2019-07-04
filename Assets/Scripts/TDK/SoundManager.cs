using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;



/// <summary>
/// 音效控制脚本 单例 
/// </summary>
public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            return instance;
        }

    }
    public AudioSource shortAudio; //短暂音效播放  
    public AudioSource bgmAudio; //bgm音效播放
    /// <summary>
    /// 音效字典集合 key = 路径
    /// </summary>
    private Dictionary<string, AudioClip> soundDic = new Dictionary<string, AudioClip>();

    //音效音量(0-1)
    public float audioVolume = 1;
    //背景音乐音量(0-1)
    public float bgmVolume = 1;
    void Awake()
    {
        instance = this;
        //初始化
        AudioSource[] audios = GetComponents<AudioSource>();
        bgmAudio = audios[0];
       shortAudio = audios[1];
    }
    /// <summary>
    /// 播放BGM
    /// </summary>
    /// <param name="path"></param>
    public void PlayAudio(string path, bool isBgm, int lessTime, int upTime)
    {
        AudioClip clip = LoadAudioClipByPath(path);
        this.StartCoroutine(this.FIFO(clip, isBgm, lessTime, upTime));
    }
    /// <summary>
    /// BGM音量淡入淡出
    /// </summary>
    public IEnumerator FIFO(AudioClip clip, bool isBgm, int lessTime, int upTime)
    {

        //bgm喇叭 有音频资源
        if (isBgm)
        {
            if (bgmAudio.clip != null)
            {
                for (int i = 0; i <= lessTime; i++)
                {
                    bgmAudio.volume = Mathf.Lerp(bgmVolume, 0, 0.1f * i);
                    yield return new WaitForSeconds(0.1f);
                }
            }

            //更换背景音频资源
            //从小到大
            bgmAudio.clip = clip;
            bgmAudio.Play();
            for (int i = 0; i <= upTime; i++)
            {
                bgmAudio.volume = Mathf.Lerp(0, bgmVolume, 0.1f * i);
                yield return new WaitForSeconds(0.1f);
            }
        }
        else
        {

            shortAudio.volume = 1;
            if (shortAudio.clip != null)
            {
                for (int i = 0; i <= lessTime; i++)
                {
                    shortAudio.volume = Mathf.Lerp(audioVolume, 0, 0.1f * i);
                    yield return new WaitForSeconds(0.1f);
                }
            }

            shortAudio.clip = clip;
            shortAudio.PlayOneShot(clip, audioVolume);
        }
    }

    public void CtrlVolumAddLess(bool isBgm, float num, int lessTime = 0, bool isNull = false)
    {
        StartCoroutine((CtrlVolumn(isBgm, num, lessTime, isNull)));
    }
    IEnumerator CtrlVolumn(bool isBgm, float num, int lessTime = 0, bool isNull = false)
    {
        if (isBgm)
        {
            if (bgmAudio.clip != null)
            {
                if (lessTime >= 0)
                {
                    for (int i = 0; i <= lessTime; i++)
                    {
                        bgmAudio.volume = Mathf.Lerp(bgmVolume, 0, 0.1f * i);
                        yield return new WaitForSeconds(0.1f);
                    }
                }
                bgmAudio.volume = num;
                if (isNull == true)
                {
                    bgmAudio.clip = null;
                }
            }
        }
        else
        {
            shortAudio.volume = num;
        }
    }
    /// <summary>
    /// 载入一个音频
    /// </summary>
    private AudioClip LoadAudioClipByPath(string path)
    {

        if (!soundDic.ContainsKey(path))
        {
            AudioClip clip = Resources.Load<AudioClip>("Audio/" + path);
            soundDic.Add(path, clip);
        }
        return soundDic[path];
    }


}
