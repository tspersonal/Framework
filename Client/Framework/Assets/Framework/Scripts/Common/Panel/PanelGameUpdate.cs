using UnityEngine;

namespace Assets.Framework.Scripts.Common.Panel
{
    public class PanelGameUpdate : UIBase<PanelGameUpdate>
    {
        [SerializeField]
        private UILabel _labClientV;
        [SerializeField]
        private UILabel _labServerV;
        [SerializeField]
        private UILabel _labContent;

        public override void DoStart()
        {
            base.DoStart();
            _labClientV.text = "客户端版本:" + Application.version;
            _labServerV.text = "服务器版本" + NetServerInfo.Instance.version;
            if (NetServerInfo.Instance.update_message.Contains("@"))
            {
                string[] strs = NetServerInfo.Instance.update_message.Split('@');
                _labContent.text = strs[0];
            }
            else
            {
                _labContent.text = NetServerInfo.Instance.update_message;
            }
        }

        public void DownloadApp()
        {
            if (Application.platform == RuntimePlatform.Android)
                Application.OpenURL(NetServerInfo.Instance.update_android_url);
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
                Application.OpenURL(NetServerInfo.Instance.update_ios_url);
        }
    }
}
