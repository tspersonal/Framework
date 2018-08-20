using System;
using System.Collections.Generic;
using UnityEngine;

public class ExtendMonoBehaviour : MonoBehaviour
{
    void Awake()
    {
        DoAwake();
        DoRegister();
    }

    void Start()
    {
        DoStart();
    }

    void FixedUpdate()
    {
        DoFixedUpdate();
    }

    void Update()
    {
        DoUpdate();
    }

    void OnEnable()
    {
        DoAddListener();
        DoEnable();
    }

    void OnDisable()
    {
        DoRemoveListener();
        DoOnDisable();
    }

    void OnDestroy()
    {
        DoOnDestroy();
    }

    void OnApplicationFocus(bool focusStatus)
    {
        DoOnApplicationFocus(focusStatus);
    }

    void OnApplicationQuit()
    {
        DoOnApplicationQuit();
    }

    public virtual void DoAwake()
    {

    }

    public virtual void DoStart()
    {

    }

    public virtual void DoFixedUpdate()
    {

    }

    public virtual void DoUpdate()
    {

    }

    public virtual void DoEnable()
    {

    }

    public virtual void DoOnDisable()
    {

    }

    public virtual void DoOnDestroy()
    {

    }

    public virtual void DoOnApplicationFocus(bool focusStatus)
    {

    }

    public virtual void DoOnApplicationQuit()
    {

    }

    /// <summary>
    /// 注册监听事件，例如Button、Input等
    /// </summary>
    protected virtual void DoRegister()
    {

    }

    /// <summary>
    /// 注册监听事件，用于广播事件
    /// </summary>
    protected virtual void DoAddListener()
    {

    }

    /// <summary>
    /// 取消注册监听事件，用于广播事件
    /// </summary>
    protected virtual void DoRemoveListener()
    {

    }

    /// <summary>
    /// 带入数据
    /// </summary>
    /// <param name="obj"></param>
    public virtual void DoSetData(object obj = null)
    {

    }

    /// <summary>
    /// 初始化数据
    /// </summary>
    public virtual void DoResetData()
    {

    }
}
