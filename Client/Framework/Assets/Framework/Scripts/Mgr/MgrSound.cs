using UnityEngine;

/// <summary>
/// 管理声音，继承Mono单例，挂到Init对象上
/// </summary>
public class MgrSound : SingletonMonoBehaviour<MgrSound>
{
    private AudioSource _asMusic;//背景音乐
    private AudioSource _asEffect;//音效
    private AudioSource _asVoice;//语音

    //背景音乐的音量大小
    public float FMusicVolume
    {
        get { return DataLocal.Instance.GameMusic; }
        set
        {
            if (value <= 0)
            {
                _asMusic.volume = 0;
                _asMusic.Pause();
            }
            else
            {
                _asMusic.volume = value;
                if (!_asMusic.isPlaying)
                {
                    _asMusic.Play();
                }
            }
            DataLocal.Instance.GameMusic = value;
        }
    }

    //音效的音量大小
    public float FEffectVolume
    {
        get { return DataLocal.Instance.GameEffect; }
        set
        {
            if (value <= 0)
            {
                _asEffect.volume = 0;
            }
            else
            {
                _asEffect.volume = value;
            }
            DataLocal.Instance.GameEffect = value;
        }
    }

    //语音的音量大小
    public float FVoiceVolume
    {
        get { return DataLocal.Instance.GameVoice; }
        set
        {
            if (value <= 0)
            {
                _asVoice.volume = 0;
            }
            else
            {
                _asVoice.volume = value;
            }
            DataLocal.Instance.GameVoice = value;
        }
    }

    //震动等级
    public int NGameVibration
    {
        get { return DataLocal.Instance.GameVibration; }
        set { DataLocal.Instance.GameVibration = value; }
    }


    public override void DoAwake()
    {
        base.DoAwake();
        //添加声音对象
        GameObject goSound = new GameObject("MgrSound");
        goSound.transform.SetParent(transform);
        //添加背景音乐、音效、语音的音频组件
        _asMusic = goSound.AddComponent<AudioSource>();
        _asMusic.volume = FMusicVolume;

        _asEffect = goSound.AddComponent<AudioSource>();
        _asEffect.volume = FEffectVolume;

        _asVoice = goSound.AddComponent<AudioSource>();
        _asVoice.volume = FVoiceVolume;
    }

    /// <summary>
    /// 播放背景音乐，属于背景音乐
    /// </summary>
    /// <param name="sPath"></param>
    /// <param name="bLoop"></param>
    public void PlaySoundForBgMusic(string sPath, bool bLoop)
    {
        MgrAsset.Instance.LoadAudioClipAsync(sPath, clip =>
        {
            if (clip == null)
                return;
            if (_asMusic.isPlaying)
            {
                _asMusic.Stop();
            }
            _asMusic.clip = clip;
            _asMusic.loop = bLoop;
            _asMusic.Play();
        });
    }

    /// <summary>
    /// 播放Ui声音，属于音效
    /// </summary>
    /// <param name="sPath"></param>
    /// <param name="bCover"></param>
    public void PlaySoundForUi(string sPath, bool bCover = false)
    {
        AudioClip clip = MgrAsset.Instance.LoadAudioClipSync(sPath);
        _asEffect.loop = false;
        if (bCover)
        {
            if (_asEffect.isPlaying)
            {
                _asEffect.Stop();
            }
            _asEffect.clip = clip;
            _asEffect.Play();
        }
        else
        {
            _asEffect.PlayOneShot(clip);
        }
    }

    /// <summary>
    /// 播放特效音效，考虑到有可能会播放相同种类但是音频不同的音效
    /// </summary>
    /// <param name="sPath">路径</param>
    /// <param name="nSex">性别</param>
    /// <param name="nIndex">音频下标</param>
    /// <param name="nMaxIndex">音频最大下标</param>
    /// <param name="bRandom">是否在最大下标中进行随机</param>
    /// <param name="bCover">是否覆盖</param>
    public void PlaySoundForEffect(string sPath, int nSex, int nIndex, int nMaxIndex, bool bRandom = false, bool bCover = false)
    {
        int nEndIndex = 0;
        if (bRandom)
        {
            nEndIndex = Random.Range(0, nMaxIndex);
        }
        else
        {
            nEndIndex = nIndex;
        }
        sPath = nEndIndex == 0 ? sPath : sPath + nEndIndex;

        if (nSex == 1)
        {
            //男
            sPath += "_Man";
        }
        else if (nSex == 2)
        {
            //女
            sPath += "_Woman";
        }
        else
        {
            //未设置性别，默认男性
            sPath += "_Man";
        }

        AudioClip clip = MgrAsset.Instance.LoadAudioClipSync(sPath);
        _asEffect.loop = false;
        if (bCover)
        {
            if (_asEffect.isPlaying)
            {
                _asEffect.Stop();
            }
            _asEffect.clip = clip;
            _asEffect.Play();
        }
        else
        {
            _asEffect.PlayOneShot(clip);
        }
    }

    /// <summary>
    /// 播放游戏中的音效
    /// </summary>
    /// <param name="sPath"></param>
    /// <param name="bCover"></param>
    public void PlaySoundForGame(string sPath, bool bCover = false)
    {
        AudioClip clip = MgrAsset.Instance.LoadAudioClipSync(sPath);
        _asEffect.loop = false;
        if (bCover)
        {
            if (_asEffect.isPlaying)
            {
                _asEffect.Stop();
            }
            _asEffect.clip = clip;
            _asEffect.Play();
        }
        else
        {
            _asEffect.PlayOneShot(clip);
        }
    }

    /// <summary>
    /// 播放讲话的声音，这里指快捷语，暂不包括语音
    /// </summary>
    /// <param name="sPath"></param>
    /// <param name="bCover"></param>
    public void PlaySoundForSpeak(string sPath, bool bCover = false)
    {
        AudioClip clip = MgrAsset.Instance.LoadAudioClipSync(sPath);
        _asVoice.loop = false;
        if (bCover)
        {
            if (_asVoice.isPlaying)
            {
                _asVoice.Stop();
            }
            _asVoice.clip = clip;
            _asVoice.Play();
        }
        else
        {
            _asVoice.PlayOneShot(clip);
        }
    }

    /// <summary>
    /// 清空数据
    /// </summary>
    public override void DoClearData()
    {
        base.DoClearData();
        _asMusic.Stop();
        _asEffect.Stop();
        _asVoice.Stop();
    }
}