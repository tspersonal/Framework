using UnityEngine;

public class SingletonMonoBehaviour<T> : ExtendMonoBehaviour where T : SingletonMonoBehaviour<T>, new()
{
    private static T _instance;
    private static readonly object OLock = new object();

    public override void DoAwake()
    {
        base.DoAwake();
        _instance = gameObject.GetComponent<T>();
        if (_instance == null)
        {
            _instance = gameObject.AddComponent<T>();
            DontDestroyOnLoad(gameObject);
        }
    }

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (OLock)
                {
                    if (_instance == null)
                    {
                        string sName = typeof(T).Name;
                        GameObject goTemp = GameObject.Find(sName);
                        if (goTemp == null)
                        {
                            goTemp = new GameObject(sName);
                        }
                        _instance = goTemp.AddComponent<T>();
                        DontDestroyOnLoad(goTemp);
                    }
                }
            }
            return _instance;
        }
    }

    public static void Clear()
    {
        _instance = new T();
    }
}

