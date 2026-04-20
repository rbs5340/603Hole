using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoostButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Button button;
    string originalText;

    private void Awake()
    {
        originalText = text.text;
    }

    private void Start()
    {
        button.onClick.AddListener(TransactionManager.Instance.OpenPanel);
    }

    private void Update()
    {
        if (ResourceManager.Instance.CoinBoost)
        {
            button.interactable = false;
            text.text = $"{ResourceManager.Instance.BoostTime:F1}s";
        }
        else
        {
            button.interactable = true;
            text.text = originalText;
        }
    }
}
