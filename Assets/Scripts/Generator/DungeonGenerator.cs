using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class DungeonGenerator : MonoBehaviour
{

    [SerializeField]
    List<DungeonObstacle> dungeonObstacles;

    [SerializeField]
    Transform[] spawnPositions;

    public List<Door> doors;

    public List<Direction> directions;


    private void Start()
    {
        if (doors.Count == 0)
        {
            SearchDoors();
        }
        if (directions.Count == 0)
        {
            SetDirections();
        }
    }

    public void StartDungeon()
    {
        BuildEnvironment();
        BuildNavMesh();
        SpawnEnemies();
    }

    void BuildEnvironment()
    {
        // Spawn Obstacles
        // Spawn Traps
    }

    void BuildNavMesh()
    {
        DungeonMaster.Instance.navi.BuildNavMesh();
    }

    void SpawnEnemies()
    {
        // Spawn Enemies with Navmeshagents
    }

    void SearchDoors()
    {
        doors = new List<Door>();
        if(doors.Count == 0)
            doors = GetComponentsInChildren<Door>().ToList();
    }

    private void SetDirections()
    {
        directions = new List<Direction>();
        // Set the Direction for this Dungeon
        if (doors != null && doors.Count != 0)
        {
            directions = doors.ConvertAll(d => d.DoorInLevelDirection);
        }
    }

    private void OnValidate()
    {
        if(doors == null || doors.Count == 0)
        {
            SearchDoors();
        }
        if(directions == null || directions.Count == 0)
        {
            SetDirections();
        }

        int sum = dungeonObstacles.ConvertAll(d => d.SpawnPercentage).Sum();
        if (sum > 100)
        {
            int overhead = sum - 100;


            for (int i = 0; i < dungeonObstacles.Count; i++)
            {
                if(dungeonObstacles[i].SpawnPercentage != 0 && dungeonObstacles[i].SpawnPercentage - overhead >= 0)
                {
                    
                    dungeonObstacles[i].SpawnPercentage -= overhead;
                    break;

                }
            }




        }
    }
}
