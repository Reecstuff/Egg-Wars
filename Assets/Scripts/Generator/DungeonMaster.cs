using UnityEngine;
using UnityEngine.AI;

public class DungeonMaster : MonoBehaviour
{
    public static DungeonMaster Instance;
    public NavMeshSurface navi;

    [SerializeField]
    GameObject[] dungeons;

    

    private void Awake()
    {
        MakeSingelton();
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

    public void SetNewDungeons()
    {

    }

    public void AdvanceLevel()
    {

    }
}
