using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData", order = 1)]
public class GameData : ScriptableObject
{
    [SerializeField]
    public StageLink.StageObject[] skills;
    public StageLink.StageObject diamond;
}
