using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class AssetInfo
{
    private string _sName;
    private string _sPath;
    private int _nAssetType;
    private Object _obj;

    private bool _bInstantiate = false;//是否需要实例化
    private ResourceRequest _request;//用于异步加载
    public List<Action<Object>> ListListener;//用于回调事件

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sName">资源名字</param>
    /// <param name="sPath">资源路径</param>
    /// <param name="nAssetType">资源类型</param>
    /// <param name="obj"></param>
    public AssetInfo(string sName, string sPath, int nAssetType, Object obj)
    {
        this._sName = sName;
        this._sPath = sPath;
        this._nAssetType = nAssetType;
        this._obj = obj;
    }
    ////用于异步加载
    //public AssetInfo(string sName, string sPath, int nAssetType, Object obj, ResourceRequest req)
    //{
    //    this._sName = sName;
    //    this._sPath = sPath;
    //    this._nAssetType = nAssetType;
    //    this._obj = obj;
    //    this._request = req;
    //}

    public string SName
    {
        get
        {
            return _sName;
        }

        set
        {
            _sName = value;
        }
    }

    public string SPath
    {
        get
        {
            return _sPath;
        }

        set
        {
            _sPath = value;
        }
    }

    public int NAssetType
    {
        get
        {
            return _nAssetType;
        }

        set
        {
            _nAssetType = value;
        }
    }

    public Object Obj
    {
        get
        {
            return _obj;
        }

        set
        {
            _obj = value;
        }
    }

    public ResourceRequest Request
    {
        get
        {
            return _request;
        }

        set
        {
            _request = value;
        }
    }

    public bool BInstantiate
    {
        get
        {
            return _bInstantiate;
        }

        set
        {
            _bInstantiate = value;
        }
    }

    /// <summary>
    /// 同步加载返回Object
    /// </summary>
    public T LoadSync<T>(string sPath) where T : Object
    {
        return Resources.Load<T>(sPath);
    }

    /// <summary>
    /// 同步加载返回Object
    /// </summary>
    public T[] LoadAllSync<T>(string sPath) where T : Object
    {
        return Resources.LoadAll<T>(sPath);
    }

    /// <summary>
    /// 异步加载
    /// </summary>
    /// <returns></returns>
    public ResourceRequest LoadAsync(string sPath)
    {
        var req = Resources.LoadAsync(sPath);
        _request = req;
        return req;
    }

    /// <summary>
    /// 异步加载返回Object
    /// </summary>
    /// <returns></returns>
    public T LoadAsync<T>(string sPath) where T : Object
    {
        _request = Resources.LoadAsync(sPath);
        return (T)Request.asset;
    }

    /// <summary>
    /// 添加回调
    /// </summary>
    /// <param name="fun"></param>
    /// <param name="bInstantiate"></param>
    public void AddListener(Action<Object> fun, bool bInstantiate = false)
    {
        _bInstantiate = bInstantiate;

        if (ListListener == null)
            ListListener = new List<Action<Object>>();

        if (ListListener.Contains(fun))
            return;

        ListListener.Add(fun);
    }

}
