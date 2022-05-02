using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    string checkTag;

    public Text healthText;
	public Image healthBar;
    public float damagePoints = 30f;

    private IEnumerator coroutine;
    private bool canDamage = true;
    private float damageDelay = 3f;
    public bool isDead = false;
    
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
            isDead = true;
            Invoke("RevivePlayer", 2f);
        }
        else
        {
            isDead = false;
        }
    }

    void HealthBarFiller()
    {
        healthBar.fillAmount = currentHealth / maxHealth;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(canDamage == true)
        {
            Rigidbody rigidbody = hit.collider.attachedRigidbody;

            if (rigidbody == null || rigidbody != rigidbody.CompareTag(checkTag))
            {
                return;
            }

            // Check if collision object is an enemy
            if (rigidbody == rigidbody.CompareTag(checkTag))
            {
                if (currentHealth > 0)
                {
                    currentHealth -= damagePoints;
                }
            }

            canDamage = false;
            coroutine = WaitToDamage(damageDelay);
            StartCoroutine(coroutine);
        }
    }

    private IEnumerator WaitToDamage(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        canDamage = true;
    }

    public void RevivePlayer()
    {
        currentHealth = maxHealth;
    }

}