using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class SetVersion : MonoBehaviour
{
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = "Version " + Application.version;
    }
}
