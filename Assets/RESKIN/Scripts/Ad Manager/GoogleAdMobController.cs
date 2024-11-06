using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;

using GoogleMobileAds.Sample;
using System.Collections;

/// <summary>
/// Demonstrates how to use the Google Mobile Ads MobileAds Instance.
/// </summary>
[AddComponentMenu("GoogleMobileAds/Samples/GoogleMobileAdsController")]
public class GoogleAdMobController : MonoBehaviour
{
    public string appId;
    public string smallBannerId;
    public string rectBannerId;
    public string interstitialId;
    public string rewardedId;
    public bool testAds;
    [Space]
    public static GoogleAdMobController Instance;
    private static bool _isInitialized;

    public BannerViewController bannerViewController;
    public InterstitialAdController interstitialAdController;
    public RewardedAdController rewardedAdController;
    public GoogleUmpController umpConsentController;
    internal int activityCounter = 0;
    bool firebaseInitialized = false;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        DontDestroyOnLoad(this);


    }

   /* internal void HideRectBannerAd()
    {
        rectBannerViewController.HideAd();
    }*/

    /// <summary>
    /// Initializes the MobileAds SDK
    /// </summary>
    private void Start()
    {
        // Demonstrates how to configure Google Mobile Ads.
        // Google Mobile Ads needs to be run only once and before loading any ads.
        if (_isInitialized)
        {
            return;
        }


        if (!testAds)
        {

            bannerViewController._adUnitId = smallBannerId;
          /*  rectBannerViewController._adUnitId = rectBannerId;*/
            interstitialAdController._adUnitId = interstitialId;
            rewardedAdController._adUnitId = rewardedId;

#if !UNITY_EDITOR
            Debug.unityLogger.logEnabled = false;
#endif
        }

        firebaseInitialized = false;
        // On Android, Unity is paused when displaying interstitial or rewarded video.
        // This setting makes iOS behave consistently with Android.
        MobileAds.SetiOSAppPauseOnBackground(true);

        // When true all events raised by GoogleMobileAds will be raised
        // on the Unity main thread. The default value is false.
        // https://developers.google.com/admob/unity/quick-start#raise_ad_events_on_the_unity_main_thread
        MobileAds.RaiseAdEventsOnUnityMainThread = true;

        // Set your test devices.
        // https://developers.google.com/admob/unity/test-ads
        List<string> deviceIds = new List<string>()
            {
                AdRequest.TestDeviceSimulator,
                // Add your test device IDs (replace with your own device IDs).
#if UNITY_IPHONE
                "96e23e80653bb28980d3f40beb58915c"
#elif UNITY_ANDROID
                "75EF8D155528C04DACBBA6F36F433035"
#endif
            };

        // Configure your RequestConfiguration with Child Directed Treatment
        // and the Test Device Ids.
        RequestConfiguration requestConfiguration = new RequestConfiguration
        {
            TestDeviceIds = deviceIds
        };
        MobileAds.SetRequestConfiguration(requestConfiguration);

        // Initialize the Google Mobile Ads SDK.
        Debug.Log("Google Mobile Ads Initializing.");
        MobileAds.Initialize((InitializationStatus initstatus) =>
        {
            if (initstatus == null)
            {
                Debug.LogError("Google Mobile Ads initialization failed.");
                return;
            }

            // If you use mediation, you can check the status of each adapter.
            var adapterStatusMap = initstatus.getAdapterStatusMap();
            if (adapterStatusMap != null)
            {
                foreach (var item in adapterStatusMap)
                {
                    Debug.Log(string.Format("Adapter {0} is {1}",
                        item.Key,
                        item.Value.InitializationState));
                }
            }

            StartCoroutine(InitAds());

            Debug.Log("Google Mobile Ads initialization complete.");
            _isInitialized = true;
        });
    }



    internal bool CanShowInerAdActivityCounter()
    {
        if (interstitialAdController.IsInterLoaded() && activityCounter > 2)
            return true;

        return false;
    }

    WaitForSeconds waitTime = new WaitForSeconds(0.5f);
    internal bool removeads = false;

    IEnumerator InitAds()
    {
      /*  umpConsentController.UpdateConsentInformation();
        yield return waitTime;*/
        bannerViewController.LoadAd();
        yield return waitTime;
       
        interstitialAdController.LoadAd();
        yield return waitTime;
        rewardedAdController.LoadAd();
        yield return waitTime;
     
    }

    

    internal void HideAdmobBanner()
    {
        bannerViewController.HideAd();
    }

    public void ShowAdmobBanner()
    {
        bannerViewController.ShowAd(AdPosition.Top);
    }

    public void ShowTopRightBannerAd()
    {
        bannerViewController.ShowAd(AdPosition.TopRight);
    }


    public void ShowInterstitialAd(UnityAction callBackFunction)
    {
        interstitialAdController.ShowAd(callBackFunction);
    }

    public void ShowRewardedAd(UnityAction unityAction)
    {
        rewardedAdController.ShowAd(unityAction);
    }


 
    /// <summary>
    /// Opens the AdInspector.
    /// </summary>
    public void OpenAdInspector()
    {
        Debug.Log("Opening ad Inspector.");
        MobileAds.OpenAdInspector((AdInspectorError error) =>
        {
            // If the operation failed, an error is returned.
            if (error != null)
            {
                Debug.Log("Ad Inspector failed to open with error: " + error);
                return;
            }

            Debug.Log("Ad Inspector opened successfully.");
        });
    }
}

