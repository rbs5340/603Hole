using TMPro;
using UnityEngine;



public class ResourceArea : MonoBehaviour
{
    [SerializeField] private int maxResources;

    [SerializeField] private float regenRate;

    [SerializeField] private ResourceType resourceType;

    [SerializeField] private float workRate;

    [SerializeField] private TMP_Text displayResourceAmount;

    private int numWorkers;

    private float numResources;

    private void Start()
    {
        numResources = maxResources;
    }

    // Update is called once per frame
    void Update()
    {

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
        else if (numResources < maxResources)
        {
            numResources += regenRate * Time.deltaTime;
        }

        displayResourceAmount.text = ((int)numResources + " / " + (int)maxResources).ToString();
    }

    public void AddWorker()
    {
        numWorkers++;
        gameObject.GetComponent<IconDisplayUI>().SetIconCount(numWorkers);
    }

    public void RemoveWorker()
    {
        numWorkers--;
        if (numWorkers < 0)
        {
            numWorkers = 0;
        }

        gameObject.GetComponent<IconDisplayUI>().SetIconCount(numWorkers);
    }
}
