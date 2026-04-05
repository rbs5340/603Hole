using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FillProgressUI : MonoSingleton<FillProgressUI>
{
    [SerializeField] private Image fillBar;
    [SerializeField] private TextMeshProUGUI fillText;
    [SerializeField] private float maxHFU;

    //filledWeight is in HFU
    public static void SetProgress(float filledWeight)
    {
        if (Instance != null)
        {
            Instance.fillBar.fillAmount = filledWeight / Instance.maxHFU;
            Instance.fillText.text = $"Filled {filledWeight} / {Instance.maxHFU}";
        }
    }
}
