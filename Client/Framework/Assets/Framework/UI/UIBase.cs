using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 提供泛型的UI基类
/// </summary>
/// <typeparam name="T"></typeparam>
public class UIBase<T> : ExtendMonoBehaviour where T : UIBase<T>, new()
{
    [HideInInspector]
    public List<UIPanel> ListPanel = new List<UIPanel>();//该界面中所有的panel，用于层级管理

    public override void DoAwake()
    {
        base.DoAwake();
        DoRegister();
    }

    /// <summary>
    /// 注册监听事件，例如Button、Input等
    /// </summary>
    protected virtual void DoRegister()
    {

    }
}
