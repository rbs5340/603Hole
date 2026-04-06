using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

/// <summary>
/// Keeps a pool of icons, and display desired amount of icons on demand.
/// </summary>
public class IconDisplayUI : MonoBehaviour
{
    public Sprite iconSprite;
    [SerializeField] private TextMeshProUGUI numText;
    [SerializeField] private Image iconPrefab;
    [SerializeField] private RectTransform iconContainer;

    List<Image> activeIcons = new List<Image>();
    ObjectPool<Image> iconPool;

    private void Awake()
    {
        iconPool = new ObjectPool<Image>(
            createFunc: () =>
            {
                var icon = Instantiate(iconPrefab, iconContainer);
                icon.sprite = iconSprite;
                return icon;
            },
            actionOnGet: icon => icon.gameObject.SetActive(true),
            actionOnRelease: icon => icon.gameObject.SetActive(false),
            actionOnDestroy: icon => Destroy(icon.gameObject),
            defaultCapacity: 256
        );

        iconPrefab.gameObject.SetActive(false);
        ClearIcon();
        SetDisplayNumer(0);
    }

    public void SetDisplayNumer(int number)
    {
        numText.text = number.ToString();
    }

    public void SetIconCount(int count)
    {
        if (count < 0) return;

        var currentNum = activeIcons.Count;
        if (count > currentNum)
        {
            for (int i = 0; i < count - currentNum; i++)
            {
                AddIcon();
            }
        }
        else if (count < currentNum)
        {
            for (int i = 0; i < currentNum - count; i++)
            {
                RemoveIcon();
            }
        }
    }

    public void AddIcon()
    {
        var icon = iconPool.Get();
        activeIcons.Add(icon);
    }

    public void RemoveIcon()
    {
        if (activeIcons.Count > 0)
            iconPool.Release(activeIcons[0]);
        activeIcons.RemoveAt(0);
    }

    public void ClearIcon()
    {
        foreach (var icon in activeIcons)
            iconPool.Release(icon);
        activeIcons.Clear();
    }

    [ContextMenu("Set 256 Icons")]
    private void DebugAddIcon()
    {
        SetIconCount(256);
    }
}
