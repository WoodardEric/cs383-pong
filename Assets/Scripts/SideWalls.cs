using UnityEngine;

public class SideWalls : MonoBehaviour
{
    public BallControl ball;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Ball")
        {
            string wallName = gameObject.name;
            GameManager.Score(wallName);
            ball.RestartGame();
        }
    }
}
