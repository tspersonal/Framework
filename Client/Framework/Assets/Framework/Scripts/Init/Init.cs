using UnityEngine.SceneManagement;

public class Init : ExtendMonoBehaviour
{
    private readonly MgrGame _mgrGame = MgrGame.Instance;

    public override void DoAwake()
    {
        base.DoAwake();
        if (_mgrGame != null)
            _mgrGame.DoMgrAwake();

        DontDestroyOnLoad(gameObject);
    }

    public override void DoStart()
    {
        base.DoStart();
        if (_mgrGame != null)
            _mgrGame.DoMgrStart();
    }

    public override void DoUpdate()
    {
        base.DoUpdate();
        if (_mgrGame != null)
            _mgrGame.DoMgrUpdate();
    }

    public override void DoEnable()
    {
        base.DoEnable();
        if (_mgrGame != null)
            _mgrGame.DoMgrOnEnable();
    }

    public override void DoOnDisable()
    {
        base.DoOnDisable();
        if (_mgrGame != null)
            _mgrGame.DoMgrOnDisable();
    }

    public override void DoOnDestroy()
    {
        base.DoOnDestroy();
        if (_mgrGame != null)
            _mgrGame.DoMgrDestroy();
    }
}
