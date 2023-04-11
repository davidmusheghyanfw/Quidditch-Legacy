using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyCurveDefinition", menuName = "EnemyCurve", order = 2)]
public class EnemyCurveDefinition : ScriptableObject
{
    public List<AnimationCurve> curveList = new List<AnimationCurve>();  
}

