using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextMeshOverideOnResolution : MonoBehaviour
{

    public float min480autosize;
    public float min720autosize;
    public float min1080autosize;
    public float min1440autosize;


    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        CalculateMinimumAutoSize();
    }

    private void Update()
    {
        CalculateMinimumAutoSize();
    }

    private void CalculateMinimumAutoSize()
    {

            text.fontSizeMin = min480autosize;

        if (Screen.width >= 720)
            text.fontSizeMin = min720autosize;

        if (Screen.width >= 1080)
            text.fontSizeMin = min1080autosize;

        if (Screen.width >= 1440)
            text.fontSizeMin = min1440autosize;
    }
}
