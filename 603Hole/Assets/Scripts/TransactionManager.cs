using UnityEngine;

public class TransactionManager : MonoSingleton<TransactionManager>
{
    [SerializeField] TransactionPanel transactionPanel;
    public float boostIncomeTime = 300;

    protected override void Awake()
    {
        base.Awake();
    }

    public void OpenPanel()
    {
        transactionPanel.gameObject.SetActive(true);
    }
    public void ClosePanel()
    {
        transactionPanel.gameObject.SetActive(false);
    }

    public void OnSuccussTransaction()
    {
        ClosePanel();
        ResourceManager.Instance.BoostCoinIncome(boostIncomeTime);
    }
}
