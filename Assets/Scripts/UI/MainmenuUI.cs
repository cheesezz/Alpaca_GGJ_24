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
    [SerializeField] private GameObject optionsObject;
    [SerializeField] private EventSystem eventSystem;

    private Animator optionsAnimator;

    private void Awake()
    {
        optionsAnimator = optionsObject.GetComponent<Animator>();

        // Add event for clicking on play button
        playButton.onClick.AddListener(() =>
        {
            // Call Loader function to load game
            Loader.LoadScene(Loader.Scene.JoinRoom);
        });

        // Add event for clicking on options button
        optionsButton.onClick.AddListener(() =>
        {
            print(optionsButton.name + " was pressed!");

            // Set every child active to true in options
            foreach (Transform child in optionsObject.transform)
                child.gameObject.SetActive(true);

            // Set every child active to false in mainmenu
            foreach (Transform child in this.transform)
                child.gameObject.SetActive(false);

            // Set selected button to close button
            eventSystem.SetSelectedGameObject(closeButton.gameObject);

            // Play the expanding animation
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

            // Set every child active to true in mainmenu
            foreach (Transform child in this.transform)
                child.gameObject.SetActive(true);

            // Set the selected to options button
            eventSystem.SetSelectedGameObject(optionsButton.gameObject);

            // Play the shrinking animation
            optionsAnimator.Play("Shrink");
        });
    }
}
