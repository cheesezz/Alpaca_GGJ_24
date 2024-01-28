using UnityEngine;

public class LoaderCallback : MonoBehaviour
{
    private const float LOADING_TIME = 0.5f;

    private bool isFirstUpdate = true;
    private float minLoadingTime = LOADING_TIME;

    private void Update()
    {
        if (isFirstUpdate)
        {
            minLoadingTime -= Time.deltaTime;
            if (minLoadingTime <= 0 )
            {
                isFirstUpdate = false;      // Not the first update after this so set to false
                minLoadingTime = LOADING_TIME;

                Loader.LoaderCallback();    // Only call LoaderCallback function on the first update call
            }
        }
    }
}
