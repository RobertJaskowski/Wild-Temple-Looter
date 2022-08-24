using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpBar : MonoBehaviour
{

    public GameObject dungeonButtonReturnToTown;
    public GameObject barObject;
    public GameObject deathScreen;
    public Slider bar;

    [Space(20)]
    public FloatReference PlayerHpDefault;
    public FloatReference MaxHp;
    public FloatReference CurrentHp;


    //private void Update()
    //{
    //    CheckIfDead();
    //}
    private void OnEnable()
    {
        CheckIfDead();
    }

    public void CheckIfDead()
    {
        if(CurrentHp.Value <= 0)
        {
            //L.og("check if dead " + CurrentHp.Value);
            dungeonButtonReturnToTown.SetActive(false);

            FightManager.instance.InCombat = false;

            ShowPlayerDiedScreen();
        }
        else
        {
            dungeonButtonReturnToTown.SetActive(true);
            deathScreen.SetActive(false);
        }
    }

    private void ShowPlayerDiedScreen()
    {
        deathScreen.SetActive(true);
        deathScreen.transform.parent.GetComponent<DeathScreen>().Init();
    }

    public void InitBar()
    {
        barObject.SetActive(true);

        MaxHp.Value = PlayerHpDefault.Value;
        CurrentHp.Value = MaxHp.Value;
        UpdateProgressBar((float)PlayerHpBarUpdateValue.Null);
    }

    public void HideBar()
    {
        barObject.SetActive(false);
    }



    public void UpdateProgressBar(float value)
    {



        if(value == (float)PlayerHpBarUpdateValue.Init)
        {
            InitBar();
            FightUIManager.instance.ShowHealedText();
        }

        float hpNormalized = CurrentHp.Value / MaxHp.Value;


        //enemyHealthProgressBar.value = hpNormalized;
        //enemyHealthText.text = Mathf.RoundToInt(CurrentHp.Value).ToString();


        StopProgressBarAnimation();
        progress = StartCoroutine(ProgressBarAnimation(hpNormalized));
    }

    public void SetBarToCurrent()
    {
        bar.value = CurrentHp.Value / MaxHp.Value;
    }

    Coroutine progress;
    bool IsProgressBarRunning { get { return progress != null; } }
    float progressBarTarget;
    IEnumerator ProgressBarAnimation(float target)
    {
        progressBarTarget = target;

        while (bar.value - progressBarTarget > 0.001f)
        {
            bar.value = Mathf.Lerp(bar.value, progressBarTarget, Time.deltaTime * 4f);
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

public enum PlayerHpBarUpdateValue
{
    Null,
    Init
}
