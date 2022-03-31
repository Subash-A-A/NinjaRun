using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Text scoreText;

    private void Update()
    {
        scoreText.text = player.position.z.ToString("0");
    }

}
