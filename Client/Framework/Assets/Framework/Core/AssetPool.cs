using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public class AssetPool
    {
        private static Dictionary<int,Dictionary<string,AssetInfo>> _dic = new Dictionary<int, Dictionary<string, AssetInfo>>();


        /// <summary>
        /// 获取某类资源对象
        /// </summary>
        public static AssetInfo AssetGet(string sName,string sPath,int nType)
        {
            if (IsAssetType(nType))
            {
                Dictionary<string, AssetInfo> dic = _dic[nType];
                if (IsAsset(dic,sName))
                {
                    return dic[sName];
                }
            }
            return null;
        }

        /// <summary>
        /// 对象生成后进行缓存
        /// </summary>
        public static void AssetCache(string sName, string sPath, int nType,Object obj)
        {
            if (!IsAssetType(nType))
            {
                _dic[nType] = new Dictionary<string, AssetInfo>();
            }
            AssetInfo ai = new AssetInfo(sName,sPath,nType,obj);
            _dic[nType].Add(sName,ai);
        }

        /// <summary>
        /// 对象用完之后进行回收
        /// </summary>
        public static void AssetRecycle()
        {
            
        }
        /// <summary>
        /// 清理某种类型的全部资源
        /// </summary>
        /// <param name="nType"></param>
        public static void AssetTypeClear(int nType)
        {
            if (IsAssetType(nType))
            {
                _dic[nType].Clear();
            }
        }

        public static void AssetTypeClearOfOne(int nType, string sName)
        {
            if (IsAssetType(nType))
            {
                Dictionary<string, AssetInfo> dic = _dic[nType];
                if (IsAsset(dic, sName))
                {
                    dic[sName] = null;
                }
            }
        }

        /// <summary>
        /// 某个资源是否存在
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="sName"></param>
        /// <returns></returns>

        private static bool IsAsset(Dictionary<string, AssetInfo> dic, string sName)
        {
            if (dic.ContainsKey(sName))
                return true;
            return false;
        }
        /// <summary>
        /// 某种类型的资源是否存在
        /// </summary>
        /// <param name="nType"></param>
        /// <returns></returns>
        public static bool IsAssetType(int nType)
        {
            if (_dic.ContainsKey(nType))
            {
                return true;
            }
            return false;
        }
    }
}