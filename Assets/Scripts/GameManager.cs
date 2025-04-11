using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player player;
    public Text scoreText;
    public GameObject playButton;
    public GameObject[] uiStates; // [0] = Start Game, [1] = Game Over
    public EffectManager effectManager;

    private int score;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        Pause();

        scoreText.gameObject.SetActive(false);
        uiStates[0].SetActive(true); // Show "Tap to Play"
        uiStates[1].SetActive(false); // Hide "Game Over"
    }

    public void Play()
    {
        score = 0;
        scoreText.text = score.ToString();

        scoreText.gameObject.SetActive(true);
        playButton.SetActive(false);
        uiStates[0].SetActive(false);
        uiStates[1].SetActive(false);

        Time.timeScale = 1f;
        player.enabled = true;

        Pipes[] pipes = Object.FindObjectsByType<Pipes>(FindObjectsSortMode.None);
        for (int i = 0; i < pipes.Length; i++)
        {
            Destroy(pipes[i].gameObject);
        }

        // Reset the world to normal before starting flip cycle
        effectManager.ResetToNormal();
        effectManager.StartCycle();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        player.enabled = false;
    }

    public void GameOver()
    {
        uiStates[1].SetActive(true); // Show "Game Over"
        playButton.SetActive(true);  // Show Play Again button

        Pause();

        // Stop timer but don't reset visuals — leave it upside down if it was
        effectManager.StopCycle();
    }

    public void IncreasesScore()
    {
        score++;
        scoreText.text = score.ToString();
    }
}
