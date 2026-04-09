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
    public int Coins { 
        get { return coins; } 
        
        set { 
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
}
