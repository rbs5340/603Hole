using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SceneSwitchButton : MonoBehaviour
{
    public string targetScene;

    private void Awake()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(() => SceneManager.LoadScene(targetScene));
    }
}
