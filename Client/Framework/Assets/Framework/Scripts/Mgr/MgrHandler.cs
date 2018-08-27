using System;
using FrameworkForCSharp.NetWorks;

/// <summary>
/// 分发服务器消息
/// </summary>
public class MgrHandler : Singleton<MgrHandler>, IMgr
{
    // ByteBuffer
    private static Action<NetworkMessage>[] message_handlers = new Action<NetworkMessage>[1024];


    public void DoMgrAwake()
    {
        //TODO:注册其他的网络事件

    }

    public void DoMgrDestroy()
    {

    }

    public void DoMgrOnDisable()
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

    }

    public static void AddServerHandler(Opcodes opcode, Action<NetworkMessage> handler)
    {
        message_handlers[(UInt16)opcode] = handler;
    }

    public static void DispatchMessage(NetworkMessage message)
    {
        if (message.cmd < message_handlers.Length && message_handlers[message.cmd] != null)
        {
            message_handlers[message.cmd](message);
        }
        else
        {
            //Logger.info("client:{0}:{1} request unprocessed cmd:{2}, kick it!", player.name, player.guid, (Opcodes)message.cmd);
        }
    }
}
