using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    [SerializeField]
    List<Door> doors;

    [HideInInspector]
    public List<Direction> directions;

    void OnEnable()
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
        DungeonMaster.Instance?.navi.BuildNavMesh();
    }

    void SpawnEnemies()
    {

    }

    private void OnValidate()
    {
        // Set the Direction for this Dungeon
        if(doors.Count != 0)
        {
            directions.Clear();
            directions = doors.ConvertAll(d => d.DoorInLevelDirection);
        }
    }
}
