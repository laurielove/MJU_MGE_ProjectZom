using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FadeIn : MonoBehaviour
{
    [SerializeField]
    [Range(0.5f, 3.0f)]
    private float fadeTime;

    private CanvasGroup cg;

    [SerializeField]
    private TextMeshProUGUI dayText;

    private void Awake()  
    {
        if (!TryGetComponent<CanvasGroup>(out cg))
            Debug.Log("FadeIn.cs - Awake() - cg 참조 오류");
    }

    public void DayFadeStart()
    {
        dayText.text = $"D-{29 - GameManager.Inst.dayCount}";
        gameObject.SetActive(true);
        StartCoroutine(Fade(1f, 0f));
    }

    public void MoveFadeStart()
    {
        dayText.text = $"";
        gameObject.SetActive(true);
        StartCoroutine(Fade(1f, 0f));
    }

    private IEnumerator Fade(float start, float end)
    {
        float currentTime = 0f;
        float percent = 0f;

        cg.alpha = 1;

        while (percent < 1f)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;
            cg.alpha = Mathf.Lerp(start, end, percent);

            yield return null;
        }

        gameObject.SetActive(false);
    }
}
