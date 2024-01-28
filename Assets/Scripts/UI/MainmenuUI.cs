using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainmenuUI : MonoBehaviour
{
    // Make buttons assignable from editor
    [SerializeField] private Button playButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button closeButton;

    [SerializeField] private GameObject mainButtonsObject;
    [SerializeField] private GameObject optionsObject;

    [SerializeField] private EventSystem eventSystem;

    private Animator optionsAnimator;
    private bool optionsShrink = false;

    private void Awake()
    {
        optionsAnimator = optionsObject.GetComponent<Animator>();

        //foreach (Transform child in optionsObject.transform)
        //    child.gameObject.SetActive(false);

        // Add event for clicking on play button
        playButton.onClick.AddListener(() =>
        {
            // Call Loader function to load game
            Loader.LoadScene(Loader.Scene.DemoGameScene);
        });

        // Add event for clicking on options button
        optionsButton.onClick.AddListener(() =>
        {
            print(optionsButton.name + " was pressed!");

            foreach (Transform child in optionsObject.transform)
                child.gameObject.SetActive(true);

            mainButtonsObject.gameObject.SetActive(false);
            eventSystem.SetSelectedGameObject(closeButton.gameObject);

            optionsAnimator.Play("Expand");
        });

        // Add event for clicking on quit button
        quitButton.onClick.AddListener(() =>
        {
            print(quitButton.name + " was pressed!");
            // Exit the application
            Application.Quit();
        });

        closeButton.onClick.AddListener(() =>
        {
            print(closeButton.name + " was pressed!");

            optionsAnimator.Play("Shrink");
            optionsShrink = true;

            mainButtonsObject.gameObject.SetActive(true);
            eventSystem.SetSelectedGameObject(optionsButton.gameObject);
        });
    }

    private void Update()
    {
        if (optionsShrink)
        {
            print("Closed!");
            optionsShrink = false;
        }
    }
}
