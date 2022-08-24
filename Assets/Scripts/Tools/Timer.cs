using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public float time;
    public float tickCheck;

    [Space(20)]
    public UnityEvent onTimerEndUnity;
    public GameEvent onTimerEnd;

    private bool triggerEvents = true;
    private float currentTimerValue;
    private Coroutine timerAnimationProgress;
    public bool IsTimerProgressAnimationRunning { get { return timerAnimationProgress != null; } }
    private float timerUpdateProgressTarget;


    public void Start()
    {
        StopTimerAnimation();
        StartCoroutine(TimerUpdateEnumerator());
    }


    public void Start(float triggerAfterSeconds, float tickUpdateSeconds, bool trigerEventsAtEnd = true)
    {
        triggerEvents = trigerEventsAtEnd;
        time = triggerAfterSeconds;
        tickCheck = tickUpdateSeconds;
        StopTimerAnimation();
        timerAnimationProgress = StartCoroutine(TimerUpdateEnumerator());
    }




    IEnumerator TimerUpdateEnumerator()
    {

        currentTimerValue = time;
        while (currentTimerValue > 0)
        {
            yield return new WaitForSeconds(tickCheck);
            currentTimerValue--;
        }

        if (triggerEvents)
            RaiseEvents();

        StopTimerAnimation();
    }

    private void RaiseEvents()
    {
        onTimerEndUnity?.Invoke();
        onTimerEnd?.Raise();
    }

    public void StopTimerAnimation()
    {
        if (IsTimerProgressAnimationRunning)
        {
            StopCoroutine(timerAnimationProgress);

        }
        timerAnimationProgress = null;
        triggerEvents = true;
    }

}
