using Assets.Framework.Scripts.Common.Panel;
using UnityEngine;

/// <summary>
/// 断线重连
/// </summary>
public class NetReconnectServer : SingletonMonoBehaviour<NetReconnectServer>
{

    public static float IntervalTime = 3.0f;//重新连接时间
    static NetworkReachability _curNetworkType = NetworkReachability.NotReachable;

    public override void DoStart()
    {
        base.DoStart();
        _curNetworkType = Application.internetReachability;
    }

    public override void DoUpdate()
    {
        base.DoUpdate();
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            if (_curNetworkType != NetworkReachability.NotReachable)
            {
                //  UIManager.Instance.ShowUiPanel(UIPaths.ReconectTipPanel);
            }

            _curNetworkType = NetworkReachability.NotReachable;

            // UIManager.Instance.ShowUiPanel(UIPaths.ReconectTipPanel);
        }
        else if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
        {


            if (_curNetworkType != NetworkReachability.ReachableViaCarrierDataNetwork)
            {
                NetConnectServer.Instance.DisconnectServer();
                // AndroidOrIOSResult.GetMask();
                IntervalTime = 0;
            }
            _curNetworkType = NetworkReachability.ReachableViaCarrierDataNetwork;
            if (!NetConnectServer.m_IsConnectServer)//断开服务器
            {
                if (IntervalTime <= 0)
                {
                    IntervalTime = 3;
                    NetConnectServer.m_WaitServerMsgCount = 0;
                    if (ServerInfo.Instance.Ip != null)
                    {
                        NetConnectServer.ConnectionServer(Tool.GetServerIP(ServerInfo.Instance.Ip), ServerInfo.Instance.Port);
                    }
                }
                else
                {
                    IntervalTime -= Time.deltaTime;
                }
            }

            //  UIManager.Instance.HideUiPanel(UIPaths.ReconectTipPanel);
        }
        else if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
        {


            if (_curNetworkType != NetworkReachability.ReachableViaLocalAreaNetwork)
            {
                NetConnectServer.Instance.DisconnectServer();
                //  AndroidOrIOSResult.GetMask();
                IntervalTime = 0;
            }
            _curNetworkType = NetworkReachability.ReachableViaLocalAreaNetwork;
            if (!NetConnectServer.m_IsConnectServer)//断开服务器
            {
                if (IntervalTime <= 0)
                {
                    IntervalTime = 3;
                    NetConnectServer.m_WaitServerMsgCount = 0;
                    if (ServerInfo.Instance.Ip != null)
                    {
                        NetConnectServer.ConnectionServer(Tool.GetServerIP(ServerInfo.Instance.Ip), ServerInfo.Instance.Port);
                    }
                }
                else
                {
                    IntervalTime -= Time.deltaTime;
                }
            }

            //    UIManager.Instance.HideUiPanel(UIPaths.ReconectTipPanel);
        }
    }
}
