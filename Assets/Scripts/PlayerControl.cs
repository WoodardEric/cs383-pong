using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public KeyCode upKey = KeyCode.W;
    public KeyCode downKey = KeyCode.S;
    public float speed = 10.0f;
    public float boundY;
    public bool AI = false;

    public  GameObject ball;
    private Rigidbody2D rb2d;
    private Vector3 originalScale;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        float temp = transform.localScale.y / 2;

        // bind the baddle to within the camera
        boundY = (float)(Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0)).y - temp); 
        originalScale = transform.localScale;
    }

    void ResetPaddle()
    {
        var pos = transform.position;
        pos.y = 0;
        transform.position = pos;
        transform.localScale = originalScale;
    }

    public void ToggleAI(bool newValue)
    {
        AI = newValue;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.CompareTag("Ball"))
        {
            var scaleChangeY = 0.08f;
            var size = transform.localScale;
            size.y -= scaleChangeY;
            transform.localScale = size;
            boundY += scaleChangeY;
        }
    }


    void Update()
    {
        if (!AI)
        {
            var velocity = rb2d.velocity;
            if (Input.GetKey(upKey))
            {
                velocity.y = speed;
            }
            else if (Input.GetKey(downKey))
            {
                velocity.y = -speed;
            }
            else
            {
                velocity.y = 0;
            }
            rb2d.velocity = velocity;

        }
        else
        {
            var velocity = rb2d.velocity;
            var paddlePosition = transform.position;
            var ballPosition = ball.transform.position;
            if (ballPosition.y > paddlePosition.y + 0.3)
            {
                velocity.y = speed;
            }
            else if (ballPosition.y < paddlePosition.y - 0.3)
            {
                velocity.y = -speed;
            }
            else
            {
                velocity.y = 0;
            }
            rb2d.velocity = velocity;
        }

        var position = transform.position;
        if (position.y > boundY)
        {
            position.y = boundY;
        }
        else if (position.y < -boundY)
        {
            position.y = -boundY;
        }
        transform.position = position;
    }
}
