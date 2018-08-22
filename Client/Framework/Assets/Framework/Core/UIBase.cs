using System;
using UnityEngine;
using UnityEngine.Events;

public enum OnChangeType
{
    None,
    Input,//输入框
    ProgressBar,//进度条
    PopupList,//下拉框
    Toggle,//文本框
    Widget,//
}


/// <summary>
/// 提供泛型的UI基类
/// </summary>
/// <typeparam name="T"></typeparam>
public class UIBase<T> : ExtendMonoBehaviour where T : UIBase<T>, new()
{
    /// <summary>
    /// 点击处理
    /// </summary>
    /// <param name="go"></param>
    /// <param name="fun"></param>
    public static void OnClickAddListener(GameObject go, Action fun)
    {
        UIEventListener.Get(go).onClick = obj =>
        {
            //TODO:音效控制
            fun();
        };
    }

    /// <summary>
    /// 双击处理
    /// </summary>
    /// <param name="go"></param>
    /// <param name="fun"></param>
    public static void OnDoubleClickAddListener(GameObject go, Action fun)
    {
        UIEventListener.Get(go).onDoubleClick = obj =>
        {
            //TODO:音效控制
            fun();
        };
    }

    /// <summary>
    /// 变化处理
    /// </summary>
    /// <param name="go"></param>
    /// <param name="changeType"></param>
    /// <param name="fun"></param>
    public static void OnChangeAddListener(GameObject go, OnChangeType changeType,Action fun)
    {
        switch (changeType)
        {
            case OnChangeType.None:
                break;
            case OnChangeType.Input:
                UIInput inp = go.GetComponent<UIInput>();
                if (inp != null)
                {
                    EventDelegate.Add(inp.onChange, () =>
                    {
                        //TODO:音效控制
                        fun();
                    });
                }
                else
                {
                    throw new Exception("UIInput组件不存在，注册事件<" + fun + ">失败！");
                }
                break;
            case OnChangeType.ProgressBar:
                UIProgressBar pro = go.GetComponent<UIProgressBar>();
                if (pro != null)
                {
                    EventDelegate.Add(pro.onChange, () =>
                    {
                        //TODO:音效控制
                        fun();
                    });
                }
                else
                {
                    throw new Exception("UIInput组件不存在，注册事件<" + fun + ">失败！");
                }
                break;
            case OnChangeType.PopupList:
                UIPopupList pop = go.GetComponent<UIPopupList>();
                if (pop != null)
                {
                    EventDelegate.Add(pop.onChange, () =>
                    {
                        //TODO:音效控制
                        fun();
                    });
                }
                else
                {
                    throw new Exception("UIInput组件不存在，注册事件<" + fun + ">失败！");
                }
                break;
            case OnChangeType.Toggle:
                UIToggle tog = go.GetComponent<UIToggle>();
                if (tog != null)
                {
                    EventDelegate.Add(tog.onChange, () =>
                    {
                        //TODO:音效控制
                        fun();
                    });
                }
                else
                {
                    throw new Exception("UIInput组件不存在，注册事件<" + fun + ">失败！");
                }
                break;
        }
    }

    /// <summary>
    /// 提交处理，即手机输入法的完成键
    /// </summary>
    /// <param name="go"></param>
    /// <param name="fun"></param>
    public static void OnSubmitAddListener(GameObject go, Action fun)
    {
        UIEventListener.Get(go).onSubmit = obj =>
        {
            //TODO:音效控制
            fun();
        };
    }

    /// <summary>
    /// 停留处理
    /// </summary>
    /// <param name="go"></param>
    /// <param name="fun"></param>
    public static void OnHoverAddListener(GameObject go, Action fun)
    {

        UIEventListener.Get(go).onHover = (obj, isHover) =>
        {
            if (isHover)
            {
                //TODO:音效控制
                fun();
            }
            else
            {

            }
        };
    }
    
    /// <summary>
    /// 按压处理
    /// </summary>
    /// <param name="go"></param>
    /// <param name="fun"></param>
    public static void OnPressAddListener(GameObject go, Action fun)
    {

        UIEventListener.Get(go).onPress = (obj, isPress) =>
        {
            if (isPress)
            {
                //TODO:音效控制
                fun();
            }
            else
            {
                
            }
        };

    }

    /// <summary>
    /// 选中处理
    /// </summary>
    /// <param name="go"></param>
    /// <param name="fun"></param>
    public static void OnSelectAddListener(GameObject go, Action fun)
    {

        UIEventListener.Get(go).onSelect = (obj, isSelect) =>
        {
            if (isSelect)
            {
                //TODO:音效控制
                fun();
            }
            else
            {
                
            }
        };
    }

    /// <summary>
    /// 滑动处理
    /// </summary>
    /// <param name="go"></param>
    /// <param name="fun"></param>
    public static void OnScrollAddListener(GameObject go, Action fun)
    {

        UIEventListener.Get(go).onScroll = (obj, delta) =>
        {
            //TODO:音效控制
            fun();
        };
    }

    /// <summary>
    /// 开始拖动处理
    /// </summary>
    /// <param name="go"></param>
    /// <param name="fun"></param>
    public static void OnDragStartAddListener(GameObject go, Action fun)
    {

        UIEventListener.Get(go).onDragStart = obj =>
        {
            //TODO:音效控制
            fun();
        };
    }

    /// <summary>
    /// 拖动处理
    /// </summary>
    /// <param name="go"></param>
    /// <param name="fun"></param>
    public static void OnDragAddListener(GameObject go, Action fun)
    {

        UIEventListener.Get(go).onDrag = (obj, delta) =>
        {
            //TODO:音效控制
            fun();
        };
    }

    /// <summary>
    /// 结束拖动处理
    /// </summary>
    /// <param name="go"></param>
    /// <param name="fun"></param>
    public static void OnDragEndtAddListener(GameObject go, Action fun)
    {

        UIEventListener.Get(go).onDragEnd = obj =>
        {
            //TODO:音效控制
            fun();
        };
    }

    /// <summary>
    /// 拖动松开处理
    /// </summary>
    /// <param name="go"></param>
    /// <param name="fun"></param>
    public static void OnDropAddListener(GameObject go, Action fun)
    {

        UIEventListener.Get(go).onDrop = (obj, objDrop) =>
        {
            //TODO:音效控制
            fun();
        };
    }

    /// <summary>
    /// 键盘按下处理
    /// </summary>
    /// <param name="go"></param>
    /// <param name="fun"></param>
    public static void OnKeyAddListener(GameObject go, Action fun)
    {

        UIEventListener.Get(go).onKey = (obj, key) =>
        {
            //TODO:音效控制
            fun();
        };
    }
}
