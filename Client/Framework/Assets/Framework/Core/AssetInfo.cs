using UnityEngine;

namespace Assets.Scripts.Core
{
    public class AssetInfo
    {
        private string _sName;
        private string _sPath;
        private int _nAssetType;
        private Object _obj;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sName">资源名字</param>
        /// <param name="sPath">资源路径</param>
        /// <param name="nAssetType">资源类型</param>
        public AssetInfo(string sName,string sPath,int nAssetType,Object obj)
        {
            this._sName = sName;
            this._sPath = sPath;
            this._nAssetType = nAssetType;
            this._obj = obj;
        }

        public string GetName()
        {
            return _sName;
        }

        public void SetName(string value)
        {
            _sName = value;
        }

        public string GetPath()
        {
            return _sPath;
        }

        public void SetPath(string value)
        {
            _sPath = value;
        }
        public Object GetObj()
        {
            return _obj;
        }

        public void SetObj(Object value)
        {
            _obj = value;
        }
        public int GetAssetType()
        {
            return _nAssetType;
        }

        public void SetAssetType(int value)
        {
            _nAssetType = value;
        }

    }
}