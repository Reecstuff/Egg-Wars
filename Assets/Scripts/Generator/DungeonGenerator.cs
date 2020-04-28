using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public List<Door> doors;

    [HideInInspector]
    public List<Direction> directions;

    private void Start()
    {
        SetDirections();
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

    private void SetDirections()
    {
        // Set the Direction for this Dungeon
        if (doors != null && doors.Count != 0)
        {
            directions = doors.ConvertAll(d => d.DoorInLevelDirection);
        }
    }
}
