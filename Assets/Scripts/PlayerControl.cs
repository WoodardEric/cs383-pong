using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public KeyCode upKey = KeyCode.W;
    public KeyCode downKey = KeyCode.S;
    public float speed = 8.0f;
    public float yBoundry;
    public bool AI = false;

    public  BallControl ball;
    private Rigidbody2D rb2d;
    private Vector3 originalScale;
    private float originalBoundY;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        float temp = transform.localScale.y / 2;

        // bind the baddle to within the camera
        yBoundry = originalBoundY = (float)(Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0)).y - temp); 
        originalScale = transform.localScale;
    }

    void ResetPaddle()
    {
        var pos = transform.position;
        pos.y = 0;
        transform.position = pos;
        transform.localScale = originalScale;
        yBoundry = originalBoundY;
    }

    public void ToggleAI(bool newValue)
    {
        AI = newValue;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ball"))
        {
            var scaleChangeY = 0.08f;
            var size = transform.localScale;
            if (size.y > 0.2f)
            { 
                size.y -= scaleChangeY;
            }
            transform.localScale = size;
            yBoundry += scaleChangeY;
        }
    }

    private void UpdateAI()
    {
        var velocity = rb2d.velocity;
        var paddlePosition = transform.position;
        var ballPosition = ball.transform.position;
        var ballVelocity = ball.Velocity;
        float reactionTime = .05f;
        if (ball.Velocity.x < 0)
        {
            velocity.y = 0;
        }
        else if (ballPosition.y > paddlePosition.y + 0.3 && ballPosition.x > 0 - reactionTime)
        {
            velocity.y = speed;
        }
        else if (ballPosition.y < paddlePosition.y - 0.3 && ballPosition.x > 0 - reactionTime)
        {
            velocity.y = -speed;
        }
        else
        {
            velocity.y = 0;
        }
        rb2d.velocity = velocity;
    }

    void Update()
    {
        if (!AI)
        {
            var velocity = rb2d.velocity;
            if (Input.GetKey(upKey))
            {
                velocity.y = Input.GetAxis("Vertical") + speed;
            }
            else if (Input.GetKey(downKey))
            {
                velocity.y = Input.GetAxis("Vertical") - speed;
            }
            else
            {
                velocity.y = 0;
            }
            rb2d.velocity = velocity;
        }
        else
        {
            UpdateAI();
        }

        var position = transform.position;
        if (position.y > yBoundry)
        {
            position.y = yBoundry;
        }
        else if (position.y < -yBoundry)
        {
            position.y = -yBoundry;
        }
        transform.position = position;
    }
}
