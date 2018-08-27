using FrameworkForCSharp.NetWorks;
using UnityEngine;

/// <summary>
/// 连接服务器
/// </summary>
public class NetConnectServer : SingletonMonoBehaviour<NetConnectServer>
{
    public static bool IsConnectServer = false;
    public static int WaitServerMsgCount = 0;//消息计数
    public static bool IsIpv6 = false;

    public static class Global
    {
        public static TcpClient TcpGateway;//作为客户端连接GatewayServer 
    }

    public class GatewayClient : TcpClient
    {
        protected override void onConnectedFailed(string ip, ushort port)
        {
            connect<NetGateway>(ip, port);
        }
    }

    public override void DoStart()
    {
        base.DoStart();

        string url = "";
        //string url = "http://game.youthgamer.com:97/ClientQuery.aspx?method=queryServerAddress&gameId={0}&clientVersion={1}";
        //url = string.Format(url, "tqq", Application.version);

        MgrDownLoad.Instance.DownLoadText(url, GetServerConfig);
    }

    public override void DoUpdate()
    {
        base.DoUpdate();
        //Connection.update();
    }

    /// <summary>
    /// 获取服务器配置
    /// </summary>
    private void GetServerConfig(string sConfig)
    {
        if(!string.IsNullOrEmpty(sConfig))
        {
            NetServerInfo.Instance = JsonUtility.FromJson<NetServerInfo>(sConfig);
            if (NetServerInfo.Instance.statusCode == "Success")
            {
                if (NetServerInfo.Instance.version == Application.version)
                {
                    //ConnectionServer(Tool.GetServerIp(NetServerInfo.Instance.ip), (ushort) NetServerInfo.Instance.port);
                }
                else
                {
                    //TODO:显示游戏更新面板
                    //UIManager.Instance.ShowUiPanel(UIPaths.GameUpdate, OpenPanelType.MinToMax);
                }
            }
            else
            {
                //未找到服务器
            }
        }
    }

    /// <summary>
    /// 连接服务器
    /// </summary>
    /// <param name="ip"></param>
    /// <param name="port"></param>
    public static void ConnectionServer(string ip, ushort port)
    {
        Global.TcpGateway = new GatewayClient();//
        if (IsIpv6)
            Global.TcpGateway.connectIpv6<NetGateway>(ip, port);
        else
            Global.TcpGateway.connect<NetGateway>(ip, port);
    }

    /// <summary>
    /// 断掉socket
    /// </summary>
    public void DisconnectServer()
    {
        if (Global.TcpGateway != null)
        {
            Connection conn = Global.TcpGateway[0];
            if (conn != null)
                conn.close();
        }
    }
}
