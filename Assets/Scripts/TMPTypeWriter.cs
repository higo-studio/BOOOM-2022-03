using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TMPro.TextMeshProUGUI))]
public class TMPTypeWriter : TypeWriter
{
    private TMPro.TextMeshProUGUI textComp;
    private void Start()
    {
        textComp = GetComponent<TMPro.TextMeshProUGUI>();
    }

    public override void UpdateText()
    {
        textComp.text = OutputText;
    }
}
