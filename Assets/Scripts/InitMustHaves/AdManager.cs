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
    private string _adUnitId = "ca-app-pub-4670629006148539/6395638256";
    #else
    private string _adUnitId = "unused";
    #endif


    private ISaveManager saveManager;
    private IAudioManager audioManager;
    private void Awake() {
        saveManager = ServiceLocator.Resolve<ISaveManager>();
        audioManager = ServiceLocator.Resolve<IAudioManager>();
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
    public void LoadRewardedAd(Boolean isMenu)
    {

        // Nothing is happening with coinsEarned as a parameter because I am adding the coins in the game over state but I could add them here after the
        // ad is finished to make sure that the people are finishing the ad. 
        // Can't have more than one parameter because using it in the inspector for a button only allows one parameter. 

        if (isMenu == true) {
            if (saveManager.saveData.numVidsWatchedToday < 2) {
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

                rewardedAd.OnUserEarnedReward += (object sender, Reward reward) => {

                    print("Should play music");
                    if (saveManager.saveData.musicOn == true) {
                        audioManager.PlayMusic("MenuBackgroundMusic");
                    }
                    if (isMenu != true) {
                        SceneManager.LoadScene("MainMenu");
                    }
                    saveManager.saveData.numVidsWatchedToday += 1;
                    saveManager.saveData.coins += 10;
                    saveManager.saveData.vidAdDate = DateTime.Now;
                    saveManager.Save();

                    // Need to destroy the rewardedAd
                    rewardedAd.Destroy();
                };
                rewardedAd.OnAdOpening += (object sender, EventArgs eventArgs) => {
                    // Pause music
                    print("Should pause music");
                    audioManager.StopMusic("MenuBackgroundMusic");
                };
                rewardedAd.OnAdClosed += (object sender, EventArgs eventArgs) => {
                    // Play music
                    print("Should play music");
                    if (saveManager.saveData.musicOn == true) {
                        audioManager.PlayMusic("MenuBackgroundMusic");
                    }

                    // Since isMenu is true that means its coming from the menu and it should give you coins but not go to the main menu
                    saveManager.saveData.numVidsWatchedToday += 1;
                    saveManager.saveData.coins += 10;
                    
                    // Sets so you have watched a video today
                    saveManager.saveData.vidAdDate = DateTime.Now;
                    saveManager.Save();
                };
                rewardedAd.OnAdLoaded += (object sender, EventArgs eventArgs) => {
                    print("On ad loaded");
                    ShowRewardedAd();
                };

                rewardedAd.OnAdFailedToLoad += (object sender, AdFailedToLoadEventArgs failedEventArgs) => {
                    print("On ad failed to load");
                };

                // Load request
                this.rewardedAd.LoadAd(request);
            }
        } else {
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

            rewardedAd.OnUserEarnedReward += (object sender, Reward reward) => {

                // Need to destroy the rewardedAd
                rewardedAd.Destroy();

                print("Should play music");
                if (saveManager.saveData.musicOn == true) {
                    audioManager.PlayMusic("MenuBackgroundMusic");
                }
                if (isMenu != true) {
                    SceneManager.LoadScene("MainMenu");
                }
            };
            rewardedAd.OnAdOpening += (object sender, EventArgs eventArgs) => {
                // Pause music
                print("Should pause music");
                audioManager.StopMusic("MenuBackgroundMusic");
            };
            rewardedAd.OnAdClosed += (object sender, EventArgs eventArgs) => {
                // Play music
                print("Should play music");
                if (saveManager.saveData.musicOn == true) {
                    audioManager.PlayMusic("MenuBackgroundMusic");
                }

                // saveManager.saveData.numVidsWatchedToday += 1;

                // Since isMenu is false that means its coming from the story or survival and it shouldn't give coins but should bring you to the main menu
                if (isMenu != true) {
                    SceneManager.LoadScene("MainMenu");
                }
            };
            rewardedAd.OnAdLoaded += (object sender, EventArgs eventArgs) => {
                print("On ad loaded");
                ShowRewardedAd();
            };

            rewardedAd.OnAdFailedToLoad += (object sender, AdFailedToLoadEventArgs failedEventArgs) => {
                print("On ad failed to load");
            };

            // Load request
            this.rewardedAd.LoadAd(request);
        }
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



