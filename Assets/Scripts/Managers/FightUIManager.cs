using UnityEngine;


public class FightUIManager : MonoBehaviour
{
    #region instance

    public static FightUIManager instance;

    private void Instantiate()
    {
        if (instance != null)
        {
            Debug.Log("FightUIManager" +
                " already exists");
            Destroy(gameObject);
        }
        else
        {
            instance = this;

        }

    }

    #endregion

    public GameObject floatingTextPrefab;
    public GameObject dropRoot;

    private void Awake()
    {
        Instantiate();
    }

    private void InitDropRoot()
    {
        if (dropRoot == null)
            dropRoot = GameObject.Find("DropRoot");
    }

    public void ShowHealedText()
    {
        FloatingTextMovement text = Instantiate(floatingTextPrefab, dropRoot.transform).GetComponent<FloatingTextMovement>();
        text.Init("Healed", FloatingTextSize.BIG);
    }

    public void ShowDamageOnScreen(int damage, double modifier)
    {

        if (modifier == 1)
        {

            FloatingTextMovement text = Instantiate(floatingTextPrefab, dropRoot.transform).GetComponent<FloatingTextMovement>();
            text.Init(damage, FloatingTextSize.MED);
        }
        else if (modifier > 1)
        {
            FloatingTextMovement text = Instantiate(floatingTextPrefab, dropRoot.transform).GetComponent<FloatingTextMovement>();
            text.Init(damage, FloatingTextSize.BIG);

            FloatingTextMovement textMod = Instantiate(floatingTextPrefab, dropRoot.transform).GetComponent<FloatingTextMovement>();
            textMod.Init("Weak", FloatingTextSize.SMALL);
        }
        else if (modifier == 0)
        {
            FloatingTextMovement text = Instantiate(floatingTextPrefab, dropRoot.transform).GetComponent<FloatingTextMovement>();
            text.Init(damage, FloatingTextSize.SMALL);

            FloatingTextMovement textMod = Instantiate(floatingTextPrefab, dropRoot.transform).GetComponent<FloatingTextMovement>();
            textMod.Init("Nullified", FloatingTextSize.MED);
        }
        else if (modifier < 1)
        {
            FloatingTextMovement text = Instantiate(floatingTextPrefab, dropRoot.transform).GetComponent<FloatingTextMovement>();
            text.Init(damage, FloatingTextSize.SMALL);

            FloatingTextMovement textMod = Instantiate(floatingTextPrefab, dropRoot.transform).GetComponent<FloatingTextMovement>();
            textMod.Init("Resisted", FloatingTextSize.SMALL);
        }

    }




}

