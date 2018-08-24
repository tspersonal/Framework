/// <summary>
/// 存储游戏的资源路径数据
/// </summary>
public class DataAssetPath
{
    //-------------------------------Image------------------------------//


    //-------------------------------Texture----------------------------//


    //-------------------------------View-------------------------------//


    //-------------------------------Panel------------------------------//


    //-------------------------------Sound------------------------------//


    //-------------------------------Effect-----------------------------//


    //-------------------------------Shader-----------------------------//


    //-------------------------------Material---------------------------//



    /*
        /// <summary>
        /// 获取路径
        /// </summary>
        /// <param name="sName"></param>
        /// <param name="rt"></param>
        /// <param name="gt"></param>
        /// <returns></returns>
        public string GetPath(string sName, ResType rt, GameType gt)
        {
            string sType = gt.ToString();
            string sPath = "";
            switch (rt)
            {
                case ResType.None:
                    break;
                case ResType.Image:
                    sPath = "Image/" + sType + "/" + sName;
                    break;
                case ResType.Texture:
                    sPath = "Texture/" + sType + "/" + sName;
                    break;
                case ResType.View:
                    sPath = "Prefabs/" + sType + "/View/" + sName;
                    break;
                case ResType.Panel:
                    sPath = "Prefabs/" + sType + "/Panel/" + sName;
                    break;
                case ResType.Sound:
                    sPath = "Snds/" + sType + "/" + sName;
                    break;
                case ResType.Effect:
                    sPath = "Prefabs/" + sType + "/Effect/" + sName;
                    break;
                case ResType.Shader:
                    break;
                case ResType.Material:
                    sPath = "Material/" + sType + "/" + sName;
                    break;
            }
            return sPath;
        }*/
}
