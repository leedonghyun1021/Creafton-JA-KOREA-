using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("�̵� ����")]
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

    void HandleMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");  // ��/�� or A/D
        rb.linearVelocity = new Vector2(x * moveSpeed, rb.linearVelocity.y);

        // �ٶ󺸴� ���⿡ ���� ��������Ʈ ������ (����)
        if (x != 0)
            transform.localScale = new Vector3(Mathf.Sign(x), 1, 1);
    }
}
