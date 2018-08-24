using UnityEngine;

/// <summary>
/// 本地永久的数据，不会随着账号切换，等操作清空数据
/// </summary>
public class DataLocalPermanent
{
    public void ClearData()
    {
        PlayerPrefs.DeleteAll();
    }
}
