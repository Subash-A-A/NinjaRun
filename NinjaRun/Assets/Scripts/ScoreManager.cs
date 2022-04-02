using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Text scoreText;
    [SerializeField] Text highScoreText;
    [SerializeField] Animator anim;
    [SerializeField] ParticleSystem explosion;

    int highScore = 0;
    private bool playAnim = false;


    private void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "/" + highScore.ToString();
    }

    private void Update()
    {
        updateScore();
        updateHighScore();
    }

    void updateScore()
    {
        scoreText.text = player.position.z.ToString("0");
    }
    void updateHighScore()
    {
        int score = int.Parse(scoreText.text);

        if (highScore == score && !playAnim && highScore > 0f)
        {
            anim.SetTrigger("PowerUp");
            explosion.Play();
            playAnim = true;
        }

        if (highScore < score)
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScoreText.text = "/" + score.ToString();
        }
    }

}
