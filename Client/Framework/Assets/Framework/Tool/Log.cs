public class Log
{
    public static void Debug(object text)
    {
//#if UNITY_EDITOR
        UnityEngine.Debug.Log(text.ToString());
//#endif
    }
    public static void DebugError(object text)
    {
//#if UNITY_EDITOR
        UnityEngine.Debug.LogError(text.ToString());
//#endif
    }
}
