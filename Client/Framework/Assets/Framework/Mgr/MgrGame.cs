using UnityEngine;

/// <summary>
/// 管理游戏中的继承IMgr接口的类，被Init实例化
/// </summary>
public class MgrGame : Singleton<MgrGame>, IMgr
{
    public void DoMgrAwake()
    {
        MgrAsset.Instance.DoMgrAwake();
    }

    public void DoMgrOnEnable()
    {
        MgrAsset.Instance.DoMgrOnEnable();
    }

    public void DoMgrStart()
    {
        MgrAsset.Instance.DoMgrStart();
    }

    public void DoMgrUpdate()
    {
        MgrAsset.Instance.DoMgrUpdate();
    }

    public void DoMgrOnDisable()
    {
        MgrAsset.Instance.DoMgrOnDisable();
    }

    public void DoMgrDestroy()
    {
        //卸载资源
        Resources.UnloadUnusedAssets();
        System.GC.Collect();
    }
}