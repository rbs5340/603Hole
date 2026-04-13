using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    private float workRate = 1f;

    private float timeSinceLastCollect = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timeSinceLastCollect >= workRate)
        {
            CollectCoin();
            timeSinceLastCollect = 0f;
        }
        else
        {
            timeSinceLastCollect += Time.deltaTime;
        }
    }

    public void CollectCoin()
    {
        if (Hole.Instance.Coins.Count > 0)
        {
            Coin coin = Hole.Instance.Coins[0];
            transform.position = coin.transform.position;
            Hole.Instance.Coins.RemoveAt(0);
            ResourceManager.Instance.Coins += coin.CoinValue;
            Destroy(coin.gameObject);
        }
    }
}
