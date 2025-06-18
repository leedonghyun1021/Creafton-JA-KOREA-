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

    void Update()
    {
        HandleMovement();
    }

    // 캐릭터 움직임
    void HandleMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");  
        rb.linearVelocity = new Vector2(x * moveSpeed, rb.linearVelocity.y);

        
        if (x != 0)
            transform.localScale = new Vector3(Mathf.Sign(x), 1, 1);
    }
}
