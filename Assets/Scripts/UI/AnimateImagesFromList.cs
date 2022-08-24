using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Image), typeof(Timer))]
public class AnimateImagesFromList : MonoBehaviour
{
    [Header("Overides timer values")]
    public float timeOfChange;
    public float waitAtStart;
    public float waitAtEnd;
    public List<Sprite> images;


    private Timer timer;
    private Image image;
    private int index = 0;

    private void Awake()
    {
        image = GetComponent<Image>();
        timer = GetComponent<Timer>();
        UnityAction action = new UnityAction(ChangeToNextImage);
        timer.onTimerEndUnity.AddListener(action);
    }


    private void Update()
    {
        

        if (timer.IsTimerProgressAnimationRunning)
            return;
        else if (waitAtStart >0 && index == 0)
        {
            timer.Start(waitAtStart, waitAtStart / 10, false);
        }
        else if(waitAtEnd > 0 && index == GetLastImageIndex())
        {
            timer.Start(waitAtEnd, waitAtStart / 10, false);

        }
        else
        {
            
            timer.Start(timeOfChange, timeOfChange / 10);
        }
    }

    private void IncrementIndex()
    {
        if (index >= GetLastImageIndex())
        {
            index = 0;
        }
        else
        {
            index++;
        }

    }

    private int GetLastImageIndex()
    {
        return images.Count - 1;
    }

    public void ChangeToNextImage()
    {
        IncrementIndex();
        image.sprite = images[index];
    }

}
