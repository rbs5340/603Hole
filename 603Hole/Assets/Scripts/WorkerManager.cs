using UnityEngine;

public class WorkerManager : MonoBehaviour
{
    //All the resource areas in the order: Garlic, Candy, Bikes, Waluigium
    [SerializeField] ResourceArea[] resourceArea;

    [SerializeField] GameObject nabbitPrefab;

    [SerializeField] GameObject chuckPrefab;

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

    public void AddWorker(int resourceType)
    {
        //ResourceType resourceType = ResourceType.None;
        switch (resourceType)
        {
            case (int)ResourceType.Coins:
                Instantiate(nabbitPrefab, gameObject.transform);
                break;
            case (int)ResourceType.None:
                Instantiate(chuckPrefab, gameObject.transform);
                break;
            case (int)ResourceType.Garlic:
                resourceArea[0].AddWorker();
                break;
            case (int)ResourceType.Bikes:
                resourceArea[1].AddWorker();
                break;
            case (int)ResourceType.Candy:
                resourceArea[2].AddWorker();
                break;
            case (int)ResourceType.Waluigium:
                resourceArea[3].AddWorker();
                break;
        }
    }

    public void RemoveWorker(int resourceType)
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
                resourceArea[1].RemoveWorker();
                break;
            case (int)ResourceType.Candy:
                resourceArea[2].RemoveWorker();
                break;
            case (int)ResourceType.Waluigium:
                resourceArea[3].RemoveWorker();
                break;
        }
    }
}
