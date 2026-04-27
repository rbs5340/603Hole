using TMPro;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum ResourceType
{
    Coins,
    Garlic,
    Candy,
    Bikes,
    Waluigium,
    None
}
public class ResourceManager : MonoBehaviour
{
    //All the player's resources
    [SerializeField] private float coins = 0;
    [SerializeField] private float garlic = 0;
    [SerializeField] private float candy = 0;
    [SerializeField] private float bikes = 0;
    [SerializeField] private float waluigium = 0;

    [SerializeField] private TMP_Text coinDisplay;
    [SerializeField] private TMP_Text garlicDisplay;
    [SerializeField] private TMP_Text candyDisplay;
    [SerializeField] private TMP_Text bikesDisplay;
    [SerializeField] private TMP_Text waluigiumDisplay;



    public bool CoinBoost { get; private set; } = false;
    public float CoinIncomeMultiplier { get; set; } = 1;
    public float BoostTime { get; private set; }

    //Getters and setters for the resources
    public float Coins { 
        get { return coins; } 
        
        set { 
            coins = value;
            coinDisplay.text = NumberFormatter.FormatLargeInteger(coins,1);
        } 
    }
    public float Garlic { get { return garlic; } 
        set { 
            garlic = value;
            garlicDisplay.text = ((int)garlic).ToString();
        } 
    }
    public float Candy { get { return candy; } 
        set 
        { 
            candy = value;
            candyDisplay.text = ((int)candy).ToString();
        }
    }
    public float Bikes { get { return bikes; } 
        set 
        { 
            bikes = value;
            bikesDisplay.text = ((int)bikes).ToString();

        }
    }
    public float Waluigium { get { return waluigium; } 
        set 
        { 
            waluigium = value;
            waluigiumDisplay.text = ((int)waluigium).ToString();

        }
    }

    private static ResourceManager _instance;

    public static ResourceManager Instance { get { return _instance; } }


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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        //iconDisplay = FindAnyObjectByType<IconDisplayUI>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BoostCoinIncome(float time)
    {
        //Assume that we won't trigger another boost before one ends.
        StartCoroutine(BoostCountdownCoroutine(time));
    }
    IEnumerator BoostCountdownCoroutine(float time)
    {
        CoinBoost = true;
        BoostTime = time;
        while (BoostTime > 0)
        {
            yield return null;
            BoostTime -= Time.deltaTime;
        }
        CoinBoost = false;
    }

    public float CurrentResourceSum => WeightedSum(Instance.Coins, 0, 0, 0, 0);
    public float FulfillProgress(Chuckable chuckable)
    {
        float currentsum = WeightedSum(
            Mathf.Min(chuckable.GoldReq, Coins),
            Mathf.Min(chuckable.WoodReq, Garlic),
            Mathf.Min(chuckable.WaterReq, Candy),
            Mathf.Min(chuckable.StoneReq, Bikes),
            Mathf.Min(chuckable.GoopReq, Waluigium)
            );
        float totalReq = WeightedSum(chuckable);
        return currentsum / totalReq;
    }
    public static float WeightedSum(Chuckable chuckable)
    {
        return WeightedSum(chuckable.GoldReq, chuckable.WoodReq, chuckable.WaterReq, chuckable.StoneReq, chuckable.GoopReq);
    }
    public static float WeightedSum(float coins, float wood, float water, float stone, float goop)
    {
        return coins + wood + water + stone + goop * 10;
    }

    public void AddResource(ResourceType resourceType, float amount)
    {
        switch (resourceType)
        {
            case ResourceType.Coins:
                Coins += amount;
                break;
            case ResourceType.Garlic:
                Garlic += amount;
                break;
            case ResourceType.Bikes:
                Bikes += amount;
                break;
            case ResourceType.Candy:
                Candy += amount;
                break;
            case ResourceType.Waluigium:
                Waluigium += amount;
                break;
        }
    }

    public float GetResourceAmount(ResourceType resourceType)
    {
        switch (resourceType)
        {
            case ResourceType.Coins:
                return Coins;
            case ResourceType.Garlic:
                return Garlic;
            case ResourceType.Bikes:
                return Bikes;
            case ResourceType.Candy:
                return Candy;
            case ResourceType.Waluigium:
                return Waluigium;
            default:
                return 0;
        }
    }
}
