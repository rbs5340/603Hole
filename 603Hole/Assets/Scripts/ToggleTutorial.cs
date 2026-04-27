using UnityEngine;

public class ToggleTutorial : MonoBehaviour
{
    [SerializeField] private GameObject tutorialPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            tutorialPanel.SetActive(!tutorialPanel.activeSelf);
        }
    }
}
