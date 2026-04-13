using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int coinValue = 1;

    public int CoinValue
    {
        get { return coinValue; } set { coinValue = value; }
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
        ResourceManager.Instance.Coins += coinValue;
        Destroy(gameObject);
    }

}
