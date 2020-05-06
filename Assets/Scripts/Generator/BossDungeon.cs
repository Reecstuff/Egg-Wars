using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossDungeon : DungeonGenerator
{

    public Transform PlayerPosition;

    [SerializeField]
    Transform bossSpawnField;

    [SerializeField]
    GameObject bossPrefab;

    GameObject currentBoss;

    List<GameObject> allObstacles = new List<GameObject>();

    DungeonObstacle changedObstacle;

    public override void StartDungeon()
    {
        SetUpBossRoom();
        base.StartDungeon();
    }

    public void Reset()
    {
        for (int i = 0; i < allObstacles.Count; i++)
        {
            Destroy(allObstacles[i]);
        }
        allObstacles.Clear();
    }

    /// <summary>
    /// Get alle Obstacles of Dungeon for later Despawning
    /// </summary>
    protected override void SetObstacle(ref GameObject obstacle,ref int i,ref int l)
    {
        base.SetObstacle(ref obstacle,ref i,ref l);

        allObstacles.Add(obstacle);
        
    }

    void SetUpBossRoom()
    {
        RecalculatePercentage();
        CalculateNoZeroDungeonObst();
    }

    /// <summary>
    /// Set Boss or Wave
    /// </summary>
    private void RecalculatePercentage()
    {
        if (DungeonMaster.Instance.levelCount % 2 == 0)
        {
            changedObstacle = dungeonObstaclesCat.Find(d => d.categorieList.name.Equals("Enemies"));
            changedObstacle.SpawnPercentage = 50;
        }
        else
        {
            changedObstacle = dungeonObstaclesCat.Find(d => d.categorieList.name.Equals("Traps"));
            changedObstacle.SpawnPercentage = 40;
            SpawnBoss();
        }

    }

    void SpawnBoss()
    {
        currentBoss = Instantiate(bossPrefab, transform);
        currentBoss.transform.position = bossSpawnField.position;
    }

    protected override void CalculateEnemies()
    {
        base.CalculateEnemies();
        allObstacles.Except(allEnemies.ConvertAll(e => e.gameObject));
    }

    /// <summary>
    /// Stage is Clear reset Level
    /// </summary>
    protected override void StageClear()
    {
        DungeonMaster.Instance.AdvanceLevel();
        CleanUpDungeon();
    }

    /// <summary>
    /// Cleanup BossDungeon
    /// </summary>
    void CleanUpDungeon()
    {
        changedObstacle.SpawnPercentage = 0;
        for (int i = 0; i < allObstacles.Count; i++)
        {
            Destroy(allObstacles[i].gameObject);
        }
        allObstacles.Clear();
    }
}
