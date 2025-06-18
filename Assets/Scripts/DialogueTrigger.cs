// DialogueTrigger.cs
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Dialogue Settings")]
    public Dialogue dialogue;
    public UnityEvent onDialogueComplete;

    private bool playerInRange = false;
    private bool hasStarted = false;  // ��ȭ ���� ����

    void Update()
    {
        // �÷��̾� ���� �ν� �� ��ȣ�ۿ� Ű
        if (playerInRange && Input.GetKeyDown(KeyCode.F) && !hasStarted)
        {
            hasStarted = true;
            DialogueManager.Instance.StartDialogue(dialogue, OnComplete);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($" TriggerEnter2D with: {other.name}");
        if (other.CompareTag("Player"))
        {
            Debug.Log("    Player IN");
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log($" TriggerExit2D with: {other.name}");
        if (other.CompareTag("Player"))
        {
            Debug.Log("    Player OUT");
            playerInRange = false;
        }
    }

    void OnComplete()
    {
        // ��ȭ�� ���� �� �ݹ� ȣ��
        onDialogueComplete?.Invoke();
    }
}