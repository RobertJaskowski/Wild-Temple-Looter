using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutosizeTransformToParent : MonoBehaviour
{

    private void Start()
    {
        transform.position = transform.parent.position;
    }
}
