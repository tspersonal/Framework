using FrameworkForCSharp.NetWorks;
using UnityEngine;

/// <summary>
/// 连接服务器
/// </summary>
public class NetConnectServer : SingletonMonoBehaviour<NetConnectServer>
{
    public static bool m_IsConnectServer = false;
    public static int m_WaitServerMsgCount = 0;//消息计数
    public static bool m_IsIpv6 = false;

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

        string sUrl = "http://hw389.cn:97/ClientQuery.aspx?method=queryServerAddress&gameId={0}&clientVersion={1}";

        MgrDownLoad.Instance.DownLoadText(sUrl, GetServerConfig);
    }

    public override void DoUpdate()
    {
        base.DoUpdate();
    }

    /// <summary>
    /// 获取服务器配置
    /// </summary>
    private void GetServerConfig(string sConfig)
    {
        if(string.IsNullOrEmpty(sConfig))
        {
            ServerInfo.Instance = JsonUtility.FromJson<ServerInfo>(sConfig);
            if (ServerInfo.Instance.StatusCode == "Success")
            {
                if (ServerInfo.Instance.Version == Application.version)
                {
                    ConnectionServer(Tool.GetServerIP(ServerInfo.Instance.Ip), (ushort)ServerInfo.Instance.Port);
                }
                else
                {
                    //TODO:显示游戏更新面板
                    //UIManager.Instance.ShowUiPanel(UIPaths.GameUpdate, OpenPanelType.MinToMax);
                }
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
        if (m_IsIpv6)
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
