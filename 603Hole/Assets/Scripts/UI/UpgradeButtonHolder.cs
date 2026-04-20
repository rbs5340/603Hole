using System.Collections.Generic;
using UnityEngine;

public class UpgradeButtonHolder : MonoBehaviour
{
    public UpgradeEntry entryPrefab;
    [SerializeField] BoostButton boostButton;
    public List<UpgradeType> upgradeTypes;

    private void Start()
    {
        entryPrefab.gameObject.SetActive(false);

        foreach (UpgradeType upgradeType in upgradeTypes)
        {
            var newEntry = Instantiate(entryPrefab, transform);
            newEntry.gameObject.SetActive(true);
            newEntry.upgradeType = upgradeType;
        }

        boostButton.transform.SetAsLastSibling();
    }
}
