using UnityEngine;

/// <summary>
/// 管理游戏中的继承IMgr接口的类，被Init实例化
/// </summary>
public class MgrGame : Singleton<MgrGame>, IMgr
{
    public void DoMgrAwake()
    {
        //游戏不休眠
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        //后台运行
        Application.runInBackground = true;
        //设置60
        Application.targetFrameRate = 60;

        MgrHandler.Instance.DoMgrAwake();
    }

    public void DoMgrOnEnable()
    {

    }

    public void DoMgrStart()
    {

    }

    public void DoMgrUpdate()
    {

    }

    public void DoMgrOnDisable()
    {

    }

    public void DoMgrDestroy()
    {
        //卸载资源
        Resources.UnloadUnusedAssets();
        System.GC.Collect();
    }
}