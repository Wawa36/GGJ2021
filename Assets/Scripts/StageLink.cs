using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageLink : SingletonManager<StageLink>
{

    [Serializable]
    public struct StageArray {
        [SerializeField]
        public string[] stageNames;
    }

    public struct StagePosition {
        public int x;
        public int y;
    }

    public enum StageMove {
        LEFT,
        RIGHT,
        UP
    }

    [SerializeField]
    StageArray[] stageFloors;

    public StagePosition findStagePosition(string stage) {
        StagePosition pos;
        pos.x = 0;
        pos.y = 0;

        for (int y = 0; y < stageFloors.Length; y++) {
            StageArray arr = stageFloors[y];
            for (int x = 0; x < arr.stageNames.Length; x++) {
                if (arr.stageNames[x] == stage) {
                    pos.x = x;
                    pos.y = y;
                    return pos;
                }
            }
        }
        return pos;
    }

    public StagePosition findStagePosition() {
        return findStagePosition(SceneManager.GetActiveScene().name);
    }

    public string getMoveScene(StageMove move)
    {
        StagePosition pos = findStagePosition();
        int stagesOnFloor = stageFloors[pos.y].stageNames.Length;
        switch (move)
        {
            case StageMove.LEFT:
                pos.x -= 1;
                pos.x = (pos.x + stagesOnFloor) % stagesOnFloor;
                break;
            case StageMove.RIGHT:
                pos.x += 1;
                pos.x = pos.x % stagesOnFloor;
                break;
            case StageMove.UP:
                pos.y += 1;

                if (pos.x >= stageFloors[pos.y].stageNames.Length) {
                    pos.x = stageFloors[pos.y].stageNames.Length;
                }
                break;
        }
        return stageFloors[pos.y].stageNames[pos.x];
    }

    public void changeScene(StageMove move) {
        string scene = getMoveScene(move);
        SceneManager.LoadScene(scene);
    }
}
