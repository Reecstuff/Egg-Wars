using UnityEngine;

[CreateAssetMenu(fileName = "ItemText", menuName = "ItemText")]
public class ItemText : ScriptableObject
{
    public string Titel;

    [TextArea(1, 4)]
    public string Description;
}
