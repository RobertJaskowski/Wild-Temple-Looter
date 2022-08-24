using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Managers/EnemyDatabase"))]
public class EnemyDatabase : ScriptableObject
{
    public List<Sprite> enemyImagesPossibility;


    public Sprite GetRandomEnemyImage()
    {
        return enemyImagesPossibility[UnityEngine.Random.Range(0, enemyImagesPossibility.Count - 1)];
    }

    public string GetRandomEnemyName()
    {

        string randomNameWithE =  enemyImagesPossibility[UnityEngine.Random.Range(0, enemyImagesPossibility.Count - 1)].texture.name;
        return randomNameWithE.Substring(0, randomNameWithE.Length - 1);
    }

    internal Sprite GetRandomEnemyImageBasedOnEnemy(string enemyNameBase, ElementalManager.ElementalType elementalType)
    {


        foreach (Sprite sprite in enemyImagesPossibility)
        {
            if (sprite.texture.name == enemyNameBase + ElementalManager.GetElementalFirstLetter(elementalType))
            {
                return sprite;
            }
        }


        return enemyImagesPossibility[UnityEngine.Random.Range(0, enemyImagesPossibility.Count - 1)];
    }
}
