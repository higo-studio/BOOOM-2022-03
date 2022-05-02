/*
 * @Author: chunibyou
 * @Date: 2022-04-18 20:46:58
 * @LastEditors: chunibyou
 * @LastEditTime: 2022-04-18 20:49:08
 * @Description: 挂载更新分数UI
 */

using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    private void FixedUpdate() 
    {
        scoreText.SetText(TaskUIManager.Instance.score.ToString());
    }
}
