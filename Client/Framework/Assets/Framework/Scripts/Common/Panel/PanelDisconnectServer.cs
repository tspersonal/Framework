using UnityEngine;

namespace Assets.Framework.Scripts.Common.Panel
{
    public class PanelDisconnectServer : UIBase<PanelDisconnectServer>
    {
        [SerializeField]
        private GameObject _btnQiut;
        [SerializeField]
        private GameObject _btnReconnect;
        [SerializeField]
        private UILabel _labDesc;

        private float _connectTime = 15;
        private bool _bConnect = true;


        protected override void DoRegister()
        {
            base.DoRegister();
            UIEventListener.Get(_btnQiut).onClick = OnClickButton;
            UIEventListener.Get(_btnReconnect).onClick = OnClickButton;
        }

        public override void DoUpdate()
        {
            base.DoUpdate();
            if (!_bConnect)
            {
                if (_connectTime <= 0)
                {
                    _connectTime = 15;
                    _bConnect = true;
                    _labDesc.text = "连接失败...";
                }
                else
                {
                    _connectTime -= Time.deltaTime;
                }
            }
        }

        void OnClickButton(GameObject go)
        {
            if (go == _btnQiut)
            {
                Application.Quit();
            }
            else if (go == _btnReconnect)
            {
                if(_bConnect)
                {
                    _bConnect = false;
                    _labDesc.gameObject.SetActive(true);
                    _labDesc.text = "正在连接中...";
                    NetConnectServer.Instance.DisconnectServer();
                    NetConnectServer.ConnectionServer(Tool.GetServerIp(NetServerInfo.Instance.ip), (ushort)NetServerInfo.Instance.port);
                }
            }
        }
    }
}
