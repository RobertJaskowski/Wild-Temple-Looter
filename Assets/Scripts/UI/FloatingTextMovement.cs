using TMPro;
using UnityEngine;

public class FloatingTextMovement : MonoBehaviour
{

    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }


    internal void Init(int damage, FloatingTextSize size)
    {
        text.text = damage.ToString();
        text.fontSize = (float)size;
        text.fontSizeMax = (float)size;
        InitTextAnim();
    }

    internal void Init(string textToDisplay, FloatingTextSize size)
    {
        text.text = textToDisplay;
        text.fontSize = (float)size;
        text.fontSizeMax = (float)size;
        text.color = GetColorBasedOnText(textToDisplay);
        InitTextAnim();
    }

    private Color GetColorBasedOnText(string text)
    {
        if (text == "Weak")
        {
            return Color.green;
        }
        else if (text == "Resisted")
        {
            return Color.yellow;
        }
        else if (text == "Healed")
        {
            return Color.red;
        }
        else
        {
            return Color.red;
        }
    }

    public void InitTextAnim()
    {
        int randomDirection = Random.Range(-500, 500);
        int randomImpulse = Random.Range(400, 1000);
        Vector2 impulse = new Vector2(randomDirection, randomImpulse);
        gameObject.GetComponent<Rigidbody2D>().AddForce(impulse, ForceMode2D.Impulse);

    }

    private void Update()
    {
        if (transform.position.x < 0 || transform.position.x > Screen.width || transform.position.y < 0 || transform.position.y > Screen.height)
            Destroy(gameObject);
    }
}



public enum FloatingTextSize
{
    BIG = 180,
    MED = 120,
    SMALL = 80
}