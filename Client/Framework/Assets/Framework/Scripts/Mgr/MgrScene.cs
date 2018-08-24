using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 场景类型
/// </summary>
public enum SceneType
{
    Init,
    Login,
    Loading,
    Main,
    Game,
}

/// <summary>
/// 用于管理场景的切换
/// </summary>
public class MgrScene : Singleton<MgrScene>
{
    private SceneType _currentScene;
    private SceneType _nextScene;

    public SceneType CurrentScene
    {
        get
        {
            return _currentScene;
        }

        set
        {
            _currentScene = value;
        }
    }
    public SceneType NextScene
    {
        get
        {
            return _nextScene;
        }

        set
        {
            _nextScene = value;
        }
    }


    //同步加载
    public void LoadSceneSync(string sName)
    {
        SceneManager.LoadScene(sName);
    }

    //异步加载
    public AsyncOperation LoadSceneAsync()
    {
        return SceneManager.LoadSceneAsync(_nextScene.ToString());
    }

    //不需要过渡场景
    private void LoadScene(SceneType type)
    {
        _nextScene = type;
        LoadSceneAsync();
    }

    //需要过渡场景
    private void LoadSceneByLoading(SceneType type)
    {
        _nextScene = type;
        LoadSceneSync(SceneType.Loading.ToString());
    }

    //切换场景
    public void SwitchScene(SceneType type)
    {
        //清楚数据
        ClearData();
        _currentScene = type;
        switch (type)
        {
            case SceneType.Init:
                LoadScene(SceneType.Init);
                break;
            case SceneType.Login:
                LoadScene(SceneType.Login);
                break;
            case SceneType.Loading:
                LoadScene(SceneType.Loading);
                break;
            case SceneType.Main:
                LoadSceneByLoading(SceneType.Main);
                break;
            case SceneType.Game:
                LoadSceneByLoading(SceneType.Game);
                break;
            default:
                Log.Debug("没有找到场景" + type.ToString());
                break;
        }
    }

    /// <summary>
    /// 清楚切换场景的数据残留
    /// </summary>
    public void ClearData()
    {
        //关闭所有计时器
        TimerCount.CancelAll();
        //清楚界面缓存
        UIManager.Instance.DoClearData();
        //关闭音乐、音效、语音
        MgrSound.Instance.DoClearData();
        //卸载资源
        Resources.UnloadUnusedAssets();
        System.GC.Collect();
    }
}
