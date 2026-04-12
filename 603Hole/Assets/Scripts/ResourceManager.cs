using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    //All the player's resources
    [SerializeField] private int coins = 0;
    [SerializeField] private int wood = 0;
    [SerializeField] private int water = 0;
    [SerializeField] private int stone = 0;
    [SerializeField] private int goop = 0;

    [SerializeField] private IconDisplayUI iconDisplay;


    //Getters and setters for the resources
    public int Coins
    {
        get { return coins; }

        set
        {
            coins = value;
            iconDisplay.SetDisplayNumer(coins);
            iconDisplay.SetIconCount(coins);
        }
    }
    public int Wood { get { return coins; } set { coins = value; } }
    public int Water { get { return coins; } set { coins = value; } }
    public int Stone { get { return coins; } set { coins = value; } }
    public int Goop { get { return coins; } set { coins = value; } }

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
        iconDisplay = FindAnyObjectByType<IconDisplayUI>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public float CurrentResourceSum => WeightedSum(Instance.Coins, 0, 0, 0, 0);
    public float FulfillProgress(Chuckable chuckable)
    {
        float currentsum = WeightedSum(
            Mathf.Min(chuckable.GoldReq, Coins),
            Mathf.Min(chuckable.WoodReq, Wood),
            Mathf.Min(chuckable.WaterReq, Water),
            Mathf.Min(chuckable.StoneReq, Stone),
            Mathf.Min(chuckable.GoopReq, Goop)
            );
        float totalReq = WeightedSum(chuckable);
        return currentsum / totalReq;
    }
    public static float WeightedSum(Chuckable chuckable)
    {
        return WeightedSum(chuckable.GoldReq, chuckable.WoodReq, chuckable.WaterReq, chuckable.StoneReq, chuckable.GoopReq);
    }
    public static float WeightedSum(int coins, int wood, int water, int stone, int goop)
    {
        return coins + wood + water + stone + goop * 10;
    }
}
