using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BoostButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    Button button;
    string originalText;

    private void Awake()
    {
        button = GetComponent<Button>();
        originalText = text.text;
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
