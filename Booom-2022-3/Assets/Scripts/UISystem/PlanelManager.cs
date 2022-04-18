/*
 * @Author: chunibyou
 * @Date: 2022-04-11 23:17:31
 * @LastEditors: chunibyou
 * @LastEditTime: 2022-04-12 21:50:31
 * @Description: Panel管理
 */


using System.Collections.Generic;

namespace HIGO_UISystem
{
    public class PlanelManager
    {
        private Stack<BasePanel> panelStack;

        private BasePanel currPlanel;

        public void EnterNewPlanel(BasePanel newPlanel)
        {
            if(panelStack.Count > 0)
            {
                
                panelStack.Peek().OnPause();
            }
            panelStack.Push(newPlanel);    
            currPlanel = newPlanel;
            currPlanel.OnEnter();
        }

        public void ExitCurrPlanel()
        {
            if(panelStack.Count <= 0)
                return;
            panelStack.Pop();
            currPlanel = panelStack.Peek();
        }

    }
}
