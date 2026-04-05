using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private Hole hole;
    private static UpgradeManager _instance;

    [SerializeField] private int holeUpgradeCost = 1;
    public int HoleUpgradeCost { get { return holeUpgradeCost; } }

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
    }


    public void UpgradeHole()
    {
        if (ResourceManager.Instance.Coins >= holeUpgradeCost)
        {
            hole.CoinsToSpawn += 1;
            ResourceManager.Instance.Coins -= holeUpgradeCost;
        }
    }
}
