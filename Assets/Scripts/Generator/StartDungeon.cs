using UnityEngine;

public class StartDungeon : MonoBehaviour
{

    [SerializeField]
    GameObject firstDungeon;

    [SerializeField]
    GameObject playerObject;


    void Start()
    {
        DungeonMaster.Instance.dungeonStarter = this;
    }

    private void OnEnable()
    {
        DungeonOn();
    }

    public void DungeonOn()
    {
        GameObject newStartDungeon = Instantiate(firstDungeon);
        newStartDungeon.transform.position = Vector3.zero;

        playerObject.transform.position = Vector3.up * 2;
    }
}
