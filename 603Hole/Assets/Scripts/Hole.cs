using UnityEngine;
using Random = UnityEngine.Random;

public class Hole : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab; // Prefab for the coin to spawn

    [SerializeField] private int coinsToSpawn = 1; // Number of coins to spawn when the hole is clicked
    public int CoinsToSpawn { get { return coinsToSpawn; } set { coinsToSpawn = value; } }

    private int width;
    private int height;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        width = (int)gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
        height = (int)gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        for(int i = 0; i < coinsToSpawn; i++)
        {
            SpawnCoin();
        }
    }

    public void SpawnCoin()
    {
        GameObject newCoin = Instantiate(coinPrefab, transform.position, Quaternion.identity);
        float theta = Random.Range(0, 2 * Mathf.PI);
        newCoin.transform.position += new Vector3(Mathf.Cos(theta) * (width + 1f) / 2, Mathf.Sin(theta) * (height + 1f) / 2, 0);
    }

}
