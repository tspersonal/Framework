using UnityEngine;

namespace Assets
{
    public class Test : UIBase<Test>
    {
        UIButton ui;
        //private GameObject gogogo = null;
        //private TimeSpan startTime1;
        //private TimeSpan startTime2;
        //private TimeSpan startTime3;
        //private Stopwatch stopwatch;

        public override void DoStart()
        {
            base.DoStart();
            ui = this.GetComponent<UIButton>();
            int i = 0;
            
            UIAddListener.OnClickAddListener(ui.gameObject, delegate
            {
                AddListener(i);
            });

            //stopwatch = new Stopwatch();
            //stopwatch.Start();
            //startTime1 = stopwatch.Elapsed;
            //startTime2 = startTime1;
            //startTime3 = startTime1;
            //Log.Debug("开始异步回调加载：" + startTime1);
            //MgrAsset.Instance.LoadGameObjectAsync("GiftObj", Test1, true);

            //Log.Debug("开始异步返回加载");
            //GameObject go = MgrAsset.Instance.LoadGameObjectAsync("GiftObj");
            //go.name = "异步返回";
            //go.transform.SetParent(transform, false);
            //TimeSpan ts2 = stopwatch.Elapsed;
            //Log.Debug("结束异步返回加载，花费时间：" + (ts2 - startTime2));


            //Log.Debug("开始同步返回加载" + startTime1);
            //GameObject go1 = MgrAsset.Instance.LoadGameObjectSync("GiftObj");
            //go1.name = "同步返回";
            //go1.transform.SetParent(transform, false);
            //TimeSpan ts3 = stopwatch.Elapsed;
            //Log.Debug("结束同步加载，花费时间：" + (ts3 - startTime3));
        }

        public override void DoUpdate()
        {
            base.DoUpdate();
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                MgrAsset.Instance.LoadGameObjectAsync("GiftObj", Test1, true);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                GameObject go = MgrAsset.Instance.LoadGameObjectAsync("GiftObj");
                Log.Debug("结束异步返回加载");
                //go.name = "异步返回";
                //go.transform.SetParent(transform, false);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                GameObject go1 = MgrAsset.Instance.LoadGameObjectSync("GiftObj");
                Log.Debug("结束同步返回加载");
                //go1.name = "同步返回";
                //go1.transform.SetParent(transform, false);
            }
        }


        public void AddListener(int i)
        {
            Log.Debug(this.GetType() + "AddListener");
        }

        public void Test1(GameObject go)
        {
            Log.Debug("结束异步回调加载");
            //go.name = "异步回调";
            //go.transform.SetParent(transform, false);
            //stopwatch.Stop();
            //TimeSpan ts1 = stopwatch.Elapsed;
            //Log.Debug("结束异步回调加载，花费时间：" + (ts1 - startTime1));
        }


    }
}