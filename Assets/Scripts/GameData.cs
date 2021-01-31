﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData", order = 1)]
public class GameData : ScriptableObject
{
    public StageLink.StageObject[] skills;
    public StageLink.StagePosition[] spawns;
    public StageLink.StageObject diamond;
}
