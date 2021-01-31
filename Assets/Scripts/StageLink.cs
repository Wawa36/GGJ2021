using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageLink : SingletonManager<StageLink>
{

    [Serializable]
    public struct StageArray {
        [SerializeField]
        public string[] stageNames;
    }

    [Serializable]
    public struct StagePosition {
        public int x;
        public int y;
    }

    [Serializable]
    public class StageObject {
        [SerializeField]
        public StagePosition pos;
        public string name;
        public GameObject icon;
        public GameObject toInstantiate;
        public bool activated = false;
        public bool taken = false;
    }

    public enum StageMove {
        LEFT = 0,
        RIGHT = 1,
        UP = 2,
        NONE = 3
    }

    [SerializeField]
    StageArray[] stageFloors;

    [SerializeField]
    int[] maxSkillsOnFloor;

    [SerializeField]
    private GameData baseGameData;

    [SerializeField]
    private GameData gameData;

    List<StageObject> stageObjects;

    List<int> characters;


    private int currentCharacter;

    [SerializeField]
    private int minLatDistBetweenSkills = 1;

    [SerializeField]
    private Vector3 center = Vector3.zero;

    [SerializeField]
    private float objectsInstanceOffset = 3f;


    [SerializeField]
    private GameObject playerPrefab;

    [SerializeField]
    private int lastSceneHealth = 3;

    [SerializeField]
    private StageMove lastMove = StageMove.NONE;

    [SerializeField]
    private Vector3[] spawnPoints;

    public void Start()
    {
        gameData = Instantiate(baseGameData);
        stageObjects = new List<StageObject>();
        stageObjects.Add(gameData.diamond);

        foreach (StageObject obj in gameData.skills) {
            stageObjects.Add(obj);
        }

        characters = new List<int>();
        fillCharacters();

       
        createObjects();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void fillCharacters() {
        characters.Clear();
        for (int i = 0; i < gameData.skills.Length; i++) {
            characters.Add(i);
        }
    }
    public List<StageObject> findStageObjects(StagePosition pos) {
        List<StageObject> objs = new List<StageObject>();

        foreach (StageObject obj in stageObjects) {
            if (obj.activated && !obj.taken && obj.pos.x == pos.x && obj.pos.y == pos.y) {
                objs.Add(obj);
            }
        }
        return objs;
    }
    public List<StageObject> findStageObjects(StagePosition pos, StageMove move) {
        return findStageObjects(getPosition(pos, move));
    }

    public List<StageObject> findStageObjects(StageMove move) {
        return findStageObjects(getPosition(findStagePosition(SceneManager.GetActiveScene().name), move));
    }

    public List<StageObject> findStageObjects() {
        return findStageObjects(StageMove.NONE);
    }

    public List<StageObject> allActiveStageObjects()
    {
        List<StageObject> objs = new List<StageObject>();
        foreach (StageObject obj in stageObjects)
        {
            if (obj.activated && !obj.taken)
            {
                objs.Add(obj);
            }
        }

        return objs;
    }
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

    public void activateSkillObject(string name, bool activate) {

        for (int i = 0; i < gameData.skills.Length; i++) {
            if (gameData.skills[i].name == name) {
                gameData.skills[i].activated = activate;
            }
        }
    }
    public StagePosition findStagePosition() {
        return findStagePosition(SceneManager.GetActiveScene().name);
    }

    public void reorganizeSkills(StageObject obj) {
        int[] skillCountEachFloor = new int[stageFloors.Length];

        for (int i = 0; i < skillCountEachFloor.Length; i++) {
            skillCountEachFloor[i] = 0;
        }

        for (int i = 0; i < gameData.skills.Length; i++) {
            if (gameData.skills[i].activated && !gameData.skills[i].taken)
            {
                skillCountEachFloor[gameData.skills[i].pos.y] += 1;
            }
        }


        int oldFloor = obj.pos.y;
        int newFloor = obj.pos.y;
        while (skillCountEachFloor[newFloor] > maxSkillsOnFloor[newFloor] && newFloor > 0) {
            skillCountEachFloor[newFloor]--;
            skillCountEachFloor[newFloor -= 1]++;
        }

        while (skillCountEachFloor[newFloor] > maxSkillsOnFloor[newFloor] && newFloor < stageFloors.Length)
        {
            skillCountEachFloor[newFloor]--;
            skillCountEachFloor[newFloor += 1]++;
        }

        //Move a skill from oldFloor to new floor

        StageObject movedSkill = gameData.skills[0];

        bool findMovedSkill = false;

        for (int i = 0; i < gameData.skills.Length; i++)
        {

            if ((maxSkillsOnFloor[oldFloor] == 0 || gameData.skills[i].name != obj.name) && gameData.skills[i].pos.y == oldFloor)
            {
                gameData.skills[i].pos.y = newFloor;
                movedSkill = gameData.skills[i];

                findMovedSkill = true;
            }
        }

        if (findMovedSkill)
        {
            for (int i = 0; i < gameData.skills.Length; i++)
            {

                int dist = gameData.skills[i].pos.x - movedSkill.pos.x;

                if (gameData.skills[i].pos.y == movedSkill.pos.y
                    && gameData.skills[i].name != movedSkill.name
                    && Mathf.Abs(dist) < minLatDistBetweenSkills)
                {
                    movedSkill.pos.x = gameData.skills[i].pos.x - minLatDistBetweenSkills * (dist > 0 ? 1:-1);
                }
            }
        }
        else {
            Debug.Log("Failed to find a skill to move on floor " + oldFloor);
        }
    }

    public StageObject nextCharacter() {

        int precCharacter = currentCharacter;
        if (characters.Count == 0) {
            fillCharacters();
        }

        int index = UnityEngine.Random.Range(0, characters.Count);

        int charac = characters[index];
        characters.RemoveAt(index);


        currentCharacter = charac;

        gameData.skills[precCharacter].activated = true;
        gameData.skills[precCharacter].taken = false;
        gameData.skills[currentCharacter].pos = findStagePosition();

        gameData.diamond.pos = gameData.skills[precCharacter].pos;
        gameData.diamond.taken = false;

        gameData.skills[currentCharacter].activated = false;
        gameData.skills[currentCharacter].taken = true;
        
        return gameData.skills[currentCharacter];
    }

    public StagePosition getPosition(StagePosition pos, StageMove move) {
        StagePosition npos;
        int stagesOnFloor = stageFloors[pos.y].stageNames.Length;

        switch (move)
        {
            case StageMove.LEFT:
                npos.y = pos.y;
                npos.x = pos.x - 1;
                npos.x = (npos.x + stagesOnFloor) % stagesOnFloor;
                break;
            case StageMove.RIGHT:
                npos.y = pos.y;
                npos.x = pos.x + 1;
                npos.x = npos.x % stagesOnFloor;
                break;
            case StageMove.UP:
                npos.x = pos.x;
                npos.y = pos.y + 1;

                if (npos.x >= stageFloors[npos.y].stageNames.Length)
                {
                    npos.x = stageFloors[npos.y].stageNames.Length - 1;
                }
                break;
            default:
                npos.x = pos.x;
                npos.y = pos.y;
                break;
        }
        return npos;
    }

    public string getMoveScene(StageMove move)
    {
        StagePosition pos = findStagePosition();
        pos = getPosition(pos, move);
        return stageFloors[pos.y].stageNames[pos.x];
    }

    public Vector3 positionToVector(StagePosition position) {
        Vector3 vec = Vector3.up * (0.05f + 0.95f * (stageFloors.Length - position.y) / stageFloors.Length);

        vec = Quaternion.AngleAxis(360.0f * position.x / stageFloors[position.y].stageNames.Length, Vector3.forward) * vec;

        return vec;
    }

    public void changeScene(StageMove move) {
        lastSceneHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().currentHealth;
        lastMove = move;
        string scene = getMoveScene(move);
        loadScene(scene);
    }

    public void loadScene(string scene)
    {
        SceneManager.LoadScene(scene);

    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        createObjects();
        UISingleton.instance.GetComponentInChildren<MinimapScript>().UpdateMinimap();
    }
    public void createObjects() {

        List<StageObject> objects = findStageObjects();

        if (objects.Count == 1)
        {
            GameObject prefab = Instantiate(objects[0].toInstantiate);


            prefab.transform.position = center + Vector3.up * prefab.transform.position.y;

        }
        else {
            for (int i = 0; i < objects.Count; i++) {
                Vector3 off = Vector3.right * objectsInstanceOffset;

                off = Quaternion.AngleAxis(i * 360f / objects.Count, Vector3.up) * off;

                GameObject prefab = Instantiate(objects[0].toInstantiate);

                off.y += prefab.transform.position.y;
                prefab.transform.position = center + off;
            }
        }

        if (objects.Count == 0) {
            Debug.Log("No objects");
        }

        createPlayer();
    }

    public void updatePlayerSkills(GameObject player) {

        foreach (StageObject skill in gameData.skills)
        {
            switch (skill.name[0])
            {
                case 'p':
                    player.GetComponent<Ability_Protection>().enabled = skill.taken;
                    break;
                case 'j':
                    player.GetComponent<Ability_Jump>().enabled = skill.taken;
                    break;
                case 'd':
                    player.GetComponent<Ability_Dash>().enabled = skill.taken;
                    break;
                case 'm':
                    player.GetComponent<Ability_MaxHealth>().enabled = skill.taken;
                    break;
                case 's':
                    player.GetComponent<Ability_Shock>().enabled = skill.taken;
                    break;
                default:
                    break;
            }
        }
    }
    public void takeAndDeactivateObject(string name) {
        foreach (StageObject obj in stageObjects) {
            if (obj.name == name)
            {
                obj.taken = true;
                obj.activated = false;
                
            }
        }
        UISingleton.instance.GetComponentInChildren<MinimapScript>().UpdateMinimap();
        updatePlayerSkills(GameObject.FindGameObjectWithTag("Player"));
    }

    public void createPlayer() {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject pl in players) {
            Destroy(pl);
        }

        GameObject player = Instantiate(playerPrefab);
        player.transform.position = spawnPoints[(int)lastMove];
        updatePlayerSkills(player);
        player.GetComponent<PlayerHealth>().currentHealth = lastSceneHealth;

    }
}
