using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChuckableEntry : MonoBehaviour
{
    Chuckable _chuckable;
    public Chuckable Chuckable { get => _chuckable; set { SetChuckable(value); } }

    [SerializeField] public Button buyButton;
    [SerializeField] public Button getByAdButton;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI HFUText;
    [SerializeField] private TextMeshProUGUI GoldReqText;
    [SerializeField] private TextMeshProUGUI WoodReqText;
    [SerializeField] private TextMeshProUGUI WaterReqText;
    [SerializeField] private TextMeshProUGUI StoneReqText;
    [SerializeField] private TextMeshProUGUI GoopReqText;

    void SetChuckable(Chuckable chuckable)
    {
        _chuckable = chuckable;
        icon.sprite = chuckable.Icon;
        nameText.text = chuckable.Name;
        HFUText.text = chuckable.HFU.ToString() + " HFU";
        GoldReqText.text = chuckable.GoldReq.ToString();
        WoodReqText.text = chuckable.WoodReq.ToString();
        WaterReqText.text = chuckable.WaterReq.ToString();
        StoneReqText.text = chuckable.StoneReq.ToString();
        GoopReqText.text = chuckable.GoopReq.ToString();
        WoodReqText.transform.parent.gameObject.SetActive(chuckable.WoodReq > 0);
        WaterReqText.transform.parent.gameObject.SetActive(chuckable.WaterReq > 0);
        StoneReqText.transform.parent.gameObject.SetActive(chuckable.StoneReq > 0);
        GoopReqText.transform.parent.gameObject.SetActive(chuckable.GoopReq > 0);
    }

    public void BindCallback(Action<ChuckableEntry> callback, Action<ChuckableEntry> callbackAd)
    {
        buyButton.onClick.AddListener(new UnityAction(() => callback.Invoke(this)));
        getByAdButton.onClick.AddListener(new UnityAction(() => callbackAd.Invoke(this)));
    }
    public void SetButtonState(bool buyable, bool ADable)
    {
        buyButton.interactable = buyable;
        buyButton.GetComponentInChildren<TextMeshProUGUI>().text = buyable ? "BUY" : "TOO POOR";
        getByAdButton.gameObject.SetActive(ADable);
    }

    public Vector2 GetIconPosition()
    {
        return icon.transform.position;
    }

}
