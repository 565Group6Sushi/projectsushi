using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SushiCollectible : MonoBehaviour
{
    float floatSpeed = 1f, floatHeight = 0.1f;
    float collectSpeed = 4f, collectHeight = 4f, currentMovedHeight = 0f;
    float scaleSpeed = 4f;
    bool collected = false;
    Vector3 pos;

    void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        DefaultAnimation();

        if (collected)
        {
            CollectedAnimation();
        }

        if (currentMovedHeight > collectHeight)
        {
            gameObject.SetActive(false);
            return;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "SushiDude")
        {
            collected = true;

            CollectibleInventory inventory = col.GetComponent<CollectibleInventory>();
            if (inventory != null)
            {
                inventory.SushiCollected();
            }

            PointsFading pointFade = col.GetComponent<PointsFading>();
            if (pointFade != null)
            {
                pointFade.UpdateFadeStatus(true);
            }
        }
    }

        private void DefaultAnimation()
    {
        // Handle rotation
        transform.Rotate(0.0f, 1.0f, 0.0f, Space.Self);

        // Handle floating
        float newY = Mathf.Sin(Time.time * floatSpeed) * floatHeight + pos.y;
        transform.position = new Vector3(pos.x, newY, pos.z);
    }

    private void CollectedAnimation()
    {
        var yChange = (pos.y + collectSpeed) * Time.deltaTime;
        currentMovedHeight += yChange;
        pos += new Vector3(0f, yChange, 0f);

        if (transform.localScale.y > 0)
        {
            transform.localScale -= new Vector3(scaleSpeed, scaleSpeed, scaleSpeed);
        }
    }
}
