namespace Assets.Framework.Scripts.Common.Panel
{
    public class PanelLoadingObj : UIBase<PanelLoadingObj>
    {
        public override void DoStart()
        {
            base.DoStart();
            TimerCount.In(5, ShowDisconnectServer);

            //UIManager.Instance.Hide(DataAssetPath.PanelCreatRoom);
            //UIManager.Instance.Hide(DataAssetPath.PanelJoinRoom);
        }

        void ShowDisconnectServer()
        {
            //  UIManager.Instance.ShowUiPanel(UIPaths.DisconnectServer);
            NetConnectServer.m_WaitServerMsgCount = 0;
            //UIManager.Instance.Hide(DataAssetPath.LoadingObj);
        }
    }
}
