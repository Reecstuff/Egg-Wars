using UnityEditor.Experimental.GraphView;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Door : MonoBehaviour
{
    [HideInInspector]
    public Door NextDoor;

    [HideInInspector]
    public Vector3 NextDungeonPosition;

    [HideInInspector]
    public DungeonGenerator dungeon;

    public Direction DoorInLevelDirection;

    [SerializeField]
    Transform PlayerSpawnPosition;

    bool doorVisited;
    
    private void Start()
    {
        GetComponent<BoxCollider>().isTrigger = true;
        doorVisited = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        // Check for player
        if(other.gameObject.GetComponent<PlayerController>())
        {
            // Check if generate Dungeon
            if(!doorVisited)
            {
                if(DungeonMaster.Instance.BossRoomTime)
                {
                    other.gameObject.transform.position = DungeonMaster.Instance.GetBossRoom();
                }
                else
                {
                    other.gameObject.transform.position = MovePosition();
                    // Generate Dungeons in next Dungeon
                    CallDungeonGeneration();
                }
            }
            else
            {
                other.gameObject.transform.position = MovePosition();

            }
        }
    }


    Vector3 MovePosition()
    {
        return PlayerSpawnPosition.position;
    }

    // Generate other Dungeons in next Dungeon
    void CallDungeonGeneration()
    {
        DungeonMaster.Instance.SetNewDungeons(DoorInLevelDirection, dungeon);
    }
}
