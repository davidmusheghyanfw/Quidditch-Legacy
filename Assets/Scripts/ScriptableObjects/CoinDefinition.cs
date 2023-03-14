using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCoinDefinition", menuName = "CoinDefinition", order = 2)]
public class CoinDefinition : ScriptableObject
{
    public List<CoinParameter> CoinSegments = new List<CoinParameter>();  
}

[System.Serializable]
public struct CoinParameter
{
    public int coinCount;
    public int offset;
    public Vector3 position;
 
}