using UnityEngine;

public class ObstacleCollision : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] ParticleSystem explosion;

    private Rigidbody rb;
    private PlayerController pc;
    private EffectsManager ec;

    public static bool endGame = false;
    private bool startedPlaying = false;

    private void Awake()
    {
        endGame = false;
        rb = GetComponent<Rigidbody>();
        pc = GetComponent<PlayerController>();
        ec = FindObjectOfType<EffectsManager>();
    }

    private void Update()
    {
        GameOver();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Obstacle")
        {
            endGame = true;
        }
    }

    void GameOver()
    {
        if (endGame)
        {
            pc.enabled = false;
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, 25 * Time.deltaTime);

            if (!startedPlaying && ec.velRatio >= ec.arcStart - 0.3f)
            {
                explosion.Play();
                startedPlaying = true;
            }
            ec.HueShift();
            Invoke("DisableRagDoll", 0.1f);
        }
    }

    void DisableRagDoll()
    {
        anim.enabled = false;
    }
}
