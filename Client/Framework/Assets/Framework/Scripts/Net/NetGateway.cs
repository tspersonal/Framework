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
        Log.Debug("连接服務器成功...");
        NetConnectServer.IsConnectServer = true;

        if (!DataPlayer.Instance.IsLogin)
        {
            MgrScene.Instance.SwitchScene(SceneType.Login);
            //AmapLocationManager.Instance.StartAmapLocation(); //开始定位
        }
        else
        {
            NetSendMessageToServer.Instance.Send(Opcodes.Client_Character_Create, DataPlayer.Instance.OpenId, DataPlayer.Instance.Name,
                DataPlayer.Instance.HeadId, (byte)DataPlayer.Instance.Sex);
        }

    }
    protected override void OnDisconnected()
    {
        NetConnectServer.IsConnectServer = false;
        Log.Debug("断开服務器成功...");
    }
    protected override void DefaultHandleMessage(NetworkMessage message)
    {
        MgrHandler.DispatchMessage(message);
        if (NetConnectServer.WaitServerMsgCount > 0)
            NetConnectServer.WaitServerMsgCount--;
    }
}


