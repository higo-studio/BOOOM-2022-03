using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public class JsonArrayWrap<T>
{
    public T[] items;
}
[Serializable]
public class DialogueItem
{
    public string speaker;
    public string content;
    public string menuText;
    public int[] linkTo;
}

public class Dialogue : MonoBehaviour
{
    public TMPro.TMP_Text Speaker;
    public TypeWriter Writer;
    public Button SkipButton;
    public TextAsset Json;
    public Transform OptionGroup;
    public event Action OnComplete;

    private Button[] optionButtons;
    private DialogueItem[] items;
    private int cursor = 0;
    // Start is called before the first frame update
    private ITalkable speakerHandle;

    private void Awake()
    {
        SkipButton.onClick.AddListener(OnSkipButtonClicked);
        optionButtons = OptionGroup.GetComponentsInChildren<Button>(true);
        for (var i = 0; i < optionButtons.Length; i++)
        {
            var btn = optionButtons[i];
            var idx = i;
            btn.onClick.AddListener(() => OnOptionButtonClicked(idx));
        }
        OptionGroup.gameObject.SetActive(false);
    }
    void Start()
    {
        DisabledUI();
    }

    void SetJson(TextAsset jsonAseet)
    {
        items = JsonUtility.FromJson<JsonArrayWrap<DialogueItem>>(jsonAseet.text).items;
    }

    // Update is called once per frame
    void Update()
    {
        if(speakerHandle != null && !speakerHandle.InArea())
        {
            speakerHandle.OnTalkEnd(false);
            DisabledUI();
        }
    }

    public void Refresh(int index)
    {
        OptionGroup.gameObject.SetActive(false);
        if (index < 0)
        {
            Writer.Pause();
            OnComplete?.Invoke();
            if(speakerHandle != null)
                speakerHandle.OnTalkEnd(true);       
            DisabledUI();                             
            return;
        }
        cursor = Mathf.Clamp(index, 0, items.Length - 1);
        var item = items[cursor];
        Writer.Play(item.content);
        Speaker.text = item.speaker + ":";
    }

    public void OnSkipButtonClicked()
    {
        if (Writer.IsPlaying && !Writer.IsStopped)
        {
            Writer.Skip();
        }
        else
        {
            var item = items[cursor];
            if (item.linkTo.Length == 0)
            {
                Refresh(cursor + 1);
            }
            else if (item.linkTo.Length == 1)
            {
                Refresh(item.linkTo[0]);
            }
            else
            {
                OptionGroup.gameObject.SetActive(true);
                for (var i = 0; i < optionButtons.Length; i++)
                {
                    var btn = optionButtons[i];
                    if (i >= item.linkTo.Length)
                    {
                        btn.gameObject.SetActive(false);
                        continue;
                    }
                    btn.gameObject.SetActive(true);
                    var nextItem = items[item.linkTo[i]];
                    btn.GetComponent<TMPro.TextMeshProUGUI>().text = string.IsNullOrEmpty(nextItem.menuText) ? nextItem.content : nextItem.menuText;
                }
            }
        }

    }

    public void OnOptionButtonClicked(int buttonIndex)
    {
        var item = items[cursor];
        var nextIndex = item.linkTo[buttonIndex];
        Refresh(nextIndex);
    }

    public void Speak(ITalkable speakH, TextAsset jsonAsset)
    {
        SetJson(jsonAsset);
        EnableUI();
        speakerHandle = speakH;
        Refresh(0);
    }

    public void EnableUI()
    {
        CanvasGroup cg = GetComponent<CanvasGroup>();
        cg.alpha = 1;
        cg.blocksRaycasts = true;
        cg.interactable = true;
    }

    public void DisabledUI()
    {
        CanvasGroup cg = GetComponent<CanvasGroup>();
        cg.alpha = 0;
        cg.blocksRaycasts = false;
        cg.interactable = false;
    }
}
