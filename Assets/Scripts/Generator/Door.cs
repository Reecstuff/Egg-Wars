using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(BoxCollider))]
public class Door : MonoBehaviour
{
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
                    player.transform.position = DungeonMaster.Instance.GetBossRoom();
                    DungeonMaster.Instance.BossRoomTime = false;
                }
                else
                {
                    // Generate Dungeons in next Dungeon
                    CallDungeonGeneration();
                    ActivateNextDungeon();
                    player.transform.position = MovePosition();
                }
                doorVisited = true;
            }
            else
            {
                player.transform.position = MovePosition();
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
        if(NextDoor == null)
        {
          Debug.Log(dungeon);
        }

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
                PlayerSpawnPosition = transform.position + Vector3.right * 2;
                break;
            case Direction.right:
                PlayerSpawnPosition = transform.position + Vector3.left * 2;
                break;
            case Direction.up:
                PlayerSpawnPosition = transform.position + Vector3.back * 2;
                break;
            case Direction.down:
                PlayerSpawnPosition = transform.position + Vector3.forward * 2;
                break;
            default:
                break;
        }
    }

    private void OnValidate()
    {
        if (DoorInLevelDirection == Direction.nothing)
        {
            CalculateDoorDirection();
        }
        if(GetComponent<BoxCollider>().size.y <= 1f)
        {
            CalculateBoxCollider();
        }
    }

    void CalculateDoorDirection()
    {
        
        if(Mathf.Abs(transform.localPosition.x) > Mathf.Abs(transform.localPosition.z))
        {
            if(transform.localPosition.x > 0)
            {
                DoorInLevelDirection = Direction.right;
            }
            else
            {
                DoorInLevelDirection = Direction.left;
            }
        }
        else
        {
            if (transform.localPosition.z > 0)
            {
                DoorInLevelDirection = Direction.up;
            }
            else
            {
                DoorInLevelDirection = Direction.down;
            }
        }
    }

    void CalculateBoxCollider()
    {
        Bounds boxSize = new Bounds();
        boxSize.size = Vector3.zero;

        MeshFilter[] meshes = GetComponentsInChildren<MeshFilter>();

        for (int i = 0; i < meshes.Length; i++)
        {
            boxSize.Encapsulate(meshes[i].sharedMesh.bounds);
        }

        switch (DoorInLevelDirection)
        {
            case Direction.left:
            case Direction.right:
                boxSize.size += Vector3.right * .5f;
                break;
            case Direction.up:
            case Direction.down:
                boxSize.size += Vector3.forward * .5f;
                break;
        }

        GetComponent<BoxCollider>().size = boxSize.size;

    }
}
