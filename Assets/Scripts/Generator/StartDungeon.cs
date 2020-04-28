﻿using UnityEngine;
using UnityEngine.AI;

public class StartDungeon : MonoBehaviour
{

    [SerializeField]
    DungeonGenerator firstDungeon;

    [SerializeField]
    GameObject playerObject;

    [SerializeField]
    NavMeshSurface navi;


    void Start()
    {
        DungeonMaster.Instance.dungeonStarter = this;
        DungeonMaster.Instance.navi = navi;

        DungeonOn();
    }


    public void DungeonOn()
    {
        GameObject newStartDungeon = Instantiate(firstDungeon.gameObject);
        newStartDungeon.transform.position = Vector3.zero * 1.5f;
        playerObject.transform.position = Vector3.up;

        firstDungeon = newStartDungeon.GetComponent<DungeonGenerator>();

        firstDungeon.StartDungeon();

        DungeonMaster.Instance.currentLevelDungeons.Add(firstDungeon);
        DungeonMaster.Instance.SetNewDungeons(firstDungeon, Direction.nothing);
    }
}
