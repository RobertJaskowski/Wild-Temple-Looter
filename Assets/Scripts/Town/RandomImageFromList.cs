using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomImageFromList : MonoBehaviour
{

    public List<Sprite> images;

    private Image image;


    void Start()
    {
        image = GetComponent<Image>();

        image.sprite = images[Random.Range(0, images.Count)];
    }
      
}
