using System;
using UnityEngine;
using UnityEngine.Events;
using GoogleMobileAds.Api;

namespace GoogleMobileAds.Sample
{
    /// <summary>
    /// Demonstrates how to use Google Mobile Ads interstitial ads.
    /// </summary>
    [AddComponentMenu("GoogleMobileAds/Samples/InterstitialAdController")]
    public class InterstitialAdController : MonoBehaviour
    {
      

        // These ad units are configured to always serve test ads.
#if UNITY_ANDROID
        public   string _adUnitId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IPHONE
        private const string _adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        private const string _adUnitId = "unused";
#endif

        private InterstitialAd _interstitialAd;


        public UnityAction handleInterstialClosed;
        /// <summary>
        /// Loads the ad.
        /// </summary>
        public void LoadAd()
        {
            if (GoogleAdMobController.Instance.removeads)
                return;
            // Clean up the old ad before loading a new one.
            if (_interstitialAd != null)
            {
                DestroyAd();
            }

            Debug.Log("Loading interstitial ad.");

            // Create our request used to load the ad.
            var adRequest = new AdRequest();

            // Send the request to load the ad.
            InterstitialAd.Load(_adUnitId, adRequest, (InterstitialAd ad, LoadAdError error) =>
            {
                // If the operation failed with a reason.
                if (error != null)
                {
                    Debug.LogError("Interstitial ad failed to load an ad with error : " + error);
                    return;
                }
                // If the operation failed for unknown reasons.
                // This is an unexpected error, please report this bug if it happens.
                if (ad == null)
                {
                    Debug.LogError("Unexpected error: Interstitial load event fired with null ad and null error.");
                    return;
                }

                // The operation completed successfully.
                Debug.Log("Interstitial ad loaded with response : " + ad.GetResponseInfo());
                _interstitialAd = ad;

                // Register to ad events to extend functionality.
                RegisterEventHandlers(ad);

          
            });
        }

        /// <summary>
        /// Shows the ad.
        /// </summary>
        public void ShowAd(UnityAction unityAction) 
        {
            if (_interstitialAd != null && _interstitialAd.CanShowAd())
            {
                handleInterstialClosed = unityAction;
                Debug.Log("Showing interstitial ad.");
                _interstitialAd.Show();
            }
            else
            {
                Debug.LogError("Interstitial ad is not ready yet.");
            }

        }

        public bool IsInterLoaded()
        {
            if (_interstitialAd != null && _interstitialAd.CanShowAd())
            {
                return true;
            }

             return false;
        }

        /// <summary>
        /// Destroys the ad.
        /// </summary>
        public void DestroyAd()
        {
            if (_interstitialAd != null)
            {
                Debug.Log("Destroying interstitial ad.");
                _interstitialAd.Destroy();
                _interstitialAd = null;
            }

        
        }

        /// <summary>
        /// Logs the ResponseInfo.
        /// </summary>
        public void LogResponseInfo()
        {
            if (_interstitialAd != null)
            {
                var responseInfo = _interstitialAd.GetResponseInfo();
                UnityEngine.Debug.Log(responseInfo);
            }
        }

        private void RegisterEventHandlers(InterstitialAd ad)
        {
            // Raised when the ad is estimated to have earned money.
            ad.OnAdPaid += (AdValue adValue) =>
            {
                Debug.Log(String.Format("Interstitial ad paid {0} {1}.",
                    adValue.Value,
                    adValue.CurrencyCode));
            };
            // Raised when an impression is recorded for an ad.
            ad.OnAdImpressionRecorded += () =>
            {
                Debug.Log("Interstitial ad recorded an impression.");
            };
            // Raised when a click is recorded for an ad.
            ad.OnAdClicked += () =>
            {
                Debug.Log("Interstitial ad was clicked.");
            };
            // Raised when an ad opened full screen content.
            ad.OnAdFullScreenContentOpened += () =>
            {
                Debug.Log("Interstitial ad full screen content opened.");
            };
            // Raised when the ad closed full screen content.
            ad.OnAdFullScreenContentClosed += () =>
            {
                handleInterstialClosed?.Invoke();
              
                LoadAd();
                Debug.Log("Interstitial ad full screen content closed.");
            };
            // Raised when the ad failed to open full screen content.
            ad.OnAdFullScreenContentFailed += (AdError error) =>
            {
                Debug.LogError("Interstitial ad failed to open full screen content with error : "
                    + error);
            };
        }
    }
}