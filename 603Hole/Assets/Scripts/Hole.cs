using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using Random = UnityEngine.Random;

public class Hole : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab; // Prefab for the coin to spawn
    [SerializeField] private Sprite mushroomSprite; 

    [SerializeField] private int coinsToSpawn = 1; // Number of coins to spawn when the hole is clicked

    [SerializeField] private float horizDistFromEdge = 2;

    [SerializeField] private float vertDistFromEdge = 2;

    [SerializeField] private float amountFilled = 0;

    private List<Coin> coins;

    public List<Coin> Coins { get { return coins; } set { coins = value; } }

    public int CoinsToSpawn { get { return coinsToSpawn; } set { coinsToSpawn = value; } }
    public float FillPerMushroom { get; set; } = 1;

    private int width;
    private int height;

    private static Hole _instance;

    public static Hole Instance { get { return _instance; } }

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
        coins = new List<Coin>();
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

    private void OnMouseDown()
    {
        //FillHole(coinsToSpawn * FillPerMushroom);
        bool verticalSide = Random.Range(0, 2)==1;
        Vector2 startPos = verticalSide ? 
            (new Vector2(Random.Range(0, 2) * Screen.width, Random.Range(0, Screen.height + 1))):
            (new Vector2(Random.Range(0, Screen.width + 1),Random.Range(0, 2) * Screen.height ));
        float theta = Random.Range(0, 2 * Mathf.PI);
        var endPos = new Vector3(Mathf.Cos(theta) * (width) / 4, Mathf.Sin(theta) * (height) / 4);
        endPos += transform.position;
        IconProjectileHolder.Instance.Create(
            startPos,
            Camera.main.WorldToScreenPoint(endPos),
            mushroomSprite,
            () => FillHole(coinsToSpawn * FillPerMushroom),
            1000
            );
    }

    public void SpawnCoin()
    {
        float theta = Random.Range(0, 2 * Mathf.PI);
        Vector3 pos = new Vector3(Mathf.Cos(theta) * (width + horizDistFromEdge) / 2, Mathf.Sin(theta) * (height + vertDistFromEdge) / 2, 100);
        pos += transform.position;
        GameObject newCoin = Instantiate(coinPrefab, pos, Quaternion.identity);
        coins.Add(newCoin.GetComponent<Coin>());
    }

    public void FillHole(float amount)
    {
        amountFilled += amount;
        FillProgressUI.SetProgress(amountFilled);
        for (int i = 0; i < amount; i++)
        {
            SpawnCoin();
        }
    }

}
