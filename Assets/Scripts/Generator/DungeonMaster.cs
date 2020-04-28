using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class DungeonMaster : MonoBehaviour
{
    public static DungeonMaster Instance;
    public NavMeshSurface navi;
    public bool BossRoomTime;
    [HideInInspector]
    public StartDungeon dungeonStarter;

    [SerializeField]
    int dungeonOffset = 100;

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
        currentLevelDungeons = new List<DungeonGenerator>();
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

    public void SetNewDungeons(DungeonGenerator currentDungeon, Direction originDirection)
    {
        Direction opposite = GetOppositeDirection(originDirection);

        // Generate Multiple Dungeon based on current
        for (int i = 0; i < currentDungeon.directions.Count; i++)
        {
            if(opposite != currentDungeon.directions[i])
                PlaceNewDungeon(ref currentDungeon, currentDungeon.directions[i]);
        }
      
    }

    void PlaceNewDungeon(ref DungeonGenerator currentDungeon, Direction currentDirection)
    {
        Direction oppositeOfCurrent = GetOppositeDirection(currentDirection);

        Vector3 nextDungeonPosition = currentDungeon.gameObject.transform.position + AddOffset(currentDirection);

        DungeonGenerator nextDungeon = null;

        //Check if Dungeon already exits
        if (nextDungeon = GetDungeonOnPoint(nextDungeonPosition, currentDungeon))
        {
            // Set Up Door in Direction to this Dungeon
            SetUpDoor(ref currentDungeon, nextDungeon, currentDirection);
        }
        else
        {
            // Get Dungeons with dockable Doors
            DungeonGenerator[] dungeonsInDirection = dungeons.Where(d => d.directions.Any(x => x == oppositeOfCurrent)).ToArray();

            // Instantiate Dungeon
            GameObject newDungeon = Instantiate(dungeonsInDirection[GetRandomIndex(dungeonsInDirection.Length)].gameObject);

            // Set Position
            newDungeon.transform.position = nextDungeonPosition;

            // Add Dungeon to List
            currentLevelDungeons.Add(newDungeon.GetComponent<DungeonGenerator>());

            // Set Up Door in Direction to the Dungeon
            SetUpDoor(ref currentDungeon, currentLevelDungeons.Last(), currentDirection);
        }
    }

    void SetUpDoor(ref DungeonGenerator currentDungeon, DungeonGenerator nextDungeon, Direction currentDirection)
    {
        // Find Door in current Dungeon
        Door currentDoor = currentDungeon.doors.Find(d => d.DoorInLevelDirection == currentDirection);
        currentDoor.dungeon = currentDungeon;

        // Find Door in next Dungeon
        Door oppositeDoor = GetOppositeDoor(currentDirection, nextDungeon);
        currentDoor.NextDoor = oppositeDoor;
        oppositeDoor.dungeon = nextDungeon;
        oppositeDoor.NextDoor = currentDoor;
        oppositeDoor.CalculatePlayerPostion();
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
                opposite = Direction.nothing;
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

    /// <summary>
    /// Check through list if Dungeon exits on this Point
    /// </summary>
    /// <returns></returns>
    DungeonGenerator GetDungeonOnPoint(Vector3 dungeonPosition, DungeonGenerator currentDungeon)
    {
        return currentLevelDungeons.Find(cd => Vector3.Distance(cd.gameObject.transform.position, dungeonPosition) <= 50.0 && cd != currentDungeon);
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
