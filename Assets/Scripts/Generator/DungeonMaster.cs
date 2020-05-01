using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class DungeonMaster : MonoBehaviour
{
    public static DungeonMaster Instance;
    public NavMeshSurface navi;


    /// <summary>
    /// Player is moving between Doors
    /// </summary>
    public bool PlayerMoving = false;
    public bool BossRoomTime;

    public float PlayerMovingTime = 0.3f;

    public PlayerController player;

    [SerializeField]
    Material bossDoorsMaterial;

    [HideInInspector]
    public StartDungeon dungeonStarter;

    [SerializeField]
    int dungeonOffset = 100;

    [SerializeField]
    List<DungeonGenerator> dungeons;

    public List<DungeonGenerator> currentLevelDungeons;

    int currentDungeonCount = 0;
    int currentDungeonMax = 3;
    public int levelCount = 1;

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



            newDungeon.SetActive(false);
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

    /// <summary>
    /// Call this on Boss Defeat
    /// </summary>
    public void AdvanceLevel()
    {
        // Destroy every Dungeon
        for (int i = 0; i < currentLevelDungeons.Count; i++)
        {
            Destroy(currentLevelDungeons[i].gameObject);
        }

        // Clean up
        currentLevelDungeons.Clear();
        levelCount++;
        currentDungeonCount = 0;
        currentDungeonMax += levelCount;

        player.transform.position = Vector3.up * 3;

        dungeonStarter.DungeonOn();

        BossRoomTime = false;

        // Reset Dungeon
    }

    public void RaiseDungeonCount()
    {
        currentDungeonCount++;
        if(currentDungeonCount >= currentDungeonMax)
        {
            // Set Value for Bossroom
            BossRoomTime = true;
            SetUpBossRoom();
            GameAudio.Instance.SetBossMusic();
        }
    }

    public void  SetUpBossRoom()
    {
        // Go through all Doors that are not visited and change the Material
        for (int i = 0; i < currentLevelDungeons.Count; i++)
        {
            for (int l = 0; l < currentLevelDungeons[i].doors.Count; l++)
            {
                if(currentLevelDungeons[i].doors[l].doorVisited == false)
                {
                    currentLevelDungeons[i].doors[l].GetComponentsInChildren<Renderer>().All(r => r.sharedMaterial = bossDoorsMaterial);
                }
            }
        }
    }

    public Vector3 GetBossRoom()
    {
        dungeonStarter.bossRoom.StartDungeon();
        dungeonStarter.bossRoom.gameObject.SetActive(true);

        return dungeonStarter.bossRoom.transform.position  + Vector3.up;
    }

    int GetRandomIndex(int length)
    {
        return Random.Range(0, length);
    }
}
