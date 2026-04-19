using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private Hole hole;
    private static UpgradeManager _instance;

    [SerializeField] private int holeUpgradeCost = 10;
    public int HoleUpgradeCost { get { return Mathf.CeilToInt(holeUpgradeCost * Mathf.Pow(1.2f, hole.CoinsToSpawn - 1)); } }

    private Dictionary<UpgradeType, Upgrade> upgrades = new();
    public IReadOnlyDictionary<UpgradeType, Upgrade> Upgrades => upgrades;
    private Dictionary<UpgradeType, int> upgradeLevels = new();
    public IReadOnlyDictionary<UpgradeType, int> UpgradeLevels => upgradeLevels;

    public static UpgradeManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        var upgradeArray = Resources.LoadAll<Upgrade>("Upgrades");
        foreach (var upgrade in upgradeArray)
        {
            upgrades.Add(upgrade.UpgradeType, upgrade);
            upgradeLevels.Add(upgrade.UpgradeType, 0);
        }
    }

    private void OnEnable()
    {
        hole = FindAnyObjectByType<Hole>();
    }


    public void UpgradeHole()
    {
        if (ResourceManager.Instance.Coins >= holeUpgradeCost)
        {
            hole.CoinsToSpawn += 1;
            ResourceManager.Instance.Coins -= holeUpgradeCost;
        }
    }

    public void Upgrade(UpgradeType upgradeType)
    {
        if (upgradeType == UpgradeType.DoubleMoney) return;

        if (upgrades.TryGetValue(upgradeType, out var upgrade))
        {
            float cost = GetCost(upgradeType);
            int level = upgradeLevels[upgradeType];
            if (ResourceManager.Instance.Coins >= cost)
            {
                switch (upgradeType)
                {
                    case UpgradeType.MegaMushroom:
                        hole.FillPerMushroom = upgrade.Effect(level);
                        break;
                    case UpgradeType.ImANumberOne:
                        ResourceManager.Instance.CoinIncomeMultiplier = upgrade.Effect(level);
                        break;
                        //TODO: fill other effects
                }
                ResourceManager.Instance.Coins -= cost;
                upgradeLevels[upgradeType]++;
            }
        }
    }

    public float GetCost(UpgradeType upgradeType)
    {
        if (upgradeType == UpgradeType.DoubleMoney) return 0;
        if (upgrades.TryGetValue(upgradeType, out var upgrade))
        {
            return upgrade.Cost(upgradeLevels[upgradeType]);
        }
        return 0;
    }

    public bool IsUpgradable(UpgradeType upgradeType)
    {
        if (upgradeType == UpgradeType.DoubleMoney) return true;
        if (upgrades.TryGetValue(upgradeType, out var upgrade))
        {
            return ResourceManager.Instance.Coins >= GetCost(upgradeType);
        }
        return false;
    }



    public string GetDisplayedCost(UpgradeType upgradeType)
    {
        if (upgradeType == UpgradeType.DoubleMoney) return "$5 USD";
        return NumberFormatter.FormatLargeNumber(GetCost(upgradeType));
    }
}
