using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgressBar : MonoBehaviour
{
    public TextMeshProUGUI levelProgressText;
    public Slider levelProgressBar;

    public IntReference MaxProgress;
    public IntReference LevelProgress;



    public void UpdateProgressBar()
    {
       // L.og("Updateing level progress " + LevelProgress.Value + " " + MaxProgress.Value);

        SetBarTextToCurrentProgress();


        StopProgressBarAnimation();
        progress = StartCoroutine(ProgressBarAnimation(LevelProgress.Value));
    }

    public void SetBarToCurrentProgress()
    {
        float levelProgressNormalized = (float)LevelProgress.Value / (float)MaxProgress.Value;
        levelProgressBar.value = levelProgressNormalized;
    }

    public void SetBarTextToCurrentProgress()
    {
        float levelProgressNormalized = (float)LevelProgress.Value / (float)MaxProgress.Value;
        levelProgressText.text = ((levelProgressNormalized) * 100).ToString() + "%";
    }

    Coroutine progress;
    bool IsProgressBarRunning { get { return progress != null; } }
    float progressBarTarget;
    IEnumerator ProgressBarAnimation(float target)
    {
        progressBarTarget = target / MaxProgress.Value;
        while (progressBarTarget - levelProgressBar.value > 0.001f)
        {
            levelProgressBar.value = Mathf.Lerp(levelProgressBar.value, progressBarTarget , Time.deltaTime);

            yield return new WaitForEndOfFrame();

        }

        SetBarToCurrentProgress();
        
        StopProgressBarAnimation();
    }

    public void StopProgressBarAnimation()
    {
        if (IsProgressBarRunning)
        {
            StopCoroutine(progress);

        }
        progress = null;
    }

}
