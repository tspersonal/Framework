
public class NetServerInfo : Singleton<NetServerInfo>
{
    public string statusCode;//状态码
    public bool has_server;//是否连接服务器
    public bool login_with_device;//登录版本号
    public string version;//版本号
    public string update_message;//更新信息
    public string update_android_url;//安卓更新地址
    public string update_ios_url;//ios更新地址
    public string ip;//ip地址
    public ushort port;//端口号
    
}
