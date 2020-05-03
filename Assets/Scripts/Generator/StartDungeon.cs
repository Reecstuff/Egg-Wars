using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class StartDungeon : MonoBehaviour
{

    public DungeonGenerator firstDungeon;

    public BossDungeon bossRoom;

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
        GameAudio.Instance.SetNormalMusic();
        SetupDungeon();
        SetupPlayer();

    }

    private void SetupPlayer()
    {
        DungeonMaster.Instance.PlayerMoving = true;
        Invoke(nameof(ResetMoving), 2.0f);
        playerObject.transform.DOJump(Vector3.up * 1.5f, 1, 1, 2f);
    }

    private void SetupDungeon()
    {
        GameObject newStartDungeon = Instantiate(firstDungeon.gameObject);
        newStartDungeon.transform.position = Vector3.zero * 1.5f;

        firstDungeon = newStartDungeon.GetComponent<DungeonGenerator>();

        firstDungeon.gameObject.SetActive(true);
        firstDungeon.StartDungeon();

        DungeonMaster.Instance.currentLevelDungeons.Add(firstDungeon);
        DungeonMaster.Instance.SetNewDungeons(firstDungeon, Direction.nothing);
        DescriptionText.Instance.fadeOutInfo.FadeOutText(string.Concat("Level ", DungeonMaster.Instance.levelCount));
    }

    void ResetMoving()
    {
        DungeonMaster.Instance.PlayerMoving = false;
    }
}
