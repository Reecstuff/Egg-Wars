using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class DungeonMaster : MonoBehaviour
{
    public static DungeonMaster Instance;
    public NavMeshSurface navi;
    public bool BossRoomTime;
    public StartDungeon dungeonStarter;

    [SerializeField]
    int dungeonOffset;

    [SerializeField]
    List<DungeonGenerator> dungeons;

    public List<DungeonGenerator> currentLevelDungeons;

    

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

    public void SetNewDungeons(DungeonGenerator currentDungeon)
    {
        // Generate Multiple Dungeon based on current
        for (int i = 0; i < currentDungeon.directions.Count; i++)
        {
            PlaceNewDungeon(ref currentDungeon, currentDungeon.directions[i]);
        }
      
    }

    void PlaceNewDungeon(ref DungeonGenerator currentDungeon, Direction currentDirection)
    {
        Direction oppositeOfCurrent = GetOppositeDirection(currentDirection);
        // Get Dungeons with dockable Doors
        DungeonGenerator[] dungeonsInDirection = dungeons.Where(d => d.directions.Any(x => x == oppositeOfCurrent)).ToArray();

        // Instantiate Dungeon
        GameObject newDungeon = Instantiate(dungeonsInDirection[GetRandomIndex(dungeonsInDirection.Length)].gameObject);
        
        // Dont activate Dungeon
        newDungeon.SetActive(false);

        // Set Position

        newDungeon.transform.position = currentDungeon.gameObject.transform.position + AddOffset(currentDirection);

        currentLevelDungeons.Add(newDungeon.GetComponent<DungeonGenerator>());

        // Set Up Door in Direction to the Dungeon
        SetUpDoor(ref currentDungeon, currentLevelDungeons.Last(), currentDirection);
    }

    void SetUpDoor(ref DungeonGenerator currentDungeon, DungeonGenerator nextDungeon, Direction currentDirection)
    {
        Door currentDoor = currentDungeon.doors.Find(d => d.DoorInLevelDirection == currentDirection);
        currentDoor.dungeon = currentDungeon;
        currentDoor.NextDoor = GetOppositeDoor(currentDirection, nextDungeon);
    }

    Door GetOppositeDoor(Direction doorDirection, DungeonGenerator nextDungeon)
    {
        Direction opposite = GetOppositeDirection(doorDirection);
       
        return nextDungeon.doors.Find(d => d.DoorInLevelDirection == opposite);
    }

    Direction GetOppositeDirection(Direction findOpposite)
    {
        Direction opposite;

        switch (findOpposite)
        {
            case Direction.nothing:
                opposite = Direction.down;
                break;
            case Direction.left:
                opposite = Direction.right;
                break;
            case Direction.right:
                opposite = Direction.left;
                break;
            case Direction.up:
                opposite = Direction.down;
                break;
            case Direction.down:
                opposite = Direction.up;
                break;
            default:
                opposite = Direction.down;
                break;
        }

        return opposite;
    }

    Vector3 AddOffset(Direction offsetDirection)
    {
        switch (offsetDirection)
        {
            case Direction.nothing:
                return Vector3.forward * dungeonOffset;
            case Direction.left:
                return -Vector3.right * dungeonOffset;
            case Direction.right:
                return Vector3.right * dungeonOffset;
            case Direction.up:
                return Vector3.forward * dungeonOffset;
            case Direction.down:
                return -Vector3.forward * dungeonOffset;
            default:
                return Vector3.forward * dungeonOffset;
        }
    }


    public void AdvanceLevel()
    {
        // Reset Dungeon except First one
        
        // Make a Bossroom
    }

    public Vector3 GetBossRoom()
    {
        return Vector3.zero;
    }

    int GetRandomIndex(int length)
    {
        return Random.Range(0, length);
    }
}
