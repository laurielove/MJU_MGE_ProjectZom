using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement; // 씬 전환을 위해 추가

public class God : MonoBehaviour
{
    [SerializeField]
    private GameObject askPanel;
    [SerializeField]
    private GameObject ClearText;
    [SerializeField]
    private List<GameObject> prefabs; // 비활성화된 6개의 프리팹 할당
    [SerializeField]
    private GameObject player; // 플레이어 객체 참조
    [SerializeField]
    //private ClearCount clearCountUI; // ClearCount 스크립트 참조

    public static GameManager instance = null; // 싱글톤 패턴

    private GameObject activePrefab = null; // 현재 활성화된 프리팹

    private int clearCount = 0; // 클리어 횟수

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

    // 랜덤으로 프리팹 활성화
    private void ActivateRandomPrefab()
    {
        if (activePrefab != null) // 중복 활성화 방지
        {
            activePrefab.SetActive(false);
        }

        // 비활성화된 6개의 프리팹 중 아무것도 활성화하지 않는 경우를 포함해 랜덤 선택
        int index = Random.Range(0, prefabs.Count + 1);
        if (index < prefabs.Count)
        {
            activePrefab = prefabs[index];
            activePrefab.SetActive(true);
        }
        else
        {
            activePrefab = null; // 아무 프리팹도 활성화하지 않음
        }
    }

    // ASK UI 활성화 메서드
    public void ASKUIActive()
    {
        if (askPanel != null)
        {
            askPanel.SetActive(true);
        }
    }

    // ASK UI 비활성화 메서드
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

    // 플레이어 위치 초기화 메서드
    public void ResetPlayerPosition()
    {
        if (player != null)
        {
            player.transform.position = new Vector3(-41f, -3.8f, 0f);
        }
    }

    // 클리어 카운트 초기화 메서드
    public void ResetClearCount()
    {
        clearCount = 0;
        //clearCountUI.UpdateClearCountText(clearCount); // UI 업데이트
    }

    // Tracer 프리팹을 비활성화하고 랜덤 프리팹 활성화
    public void DeactivateTracerAndActivateRandomPrefab()
    {
        if (activePrefab != null && activePrefab.CompareTag("Tracer"))
        {
            activePrefab.SetActive(false);
            activePrefab = null;
        }
        ActivateRandomPrefab();
    }

    // 게임 클리어 로직
    private void GameClear()
    {
        //ClearTextActive();
        SceneManager.LoadScene("ClearScene"); // ClearScene으로 전환
    }

    // Yes 버튼 클릭 시 호출
    public void OnYesButton()
    {
        if (activePrefab != null)
        {
            clearCount++;
            //clearCountUI.UpdateClearCountText(clearCount); // UI 업데이트
            if (clearCount >= 4)
            {
                GameClear();
                Debug.Log("게임 종료");
            }
            else
            {
                ActivateRandomPrefab();
            }
        }
        else
        {
            // No가 정답인 경우
            clearCount = 0;
            //clearCountUI.UpdateClearCountText(clearCount); // UI 업데이트
            ActivateRandomPrefab();
        }

        ASKUIDelete();
        ResetPlayerPosition(); // 플레이어 위치 초기화
    }

    // No 버튼 클릭 시 호출
    public void OnNoButton()
    {
        if (activePrefab == null) // 아무 프리팹도 활성화되지 않은 상태(No가 정답)
        {
            clearCount++;
            //clearCountUI.UpdateClearCountText(clearCount); // UI 업데이트
            if (clearCount >= 4)
            {
                GameClear();
                Debug.Log("게임 종료");
            }
            else // 아니라면
            {
                ActivateRandomPrefab(); // 프리팹 메서드 재활성화.
            }
        }
        else
        {
            clearCount = 0; // 그리고 클리어 카운트 0으로 초기화.
            //clearCountUI.UpdateClearCountText(clearCount); // UI 업데이트
            ActivateRandomPrefab();
        }

        ASKUIDelete();
        ResetPlayerPosition(); // 플레이어 위치 초기화
    }
}

