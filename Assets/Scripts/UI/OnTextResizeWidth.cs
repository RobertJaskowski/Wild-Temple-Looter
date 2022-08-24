using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OnTextResizeWidth : MonoBehaviour
{
    TextMeshProUGUI gold;
    // Start is called before the first frame update
    RectTransform rect;

    void Start()
    {
        gold = GetComponent<TextMeshProUGUI>();
        if (rect == null)
            rect = GetComponent<RectTransform>();
        textSize = gold.fontSize;


        ResizeDownWidth();
    }

    bool isRightSize = false;

    float textSize;

    void Update()
    {
        if (!isRightSize)
            ResizeDownWidth();
    }

    private void ResizeDownWidth()
    {


        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rect.rect.width - 10);
        Debug.Log("asd", this);
        if (textSize != gold.fontSize)
        {
            isRightSize = true;
        }
        
    }
}
