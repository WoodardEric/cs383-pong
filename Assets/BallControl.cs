using UnityEngine;

public class BallControl : MonoBehaviour
{
    private Rigidbody2D rb2d;

    private void StartBall()
    {
        rb2d.AddForce(new Vector2(30, -15));
    }

    void ResetBall()
    {
        rb2d.velocity = Vector2.zero;
        transform.position = Vector2.zero;
    }

    void RestartGame()
    {
        ResetBall();
        Invoke("StartBall", 1);
    }


    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.CompareTag("Player"))
        {
            Vector2 velocity;
            velocity.x = rb2d.velocity.x;
            velocity.y = (rb2d.velocity.y / 2) + (coll.collider.attachedRigidbody.velocity.y / 3);
            rb2d.velocity = velocity;
        }

        GetComponent<SpriteRenderer>().color = Random.ColorHSV(0f, 1f, 1f, 1f, .8f, 1f);
    }


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Invoke("StartBall", 2);
    }
}
