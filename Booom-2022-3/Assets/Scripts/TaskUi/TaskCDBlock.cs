/*
 * @Author: chunibyou
 * @Date: 2022-04-18 17:34:02
 * @LastEditors: chunibyou
 * @LastEditTime: 2022-04-18 18:29:43
 * @Description: 显示处理倒计时UI
 */

using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(CanvasGroup))]
public class TaskCDBlock : MonoBehaviour
{
    public Image image;

    public TextMeshProUGUI text;

    private TaskTarget target;

    private void EnableUI()
    {
        var cg = GetComponent<CanvasGroup>();
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }

    private void DisabledUI()
    {
        var cg = GetComponent<CanvasGroup>();
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (image == null)
            Debug.LogError("TaskCDBlock : 缺少Image引用");
        if (text == null)
            Debug.LogError("TaskCDBlock : 缺少Text引用");
    }

    // Update is called once per frame
    void Update()
    {
        target = TaskUIManager.Instance.GetCurrTarget();
        if (target != null && !target.IsTargetDone())
        {
            EnableUI();
            float holdingTime = target.holdingTime;
            float currTime = holdingTime - target.GetCurrStayTime();
            float amount = currTime / holdingTime;
            if (currTime < 0.0f)
            {
                amount = 1.0f;
                currTime = 0.0f;
            }
            image.fillAmount = amount;
            text.SetText(currTime.ToString("0.0"));
        }
        else
        {
            DisabledUI();
            image.fillAmount = 0;
            text.SetText("-1");
        }

    }

}
