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

        switch (carriedResource)
        {
            case ResourceType.Garlic:
                ThrowIcon(2);
                idle = false;

                break;
            case ResourceType.Bikes:
                ThrowIcon(2);
                idle = false;

                break;
            case ResourceType.Candy:
                ThrowIcon(2);
                idle = false;

                break;
            case ResourceType.Waluigium:
                ThrowIcon(100);
                idle = false;
                break;
            default:
                idle = true;
                break;
        }
        if (carriedResource == ResourceType.None)
            targetPos = GetHighestResourceArea();

        targetPos.z = 0;

        void ThrowIcon(float fillAmount)
        {
            Hole hole = Hole.Instance;
            var holeBounds = hole.GetComponent<SpriteRenderer>().bounds;
            float endPointRandomness = 0.25f;
            float endWorldPosX = Mathf.Lerp(holeBounds.min.x, holeBounds.max.x, Random.Range(0.5f - endPointRandomness, 0.5f + endPointRandomness));
            float endWorldPosY = Mathf.Lerp(holeBounds.min.y, holeBounds.max.y, Random.Range(0.5f - endPointRandomness, 0.5f + endPointRandomness));
            var endWorldPos = new Vector2(endWorldPosX, endWorldPosY);

            IconProjectileHolder.Instance.Create(
                Camera.main.WorldToScreenPoint(transform.position),
                Camera.main.WorldToScreenPoint(endWorldPos),
                IconProjectileHolder.GetResourceSprite(carriedResource),
                () => Hole.Instance.FillHole(fillAmount),
                250
                );
        }
    }

    private Vector3 GetHighestResourceArea()
    {
        float greatestAmount = 0;
        ResourceType highest = ResourceType.None;

        for (int i = 1; i < 5; i++)
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
        if (collision.gameObject.GetComponent<ResourceArea>() != null && carriedResource == ResourceType.None && collision.gameObject.GetComponent<ResourceArea>().ResourceType == targetResource)
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
