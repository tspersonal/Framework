using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public enum DownType
{
    None,
    Text,
    Texture2D,
    AssetBundle,
}

/// <summary>
/// 下载资源的单例类，挂在Init
/// </summary>
public class MgrDownLoad : SingletonMonoBehaviour<MgrDownLoad>
{
    /// <summary>
    /// 缓存文本
    /// </summary>
    private Dictionary<string, string> _dicCacheText = new Dictionary<string, string>();
    /// <summary>
    /// 缓存2D纹理
    /// </summary>
    private Dictionary<string, Texture2D> _dicCacheTexture2D = new Dictionary<string, Texture2D>();
    /// <summary>
    /// 缓存AssetBundle
    /// </summary>
    private Dictionary<string, AssetBundle> _dicCacheAssetBundle = new Dictionary<string, AssetBundle>();


    #region 下载文本

    public void DownLoadText(string sPath, Action<string> fun, bool bCover = false)
    {
        string sMd5 = Tool.CalculateMd5Hash(sPath);

        if (_dicCacheText.ContainsKey(sMd5))
        {
            if (!bCover)
            {
                fun(_dicCacheText[sMd5]);
                return;
            }
        }
        StartCoroutine(IeDownLoadText(sPath, sMd5, fun));
    }

    private IEnumerator IeDownLoadText(string sPath, string sMd5, Action<string> fun)
    {
        WWW www = new WWW(sPath);
        yield return www;
        if (www.isDone && www.error == null)
        {
            string value = www.text;
            if (_dicCacheText.ContainsKey(sMd5))
            {
                _dicCacheText[sMd5] = value;
            }
            else
            {
                _dicCacheText.Add(sMd5, value);
            }
            fun(value);
        }
        else if (www.error != null)
        {
            Log.DebugError("下载Text错误：" + www.error);
        }
    }

    #endregion

    #region 下载2D纹理

    public void DownLoadTexture2D(string sPath, Action<Texture2D> fun, bool bCover = false, string sDefaultLocal = "")
    {
        string sMd5 = Tool.CalculateMd5Hash(sPath);

        if (_dicCacheTexture2D.ContainsKey(sMd5))
        {
            if (!bCover)
            {
                fun(_dicCacheTexture2D[sMd5]);
                return;
            }
        }
        StartCoroutine(IeDownLoadTexture2D(sPath, sMd5, fun, sDefaultLocal));
    }

    private IEnumerator IeDownLoadTexture2D(string sPath, string sMd5, Action<Texture2D> fun, string sDefaultLocal)
    {
        WWW www = new WWW(sPath);
        yield return www;
        if (www.isDone && www.error == null)
        {
            Texture2D value = www.texture;
            if (value != null)
            {
                if (_dicCacheTexture2D.ContainsKey(sMd5))
                {
                    _dicCacheTexture2D[sMd5] = value;
                }
                else
                {
                    _dicCacheTexture2D.Add(sMd5, value);
                }
            }
            else
            {
                value = Resources.Load<Texture2D>(sDefaultLocal);
            }
            fun(value);
        }
        else if (www.error != null)
        {
            Log.DebugError("下载Texture2D错误：" + www.error);
        }
    }

    #endregion

    #region 下载AssetBundle

    public void DownLoadAssetBundle(string sPath, Action<AssetBundle> fun, bool bCover = false)
    {
        string sMd5 = Tool.CalculateMd5Hash(sPath);

        if (_dicCacheAssetBundle.ContainsKey(sMd5))
        {
            if (!bCover)
            {
                fun(_dicCacheAssetBundle[sMd5]);
                return;
            }
        }
        StartCoroutine(IeDownLoadAssetBundle(sPath, sMd5, fun));
    }

    private IEnumerator IeDownLoadAssetBundle(string sPath, string sMd5, Action<AssetBundle> fun)
    {
        WWW www = new WWW(sPath);
        yield return www;
        if (www.isDone && www.error == null)
        {
            AssetBundle value = www.assetBundle;
            if (_dicCacheAssetBundle.ContainsKey(sMd5))
            {
                _dicCacheAssetBundle[sMd5] = value;
            }
            else
            {
                _dicCacheAssetBundle.Add(sMd5, value);
            }
            fun(value);
        }
        else if (www.error != null)
        {
            Log.DebugError("下载AssetBundle错误：" + www.error);
        }
    }

    #endregion
  
}
