using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Video;

public class AdPlayer : MonoSingleton<AdPlayer>
{
    [SerializeField] private List<VideoClip> availableClips;
    [SerializeField] private VideoPlayer player;
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI countdown;
    [SerializeField] private GameObject contentHolder;

    [SerializeField] private float minWatchLength = 15;
    [Tooltip("Not modifiable during gameplay")]
    [SerializeField] private bool getBonusWhenClosePanel;

    private UnityEvent successCallback = new();
    private VideoClip[] shuffledClips;
    private int currentClipIndex = 0;
    private int ClipCount => availableClips.Count;

    private void Start()
    {
        List<VideoClip> copiedList = new();
        List<VideoClip> shuffledList = new();
        copiedList.AddRange(availableClips);
        int n = availableClips.Count;
        for (int i = 0; i < n; i++)
        {
            int index = Random.Range(0, copiedList.Count);
            shuffledList.Add(copiedList[index]);
            copiedList.RemoveAt(index);
        }
        shuffledClips = shuffledList.ToArray();

        closeButton.onClick.AddListener(StopAndClose);
        if (getBonusWhenClosePanel) closeButton.onClick.AddListener(InvokeSuccessCallback);
        StopAndClose();
    }

    private VideoClip GetRandomClip()
    {
        if (currentClipIndex >= ClipCount)
        {
            return shuffledClips[Random.Range(0, ClipCount)];
        }
        else
        {
            currentClipIndex++;
            return shuffledClips[currentClipIndex - 1];
        }
    }

    /// <summary>
    /// The callback is cleared every time the ad is over. So add it right before start playing.
    /// </summary>
    /// <param name="action"></param>
    public void AddSuccessCallback(UnityAction action)
    {
        successCallback.AddListener(action);
    }

    [ContextMenu("Play Ad")]
    public void OpenAndPlay()
    {
        if (contentHolder.activeSelf) return;
        player.clip = GetRandomClip();
        contentHolder.SetActive(true);
        player.Play();
        closeButton.gameObject.SetActive(false);
        countdown.gameObject.SetActive(true);
        StartCoroutine(WaitForPlaying(minWatchLength));
    }

    /// <summary>
    /// Only close the window, the success callback is called in WaitForPlaying coroutine.
    /// </summary>
    public void StopAndClose()
    {
        player.Stop();
        contentHolder.SetActive(false);
    }

    IEnumerator WaitForPlaying(float waitTime)
    {
        float timer = waitTime;
        while (timer > 0)
        {
            countdown.text = $"{Mathf.Ceil(timer):F0}";
            yield return null;
            timer -= Time.unscaledDeltaTime;//No idea why the game pauses but hope it works
        }
        closeButton.gameObject.SetActive(true);
        countdown.gameObject.SetActive(false);


        if (!getBonusWhenClosePanel) InvokeSuccessCallback();
    }

    private void InvokeSuccessCallback()
    {
        successCallback?.Invoke();
        successCallback.RemoveAllListeners();
    }
}
