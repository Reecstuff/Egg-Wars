using UnityEngine;
using UnityEngine.AI;

public class StartDungeon : MonoBehaviour
{

    [SerializeField]
    GameObject firstDungeon;

    [SerializeField]
    GameObject playerObject;

    [SerializeField]
    NavMeshSurface navi;


    void Start()
    {
        DungeonMaster.Instance.dungeonStarter = this;
        DungeonMaster.Instance.navi = navi;
        DungeonOn();
    }


    public void DungeonOn()
    {
        GameObject newStartDungeon = Instantiate(firstDungeon);
        newStartDungeon.transform.position = Vector3.zero * 1.5f;
        playerObject.transform.position = Vector3.up;

        newStartDungeon.GetComponent<DungeonGenerator>().StartDungeon();

        DungeonMaster.Instance.SetNewDungeons(newStartDungeon.GetComponent<DungeonGenerator>(), Direction.nothing);
    }
}
