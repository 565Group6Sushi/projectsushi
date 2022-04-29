using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectibleUI : MonoBehaviour
{
    private TextMeshProUGUI sushiText;

    // Start is called before the first frame update
    void Start()
    {
        sushiText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateSushiText(CollectibleInventory inventory)
    {
        var sushiScore = PlayerPrefs.GetInt("RunScore") * 100;
        sushiText.text = sushiScore.ToString();
    }
}
