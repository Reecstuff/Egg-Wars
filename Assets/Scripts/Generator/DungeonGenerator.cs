using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class DungeonGenerator : MonoBehaviour
{
    /// <summary>
    /// Dungeon Categorie
    /// </summary>
    [SerializeField]
    protected List<DungeonObstacle> dungeonObstaclesCat;

    [SerializeField]
    protected Transform[] spawnPositions;

    public List<Door> doors;

    public List<Direction> directions;

    protected List<DungeonObstacle> noZeroDungeonObst = new List<DungeonObstacle>();

    protected int sumPercentage;


    protected void Awake()
    {
        CalculateNoZeroDungeonObst();
    }

    protected void CalculateNoZeroDungeonObst()
    {
        noZeroDungeonObst.Clear();
        for (int i = 0; i < dungeonObstaclesCat.Count; i++)
        {
            if (dungeonObstaclesCat[i].SpawnPercentage > 0)
                noZeroDungeonObst.Add(dungeonObstaclesCat[i]);
        }

        noZeroDungeonObst.OrderBy(n => n.SpawnPercentage);
    }

    protected void Start()
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

    virtual public void StartDungeon()
    {
        BuildEnvironment();
        BuildNavMesh();
    }

    protected void BuildEnvironment()
    {
        if(spawnPositions.Length > 0)
        {
            // Go Through every Position
            for (int i = 0; i < spawnPositions.Length; i++)
            {
                // Roll percentage Random for every Categorie
                for (int l = 0; l < noZeroDungeonObst.Count; l++)
                {

                    int rnd = Random.Range(1, 101);

                    if(rnd <= noZeroDungeonObst[l].SpawnPercentage || l == noZeroDungeonObst.Count - 1)
                    {
                        GameObject obstacle = Instantiate(GetDungeonObject(noZeroDungeonObst[l]), transform);
                        SetObstacle(ref obstacle,ref i,ref l);
                        break;
                    }
                }
            }
        }
    }

    protected virtual void SetObstacle(ref GameObject obstacle,ref int i,ref int l)
    {
        // Roll random for which Index in Categorie to spawn
        // And Initialize it on Position
        obstacle.transform.position = spawnPositions[i].position;
        obstacle.transform.Rotate(new Vector3(0, Random.Range(0, 9) * 45, 0), Space.Self);
    }

    protected GameObject GetDungeonObject(DungeonObstacle obstacleCategorie)
    {
        return obstacleCategorie.categorieList.Obstacles[Random.Range(0, obstacleCategorie.categorieList.Obstacles.Length)];
    }


    protected void BuildNavMesh()
    {
        DungeonMaster.Instance.navi.BuildNavMesh();
    }

    protected void SearchDoors()
    {
        doors = new List<Door>();
        if(doors.Count == 0)
            doors = GetComponentsInChildren<Door>().ToList();
    }

    protected void SetDirections()
    {
        // Set the Direction for this Dungeon
        if (doors != null && doors.Count != 0)
        {
            directions = new List<Direction>();
            directions = doors.ConvertAll(d => d.DoorInLevelDirection);
        }
    }

    protected void OnValidate()
    {
        if(doors == null || doors.Count == 0)
        {
            SearchDoors();
        }
        if(directions == null || directions.Count == 0)
        {
            SetDirections();
        }

        if(dungeonObstaclesCat.Count > 0)
            sumPercentage = dungeonObstaclesCat.ConvertAll(d => d.SpawnPercentage).Sum();

        if(sumPercentage > 100)
        {
            while(sumPercentage > 100)
            {
                for (int i = 0; i < dungeonObstaclesCat.Count; i++)
                {
                    if(sumPercentage > 100)
                    {
                        if (dungeonObstaclesCat[i].SpawnPercentage > 0)
                            dungeonObstaclesCat[i].SpawnPercentage--;
                    }
                    else
                    {
                        break;
                    }
                    sumPercentage = dungeonObstaclesCat.ConvertAll(d => d.SpawnPercentage).Sum();
                }
            }
        }
    }
}
