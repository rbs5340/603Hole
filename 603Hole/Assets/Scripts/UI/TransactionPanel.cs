using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TransactionPanel : MonoBehaviour
{
    [SerializeField] private TMP_InputField cardNumberInput;
    [SerializeField] private TextMeshProUGUI cardNumberFeedback;
    [SerializeField] private TMP_InputField holderNameInput;
    [SerializeField] private TextMeshProUGUI holderNameFeedback;
    [SerializeField] private TMP_InputField expMonthInput;
    [SerializeField] private TMP_InputField expYearInput;
    [SerializeField] private TextMeshProUGUI expDateFeedback;
    [SerializeField] private TMP_InputField cvvInput;
    [SerializeField] private TextMeshProUGUI cvvFeedback;

    List<Tuple<TMP_InputField, TextMeshProUGUI>> tupleList = new();
    Tuple<TMP_InputField, TextMeshProUGUI>[] Tuples => tupleList.ToArray();

    enum EFailType
    {
        Empty,
        WrongDigit,
        InvalidDate,
        Expired,
    }

    private void Awake()
    {
        tupleList.Add(new(cardNumberInput, cardNumberFeedback));
        tupleList.Add(new(holderNameInput, holderNameFeedback));
        tupleList.Add(new(expMonthInput, expDateFeedback));
        tupleList.Add(new(expYearInput, expDateFeedback));
        tupleList.Add(new(cvvInput, cvvFeedback));
    }

    public void ClosePanel()
    {
        TransactionManager.Instance.ClosePanel();
    }

    public void ValidateAndSubmit()
    {
        if (Validate())
        {
            TransactionManager.Instance.OnSuccussTransaction();
        }
    }

    private bool Validate()
    {
        bool isValid = true;

        foreach (var item in Tuples)
        {
            item.Item2.gameObject.SetActive(false);
        }

        if (cardNumberInput.text.Length != 16) SetupFeedback(EFailType.WrongDigit, cardNumberFeedback, 16);
        if (cvvInput.text.Length != 3) SetupFeedback(EFailType.WrongDigit, cvvFeedback, 3);

        if (int.TryParse(expMonthInput.text, out int month) && int.TryParse(expYearInput.text, out int year))
        {
            if (month > 12)
                SetupFeedback(EFailType.InvalidDate, expDateFeedback);
            if (year < 26 || (year == 26 && month < 3))
                SetupFeedback(EFailType.Expired, expDateFeedback);
        }

        foreach (var item in Tuples)
        {
            if (string.IsNullOrEmpty(item.Item1.text)) SetupFeedback(EFailType.Empty, item.Item2);
        }

        return isValid;

        void SetupFeedback(EFailType failType, TextMeshProUGUI stringBuilder, params int[] args)
        {
            isValid = false;
            stringBuilder.gameObject.SetActive(true);
            switch (failType)
            {
                case EFailType.Empty:
                    stringBuilder.text = "* Field cannot be empty.";
                    break;
                case EFailType.WrongDigit:
                    string digits = "";
                    if (args.Length == 1) digits = args[0].ToString();
                    stringBuilder.text = $"* Must be {digits} digits.";
                    break;
                case EFailType.InvalidDate:
                    stringBuilder.text = "* The date is invalid.";
                    break;
                case EFailType.Expired:
                    stringBuilder.text = "* The card has expired.";
                    break;
            }
        }
    }


}
