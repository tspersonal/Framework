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
            _labServerV.text = "服务器版本" + ServerInfo.Instance.Version;
            if (ServerInfo.Instance.UpdateMessage.Contains("@"))
            {
                string[] strs = ServerInfo.Instance.UpdateMessage.Split('@');
                _labContent.text = strs[0];
            }
            else
            {
                _labContent.text = ServerInfo.Instance.UpdateMessage;
            }
        }

        public void DownloadApp()
        {
            if (Application.platform == RuntimePlatform.Android)
                Application.OpenURL(ServerInfo.Instance.UpdateAndroidUrl);
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
                Application.OpenURL(ServerInfo.Instance.UpdateIosUrl);
        }
    }
}
