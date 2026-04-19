using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeEntry : MonoBehaviour
{
    public UpgradeType upgradeType;
    [SerializeField] private TextMeshProUGUI upgradeNameText;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private TextMeshProUGUI upgradeButtonText;
    private void Start()
    {
        if (UpgradeManager.Instance.Upgrades.TryGetValue(upgradeType, out var upgrade))
        {
            if (upgradeNameText != null) upgradeNameText.text = upgrade.Name;
            if (costText != null) costText.text = UpgradeManager.Instance.GetDisplayedCost(upgradeType);
            if (descriptionText != null) descriptionText.text = upgrade.EffectDescription;
            upgradeButton.onClick.AddListener(OnUpgrade);
        }
    }

    private void OnUpgrade()
    {
        UpgradeManager.Instance.Upgrade(upgradeType);
    }

    private void Update()
    {
        UpdateContent();
    }

    private void UpdateContent()
    {
        costText.text = UpgradeManager.Instance.GetDisplayedCost(upgradeType);
        if (upgradeType == UpgradeType.DoubleMoney)
        {
            return; //Use BoostButton instead;
        }

        if (UpgradeManager.Instance.IsUpgradable(upgradeType))
        {
            upgradeButton.interactable = true;
            upgradeButtonText.text = "UPGRADE";
        }
        else
        {
            upgradeButton.interactable = false;
            upgradeButtonText.text = "TOO POOR";
        }
    }
}
