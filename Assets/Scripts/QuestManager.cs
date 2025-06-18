using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }
    private List<string> quests = new List<string>();

    void Awake()
    {
        // �̱��� ����
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        // (����) �� ��ȯ �� �ı����� �ʰ� �ϰ� ������:
        // DontDestroyOnLoad(gameObject);
    }

    // �ܺο��� ����Ʈ�� �߰��� �� �θ��� �Լ�
    public void AddQuest(string quest)
    {
        if (quests.Contains(quest))
            return; // �ߺ� ����

        quests.Add(quest);
        // UI ����
        MissionUI.Instance.UpdateMissionList(quests);
    }

    // (����) ���߿� ����Ʈ �Ϸ�, ���� �� �޼��� �߰� ����
}
