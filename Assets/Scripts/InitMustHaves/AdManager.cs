using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdManager : MonoBehaviour
{
    private BannerView bannerView;

    private AdRequest request;

/*
    // Create a 320x50 banner ad at coordinate (0,50) on screen.
    BannerView bannerView = new BannerView(adUnitId, AdSize.Banner, 0, 50);
    // Create a custom size ad
    AdSize adSize = new AdSize(250, 250);
    BannerView bannerView = new BannerView(adUnitId, adSize, AdPosition.Bottom);
*/

    public void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });

        //this.RequestBanner();

        #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-3940256099942544/6300978111"; // test id
        #elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/6300978111"; // added my id
        #else
            string adUnitId = "unexpected_platform";
        #endif

        // Create a 320x50 banner at the bottom of the screen.
        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);


        // Create an empty ad request.
        this.request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);
    }


    private void OnDestroy() {
        this.bannerView.Destroy();
    }

    private void RequestBanner()
    {
        #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-3940256099942544/6300978111";
        #elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/2934735716";
        #else
            string adUnitId = "unexpected_platform";
        #endif

        // Create a 320x50 banner at the bottom of the screen.
        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);


        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);
    }
}

