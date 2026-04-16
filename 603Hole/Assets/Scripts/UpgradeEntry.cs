using TMPro;
using UnityEngine;

public class UpgradeEntry : MonoBehaviour
{
    public UpgradeType upgradeType;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    private void Update()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        costText.text = UpgradeManager.Instance.GetDisplayedCost(upgradeType);
    }
}
