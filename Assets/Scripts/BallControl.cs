using UnityEngine;

public class BallControl : MonoBehaviour
{
    public Rigidbody2D RB2D { get; private set; }

    public Vector2 Velocity
    {
        get { return RB2D.velocity; }
        set { RB2D.velocity = value; }
    }

    private void StartBall()
    {
        RB2D.AddForce(new Vector2(40, -15));
    }

    public void ResetBall()
    {
        Velocity = Vector2.zero;
        transform.position = Vector2.zero;
    }

    public void RestartGame()
    {
        ResetBall();
        Invoke("StartBall", 1);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.CompareTag("Player"))
        {
            Vector2 velocity;
            velocity.x = Velocity.x;
            velocity.y = (RB2D.velocity.y / 2) + (coll.collider.attachedRigidbody.velocity.y / 3);
            Velocity = velocity;
        }

        GetComponent<SpriteRenderer>().color = Random.ColorHSV(0f, 1f, 1f, 1f, .8f, 1f);
    }

    void Start()
    {
        RB2D = GetComponent<Rigidbody2D>();
        RestartGame();
    }
}
