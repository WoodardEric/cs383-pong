using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int PlayerOneScore = 0;
    public static int PlayerTwoScore = 0;
    private const int MAX_SCORE = 3;
    public Canvas helpScreen;
    public GUISkin layout;

    public GameObject ball;
    public AudioSource audioSource;

    private bool restarting = false;

    void Start()
    {
        Time.timeScale = 0;
    }

    public static void Score(string wallID)
    {
        if (wallID == "RightWall")
        {
            PlayerOneScore++;
        }
        else
        {
            PlayerTwoScore++;
        }

        var players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var player in players)
        {
            player.SendMessage("ResetPaddle", SendMessageOptions.RequireReceiver);
        }
    }

    void OnGUI()
    {
        GUI.skin = layout;
        GUI.Label(new Rect(Screen.width / 2 - 150 - 12, 20, 100, 100), "" + PlayerOneScore);
        GUI.Label(new Rect(Screen.width / 2 + 150 + 12, 20, 100, 100), "" + PlayerTwoScore);

        var messageRect = new Rect(Screen.width / 2 - 250, Screen.height - 100, 2000, 1000);
        bool win = false;
        if (PlayerOneScore >= MAX_SCORE)
        {
            GUI.Label(messageRect, "PLAYER ONE WINS");
            win = true;
        }
        else if (PlayerTwoScore >= MAX_SCORE)
        {
            GUI.Label(messageRect, "PLAYER TWO WINS");
            win = true;
        }

        if (win && !restarting)
        {
            Invoke("ResetMatch", 5);
            restarting = true;
        }
    }

    private void ResetMatch()
    {
        PlayerOneScore = 0;
        PlayerTwoScore = 0;
        ball.SendMessage("RestartGame", null, SendMessageOptions.RequireReceiver);
        restarting = false;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        // pan the music to follow the ball
        audioSource.panStereo = Mathf.Clamp(ball.transform.position.x / 4, - 0.5f, 0.5f);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!helpScreen.gameObject.activeSelf)
            {
                Time.timeScale = 0;
                helpScreen.gameObject.SetActive(true);
                audioSource.bypassEffects = false;
            }
            else
            {

                Time.timeScale = 1;
                helpScreen.gameObject.SetActive(false);
                audioSource.bypassEffects = true;
            }
        }
    }
}