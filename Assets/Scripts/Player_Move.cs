using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("플레이어 속도")]
    public float moveSpeed = 5f;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //물리 기반 이동은 여기서 처리!
    void FixedUpdate()
    {
        HandleMovement(); 
    }

    void HandleMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(x * moveSpeed, rb.linearVelocity.y);

        if (x != 0)
            transform.localScale = new Vector3(Mathf.Sign(x), 1, 1);
    }
}
