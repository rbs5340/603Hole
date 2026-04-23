using TMPro;
using UnityEngine;

public class WorkerManager : MonoBehaviour
{
    //All the resource areas in the order: Garlic, Candy, Bikes, Waluigium
    [SerializeField] ResourceArea[] resourceArea;

    [SerializeField] GameObject nabbitPrefab;

    [SerializeField] GameObject chuckPrefab;

    [SerializeField] int[] workerCosts;

    private static WorkerManager _instance;

    public static WorkerManager Instance { get { return _instance; } }

    private float totalCostOfLabor;

    [SerializeField] private TMP_Text laborCostDisplay;
    [SerializeField] private TMP_Text nabbitNumDisplay;
    private int nabbitNum = 0;
    [SerializeField] private TMP_Text chuckNumDisplay;
    private int chuckNum = 0;

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
    void Start()
    {
        nabbitNumDisplay.text = nabbitNum.ToString();
        chuckNumDisplay.text = chuckNum.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        laborCostDisplay.text = ((int)GetTotalCostOfLabor()).ToString();
    }

    public void AddOneWorker(int resourceType)
    {
        AddWorker(resourceType, 1);
    }

    public void AddFiftyWorker(int resourceType)
    {
        AddWorker(resourceType, 50);
    }

    public void AddWorker(int resourceType, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            if (ResourceManager.Instance.Coins < workerCosts[resourceType])
                return;

            ResourceManager.Instance.Coins -= workerCosts[resourceType];
            //ResourceType resourceType = ResourceType.None;
            switch (resourceType)
            {
                case (int)ResourceType.Coins:
                    Instantiate(nabbitPrefab, gameObject.transform);
                    nabbitNum++;
                    nabbitNumDisplay.text = nabbitNum.ToString();
                    break;
                case (int)ResourceType.None:
                    Instantiate(chuckPrefab);
                    chuckNum++;
                    chuckNumDisplay.text = chuckNum.ToString();
                    break;
                case (int)ResourceType.Garlic:
                    resourceArea[0].AddWorker();
                    break;
                case (int)ResourceType.Bikes:
                    resourceArea[2].AddWorker();
                    break;
                case (int)ResourceType.Candy:
                    resourceArea[1].AddWorker();
                    break;
                case (int)ResourceType.Waluigium:
                    resourceArea[3].AddWorker();
                    break;
            }
        }

    }

    public void RemoveOneWorker(int resourceType)
    {
        RemoveWorker(resourceType, 1);
    }

    public void RemoveFiftyWorker(int resourceType)
    {
        RemoveWorker(resourceType, 50);
    }

    public void RemoveWorker(int resourceType, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            switch (resourceType)
            {
                case (int)ResourceType.Coins:
                    GameObject sacrifice = FindAnyObjectByType<CoinCollector>().gameObject;
                    if (sacrifice)
                    {
                        Destroy(sacrifice);
                    }
                    nabbitNum--;
                    nabbitNumDisplay.text = nabbitNum.ToString();
                    break;
                case (int)ResourceType.None:
                    GameObject sacrificeAgain = FindAnyObjectByType<ResourceThrower>().gameObject;
                    if (sacrificeAgain)
                    {
                        Destroy(sacrificeAgain);
                    }
                    chuckNum--;
                    chuckNumDisplay.text = chuckNum.ToString();
                    break;
                case (int)ResourceType.Garlic:
                    resourceArea[0].RemoveWorker();
                    break;
                case (int)ResourceType.Bikes:
                    resourceArea[2].RemoveWorker();
                    break;
                case (int)ResourceType.Candy:
                    resourceArea[1].RemoveWorker();
                    break;
                case (int)ResourceType.Waluigium:
                    resourceArea[3].RemoveWorker();
                    break;
            }
        }

    }

    private float GetTotalCostOfLabor()
    {
        float total = 0;
        foreach (ResourceArea rArea in resourceArea)
        {
            total += rArea.LaborCosts();
        }

        return total;
    }
}
