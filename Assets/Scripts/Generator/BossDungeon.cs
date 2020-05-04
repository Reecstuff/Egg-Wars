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

    List<Enemy> allEnemies = new List<Enemy>();
    List<GameObject> allObstacles = new List<GameObject>();

    DungeonObstacle changedObstacle;

    public override void StartDungeon()
    {
        SetUpBossRoom();
        base.StartDungeon();
        CalculateEnemies();
    }

    /// <summary>
    /// Get alle Obstacles of Dungeon for later Despawning
    /// </summary>
    protected override void SetObstacle(ref GameObject obstacle,ref int i,ref int l)
    {
        base.SetObstacle(ref obstacle,ref i,ref l);

        if (obstacle.GetComponent<Enemy>())
            allEnemies.Add(obstacle.GetComponent<Enemy>());
        else
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

    void CalculateEnemies()
    {
        allEnemies = GetComponentsInChildren<Enemy>().ToList();

        for (int i = 0; i < allEnemies.Count; i++)
        {
            allEnemies[i].OnEnemyDeath += EnemyKilled;
        }
        allObstacles.Except(allEnemies.ConvertAll(e => e.gameObject));
    }

    /// <summary>
    /// Enemy gets killed in Bossroom
    /// </summary>
    void EnemyKilled(Enemy enemy)
    {
        enemy.OnEnemyDeath -= EnemyKilled;
        allEnemies.Remove(enemy);
        if (allEnemies.Count == 0)
        {
            StageClear();
        }
    }


    /// <summary>
    /// Stage is Clear reset Level
    /// </summary>
    void StageClear()
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
