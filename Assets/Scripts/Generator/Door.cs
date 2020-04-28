using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(BoxCollider))]
public class Door : MonoBehaviour
{
    [HideInInspector]
    public Door NextDoor;

    [HideInInspector]
    public DungeonGenerator dungeon;

    public Direction DoorInLevelDirection;

    public Vector3 PlayerSpawnPosition;

    bool doorVisited;
    
    private void Start()
    {
        GetComponent<BoxCollider>().isTrigger = true;
        doorVisited = false;

        if (PlayerSpawnPosition == Vector3.zero)
        {
            CalculatePlayerPostion();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        // Check for player
        if(other.gameObject.GetComponent<PlayerController>())
        {
            GameObject player = other.gameObject;

            // Check if generate Dungeon
            if(!doorVisited)
            {
                if(DungeonMaster.Instance.BossRoomTime)
                {
                    player.GetComponent<NavMeshAgent>().enabled = false;
                    player.transform.position = DungeonMaster.Instance.GetBossRoom();
                    player.GetComponent<NavMeshAgent>().enabled = true;
                    DungeonMaster.Instance.BossRoomTime = false;
                }
                else
                {
                    ActivateNextDungeon();
                    player.GetComponent<NavMeshAgent>().enabled = false;
                    player.transform.position = MovePosition();
                    player.GetComponent<NavMeshAgent>().enabled = true;
                    // Generate Dungeons in next Dungeon
                    CallDungeonGeneration();
                }
                doorVisited = true;
            }
            else
            {
                player.GetComponent<NavMeshAgent>().enabled = false;
                player.transform.position = MovePosition();
                player.GetComponent<NavMeshAgent>().enabled = true;

            }
        }
    }


    Vector3 MovePosition()
    {
        return NextDoor.PlayerSpawnPosition;
    }

    // Generate other Dungeons in next Dungeon
    void CallDungeonGeneration()
    {
        DungeonMaster.Instance.SetNewDungeons(NextDoor.dungeon, DoorInLevelDirection);
    }

    void ActivateNextDungeon()
    {
        NextDoor.dungeon.StartDungeon();
        NextDoor.doorVisited = true;
    }

    public void CalculatePlayerPostion()
    {
        switch (DoorInLevelDirection)
        {
            case Direction.nothing:
                PlayerSpawnPosition = transform.position;
                break;
            case Direction.left:
                PlayerSpawnPosition = transform.position - Vector3.right;
                break;
            case Direction.right:
                PlayerSpawnPosition = transform.position + Vector3.right;
                break;
            case Direction.up:
                PlayerSpawnPosition = transform.position + Vector3.forward;
                break;
            case Direction.down:
                PlayerSpawnPosition = transform.position - Vector3.forward;
                break;
            default:
                break;
        }
    }
}
