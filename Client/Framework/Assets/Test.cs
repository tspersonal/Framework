using UnityEngine;

namespace Assets
{
    public class Test : UIBase<Test>
    {
        UIButton ui;
        public override void DoStart()
        {
            base.DoStart();
            ui = this.GetComponent<UIButton>();
            int i = 0;
            
            OnClickAddListener(ui.gameObject, delegate
            {
                AddListener(i);
            });

            MgrAsset.Instance.LoadAsyncGameObject("Test", (go) =>
            {
                go.name = "test_test";
                Debug.Log(go.transform.position);
                go.transform.localPosition += new Vector3(100, 100, 0);
                go.transform.localPosition += new Vector3(100, 100, 0);
                go.transform.localPosition += new Vector3(100, 100, 0);
                Debug.Log(go.transform.position);
            });

            GameObject go1 = MgrAsset.Instance.LoadSyncGameObject("Test1");
            go1.name = "test1_test1";

            //Debug.Log(go.transform.position);
        }

        public void AddListener(int i)
        {
            Debug.Log(this.GetType() + "AddListener");
        }
    }
}