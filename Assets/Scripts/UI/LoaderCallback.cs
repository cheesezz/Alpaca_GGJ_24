using UnityEngine;

public class LoaderCallback : MonoBehaviour
{
    private bool isFirstUpdate = true;

    private void Update()
    {
        if (isFirstUpdate)
        {
            isFirstUpdate = false;      // Not the first update after this so set to false

            Loader.LoaderCallback();    // Only call LoaderCallback function on the first update call
        }
    }
}
