using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class GemsDisplay : MonoBehaviour
{

    public TextMeshProUGUI gemsText;
    public IntReference gems;

    private void Awake()
    {
        SetToCurrentGemsValue();
    }

    public void UpdateGemsValue(int valueOfUpdate)
    {
        StopGemsUpdateAnimation();
        gemsAnimationProgress = StartCoroutine(GemsUpdateAnimation(gems.Value));
    }

    public void SetToCurrentGemsValue()
    {
        gemsText.text = gems.Value.ToString();
    }


    Coroutine gemsAnimationProgress;
    bool IsGemsProgressAnimationRunning { get { return gemsAnimationProgress != null; } }
    float gemsUpdateProgressTarget;
    float GemsUpdateSpeed = 0.5f;
    IEnumerator GemsUpdateAnimation(float target)
    {
        gemsUpdateProgressTarget = target;

        float currentValue = int.Parse(gemsText.text);
        float updateValue = target - float.Parse(gemsText.text);

        while (currentValue <= target)
        {
            currentValue += updateValue * GemsUpdateSpeed * Time.deltaTime;
            //goldText.text = Math.Round(Mathf.Lerp(float.Parse(goldText.text), goldUpdateProgressTarget, Time.deltaTime * 2f)).ToString();
            gemsText.text = Mathf.RoundToInt(currentValue).ToString();
            // L.og(goldText.text + " " + target.ToString());
            yield return new WaitForEndOfFrame();

        }

        SetToCurrentGemsValue();

        StopGemsUpdateAnimation();
    }

    public void StopGemsUpdateAnimation()
    {
        if (IsGemsProgressAnimationRunning)
        {
            StopCoroutine(gemsAnimationProgress);

        }
        gemsAnimationProgress = null;
    }
}
