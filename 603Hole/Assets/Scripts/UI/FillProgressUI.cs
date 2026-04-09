using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class FillProgressUI : MonoSingleton<FillProgressUI>
{
    [SerializeField] private Image fillBar;
    [SerializeField] private TextMeshProUGUI fillText;
    [SerializeField] private float maxHFU;
    [SerializeField] private GameObject achievement;

    //filledWeight is in HFU
    public static void SetProgress(float filledWeight)
    {
        if (Instance != null)
        {
            Instance.fillBar.fillAmount = filledWeight / Instance.maxHFU;
            Instance.fillText.text = $"Filled {filledWeight} / {Instance.maxHFU}";

            if(Instance.fillBar.fillAmount >= 1)
            {
                Instance.achievement.SetActive(true);
            }
        }
    }
}
