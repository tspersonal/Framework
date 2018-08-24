using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShowUiType
{
    None,
    MinToMax,//由小变大
    LeftToRight,//由左到右
    RightToLeft,//由右到左
    TopToBottom,//由上到下
    BottomToTop,//由下到上
    Custom,//自定义
}

/// <summary>
/// 用于管理UI的加载以及层级关系,继承自SingletonMonoBehaviour，需要使用协程，
/// </summary>
public class UIManager : SingletonMonoBehaviour<UIManager>
{
    private int _nMinDepth = 0;//最小的渲染深度，各个场景不一致
    private int _nRangeDepth = 50;//每一个界面的深度范围

    public int NMinDepth
    {
        get { return _nMinDepth; }
        set { _nMinDepth = value; }
    }
    public int NRangeDepth
    {
        get { return _nRangeDepth; }
        set { _nRangeDepth = value; }
    }

    private Dictionary<string, GameObject> _dicAllView = new Dictionary<string, GameObject>();//当前场景所有的界面
    private Dictionary<string, GameObject> _dicOpenView = new Dictionary<string, GameObject>();//存储所有打开的视图
    private List<string> _listOpenView = new List<string>();//存储所有打开的视图


    /// <summary>
    /// 显示同步加载的界面
    /// </summary>
    /// <param name="sPath">资源路径</param>
    /// <param name="showUiType">界面的打开方式</param>
    /// <param name="nDepth">0使用默认的安排的深度，否则使用传入的深度</param>
    /// <param name="objData">界面打开时带入数据</param>
    /// <param name="sBaseName">执行动画的对象</param>
    public GameObject ShowSync(string sPath, ShowUiType showUiType, int nDepth = 0, object objData = null, string sBaseName = "Base")
    {
        if (_listOpenView.Contains(sPath))
        {
            Log.DebugError("界面已经被打开！");
            return null;
        }
        //设置对象
        GameObject goView = null;
        if (_dicAllView.ContainsKey(sPath))
        {
            goView = _dicAllView[sPath];
        }
        else
        {
            goView = MgrAsset.Instance.LoadGameObjectSync(sPath);
            _dicAllView.Add(sPath, goView);
        }
        goView.SetActive(true);
        goView.transform.SetParent(GameObject.Find("UIRoot").transform);
        goView.transform.localScale = Vector3.one;
        goView.transform.localPosition = Vector3.zero;
        //添加界面
        AddOpenView(sPath, goView);
        //带入数据
        if (objData != null)
        {
            ExtendMonoBehaviour emb = goView.GetComponent<ExtendMonoBehaviour>();
            if (emb != null)
            {
                emb.DoSetData(objData);
            }
        }
        //更新层级
        if (nDepth == 0)
        {
            nDepth = _nMinDepth + _listOpenView.Count * _nRangeDepth;
        }
        goView.GetComponent<UIPanel>().depth = nDepth;
        IMgrDepth mgr = goView.GetComponent<IMgrDepth>();
        if (mgr != null)
        {
            mgr.SetPanelDepth(nDepth);
        }
        //设置动画
        StartCoroutine(PlayTween(goView, showUiType, sBaseName));
        return goView;
    }
    
    /// <summary>
    /// 关闭界面
    /// </summary>
    /// <param name="sPath"></param>
    public void Hide(string sPath)
    {
        bool bHasList = _listOpenView.Contains(sPath);
        bool bHasDic = _dicOpenView.ContainsKey(sPath);
        if (!bHasList && !bHasDic)
        {
            return;
        }
        else if (!bHasList && bHasDic)
        {
            _dicOpenView.Remove(sPath);
            return;
        }
        else if (bHasList && !bHasDic)
        {
            _listOpenView.Remove(sPath);
            return;
        }

        //界面正常在打开状态
        GameObject go = _dicOpenView[sPath];
        DeleteOpenView(sPath);
        if (go != null)
        {
            if (!_dicAllView.ContainsKey(sPath))
            {
                _dicAllView.Add(sPath, go);
            }
            go.SetActive(false);
        }
        else
        {
            Log.DebugError("删除的界面不存在！");
        }
    }

    /// <summary>
    /// 播放打开界面的动画
    /// </summary>
    /// <param name="go"></param>
    /// <param name="showUiType"></param>
    /// <param name="sBaseName"></param>
    /// <returns></returns>
    private IEnumerator PlayTween(GameObject go, ShowUiType showUiType, string sBaseName)
    {
        Transform tran = go.transform.Find(sBaseName);
        if (tran != null)
        {
            switch (showUiType)
            {
                case ShowUiType.None:
                    break;
                case ShowUiType.MinToMax:
                    tran.localScale = Vector3.one * 0.75f;
                    TweenScale.Begin(tran.gameObject, 0.2f, Vector3.one);
                    break;
                case ShowUiType.LeftToRight:
                    tran.localPosition = new Vector3(-650, 0, 0);
                    TweenPosition.Begin(tran.gameObject, 0.2f, Vector3.zero);
                    break;
                case ShowUiType.RightToLeft:
                    tran.localPosition = new Vector3(650, 0, 0);
                    TweenPosition.Begin(tran.gameObject, 0.2f, Vector3.zero);
                    break;
                case ShowUiType.TopToBottom:
                    break;
                case ShowUiType.BottomToTop:
                    break;
                case ShowUiType.Custom:
                    //自己加动画
                    break;
            }
        }
        yield break;
    }

    /// <summary>
    /// 添加打开的界面
    /// </summary>
    /// <param name="sPath"></param>
    /// <param name="go"></param>
    private void AddOpenView(string sPath, GameObject go)
    {
        if(!_listOpenView.Contains(sPath))
            _listOpenView.Add(sPath);
        if(!_dicOpenView.ContainsKey(sPath))
            _dicOpenView.Add(sPath, go);
    }

    /// <summary>
    /// 删除打开的界面
    /// </summary>
    /// <param name="sPath"></param>
    private void DeleteOpenView(string sPath)
    {
        if (_listOpenView.Contains(sPath))
            _listOpenView.Remove(sPath);
        if (_dicOpenView.ContainsKey(sPath))
            _dicOpenView.Remove(sPath);
    }

    /// <summary>
    /// 是否存在该界面
    /// </summary>
    /// <param name="sPath"></param>
    /// <returns></returns>
    public bool HasView(string sPath)
    {
        if (_dicAllView.ContainsKey(sPath))
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// 该界面是否打开
    /// </summary>
    /// <param name="sPath"></param>
    /// <returns></returns>
    public bool HasOpenView(string sPath)
    {
        if (_listOpenView.Contains(sPath))
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// 强制顺序更新所有界面层级
    /// </summary>
    public void UpdateDepth()
    {
        for (var i = 0; i < _listOpenView.Count; )
        {
            string sPath = _listOpenView[i];
            GameObject go = _dicOpenView[sPath];
            if (go == null)
            {
                _listOpenView.Remove(sPath);
                _dicOpenView.Remove(sPath);
            }
            else
            {
                int nDepth = _nMinDepth + _listOpenView.Count * _nRangeDepth;
                go.GetComponent<UIPanel>().depth = nDepth;
                IMgrDepth mgr = go.GetComponent<IMgrDepth>();
                if (mgr != null)
                {
                    mgr.SetPanelDepth(nDepth);
                }
            }
        }
    }


    /// <summary>
    /// 清空界面缓存，切换场景时要用
    /// </summary>
    public override void DoClearData()
    {
        base.DoClearData();
        _dicAllView.Clear();
        _dicOpenView.Clear();
        _listOpenView.Clear();
    }
}
