using UnityEngine;
using System;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using Dorkbots.ServiceLocatorTools;
using UnityEngine.SceneManagement;

public class AdManager : MonoBehaviour , IAdManager
{
    private BannerView bannerView;

    //private AdRequest request;


    private RewardedAd rewardedAd;




    // These ad units are configured to always serve test ads.
    #if UNITY_ANDROID
    private string _adUnitId = "ca-app-pub-3940256099942544/5224354917";
    #elif UNITY_IPHONE
    private string _adUnitId = "ca-app-pub-3940256099942544/5224354917";
    #else
    private string _adUnitId = "unused";
    #endif


    private ISaveManager saveManager;
    private void Awake() {
        saveManager = ServiceLocator.Resolve<ISaveManager>();
    }

    public void Start()
    {


        // Initialize the Google Mobile Ads SDK.
       MobileAds.Initialize(initStatus => {
            // IDK if I do my call here in the callback or not 


        });


        this.rewardedAd = new RewardedAd(_adUnitId);



        //this.RequestBanner();

        
    }





    // Loads the rewarded ad.
    public void LoadRewardedAd(int coinsEarned)
    {


        // Clean up the old ad before loading a new one.
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        this.rewardedAd = new RewardedAd(_adUnitId);

        // create our request used to load the ad.
        AdRequest request = new AdRequest.Builder().Build();
        //request.Keywords.Add("unity-admob-sample");

        // Load request
        this.rewardedAd.LoadAd(request);

        rewardedAd.OnUserEarnedReward += (object sender, Reward reward) => {
            //print("Reward given: " + (int)reward.Amount); // this is set in GoogleAdMob for each ad unit

            print("Reward given: " + coinsEarned);

            saveManager.saveData.coins += coinsEarned;

            saveManager.Save();

            SceneManager.LoadScene("MainMenu");

            // Need to destroy the rewardedAd
            rewardedAd.Destroy();
        };
        rewardedAd.OnAdOpening += (object sender, EventArgs eventArgs) => {
            // Pause music
            print("Should pause music");
        };
        rewardedAd.OnAdClosed += (object sender, EventArgs eventArgs) => {
            // Play music
            print("Should play music");
        };

        ShowRewardedAd();
    }

    // Shows the rewarded ad
    private void ShowRewardedAd() {
        if (rewardedAd.IsLoaded() == true) {
            rewardedAd.Show();
        } else {
            print("Error, ad is not loaded yet");
        }
    }

    

    
    


    
















    private void OnDestroy() {
        //this.bannerView.Destroy();
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



