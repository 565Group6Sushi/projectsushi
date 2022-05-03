using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PointsFading : MonoBehaviour
{
    private RectTransform rectTransform;
    private float leftTransform;
    private bool fadeIn = false;

    private float fadeInTime = 5f, fadeSpeed = 20f, currentFadeTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x - 500, rectTransform.offsetMin.y);
    }

    // Update is called once per frame
    void Update()
    {
        TransformFade();
    }

    void TransformFade()
    {
        if (fadeIn)
        {
            if (rectTransform.offsetMin.x < 0)
            {
                rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x + fadeSpeed, rectTransform.offsetMin.y);
            }

            currentFadeTime += Time.deltaTime;

            if (currentFadeTime > fadeInTime)
            {
                fadeIn = false;
            }
        } else
        {
            if (rectTransform.offsetMin.x > -500)
            {
                rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x - fadeSpeed, rectTransform.offsetMin.y);
            }
        }
    }

    public void UpdateFadeStatus(bool fadeBool)
    {
        fadeIn = fadeBool;
        currentFadeTime = 0;
    }
}
