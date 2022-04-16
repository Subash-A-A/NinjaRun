using UnityEngine;

public class HealthBooster : MonoBehaviour
{
    [SerializeField] float healAmount = 20f;
    [SerializeField] ParticleSystem ps;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            Health.Heal(healAmount);
            ParticleSystem particle = Instantiate(ps, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(particle, 0.7f);
        }
    }
}
