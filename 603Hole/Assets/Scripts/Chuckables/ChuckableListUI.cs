using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChuckableListUI : MonoBehaviour
{
    public ChuckableEntry entryPrefab;
    public RectTransform buttonHolder;

    Queue<Chuckable> chuckableQueue = new();
    List<ChuckableEntry> activeEntries = new();

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

    private void Update()
    {
        RefreshResources();
    }

    public void RefreshResources()
    {
        foreach (var entry in activeEntries)
        {
            var buyable = ChuckableManager.Instance.ChuckableIsBuyable(entry.Chuckable, out float progress);
            entry.SetButtonState(buyable, progress >= 0.7f);
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
        button.BindCallback(_ => OnButtonClicked(_), _ => OnAdButtonClicked(_));
        activeEntries.Add(button);
    }

    /// <summary>
    /// Called when the BUY button clicked
    /// </summary>
    private void OnButtonClicked(ChuckableEntry chuckableButton)
    {
        var chuckable = chuckableButton.Chuckable;
        if (ChuckableManager.Instance.ChuckableIsBuyable(chuckable, out _))
        {
            Chuck(chuckableButton);
        }
    }

    private void OnAdButtonClicked(ChuckableEntry chuckableButton)
    {
        var chuckable = chuckableButton.Chuckable;
        AdPlayer.Instance.AddSuccessCallback(() => OnVideoSuccess(chuckableButton));
        AdPlayer.Instance.OpenAndPlay();
    }

    private void OnVideoSuccess(ChuckableEntry targetEntry)
    {
        Chuck(targetEntry);
    }

    private void Chuck(ChuckableEntry chuckableEntry)
    {
        Hole hole = FindAnyObjectByType<Hole>();
        var holeBounds = hole.GetComponent<SpriteRenderer>().bounds;
        float endPointRandomness = 0.4f;//Normally should be less than 0.5
        endPointRandomness *= 0.5f;
        float endWorldPosX = Mathf.Lerp(holeBounds.min.x, holeBounds.max.x, Random.Range(0.5f - endPointRandomness, 0.5f + endPointRandomness));
        float endWorldPosY = Mathf.Lerp(holeBounds.min.y, holeBounds.max.y, Random.Range(0.5f - endPointRandomness, 0.5f + endPointRandomness));
        var endWorldPos = new Vector2(endWorldPosX, endWorldPosY);

        IconProjectileHolder.Instance.Create(
            chuckableEntry.GetIconPosition(),
            Camera.main.WorldToScreenPoint(endWorldPos), // world space to screen space
            chuckableEntry.Chuckable.Icon,
            () => ChuckableManager.Instance.OnChuck(chuckableEntry.Chuckable)
        );

        ChuckableManager.Instance.OnBuyChuckable(chuckableEntry.Chuckable);
        activeEntries.Remove(chuckableEntry);
        Destroy(chuckableEntry.gameObject);//No need to recycle
        if (chuckableQueue.Count > 0) ShowNextChuckable();

    }
}
