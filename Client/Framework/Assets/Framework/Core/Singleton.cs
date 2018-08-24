public class Singleton<T> where T : class, new()
{
    private static T _instance;
    private static readonly object OLock = new object();

    public static T Instance
    {
        get
        {
            if (_instance != null)
                return _instance;
            lock (OLock)
            {
                if (_instance == null)
                {
                    _instance = new T();
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
