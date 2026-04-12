using UnityEngine;
using Random = UnityEngine.Random;

public class Hole : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab; // Prefab for the coin to spawn

    [SerializeField] private int coinsToSpawn = 1; // Number of coins to spawn when the hole is clicked

    [SerializeField] private float horizDistFromEdge = 2;
    
    [SerializeField] private float vertDistFromEdge = 2;

    [SerializeField] private float amountFilled = 0;

    public int CoinsToSpawn { get { return coinsToSpawn; } set { coinsToSpawn = value; } }

    private int width;
    private int height;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        width = (int)gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
        height = (int)gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
    }

    private void OnEnable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FillHole(float fillWeight)
    {
        amountFilled += fillWeight;
        FillProgressUI.SetProgress(amountFilled);
    }

    private void OnMouseDown()
    {
        for(int i = 0; i < coinsToSpawn; i++)
        {
            SpawnCoin();
            amountFilled += coinsToSpawn;
            FillProgressUI.SetProgress(amountFilled);
        }
    }

    public void SpawnCoin()
    {
        float theta = Random.Range(0, 2 * Mathf.PI);
        Vector3 pos = new Vector3(Mathf.Cos(theta) * (width + horizDistFromEdge) / 2, Mathf.Sin(theta) * (height + vertDistFromEdge) / 2, 100);
        pos += transform.position;
        GameObject newCoin = Instantiate(coinPrefab, pos, Quaternion.identity);

        Debug.Log(newCoin.transform.position);
    }

}
