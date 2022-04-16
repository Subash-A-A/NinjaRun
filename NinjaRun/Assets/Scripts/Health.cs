using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    [SerializeField] Rigidbody player;
    [SerializeField] float maxHealth = 100f;
    [SerializeField] Image healthBar;
    public float[] healthDropRates;
    [SerializeField] float healthLerp = 10f;

    private static float currentHealth;
    private float dropMultiplier;

    private void Start()
    {
        currentHealth = maxHealth;
        dropMultiplier = healthDropRates[0];
    }

    private void Update()
    {
        pickDropRate(player.velocity.z);
        TakeDamage();
        healthFiller();
    }

    void healthFiller()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        float fillAmt = currentHealth / maxHealth;
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, fillAmt, healthLerp * Time.deltaTime);
    }

    void TakeDamage()
    {
        if (ObstacleCollision.endGame == true)
        {
            currentHealth = 0f;
        }
        else
        {
            currentHealth -= Time.deltaTime * dropMultiplier;
            if (currentHealth <= 0)
            {
                ObstacleCollision.endGame = true;
            }
        }
    }
    public static void Heal(float val)
    {
        currentHealth += val;
    }

    void pickDropRate(float velocity)
    {
        if (velocity > 20 && velocity <= 40)
        {
            dropMultiplier = healthDropRates[1];
        }
        else if (velocity > 40)
        {
            dropMultiplier = healthDropRates[2];
        }
    }
}
