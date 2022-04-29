using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollectibleInventory : MonoBehaviour
{
    public UnityEvent<CollectibleInventory> OnSushiCollected;

    public void SushiCollected()
    {
        int savedScore = PlayerPrefs.GetInt("RunScore");
        savedScore++;
        PlayerPrefs.SetInt("RunScore", savedScore);
        OnSushiCollected.Invoke(this);
    }
}
