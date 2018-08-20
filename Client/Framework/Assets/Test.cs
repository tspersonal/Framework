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
        }

        public void AddListener(int i)
        {
            Debug.Log(this.GetType() + "AddListener");
        }
    }
}