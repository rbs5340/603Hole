using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ChuckableListUI : MonoBehaviour
{
    public ChuckableEntry entryPrefab;
    public RectTransform buttonHolder;

    Queue<Chuckable> chuckableQueue = new();

    private void Start()
    {
        //clear up prefab
        entryPrefab.gameObject.SetActive(false);

        //Form available chuckables into a queue, and show the first 3
        var chuckableList = ChuckableManager.Instance.Chuckables;
        chuckableList.Sort((x, y) => x.HFU - y.HFU);
        foreach (var chuckable in chuckableList)
        {
            chuckableQueue.Enqueue(chuckable);
        }

        for (int i = 0; i < 3; i++)
        {
            ShowNextChuckable();
        }
    }

    /// <summary>
    /// Build the next chuackable entry
    /// </summary>
    private void ShowNextChuckable()
    {
        if (chuckableQueue.Count == 0) return;
        var chuckable = chuckableQueue.Dequeue();
        var button = Instantiate(entryPrefab, buttonHolder);
        button.gameObject.SetActive(true);
        button.transform.SetAsLastSibling();
        button.Chuckable = chuckable;
        button.BindCallback(_ => OnButtonClicked(_));
    }

    /// <summary>
    /// Called when the BUY button clicked
    /// </summary>
    private void OnButtonClicked(ChuckableEntry chuckableButton)
    {
        var chuckable = chuckableButton.Chuckable;
        if (ChuckableManager.Instance.ChuckableIsBuyable(chuckable))
        {
            ChuckableManager.Instance.OnBuyChuckable(chuckable);
            Destroy(chuckableButton.gameObject);//No need to recycle
            if (chuckableQueue.Count > 0) ShowNextChuckable();
        }
    }
}
