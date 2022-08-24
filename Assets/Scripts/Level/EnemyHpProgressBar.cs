using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpProgressBar : MonoBehaviour
{

    public Slider enemyHealthProgressBar;
    public TextMeshProUGUI enemyHealthText;

    [Space(20)]
    public FloatReference MaxHp;
    public FloatReference CurrentHp;


    public void UpdateProgressBar(float value)
    {
        float hpNormalized = CurrentHp.Value / MaxHp.Value;
        //if(CurrentHp.Value <= 0)
        //{

        //    hpNormalized = 0;
        //}


        //enemyHealthProgressBar.value = hpNormalized;
        enemyHealthText.text = Mathf.RoundToInt(CurrentHp.Value > 0 ? CurrentHp.Value : 0).ToString();
        

        StopProgressBarAnimation();
        progress = StartCoroutine(ProgressBarAnimation(hpNormalized));
    }

    public void SetBarToCurrent()
    {
        enemyHealthProgressBar.value = CurrentHp.Value / MaxHp.Value;
    }

    Coroutine progress;
    bool IsProgressBarRunning { get { return progress != null; } }
    float progressBarTarget;
    IEnumerator ProgressBarAnimation(float target)
    {
        progressBarTarget = target;

        while (enemyHealthProgressBar.value - progressBarTarget  > 0.001f)
        {
            enemyHealthProgressBar.value = Mathf.Lerp(enemyHealthProgressBar.value, progressBarTarget, Time.deltaTime * 4f);
            //L.og("asd" + progressBarTarget + " "  + enemyHealthProgressBar.value);
            yield return new WaitForEndOfFrame();

        }
        SetBarToCurrent();
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
