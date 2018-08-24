﻿using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

/// <summary>
/// 资源类型
/// </summary>
public enum ResType
{
    None,
    GameObject,//物体
    Sprite,//精灵
    Texture,//纹理
    Sound,//声音
    Effect,//特效
    Shader,//
    Material,//材质
}

/// <summary>
/// 游戏当前的类型
/// </summary>
public enum GameType
{
    None,
    Common,//公共
    Main,//大厅
    Mj,//麻将
    Sss,//十三水
    Ddz,//地主
    Nn,//牛牛
    Zjh,//炸金花
    Club,//俱乐部
}

public class MgrAsset : Singleton<MgrAsset>, IMgr
{
    /// <summary>
    /// 正在加载的列表
    /// </summary>
    private List<AssetInfo> _listLoading = new List<AssetInfo>();

    /// <summary>
    /// 等待加载的列表
    /// </summary>
    private Queue<AssetInfo> _queWaitLoad = new Queue<AssetInfo>();


    public void DoMgrAwake()
    {
       
    }

    public void DoMgrOnEnable()
    {
       
    }

    public void DoMgrStart()
    {
        
    }

    public void DoMgrUpdate()
    {
        if (_listLoading.Count > 0)
        {
            for (int i = 0; i < _listLoading.Count; i++)
            {
                AssetInfo asset = _listLoading[i];
                if (asset.Request == null)
                {
                    throw new Exception("回调异常！未正常创建ResourceRequest");
                }
                else
                {
                    if (asset.Request.isDone)
                    {
                        asset.Obj = asset.Request.asset;
                        if (asset.Obj == null)
                        {
                            _listLoading.RemoveAt(i);
                            throw new Exception("加载资源<" + asset.SPath +">异常！");
                        }
                        else
                        {
                            if (asset.ListListener != null)
                            {
                                for (int j = 0; j < asset.ListListener.Count; j++)
                                {
                                    if (asset.ListListener[j] != null)
                                    {
                                        if (asset.BInstantiate)
                                        {
                                            GameObject obj = Object.Instantiate((GameObject) asset.Obj);
                                            asset.ListListener[j](obj);
                                        }
                                        else
                                        {
                                            asset.ListListener[j](asset.Obj);
                                        }
                                    }
                                    else
                                    {
                                        _listLoading.RemoveAt(i);
                                        throw new Exception("加载资源<" + asset.SPath + ">后，回调异常！");
                                    }
                                }
                                AssetPool.AssetCache(asset.SName, asset.SPath, asset.NAssetType, asset.Obj);
                                _listLoading.RemoveAt(i);
                            }
                            else
                            {
                                _listLoading.RemoveAt(i);
                                throw new Exception("加载资源<" + asset.SPath + ">后，无回调类型！");
                            }
                        }
                    }
                }
            }
        }

        while (_queWaitLoad.Count > 0 && _listLoading.Count < 5)
        {
            AssetInfo asset = _queWaitLoad.Dequeue();
            _listLoading.Add(asset);
            asset.LoadAsync(asset.SPath);
        }
    }

    public void DoMgrOnDisable()
    {

    }

    public void DoMgrDestroy()
    {

    }

    #region 异步等待返回加载

    //异步加载对象，添加到异步加载队列中
    public GameObject LoadGameObjectAsync(string sPath)
    {
        GameObject asset = LoadAssetsAsync<GameObject>(sPath, ResType.GameObject);
        if (asset != null)
        {
            return Object.Instantiate(asset);
        }
        return null;
    }

    //异步加载对象，直接返回
    private T LoadAssetsAsync<T>(string sPath, ResType rt) where T : Object
    {
        string sName = GetAssetName(sPath);
        AssetInfo ai = AssetPool.AssetGet(sName, sPath, (int)rt);
        if (ai != null)
        {
            return (T)ai.Obj;
        }
        ai = new AssetInfo(sName, sPath, (int)rt, null);
        T asset = ai.LoadAsync<T>(sPath);
        AssetPool.AssetCache(sName, sPath, (int)rt, asset);
        return asset;
    }

    #endregion

    #region 异步等待回调加载

    //异步加载对象，添加到异步加载队列中
    public void LoadGameObjectAsync(string sPath, Action<GameObject> fun, bool bInstantiate)
    {
        GameObject go = LoadAssetsAsync<GameObject>(sPath, ResType.GameObject, fun, bInstantiate);
        if (go != null)
        {
            GameObject asset = Object.Instantiate(go);
            fun(asset);
        }
    }

    //音频
    public void LoadAudioClipAsync(string sPath, Action<AudioClip> fun, bool bInstantiate = false)
    {
        AudioClip asset = LoadAssetsAsync<AudioClip>(sPath, ResType.Sound, fun, bInstantiate);
        if (asset != null)
        {
            fun(asset);
        }
    }

    //添加到异步加载队列中
    private T LoadAssetsAsync<T>(string sPath, ResType rt, Action<T> fun, bool bInstantiate = false) where T : Object
    {
        string sName = GetAssetName(sPath);

        //正在被加载 还没加载完成
        foreach (AssetInfo item in _listLoading)
        {
            if (item.SName == sName)
            {
                Action<Object> action = o => fun((T)o);
                item.AddListener(action, bInstantiate);
                return null;
            }
        }
        //等待的队列里面有
        foreach (AssetInfo item in _queWaitLoad)
        {
            if (item.SName == sName)
            {
                Action<Object> action = o => fun((T)o);
                item.AddListener(action, bInstantiate);
                return null;
            }
        }

        AssetInfo ai = AssetPool.AssetGet(sName, sPath, (int)rt);
        if (ai != null)
        {
            var obj = (T) ai.Obj;
            return obj;
        }
        else
        {
            //都没有 先创建
            AssetInfo asset = new AssetInfo(sName, sPath, (int)rt, null);
            //T 转为Object类型
            Action<Object> action = o => fun((T)o);
            asset.AddListener(action, bInstantiate);

            //添加到等待队列
            _queWaitLoad.Enqueue(asset);
            return null;
        }
    }

    #endregion


    #region 同步加载
    //同步加载对象
    public GameObject LoadGameObjectSync(string sPath)
    {
        GameObject go = LoadAssetsSync<GameObject>(sPath, ResType.GameObject);
        if (go != null)
        {
            return Object.Instantiate(go);
        }
        return null;
    }
    //音频
    public AudioClip LoadAudioClipSync(string sPath)
    {
        AudioClip asset = LoadAssetsSync<AudioClip>(sPath, ResType.GameObject);
        if (asset != null)
        {
            return asset;
        }
        return null;
    }

    private T LoadAssetsSync<T>(string sPath, ResType rt) where T : Object
    {
        string sName = GetAssetName(sPath);
        AssetInfo ai = AssetPool.AssetGet(sName, sPath, (int)rt);
        if (ai != null)
        {
            return (T)ai.Obj;
        }
        ai = new AssetInfo(sName, sPath, (int)rt, null);
        T asset = ai.LoadSync<T>(sPath);
        AssetPool.AssetCache(sName, sPath, (int)rt, asset);
        return asset;
    }

    private T[] LoadAllAssetsSync<T>(string sPath, ResType rt) where T : Object
    {
        string sName = GetAssetName(sPath);
        AssetInfo ai = new AssetInfo(sName, sPath, (int)rt, null);
        T[] arr = ai.LoadAllSync<T>(sPath);
        return arr;
    }
    #endregion

    //资源名为路径转MD5码
    private string GetAssetName(string sPath)
    {
        return Tool.CalculateMd5Hash(sPath);
    }
   
}
