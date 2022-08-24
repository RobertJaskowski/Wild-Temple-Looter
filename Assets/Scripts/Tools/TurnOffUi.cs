using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffUi : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    bool asd = false;
    // Update is called once per frame
    void LateUpdate()
    {
        if (!asd)
        {
            gameObject.SetActive(false);
            asd = true;
        }
    }
}
