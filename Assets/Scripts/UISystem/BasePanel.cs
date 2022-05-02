/*
 * @Author: chunibyou
 * @Date: 2022-04-11 23:17:49
 * @LastEditors: chunibyou
 * @LastEditTime: 2022-04-11 23:45:22
 * @Description: Panel的基类，定义了生命周期函数
 */

using UnityEngine;

namespace HIGO_UISystem
{
    public abstract class BasePanel
    {
        private UIAssetInfo info;
        private GameObject obj;

        public void OnLoad() {}

        public void OnEnter() {}
        
        public void OnPause() {}

        public void OnProcced() {}

        public void OnExit() {}
    }
}