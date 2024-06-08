using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement; // �� ��ȯ�� ���� �߰�

public class God : MonoBehaviour
{
    [SerializeField]
    private GameObject askPanel;
    [SerializeField]
    private GameObject ClearText;
    [SerializeField]
    private List<GameObject> prefabs; // ��Ȱ��ȭ�� 6���� ������ �Ҵ�
    [SerializeField]
    private GameObject player; // �÷��̾� ��ü ����
    [SerializeField]
    //private ClearCount clearCountUI; // ClearCount ��ũ��Ʈ ����

    public static GameManager instance = null; // �̱��� ����

    private GameObject activePrefab = null; // ���� Ȱ��ȭ�� ������

    private int clearCount = 0; // Ŭ���� Ƚ��

    private void Awake()
    {
        if (instance == null)
        {
            //instance = this;
        }
    }

    private void Start()
    {
        ActivateRandomPrefab();
    }

    // �������� ������ Ȱ��ȭ
    private void ActivateRandomPrefab()
    {
        if (activePrefab != null) // �ߺ� Ȱ��ȭ ����
        {
            activePrefab.SetActive(false);
        }

        // ��Ȱ��ȭ�� 6���� ������ �� �ƹ��͵� Ȱ��ȭ���� �ʴ� ��츦 ������ ���� ����
        int index = Random.Range(0, prefabs.Count + 1);
        if (index < prefabs.Count)
        {
            activePrefab = prefabs[index];
            activePrefab.SetActive(true);
        }
        else
        {
            activePrefab = null; // �ƹ� �����յ� Ȱ��ȭ���� ����
        }
    }

    // ASK UI Ȱ��ȭ �޼���
    public void ASKUIActive()
    {
        if (askPanel != null)
        {
            askPanel.SetActive(true);
        }
    }

    // ASK UI ��Ȱ��ȭ �޼���
    public void ASKUIDelete()
    {
        if (askPanel != null)
        {
            askPanel.SetActive(false);
        }
    }

    public void ClearTextActive()
    {
        ClearText.SetActive(true);
    }

    public void ClearTextDelete()
    {
        ClearText.SetActive(false);
    }

    // �÷��̾� ��ġ �ʱ�ȭ �޼���
    public void ResetPlayerPosition()
    {
        if (player != null)
        {
            player.transform.position = new Vector3(-41f, -3.8f, 0f);
        }
    }

    // Ŭ���� ī��Ʈ �ʱ�ȭ �޼���
    public void ResetClearCount()
    {
        clearCount = 0;
        //clearCountUI.UpdateClearCountText(clearCount); // UI ������Ʈ
    }

    // Tracer �������� ��Ȱ��ȭ�ϰ� ���� ������ Ȱ��ȭ
    public void DeactivateTracerAndActivateRandomPrefab()
    {
        if (activePrefab != null && activePrefab.CompareTag("Tracer"))
        {
            activePrefab.SetActive(false);
            activePrefab = null;
        }
        ActivateRandomPrefab();
    }

    // ���� Ŭ���� ����
    private void GameClear()
    {
        //ClearTextActive();
        SceneManager.LoadScene("ClearScene"); // ClearScene���� ��ȯ
    }

    // Yes ��ư Ŭ�� �� ȣ��
    public void OnYesButton()
    {
        if (activePrefab != null)
        {
            clearCount++;
            //clearCountUI.UpdateClearCountText(clearCount); // UI ������Ʈ
            if (clearCount >= 4)
            {
                GameClear();
                Debug.Log("���� ����");
            }
            else
            {
                ActivateRandomPrefab();
            }
        }
        else
        {
            // No�� ������ ���
            clearCount = 0;
            //clearCountUI.UpdateClearCountText(clearCount); // UI ������Ʈ
            ActivateRandomPrefab();
        }

        ASKUIDelete();
        ResetPlayerPosition(); // �÷��̾� ��ġ �ʱ�ȭ
    }

    // No ��ư Ŭ�� �� ȣ��
    public void OnNoButton()
    {
        if (activePrefab == null) // �ƹ� �����յ� Ȱ��ȭ���� ���� ����(No�� ����)
        {
            clearCount++;
            //clearCountUI.UpdateClearCountText(clearCount); // UI ������Ʈ
            if (clearCount >= 4)
            {
                GameClear();
                Debug.Log("���� ����");
            }
            else // �ƴ϶��
            {
                ActivateRandomPrefab(); // ������ �޼��� ��Ȱ��ȭ.
            }
        }
        else
        {
            clearCount = 0; // �׸��� Ŭ���� ī��Ʈ 0���� �ʱ�ȭ.
            //clearCountUI.UpdateClearCountText(clearCount); // UI ������Ʈ
            ActivateRandomPrefab();
        }

        ASKUIDelete();
        ResetPlayerPosition(); // �÷��̾� ��ġ �ʱ�ȭ
    }
}

