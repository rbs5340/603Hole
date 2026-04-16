using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private Hole hole;
    private static UpgradeManager _instance;

    [SerializeField] private int holeUpgradeCost = 10;
    public int HoleUpgradeCost { get { return Mathf.CeilToInt(holeUpgradeCost * Mathf.Pow(1.2f, hole.CoinsToSpawn - 1)); } }

    private Dictionary<UpgradeType, Upgrade> upgrades;
    private Dictionary<UpgradeType, int> upgradeLevels;

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
            int cost = GetCost(upgradeType);
            int level = upgradeLevels[upgradeType];
            if (ResourceManager.Instance.Coins >= cost)
            {
                switch (upgradeType)
                {
                    case UpgradeType.MegaMushroom:
                        hole.CoinsToSpawn = upgrade.Effect(level);
                        break;
                        //TODO: fill other effects
                }
                ResourceManager.Instance.Coins -= cost;
            }
        }
    }

    public int GetCost(UpgradeType upgradeType)
    {
        if (upgradeType == UpgradeType.DoubleMoney) return 0;
        if (upgrades.TryGetValue(upgradeType, out var upgrade))
        {
            return upgrade.Cost(upgradeLevels[upgradeType]);
        }
        return 0;
    }


    public string GetDisplayedCost(UpgradeType upgradeType)
    {
        if (upgradeType == UpgradeType.DoubleMoney) return "$5 USD";
        return NumberFormatter.FormatLargeNumber(GetCost(upgradeType));

        //switch (upgradeType)
        //{
        //    case UpgradeType.MegaMushroom:
        //        return NumberFormatter.FormatLargeNumber(HoleUpgradeCost);
        //    case UpgradeType.DoubleMoney:
        //        return "$5 USD";
        //    default:
        //        return "";
        //}
    }
}
