/*
 * @Author: chunibyou
 * @Date: 2022-04-11 22:01:20
 * @LastEditors: chunibyou
 * @LastEditTime: 2022-04-11 22:13:13
 * @Description: 保存UI的Asset信息
 */

namespace HIGO_UISystem
{
    public class UIAssetInfo
    {
        private string name;
        
        private string path;

        public string Name { get => name; }

        private string Path { get => path; } 
        
        /// <summary>
        /// 根据资源路径创建Info
        /// </summary>
        /// <param name="_path"> UI资源路径 </param>
        public UIAssetInfo(string _path)
        {
            path = _path;
            name = _path.Substring(path.LastIndexOfAny(new char[] {'/', '\\'}) + 1);
        }
    }
}