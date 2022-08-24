using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ElementalManager
{
    public enum ElementalType
    {
        Fire,
        Nature,
        Rock,
        Electricity,
        Water,
        Dark,
        Holy
    }
    
    

    static readonly double[,] ElementalValues =
    {
        {0.7, 1.5, 1.0, 1.0, 1.0, 0.3, 0.3},
        {0.5, 0.7, 1.5, 1.0, 1.0, 0.3, 0.3},
        {1.0, 0.5, 0.7, 1.5, 1.0, 0.3, 0.3},
        {1.0, 1.0, 0.5, 0.7, 1.5, 0.3, 0.3},
        {1.5, 1.0, 1.0, 0.5, 0.7, 0.3, 0.3},
        {0.3, 0.3, 0.3, 0.3, 0.3, 0.7, 1.5},
        {0.3, 0.3, 0.3, 0.3, 0.3, 1.5, 0.7}
    };

    public static int CalculateDamage(Equipableitem attackerItem, ElementalType defenderElementalType, out double modifier)
    {

       modifier = ElementalValues[(int)attackerItem.ElementalType, (int)defenderElementalType];
        

        return Mathf.RoundToInt((float)modifier * attackerItem.Data.Attack);
    }

    public static int CalculateDamage(float attackerDamage, ElementalType attackerElementalType, ElementalType defenderElementalType, out double modifier)
    {

        modifier = ElementalValues[(int)attackerElementalType, (int)defenderElementalType];


        return Mathf.RoundToInt((float)modifier * attackerDamage);
    }

    public static Sprite GetElementalImage(ElementalType elementalType)
    {
        return Resources.Load<Sprite>("Images/Elemental/" + elementalType.ToString());
    }

    public static ElementalType GetRandomElementalType()
    {
        Array values = Enum.GetValues(typeof(ElementalType));
        ElementalType randomEnum = (ElementalType)values.GetValue(UnityEngine.Random.Range(0,values.Length));
        return randomEnum;
    }

    public static string GetElementalFirstLetter(ElementalType elementalType)
    {
        switch (elementalType)
        {
            case ElementalType.Fire:
                return "F";
            case ElementalType.Nature:
                return "N";
            case ElementalType.Rock:
                return "R";
            case ElementalType.Electricity:
                return "E";
            case ElementalType.Water:
                return "W";
            case ElementalType.Holy:
                return "H";
            case ElementalType.Dark:
                return "D";
            
        }
        return null;
    }

    
}
