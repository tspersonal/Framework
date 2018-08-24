using UnityEngine;

namespace Assets.Scripts.Core
{
    public class Fps : MonoBehaviour {
        public float f_UpdateInterval = 0.5F;
	
        private float f_LastInterval;
	
        private int i_Frames = 0;
	
        private float f_Fps;
        // Use this for initialization
        void Start () {
            f_LastInterval = Time.realtimeSinceStartup;
		
            i_Frames = 0;
        }
	
        // Update is called once per frame
        void Update () {
            ++i_Frames;
		
            if (Time.realtimeSinceStartup > f_LastInterval + f_UpdateInterval) 
            {
                f_Fps = i_Frames / (Time.realtimeSinceStartup - f_LastInterval);
			
                i_Frames = 0;
			
                f_LastInterval = Time.realtimeSinceStartup;
            }
        }
        void OnGUI() 
        {
            GUIStyle style = new GUIStyle();
            style.normal.textColor = Color.red;
            style.fontSize = 60;
            GUI.Label(new Rect(0, 0, 200, 600), "Fps:" + f_Fps.ToString("f2"), style);
//		GUI.Label(new Rect(0, 20, 200, 200), "memory:" + SystemInfo.systemMemorySize);
        }
    }
}
