using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Loads all scriptable objects, and integrates with other systems.
/// Don't deal with UI
/// </summary>
public class ChuckableManager : MonoSingleton<ChuckableManager>
{
    Chuckable[] chuckables;
    Hole theHole;
    public List<Chuckable> Chuckables => chuckables.ToList();

    //This should redirect to actual resource manager
    int currentGold => ResourceManager.Instance.Coins;
    int currentWood => ResourceManager.Instance.Wood;
    int currentWater => ResourceManager.Instance.Water;
    int currentStone => ResourceManager.Instance.Stone;
    int currentGoop => ResourceManager.Instance.Goop;

    protected override void Awake()
    {
        base.Awake();
        chuckables = Resources.LoadAll<Chuckable>("Chuckables");
        theHole = FindAnyObjectByType<Hole>();
    }


    /// <summary>
    /// Used by UI to determine button availability
    /// </summary>
    /// <param name="chuckable"></param>
    /// <returns></returns>
    public bool ChuckableIsBuyable(Chuckable chuckable, out float progress)
    {
        //TODO: validate requirements.
        progress = ResourceManager.Instance.FulfillProgress(chuckable);
        return chuckable.ValidateBuyable(currentGold, currentWood, currentWater, currentStone, currentGoop);
    }

    /// <summary>
    /// Called when a chuckable is bought.
    /// No check for whether resource is enough.
    /// Will consume all resources of a kind if that resource is insufficient.
    /// </summary>
    /// <param name="chuckable"></param>
    public void OnBuyChuckable(Chuckable chuckable)
    {
        Debug.Log($"Bought {chuckable.Name}!");
        ResourceManager.Instance.Coins -= Mathf.Min(chuckable.GoldReq, ResourceManager.Instance.Coins);
    }

    /// <summary>
    /// Called when the chuckable is actually chucked.
    /// </summary>
    /// <param name="chuckable"></param>
    public void OnChuck(Chuckable chuckable)
    {
        theHole.FillHole(chuckable.HFU);
    }
}
