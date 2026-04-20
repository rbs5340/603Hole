using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int coinValue = 1;

    public int CoinValue
    {
        get { return coinValue; }
        set { coinValue = value; }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        Hole.Instance.Coins.Remove(this);
        Debug.Log("Collected Coin!");
        ResourceManager.Instance.Coins += (ResourceManager.Instance.CoinBoost ? 2 : 1) * ResourceManager.Instance.CoinIncomeMultiplier * coinValue;
        Destroy(gameObject);
    }

}
