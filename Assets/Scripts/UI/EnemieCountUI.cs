using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class EnemieCountUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI nameText;
    [SerializeField]
    TextMeshProUGUI countText;

    char uniformChar = 'I';

    public void SetEnemieCount(int count)
    {
        if(count > 0)
        {
            nameText.enabled = true;
            countText.enabled = true;

            countText.text = string.Concat(Enumerable.Repeat(uniformChar, count));
        }
        else
        {
            nameText.enabled = false;
            countText.enabled = false;
        }
    }

}
