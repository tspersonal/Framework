using FrameworkForCSharp.NetWorks;

/// <summary>
/// 客户端向服务器发送消息
/// </summary>
public class ClientToServerMsg : Singleton<ClientToServerMsg>
{
    public void Send(Opcodes op, params object[] pms)
    {
        NetworkMessage message = NetworkMessage.Create((ushort)op, 100);
        for (int i = 0; i < pms.Length; i++)
        {
            if (pms[i].GetType() == typeof(string)) message.writeString((string)pms[i]);
            else if (pms[i].GetType() == typeof(bool)) message.writeBool((bool)pms[i]);
            else if (pms[i].GetType() == typeof(int)) message.writeInt32((int)pms[i]);
            else if (pms[i].GetType() == typeof(long)) message.writeInt64((long)pms[i]);
            else if (pms[i].GetType() == typeof(byte)) message.writeUInt8((byte)pms[i]);
            else if (pms[i].GetType() == typeof(uint)) message.writeUInt32((uint)pms[i]);
            else if (pms[i].GetType() == typeof(ulong)) message.writeUInt64((ulong)pms[i]);
            else if (pms[i].GetType() == typeof(float)) message.writeFloat((float)pms[i]);

        }
        SendMsg(message);
    }

    // 把数据发给服务器
    public void SendMsg(NetworkMessage message)
    {
        Log.Debug(((Opcodes)message.cmd).ToString());
        NetConnectServer.m_WaitServerMsgCount++;
        Connection conn = NetConnectServer.Global.TcpGateway[0];
        if (conn != null)
            conn.send(message);
    }

}
