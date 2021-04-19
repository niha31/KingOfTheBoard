using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.UI;

public class FacebookScript : MonoBehaviour
{
    private void Awake()
    {
        if(!FB.IsInitialized)
        {
            FB.Init();
        }
        else
        {
            FB.ActivateApp();
        }

    }

    public void Share()
    {
        FB.ShareLink(contentTitle:"Play King Of the Board!", contentDescription:"Obsessed with this new game!", callback:OnShare);
    }

    private void OnShare(IShareResult result)
    {
        if(result.Cancelled || !string.IsNullOrEmpty(result.Error))
        {
            Debug.Log("sharing error: " + result.Error);
        }
        else if(!string.IsNullOrEmpty(result.PostId))
        {
            Debug.Log(result.PostId);
        }
        else
        {
            Debug.Log("Share sucessfull");
        }
    }

}