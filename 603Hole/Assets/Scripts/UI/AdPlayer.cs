using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Video;

public class AdPlayer : MonoSingleton<AdPlayer>
{
    [SerializeField] private VideoClip clip;
    [SerializeField] private VideoPlayer player;
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI countdown;
    [SerializeField] private GameObject contentHolder;

    [SerializeField] private float minWatchLength = 15;

    private UnityEvent successCallback = new();

    private void Start()
    {
        player.clip = clip;
        closeButton.onClick.AddListener(StopAndClose);
        StopAndClose();
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
            timer -= Time.deltaTime;
        }
        closeButton.gameObject.SetActive(true);
        countdown.gameObject.SetActive(false);
        successCallback?.Invoke();
        successCallback.RemoveAllListeners();
    }
}
