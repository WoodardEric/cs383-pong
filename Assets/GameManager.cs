using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int PlayerOneScore = 0;
    public static int PlayerTwoScore = 0;

    public GameObject helpScreen;
    public GUISkin layout;

    GameObject ball;

    void Start()
    {
        Time.timeScale = 0;
        ball = GameObject.FindGameObjectWithTag("Ball");
        helpScreen = GameObject.FindGameObjectWithTag("HelpScreen");
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

        if (PlayerOneScore == 10)
        {
            GUI.Label(new Rect(Screen.width / 2 - 150, 200, 2000, 1000), "PLAYER ONE WINS");
            ball.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
        }
        else if (PlayerTwoScore == 10)
        {
            GUI.Label(new Rect(Screen.width / 2 - 150, 200, 2000, 1000), "PLAYER TWO WINS");
            ball.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!helpScreen.activeSelf)
            {
                Time.timeScale = 0;
                helpScreen.SetActive(true);
            }
            else
            {

                Time.timeScale = 1;
                helpScreen.SetActive(false);
            }
        }
    }
}