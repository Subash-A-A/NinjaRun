using UnityEngine;

public class ObstacleCollision : MonoBehaviour
{
    [SerializeField] Animator anim;

    private Rigidbody rb;
    private PlayerController pc;
    private bool endGame = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        pc = GetComponent<PlayerController>();
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
            Invoke("DisableRagDoll", 0.1f);
        }
    }

    void DisableRagDoll()
    {
        anim.enabled = false;
    }
}
