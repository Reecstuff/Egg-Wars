using UnityEngine;
using UnityEngine.AI;

public class DungeonGenerator : MonoBehaviour
{
    [SerializeField]
    GameObject[] doors;

    void OnEnable()
    {
        BuildEnvironment();
        BuildNavMesh();
        SpawnEnemies();
    }

    void BuildEnvironment()
    {

    }

    void BuildNavMesh()
    {
        DungeonMaster.Instance?.navi.BuildNavMesh();
    }

    void SpawnEnemies()
    {

    }
}
