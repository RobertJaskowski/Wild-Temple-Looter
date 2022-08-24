using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class GoldDisplay : MonoBehaviour
{

    public TextMeshProUGUI goldText;
    public IntReference gold;

    private void Awake()
    {
        SetToCurrentGoldValue();
    }

    public void UpdateGoldValue(int valueOfUpdate)
    {
        StopGoldUpdateAnimation();
        goldAnimationProgress = StartCoroutine(GoldUpdateAnimation(gold.Value));
    }

    public void SetToCurrentGoldValue()
    {
        goldText.text = gold.Value.ToString();
    }


    Coroutine goldAnimationProgress;
    bool IsGoldProgressAnimationRunning { get { return goldAnimationProgress != null; } }
    float goldUpdateProgressTarget;
    float GoldUpdateSpeed = 0.5f;
    IEnumerator GoldUpdateAnimation(float target)
    {
        goldUpdateProgressTarget = target;

        float currentValue = int.Parse(goldText.text);
        float updateValue = target - float.Parse(goldText.text);

        while (currentValue <= target)
        {
            currentValue += updateValue * GoldUpdateSpeed * Time.deltaTime;
            //goldText.text = Math.Round(Mathf.Lerp(float.Parse(goldText.text), goldUpdateProgressTarget, Time.deltaTime * 2f)).ToString();
            goldText.text = Mathf.RoundToInt(currentValue).ToString();
           // L.og(goldText.text + " " + target.ToString());
            yield return new WaitForEndOfFrame();

        }

        SetToCurrentGoldValue();

        StopGoldUpdateAnimation();
    }

    public void StopGoldUpdateAnimation()
    {
        if (IsGoldProgressAnimationRunning)
        {
            StopCoroutine(goldAnimationProgress);

        }
        goldAnimationProgress = null;
    }
}
