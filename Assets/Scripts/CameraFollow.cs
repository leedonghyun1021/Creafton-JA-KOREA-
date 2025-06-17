using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("������ ���")]
    public Transform target;        // �÷��̾� Transform

    [Header("������ �ӵ� (0~1)")]
    [Range(0.01f, 1f)]
    public float smoothSpeed = 0.125f;  // �������� �ε巴�� ����

    [Header("ī�޶� ������")]
    public Vector3 offset = new Vector3(0, 0, -10); // Z�� ī�޶� �Ÿ�

    void LateUpdate()
    {
        if (target == null) return;

        // 1) ��ǥ ��ġ ��� (�÷��̾� ��ġ + ������)
        Vector3 desiredPosition = target.position + offset;

        // 2) ���� ī�޶� ��ġ���� ��ǥ ��ġ�� Lerp (�ε巴�� �̵�)
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // 3) ���� ��ġ ����
        transform.position = smoothedPosition;
    }
}
