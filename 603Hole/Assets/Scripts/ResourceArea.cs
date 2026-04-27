using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class ResourceArea : MonoBehaviour
{
    [SerializeField] private int maxResources;

    private int MaxResources => Mathf.FloorToInt(maxResources * MaxResourcesMultiplier);
    public float MaxResourcesMultiplier { get; set; } = 1;

    [SerializeField] private float regenRate;
    public float RegenRateMultiplier { get; set; } = 1;

    [SerializeField] private ResourceType resourceType;

    public ResourceType ResourceType { get { return resourceType; } }

    [SerializeField] private float workRate;

    [SerializeField] private TMP_Text displayResourceAmount;

    [SerializeField] private Image displayFillerImage;

    [SerializeField] private float workerWages;

    public float WorkerWagesMultiplier { get; set; } = 1;

    [SerializeField] private TMP_Text workerNumDisplay;

    private int numWorkers;

    private float numResources;

    private void Start()
    {
        numResources = maxResources;
        workerNumDisplay.text = numWorkers.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        float wagesCost = numWorkers * workerWages * WorkerWagesMultiplier * Time.deltaTime;

        if (ResourceManager.Instance.Coins < wagesCost)
        {
            return;
        }
        else
        {
            ResourceManager.Instance.Coins -= wagesCost;
        }



        float resourcesCollected = workRate * numWorkers * Time.deltaTime;

        if (numWorkers > 0)
        {

            if (numResources < resourcesCollected)
            {
                resourcesCollected = numResources;
            }
            ResourceManager.Instance.AddResource(resourceType, resourcesCollected);

            numResources -= resourcesCollected;

            if (numResources < 0)
            {
                numResources = 0;
            }
        }
        else if (numResources < MaxResources)
        {
            numResources += regenRate * RegenRateMultiplier * Time.deltaTime;
        }

        displayResourceAmount.text = ((int)numResources + " / " + (int)MaxResources).ToString();
        displayFillerImage.fillAmount = 1 - numResources / MaxResources;//Because it's actually a hider
    }

    public void AddWorker()
    {
        numWorkers++;
        gameObject.GetComponent<IconDisplayUI>().SetIconCount(numWorkers);
        workerNumDisplay.text = numWorkers.ToString();
    }

    public void RemoveWorker()
    {
        numWorkers--;
        if (numWorkers < 0)
        {
            numWorkers = 0;
        }

        gameObject.GetComponent<IconDisplayUI>().SetIconCount(numWorkers);

        workerNumDisplay.text = numWorkers.ToString();

    }

    public float LaborCosts()
    {
        return numWorkers * workerWages;
    }
}
