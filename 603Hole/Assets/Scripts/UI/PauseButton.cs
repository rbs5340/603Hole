using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Button))]
public class PauseButton : MonoBehaviour
{
    public bool targetPauseState;
    public GameObject pausePanel;

    private void Awake()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(SetPauseState);
        Time.timeScale = 1;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetPauseState();
        }
    }
    private void SetPauseState()
    {
        Time.timeScale = targetPauseState ? 0.0f : 1.0f;
        pausePanel.SetActive(targetPauseState);
    }
}
