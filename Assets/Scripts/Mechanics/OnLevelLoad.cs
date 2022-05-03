using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnLevelLoad : MonoBehaviour
{
    public GameObject blackOutSquare;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeToBlack.FadeBlackOutSquare(blackOutSquare, false));
    }
}
