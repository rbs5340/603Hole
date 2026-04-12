using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int coinValue = 1;
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
        Debug.Log("Collected Coin!");
        ResourceManager.Instance.Coins += ResourceManager.Instance.CoinBoost ? 2 : 1 * coinValue;
        Destroy(gameObject);
    }

}
