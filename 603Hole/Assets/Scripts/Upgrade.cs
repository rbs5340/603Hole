using System.Diagnostics;
using System.Xml.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "Scriptable Objects/Upgrade")]
public class Upgrade : ScriptableObject
{
    public UpgradeType UpgradeType;
    public string Name;
    public string EffectDescription;
    public float BasePrice;
    public float PriceIncreasePerUpgrade;
    public float EffectPerUpgrade;
    public float Cost(int level) => BasePrice * Mathf.Pow(PriceIncreasePerUpgrade, level);
    public float Effect(int level) => Mathf.Pow(EffectPerUpgrade, level);
}
