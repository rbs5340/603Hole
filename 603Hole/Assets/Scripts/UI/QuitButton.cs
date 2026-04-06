using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class QuitButton : MonoBehaviour
{
    private void Awake()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(Quit);
    }

    private void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
