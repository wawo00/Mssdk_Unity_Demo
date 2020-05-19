using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Polymer;
using System;
using System.IO;


using System.Text.RegularExpressions;


public class MSSDKDemo : MonoBehaviour {

	private bool inited;

	private bool TEST_AD = true;

	// Use this for initialization
	void Start () {
		//onButtonClick();
		//onBtnExitAd_Click();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// ********** Button **********
	// init
	public void onBtnClick_InitSDK() {
		Debug.Log ("===> call onBtnClick_InitSDK");
		MSSDK.initSdk(new System.Action<string>(InitSDKCompleted));
        MSSDK.openLog(true);
    }

	// reward
	public void onBtnClick_isRewardReady()  {
		Debug.Log ("===> call onBtnClick_isRewardReady");
		bool b = MSSDK.isRewardReady();
		Debug.Log ("===> isRewardReady at: " + b);
	}

	public void onBtnClick_showReward() {
		Debug.Log ("===> call onBtnClick_showReward");
		MSSDK.setRewardCallback(new System.Action<string, string>(onRewardDidOpen),
								new System.Action<string, string>(onRewardDidClcik),
								new System.Action<string, string>(onRewardDidClose),
								new System.Action<string, string>(onRewardDidReward));
		MSSDK.showRewardAd("ssss");
	}

	// interstitial
	public void onBtnClick_isInterstitialReady () {
		Debug.Log ("===> call onBtnClick_isInterstitialReady");
		bool b = MSSDK.isInterstitialReady("sdada");
		Debug.Log ("===> isInterstitialReady at: " + b);
	}

	public void onBtnClick_showInterstitial () {
		Debug.Log ("===> call onBtnClick_showInterstitial");
		MSSDK.setInterstitialCallback(	new System.Action<string, string>(onInterstitialDidOpen),
										new System.Action<string, string>(onInterstitialDidClcik),
										new System.Action<string, string>(onInterstitialDidClose));
		MSSDK.showInterstitialAd("sdada");
	}

	// banner
	public void onBtnClick_showBanner () {
		MSSDK.setBannerCallback(new System.Action<string, string>(onBannerDidShow),
								new System.Action<string, string>(onBannerDidClcik));
		double x = 0;
		double y = UnityEngine.Screen.height/2-50;
		double width = UnityEngine.Screen.width/2;
		double height = 50;

        #if UNITY_IOS && !UNITY_EDITOR
		     MSSDK.showBannerAd("sss" , x, y, width, height);
        #elif UNITY_ANDROID && !UNITY_EDITOR
			 MSSDK.showAndroidBannerAdAtBottom("sss");
        #endif
        Debug.Log ("===> call onBtnClick_showBanner" + " x:" + x + " y:" + y + " width:" + width + " height:" + height);
	}

	public void onBtnClick_removeBanner () {
		Debug.Log ("===> call onBtnClick_removeBanner");
		MSSDK.removeBannerAd("sss");
	}

	// debug
	public void onBtnClick_openLog() {
		Debug.Log ("===> call onBtnClick_openLog");
        MSSDK.openLog(true);	
	}

	// privacy
	public void onBtnClick_grantConsent() {
		Debug.Log ("===> call onBtnClick_grantConsent");
		MSSDK.grantConsent();
	}

	public void onBtnClick_revokeConsent() {
		Debug.Log ("===> call onBtnClick_revokeConsent");
		MSSDK.revokeConsent();
	}

	// ********** Callback **********
	// init
	private void InitSDKCompleted(string str) {
		Debug.Log ("===> InitSDKCompleted Callback at: " + str);
	}

	// reward
	private void onRewardDidOpen(string cpAdUnitID, string message) {
		Debug.Log ("===> onRewardDidOpen Callback at: " + cpAdUnitID);
	}

	private void onRewardDidClcik(string cpAdUnitID, string message) {
		Debug.Log ("===> onRewardDidClcik Callback at: " + cpAdUnitID);
	}

	private void onRewardDidClose(string cpAdUnitID, string message) {
		Debug.Log ("===> onRewardDidClose Callback at: " + cpAdUnitID);
	}

	private void onRewardDidReward(string cpAdUnitID, string message) {
		Debug.Log ("===> onRewardDidReward Callback at: " + cpAdUnitID);
	}

	// interstitial
	private void onInterstitialDidOpen(string cpAdUnitID, string message) {
		Debug.Log ("===> onInterstitialDidOpen Callback at: " + cpAdUnitID);
	}

	private void onInterstitialDidClcik(string cpAdUnitID, string message) {
		Debug.Log ("===> onInterstitialDidClcik Callback at: " + cpAdUnitID);
	}

	private void onInterstitialDidClose(string cpAdUnitID, string message) {
		Debug.Log ("===> onInterstitialDidClose Callback at: " + cpAdUnitID);
	}

	// banner
	private void onBannerDidShow(string cpAdUnitID, string message) {
		Debug.Log ("===> onBannerDidShow Callback at: " + cpAdUnitID);
	}

	private void onBannerDidClcik(string cpAdUnitID, string message) {
		Debug.Log ("===> onBannerDidClcik Callback at: " + cpAdUnitID);
	}

	//获取所有路径
	public void getPathWithSetConfigurationFile(){

		XXPod.PodTool.fixPathWithSetConfigurationFile ();
	}

}
