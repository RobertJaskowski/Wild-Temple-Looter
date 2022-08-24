using UnityEngine;

public class ShowScrollbarOnUsed : MonoBehaviour
{
    void Update()
    {
        gameObject.GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);

    }
    public void ShowScrollBar()
    {

        gameObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

    }
}
