using UnityEngine;

/// <summary>
/// 本地数据，随着账号切换等操作，会被清空
/// </summary>
public class DataLocal : Singleton<DataLocal>
{
    //本地存储姓名
    public string PlayerName
    {
        get { return PlayerPrefs.GetString("PlayerName"); }
        set { PlayerPrefs.SetString("PlayerName", value); }
    }
    //本地存储头像id
    public string PlayerHeadId
    {
        get { return PlayerPrefs.GetString("PlayerHeadId"); }
        set { PlayerPrefs.SetString("PlayerHeadId", value); }
    }
    //本地存储OpenId
    public string PlayerOpenId
    {
        get { return PlayerPrefs.GetString("PlayerOpenId", "n"); }
        set { PlayerPrefs.SetString("PlayerOpenId", value); }
    }
    //本地存储性别
    public int PlayerSex
    {
        get { return PlayerPrefs.GetInt("PlayerSex", 1); }
        set { PlayerPrefs.SetInt("PlayerSex", value); }
    }


    //游戏背景音乐
    public float GameMusic
    {
        get { return PlayerPrefs.GetFloat("GameMusic", 0.5f); }
        set { PlayerPrefs.SetFloat("GameMusic", value); }
    }
    //游戏音效
    public float GameEffect
    {
        get { return PlayerPrefs.GetFloat("GameEffect", 0.5f); }
        set { PlayerPrefs.SetFloat("GameEffect", value); }
    }
    //语音
    public float GameVoice
    {
        get { return PlayerPrefs.GetFloat("GameVoice", 1f); }
        set { PlayerPrefs.SetFloat("GameVoice", value); }
    }
    //是否开启手机震动 0.关闭震动 1.开启震动
    public int GameVibration
    {
        get { return PlayerPrefs.GetInt("GameVibration", 1); }
        set { PlayerPrefs.SetInt("GameVibration", value); }
    }



    public void ClearData()
    {
        PlayerPrefs.DeleteKey("PlayerName");
        PlayerPrefs.DeleteKey("PlayerHeadId");
        PlayerPrefs.DeleteKey("PlayerOpenId");
        PlayerPrefs.DeleteKey("PlayerSex");

        PlayerPrefs.DeleteKey("GameMusic");
        PlayerPrefs.DeleteKey("GameEffect");
        PlayerPrefs.DeleteKey("GameVoice");
        PlayerPrefs.DeleteKey("GameVibration");
    }
}
