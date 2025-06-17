// DialogueSystem 설정 및 코드 예제 (수정됨)

// 1. UI 셋업
//  - Canvas → Panel (이름: DialoguePanel)
//    • Panel 크기: 화면 하단 절반
//    • Image 컴포넌트 배경 색(반투명 검정)
//  - DialoguePanel 아래에
//    • TextMeshPro - Text (이름: DialogueText)
//       • RectTransform: Stretch
//       • Margin: (10,10,10,10)
//    • Button (이름: NextButton)
//       • OnClick: DialogueManager.DisplayNextSentence()
//       • Label: "다음"
//  - NextButton은 선택 사항, 키 입력(Enter, Space)으로도 전환 가능
//  - DialoguePanel은 기본 비활성화(false)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

[System.Serializable]
public class Dialogue
{
    public string[] sentences;
}

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("UI Elements")]
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;

    [Header("Settings")]
    public float typeSpeed = 0.05f;

    private Queue<string> sentencesQueue;
    private UnityAction onDialogueEnd;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        sentencesQueue = new Queue<string>();
    }

    // 외부에서 대화 시작
    public void StartDialogue(Dialogue dialogue, UnityAction onEnd = null)
    {
        onDialogueEnd = onEnd;
        dialoguePanel.SetActive(true);
        sentencesQueue.Clear();
        foreach (string sentence in dialogue.sentences)
            sentencesQueue.Enqueue(sentence);
        DisplayNextSentence();
    }

    // 다음 문장 표시
    public void DisplayNextSentence()
    {
        if (sentencesQueue.Count == 0)
        {
            EndDialogue();
            return;
        }
        StopAllCoroutines();
        string sentence = sentencesQueue.Dequeue();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = string.Empty;
        foreach (char c in sentence)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }
    }

    // 대화 종료 시 패널 숨기고 콜백 실행
    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        onDialogueEnd?.Invoke();
    }

    void Update()
    {
        // Enter 또는 Space 키로 다음 문장 전환
        if (dialoguePanel.activeSelf && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)))
        {
            DisplayNextSentence();
        }
    }
}

// NPC 트리거 예제
public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public UnityEvent onDialogueComplete;

    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            DialogueManager.Instance.StartDialogue(dialogue, OnComplete);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) playerInRange = true;
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) playerInRange = false;
    }

    void OnComplete()
    {
        // 혼잣말 자동 실행
        string soliloquy = "이제 다음 임무를 하러 가야겠다...";
        Dialogue solo = new Dialogue { sentences = new string[] { soliloquy } };
        DialogueManager.Instance.StartDialogue(solo, () => {
            QuestManager.Instance.AddQuest("새로운 임무");
        });
        onDialogueComplete?.Invoke();
    }
}

// QuestManager 예시
public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;
    public List<string> quests = new List<string>();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddQuest(string quest)
    {
        quests.Add(quest);
        MissionUI.Instance.UpdateMissionList(quests);
    }
}

// MissionUI 예시 (Tab 키로 열기)
public class MissionUI : MonoBehaviour
{
    public static MissionUI Instance;
    public GameObject missionPanel;
    public TMP_Text missionText;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            missionPanel.SetActive(!missionPanel.activeSelf);
    }

    public void UpdateMissionList(List<string> quests)
    {
        missionText.text = string.Empty;
        foreach (string q in quests)
            missionText.text += "• " + q + "\n";
    }
}
