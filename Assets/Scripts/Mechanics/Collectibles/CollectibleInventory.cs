using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollectibleInventory : MonoBehaviour
{
    public int NumberOfSushi { get; private set; }

    public UnityEvent<CollectibleInventory> OnSushiCollected;

    public void SushiCollected()
    {
        NumberOfSushi++;
        OnSushiCollected.Invoke(this);
    }
}
