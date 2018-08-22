using System;
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
                            for (int j = 0; j < asset.ListListener.Count; j++)
                            {
                                asset.ListListener[j](asset.Obj);
                            }
                            AssetPool.AssetCache(asset.SName, asset.SPath, asset.NAssetType, asset.Obj);
                            _listLoading.RemoveAt(i);
                        }
                    }
                }
            }
        }

        while (_queWaitLoad.Count > 0 && _listLoading.Count < 5)
        {
            AssetInfo asset = _queWaitLoad.Dequeue();
            _listLoading.Add(asset);
            asset.LoadAsync();
        }
    }

    public void DoMgrOnDisable()
    {

    }

    public void DoMgrDestroy()
    {

    }

    #region 异步加载

    //异步加载对象
    public void LoadAsyncGameObject(string sPath, Action<GameObject> fun)
    {
        GameObject go = LoadAsyncAssets<GameObject>(sPath, ResType.GameObject, fun);
        if (go != null)
        {
            GameObject obj = Object.Instantiate(go);
            fun(obj);
        }
    }

    private T LoadAsyncAssets<T>(string sPath, ResType rt, Action<T> fun) where T : Object
    {
        string sName = GetAssetName(sPath);

        //正在被加载 还没加载完成
        foreach (AssetInfo item in _listLoading)
        {
            if (item.SName == sName)
            {
                item.AddListener(fun as Action<Object>);
                return null;
            }
        }
        //等待的队列里面有
        foreach (AssetInfo item in _queWaitLoad)
        {
            if (item.SName == sName)
            {
                item.AddListener(fun as Action<Object>);
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
            asset.AddListener(fun as Action<Object>);
            //添加到等待队列
            _queWaitLoad.Enqueue(asset);
            return null;
        }
    }

    private T[] LoadAsyncAssetsAll<T>(string sName, string sPath, ResType rt) where T : Object
    {

        T[] arr = Resources.LoadAll<T>(sPath);
        return arr;
    }

    #endregion


    #region 同步加载
    //同步加载对象
    public GameObject LoadSyncGameObject(string sPath)
    {
        GameObject go = LoadSyncAssets<GameObject>(sPath, ResType.GameObject);
        if (go != null)
        {
            return Object.Instantiate(go);
        }
        return null;
    }



    private T LoadSyncAssets<T>(string sPath, ResType rt) where T : Object
    {
        string sName = GetAssetName(sPath);
        AssetInfo ai = AssetPool.AssetGet(sName, sPath, (int)rt);
        if (ai != null)
        {
            return (T)ai.Obj;
        }
        T asset = Resources.Load<T>(sPath);
        AssetPool.AssetCache(sName, sPath, (int)rt, asset);
        return asset;
    }

    private T[] LoadSyncAssetsAll<T>(string sPath, ResType rt) where T : Object
    {
        string sName = GetAssetName(sPath);
        T[] arr = Resources.LoadAll<T>(sPath);
        return arr;
    }
    #endregion

    //资源名为路径转MD5码
    private string GetAssetName(string sPath)
    {
        return Tool.CalculateMd5Hash(sPath);
    }
   
}
