using UnityEngine;
using System;
using FrameworkForCSharp.NetWorks;

public class NetGateway : AuthConnectionClient
{
    public NetGateway()
    {
        protocolSecurity = true;
    }
    protected override void OnConnected()
    {
        base.OnConnected();
    }
    protected override void OnAuthSuccessed()
    {
        Log.Debug("連接服務器成功...");
        NetConnectServer.m_IsConnectServer = true;

        if (!DataPlayer.Instance.IsLogin)
        {
            MgrScene.Instance.SwitchScene(SceneType.Login);
            //AmapLocationManager.Instance.StartAmapLocation(); //开始定位
        }
        else
        {
            ClientToServerMsg.Instance.Send(Opcodes.Client_Character_Create, DataPlayer.Instance.OpenId, DataPlayer.Instance.Name,
                DataPlayer.Instance.HeadId, (byte)DataPlayer.Instance.Sex);
        }

    }
    protected override void OnDisconnected()
    {
        NetConnectServer.m_IsConnectServer = false;
        Log.Debug("断开服務器成功...");
    }
    protected override void DefaultHandleMessage(NetworkMessage message)
    {
        MgrHandler.DispatchMessage(message);
        if (NetConnectServer.m_WaitServerMsgCount > 0)
            NetConnectServer.m_WaitServerMsgCount--;
    }
}


