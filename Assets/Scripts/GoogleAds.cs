using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class GoogleAds : MonoBehaviour {

    private BannerView bannerView;
    private InterstitialAd interstitial;
    private RewardBasedVideoAd rewardBasedVideo;

    private static string ANDROID_KEY = "ca-app-pub-3940256099942544~3347511713";
    private static string IOS_KEY = "ca-app-pub-3940256099942544~1458002511";
    private static string OTHER_PLATFORM = "unexpected_platform";

    private static string ANDROID_BANNER = "ca-app-pub-3940256099942544/6300978111";
    private static string IOS_BANNER = "ca-app-pub-3940256099942544/2934735716";

    private static string ANDROID_INTERSTITIAL = "ca-app-pub-3940256099942544/1033173712";
    private static string IOS_INTERSTITIAL = "ca-app-pub-3940256099942544/4411468910";

    private static string ANDROID_REWARDED_VIDEO= "ca-app-pub-3940256099942544/5224354917";
    private static string IOS_REWARDED_VIDEO = "ca-app-pub-3940256099942544/1712485313";

    private static string TEST_DEVICE_ID = "69922574B09000879FF38B7E82BC85C7";

    void Start () {

#if UNITY_ANDROID
        string appId = ANDROID_KEY;
#elif UNITY_IPHONE
        string appId = IPHONE_KEY;
        MobileAds.SetiOSAppPauseOnBackground(true);
#else
        string appId = OTHER_PLATFORM;
#endif

        MobileAds.Initialize(appId);
        rewardBasedVideo = RewardBasedVideoAd.Instance;

        rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
        rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
        rewardBasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
        rewardBasedVideo.OnAdStarted += HandleRewardBasedVideoStarted;
        rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
        rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
        rewardBasedVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;
    }

    public void RequestBanner()
    {
#if UNITY_EDITOR
        string adUnitId = OTHER_PLATFORM;
#elif UNITY_ANDROID
        string adUnitId = ANDROID_BANNER;
#elif UNITY_IPHONE
        string adUnitId = IPHONE_BANNER;
#else
        string adUnitId = OTHER_PLATFORM;
#endif
        if (bannerView != null)
        {
            bannerView.Destroy();
        }

        bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Top);
        bannerView.OnAdLoaded += HandleAdLoaded;
        bannerView.OnAdFailedToLoad += HandleAdFailedToLoad;
        bannerView.OnAdOpening += HandleAdOpened;
        bannerView.OnAdClosed += HandleAdClosed;
        bannerView.OnAdLeavingApplication += HandleAdLeftApplication;

        bannerView.LoadAd(CreateAdRequest());

    }

    public void DestroyBanner()
    {
        bannerView.Destroy();
    }

    public void RequestInterstitial()
    {
#if UNITY_EDITOR
        string adUnitId = OTHER_PLATFORM;
#elif UNITY_ANDROID
        string adUnitId = ANDROID_INTERSTITIAL;
#elif UNITY_IPHONE
        string adUnitId = IPHONE_INTERSTITIAL";
#else
        string adUnitId = "unexpected_platform";
#endif

        if (interstitial != null)
        {
            interstitial.Destroy();
        }

        interstitial = new InterstitialAd(adUnitId);
        interstitial.OnAdLoaded += HandleInterstitialLoaded;
        interstitial.OnAdFailedToLoad += HandleInterstitialFailedToLoad;
        interstitial.OnAdOpening += HandleInterstitialOpened;
        interstitial.OnAdClosed += HandleInterstitialClosed;
        interstitial.OnAdLeavingApplication += HandleInterstitialLeftApplication;

        interstitial.LoadAd(CreateAdRequest());
    }


    public void ShowInterstitial()
    {
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
        }
        else
        {
            print("Interstitial is not ready yet");
        }
    }

    public void DestroyInterstitial()
    {
        interstitial.Destroy();
    }

    public void RequestRewardBasedVideo()
    {
#if UNITY_EDITOR
        string adUnitId = OTHER_PLATFORM;
#elif UNITY_ANDROID
        string adUnitId = ANDROID_REWARDED_VIDEO;
#elif UNITY_IPHONE
        string adUnitId = IPHONE_REWARDED_VIDEO;
#else
        string adUnitId = "unexpected_platform";
#endif

        rewardBasedVideo.LoadAd(CreateAdRequest(), adUnitId);
    }

    public void ShowRewardBasedVideo()
    {
        if (rewardBasedVideo.IsLoaded())
        {
            rewardBasedVideo.Show();
        }
        else
        {
            MonoBehaviour.print("Reward based video ad is not ready yet");
        }
    }

    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder()
            //.AddTestDevice(AdRequest.TestDeviceSimulator)
            .AddTestDevice(TEST_DEVICE_ID)
            //.AddKeyword("game")
            //.SetGender(Gender.Male)
            //.SetBirthday(new DateTime(1985, 1, 1))
            //.TagForChildDirectedTreatment(false)
            //.AddExtra("color_bg", "9B30FF")
            .Build();
    }

    #region Banner callback handlers

    public void HandleAdLoaded(object sender, EventArgs args)
    {
        print("HandleAdLoaded event received");
    }

    public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        print("HandleFailedToReceiveAd event received with message: " + args.Message);
    }

    public void HandleAdOpened(object sender, EventArgs args)
    {
        print("HandleAdOpened event received");
    }

    public void HandleAdClosed(object sender, EventArgs args)
    {
        print("HandleAdClosed event received");
    }

    public void HandleAdLeftApplication(object sender, EventArgs args)
    {
        print("HandleAdLeftApplication event received");
    }

    #endregion

    #region Interstitial callback handlers

    public void HandleInterstitialLoaded(object sender, EventArgs args)
    {
        print("HandleInterstitialLoaded event received");
    }

    public void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        print(
            "HandleInterstitialFailedToLoad event received with message: " + args.Message);
    }

    public void HandleInterstitialOpened(object sender, EventArgs args)
    {
        print("HandleInterstitialOpened event received");
    }

    public void HandleInterstitialClosed(object sender, EventArgs args)
    {
        print("HandleInterstitialClosed event received");
    }

    public void HandleInterstitialLeftApplication(object sender, EventArgs args)
    {
        print("HandleInterstitialLeftApplication event received");
    }

    #endregion

    #region RewardBasedVideo callback handlers

    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        print("HandleRewardBasedVideoLoaded event received");
    }

    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        print(
            "HandleRewardBasedVideoFailedToLoad event received with message: " + args.Message);
    }

    public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
    {
        print("HandleRewardBasedVideoOpened event received");
    }

    public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
    {
        print("HandleRewardBasedVideoStarted event received");
    }

    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        print("HandleRewardBasedVideoClosed event received");
    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        print(
            "HandleRewardBasedVideoRewarded event received for " + amount.ToString() + " " + type);
    }

    public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
    {
        print("HandleRewardBasedVideoLeftApplication event received");
    }

    #endregion
}
