using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Text healthText;
	public Image healthBar;
    public float damagePoints = 30f;

    private bool playerDeath = false;

	float currentHealth, maxHealth = 90f;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        healthText.text = currentHealth / 30 + "";

        HealthBarFiller();

        if (currentHealth == 0)
        {
            playerDeath = true;
            Invoke("RevivePlayer", 2f);
        }
    }

    void HealthBarFiller()
    {
        healthBar.fillAmount = currentHealth / maxHealth;
    }

    public void DamageAmount(float damagePoints)
    {
        if(currentHealth > 0)
        {
            currentHealth -= damagePoints;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rigidbody = hit.collider.attachedRigidbody;

        if(rigidbody == null || rigidbody != rigidbody.CompareTag("TriggerCube"))
        {
            return;
        }

        // Check if collision object has rigid body
        if (rigidbody == rigidbody.CompareTag("TriggerCube"))
        {
            if (currentHealth > 0)
            {
                currentHealth -= damagePoints;
            }
        }
    }

    public void RevivePlayer()
    {
        currentHealth = maxHealth;
    }
}