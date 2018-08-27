
public class ServerInfo : Singleton<ServerInfo>
{
    public string StatusCode;//状态码
    public bool HasServer;//是否连接服务器
    public bool LoginWithDevice;//登录版本号
    public string Version;//版本号
    public string UpdateMessage;//更新信息
    public string UpdateAndroidUrl;//安卓更新地址
    public string UpdateIosUrl;//ios更新地址
    public string Ip;//ip地址
    public ushort Port;//端口号
}
