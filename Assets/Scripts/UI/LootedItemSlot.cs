using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LootedItemSlot : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI AmountText;

    private void Start()
    {
        LayoutElement le = GetComponent<LayoutElement>();
        le.preferredWidth = Screen.width >= 480 ? 50 : le.preferredWidth;
        le.preferredWidth = Screen.width >= 720 ? 100 : le.preferredWidth;
        le.preferredWidth = Screen.width >= 1080 ? 150 : le.preferredWidth;
        le.preferredWidth = Screen.width >= 1440 ? 250 : le.preferredWidth;
    }
}
