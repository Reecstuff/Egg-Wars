using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DungeonMaster : MonoBehaviour
{
    public static DungeonMaster Instance;
    public NavMeshSurface navi;
    public bool BossRoomTime;

    [SerializeField]
    List<DungeonGenerator> dungeons;

    

    private void Awake()
    {
        MakeSingelton();
    }


    private void Start()
    {
        BossRoomTime = false;
    }

    void MakeSingelton()
    {
        if (Instance != null)
            Destroy(this.gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SetNewDungeons(Direction direction, DungeonGenerator currentDungeon)
    {
        // Generate Multiple Dungeon based on current

        if(direction.Equals(Direction.nothing) && currentDungeon == null)
        {
            // Generate Dungeon on Zero Point
        }
        else
        {
            // Generate Dungeon in Direction of Doors
        }
    }

    public void AdvanceLevel()
    {

    }

    public Vector3 GetBossRoom()
    {
        return Vector3.zero;
    }
}
