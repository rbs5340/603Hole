using UnityEngine;

public class ResourceThrower : MonoBehaviour
{
    private Vector3 targetPos;

    private ResourceType carriedResource = ResourceType.None;

    private ResourceType targetResource;

    [SerializeField] private float speed = 1;

    private bool idle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        idle = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPos, speed * Time.deltaTime);
        if (idle)
        {
            AtTarget();
        }
    }

    void AtTarget()
    {
        switch(carriedResource)
        {
            case ResourceType.Garlic:
                Hole.Instance.FillHole(2);
                idle = false;

                break;
            case ResourceType.Bikes:
                Hole.Instance.FillHole(2);
                idle = false;

                break;
            case ResourceType.Candy:
                Hole.Instance.FillHole(2);
                idle = false;

                break;
            case ResourceType.Waluigium:
                Hole.Instance.FillHole(100);
                idle = false;
                break;
            default:
                idle = true;
                break;
        }
        if(carriedResource == ResourceType.None)
            targetPos = GetHighestResourceArea();

        targetPos.z = 0;
        

    }

    private Vector3 GetHighestResourceArea()
    {
        float greatestAmount = 0;
        ResourceType highest = ResourceType.None;
        
        for ( int i = 1; i < 5; i++)
        {
            ResourceType resourceType = (ResourceType)(i);
            float amount = ResourceManager.Instance.GetResourceAmount(resourceType);
            if (amount > greatestAmount && amount > 1)
            {
                greatestAmount = amount;
                highest = resourceType;
            }
        }

        targetResource = highest;
        ResourceArea[] resourceAreas = GameObject.FindObjectsByType<ResourceArea>(FindObjectsSortMode.None);
        Vector3 target = Vector3.zero;
        foreach (ResourceArea resourceArea in resourceAreas)
        {
            if (resourceArea.ResourceType == highest)
            {
                target = resourceArea.gameObject.transform.position;
            }
        }

        return target;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided :)");
        if(collision.gameObject.GetComponent<ResourceArea>() != null && carriedResource == ResourceType.None && collision.gameObject.GetComponent<ResourceArea>().ResourceType == targetResource)
        {
            carriedResource = collision.gameObject.GetComponent<ResourceArea>().ResourceType;
            targetPos = Hole.Instance.gameObject.transform.position;
            ResourceManager.Instance.AddResource(carriedResource, -1);
            idle = false;
        }
        else if (collision.gameObject.GetComponent<Hole>() != null)
        {
            AtTarget();
            carriedResource = ResourceType.None;
            idle = true;
        }
    }
}
