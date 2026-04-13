using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum ResourceType
{
    Coins,
    Garlic,
    Bikes,
    Candy,
    Waluigium,
    None
}
public class ResourceManager : MonoBehaviour
{
    //All the player's resources
    [SerializeField] private int coins = 0;
    [SerializeField] private int garlic = 0;
    [SerializeField] private int candy = 0;
    [SerializeField] private int bikes = 0;
    [SerializeField] private int waluigium = 0;

    [SerializeField] private TMP_Text coinDisplay;
    [SerializeField] private TMP_Text garlicDisplay;
    [SerializeField] private TMP_Text candyDisplay;
    [SerializeField] private TMP_Text bikesDisplay;
    [SerializeField] private TMP_Text waluigiumDisplay;


    //Getters and setters for the resources
    public int Coins { 
        get { return coins; } 
        
        set { 
            coins = value;
            coinDisplay.text = coins.ToString();
        } 
    }
    public int Garlic { get { return garlic; } 
        set { 
            garlic = value;
            garlicDisplay.text = garlic.ToString();
        } 
    }
    public int Candy { get { return candy; } 
        set 
        { 
            candy = value;
            candyDisplay.text = candy.ToString();
        }
    }
    public int Bikes { get { return bikes; } 
        set 
        { 
            bikes = value;
            bikesDisplay.text = bikes.ToString();

        }
    }
    public int Waluigium { get { return waluigium; } 
        set 
        { 
            waluigium = value;
            waluigiumDisplay.text = waluigium.ToString();

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

    public void AddResource(ResourceType resourceType, int amount)
    {
        Debug.Log($"Adding {amount} of {resourceType}");
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
}
