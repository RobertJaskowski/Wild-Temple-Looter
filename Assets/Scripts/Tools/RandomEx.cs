using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RandomEx
{
    public static int ZeroOrOneFrom100(this Random random)
    {
        if (Random.Range(0, 100) < 50)
            return 0;
        else
            return 1;
    }


    public static int MaxRange(int maxRange)
    {
        return Random.Range(0, maxRange + 1);
    }
}
