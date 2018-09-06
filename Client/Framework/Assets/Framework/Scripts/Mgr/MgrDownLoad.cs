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
    //相同的资源进行缓存，当一个加载完毕，所有的即被赋值
    private Dictionary<string, List<Action<Texture2D>>> _dicCacheAction = new Dictionary<string, List<Action<Texture2D>>>();
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
        else if (!www.isDone)
        {
            Log.DebugError("下载Text未成功！");
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

        if (_dicCacheAction.ContainsKey(sMd5))
        {
            //如果有资源正在被加载
            if (_dicCacheTexture2D.ContainsKey(sMd5))
            {
                //如果该资源正在被加载，但是已经被加载完毕，则直接回调
                Texture2D tex = _dicCacheTexture2D[sMd5];
                if (tex != null)
                {
                    //资源不为空的话
                    if (!bCover)
                    {
                        //如果不被覆盖
                        fun(tex);
                        return;
                    }
                }
                //重新加载
                if (_dicCacheAction[sMd5] == null)
                    _dicCacheAction[sMd5] = new List<Action<Texture2D>>();
                if (!_dicCacheAction[sMd5].Contains(fun))
                    _dicCacheAction[sMd5].Add(fun);

            }
            else
            {
                //如果该资源正在被加载，但是却没被加载完毕，则保存改回调，等待加载完毕
                if (_dicCacheAction[sMd5] == null)
                    _dicCacheAction[sMd5] = new List<Action<Texture2D>>();
                if (!_dicCacheAction[sMd5].Contains(fun))
                    _dicCacheAction[sMd5].Add(fun);
                //如果这个是第一个入池的话 那么重新加载
                if (_dicCacheAction[sMd5].Count > 1)
                    return;
            }
        }
        else
        {
            //如果没有资源正在被加载
            List<Action<Texture2D>> list = new List<Action<Texture2D>>();
            list.Add(fun);
            _dicCacheAction.Add(sMd5, list);
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
                //对所有的加载入口进行回调
                for (var i = 0; i < _dicCacheAction[sMd5].Count; i++)
                {
                    Action<Texture2D> action = _dicCacheAction[sMd5][i];
                    if (action != null)
                    {
                        action(value);
                    }
                }
                _dicCacheAction[sMd5].Clear();

                //保存加载内容
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
                //对所有的加载入口进行回调
                for (var i = 0; i < _dicCacheAction[sMd5].Count; i++)
                {
                    Action<Texture2D> action = _dicCacheAction[sMd5][i];
                    if (action != null)
                    {
                        action(value);
                    }
                }
                _dicCacheAction[sMd5].Clear();
				
				 //保存加载内容
                if (_dicCacheTexture2D.ContainsKey(sMd5))
                {
                    _dicCacheTexture2D[sMd5] = null;
                }
                else
                {
                    _dicCacheTexture2D.Add(sMd5, null);
                }
            }
        }
        else if (www.isDone && www.error != null)
        {
            Log.DebugError("下载Texture2D错误：" + www.error);
            //赋默认值
            Texture2D value = Resources.Load<Texture2D>(sDefaultLocal);
            for (var i = 0; i < _dicCacheAction[sMd5].Count; i++)
            {
                Action<Texture2D> action = _dicCacheAction[sMd5][i];
                if (action != null)
                {
                    action(value);
                }
            }
            _dicCacheAction[sMd5].Clear();
			
			 //保存加载内容
                if (_dicCacheTexture2D.ContainsKey(sMd5))
                {
                    _dicCacheTexture2D[sMd5] = null;
                }
                else
                {
                    _dicCacheTexture2D.Add(sMd5, null);
                }
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
