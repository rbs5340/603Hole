using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Loads all scriptable objects, and integrates with other systems.
/// </summary>
public class ChuckableManager : MonoSingleton<ChuckableManager>
{
    Chuckable[] chuckables;
    public List<Chuckable> Chuckables => chuckables.ToList();

    //This should redirect to actual resource manager
    int currentGold => ResourceManager.Instance.Coins;

    protected override void Awake()
    {
        base.Awake();
        chuckables = Resources.LoadAll<Chuckable>("Chuckables");
    }

    /// <summary>
    /// Used by UI to determine button availability
    /// </summary>
    /// <param name="chuckable"></param>
    /// <returns></returns>
    public bool ChuckableIsBuyable(Chuckable chuckable)
    {
        //TODO: validate requirements.
        return chuckable.ValidateBuyable(currentGold, 0, 0, 0, 0);
    }

    public void OnBuyChuckable(Chuckable chuckable)
    {
        //TODO: consume resource, apply effect etc.
        Debug.Log($"Bought {chuckable.Name}!");
        ResourceManager.Instance.Coins -= chuckable.GoldReq;
    }
}
