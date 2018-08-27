using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 存储玩家信息
/// </summary>
public class DataPlayer : Singleton<DataPlayer>
{
    public bool HaveEmail = false;
    public ulong Guid;
    public string Account;
    public string Ip; //ip地址
    public string Address; //定位地址
    public bool IsLogin = false;

    public long RoomCard; //房卡
    public long Diamond; //钻石
    public long Gold; //金币

    //本地存储姓名
    public string Name
    {
        get { return DataLocal.Instance.PlayerName; }
        set { DataLocal.Instance.PlayerName = value; }
    }
    //本地存储头像id
    public string HeadId
    {
        get { return DataLocal.Instance.PlayerHeadId; }
        set { DataLocal.Instance.PlayerHeadId = value; }
    }
    //本地存储OpenId
    public string OpenId
    {
        get { return DataLocal.Instance.PlayerOpenId; }
        set { DataLocal.Instance.PlayerOpenId = value; }
    }
    //本地存储性别
    public int Sex
    {
        get { return DataLocal.Instance.PlayerSex; }
        set { DataLocal.Instance.PlayerSex = value; }
    }

}
