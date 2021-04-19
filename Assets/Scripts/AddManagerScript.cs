using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AddManagerScript : MonoBehaviour
{
    string googlePlayID = "4095891";
    public string surfacingID = "bannerPlacement";
    bool testMode = true;
    // Start is called before the first frame update
    void Start()
    {
        Advertisement.Initialize(googlePlayID, testMode);
        StartCoroutine(ShowBannerWhenInitialized());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ShowBannerWhenInitialized()
    {
        while (!Advertisement.isInitialized)
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        
        Advertisement.Banner.Show(surfacingID);
    }
}
