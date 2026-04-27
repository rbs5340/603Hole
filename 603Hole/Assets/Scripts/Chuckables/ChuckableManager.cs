using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Loads all scriptable objects, and integrates with other systems.
/// Don't deal with UI
/// </summary>
public class ChuckableManager : MonoSingleton<ChuckableManager>
{
    List<Chuckable> chuckables = new();
    Hole theHole;
    public List<Chuckable> Chuckables => chuckables.ToList();

    //This should redirect to actual resource manager
    float currentGold => ResourceManager.Instance.Coins;
    float currentGarlic => ResourceManager.Instance.Garlic;
    float currentCandy => ResourceManager.Instance.Candy;
    float currentBikes => ResourceManager.Instance.Bikes;
    float currentWaluigium => ResourceManager.Instance.Waluigium;

    protected override void Awake()
    {
        base.Awake();
        var _chuckables = Resources.LoadAll<Chuckable>("Chuckables");
        foreach (var chuckable in _chuckables)
        {
            var new_chuck = Instantiate(chuckable);
            new_chuck.GoldReq /= 2;
            new_chuck.WoodReq /= 2;
            new_chuck.StoneReq /= 2;
            new_chuck.WaterReq /= 2;
            new_chuck.GoopReq /= 2;
            chuckables.Add(new_chuck);
        }
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
        return chuckable.ValidateBuyable(currentGold, currentGarlic, currentCandy, currentBikes, currentWaluigium);
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
