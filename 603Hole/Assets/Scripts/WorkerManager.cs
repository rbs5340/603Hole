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
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        for(int i  = 0; i < amount; i++)
        {
            if (ResourceManager.Instance.Coins < workerCosts[resourceType])
                return;

            ResourceManager.Instance.Coins -= workerCosts[resourceType];
            //ResourceType resourceType = ResourceType.None;
            switch (resourceType)
            {
                case (int)ResourceType.Coins:
                    Instantiate(nabbitPrefab, gameObject.transform);
                    break;
                case (int)ResourceType.None:
                    Instantiate(chuckPrefab);
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
        for(int i = 0; i < amount; i++)
        {
            switch (resourceType)
            {
                case (int)ResourceType.Coins:
                    GameObject sacrifice = FindAnyObjectByType<CoinCollector>().gameObject;
                    if (sacrifice)
                    {
                        Destroy(sacrifice);
                    }
                    break;
                case (int)ResourceType.None:
                    GameObject sacrificeAgain = FindAnyObjectByType<ResourceThrower>().gameObject;
                    if (sacrificeAgain)
                    {
                        Destroy(sacrificeAgain);
                    }
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
}
