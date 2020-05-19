using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MSSDKJSON;
using System;
namespace Polymer {

	public class MSSDKNativeCallback : MonoBehaviour {

		private static MSSDKNativeCallback instance = null;
        public static readonly string Unity_Callback_Class_Name = "MSSDK_Callback_Object";
        public static readonly string Unity_Callback_Function_Name = "onNativeCallback";

        public static readonly string Unity_Callback_Message_Key_Function = "callbackMessageKeyFunctionName";
        public static readonly string Unity_Callback_Message_Key_Parameter = "callbackMessageKeyParameter";

		private readonly static string Unity_Callback_Message_Function_Init_Complete  			=	"MS_Init_Complete";      		// 初始化完成
		private readonly static string Unity_Callback_Message_Function_RewardAd_DidOpen   		=	"MS_RewardAd_DidOpen";      	// 激励视频广告打开
		private readonly static string Unity_Callback_Message_Function_RewardAd_DidClick  		=	"MS_RewardAd_DidClick";     	// 激励视频广告点击
		private readonly static string Unity_Callback_Message_Function_RewardAd_DidClose  		=	"MS_RewardAd_DidClose";     	// 激励视频广告关闭
		private readonly static string Unity_Callback_Message_Function_RewardAd_DidReward 		=	"MS_RewardAd_DidReward";    	// 准备发放奖励
		private readonly static string Unity_Callback_Message_Function_InterstitialAd_DidOpen  	=	"MS_InterstitialAd_DidOpen" ;  	// 插屏广告打开
		private readonly static string Unity_Callback_Message_Function_InterstitialAd_DidClick 	=	"MS_InterstitialAd_DidClick";  	// 插屏广告点击
		private readonly static string Unity_Callback_Message_Function_InterstitialAd_DidClose 	=	"MS_InterstitialAd_DidClose"; 	// 插屏广告关闭
		private readonly static string Unity_Callback_Message_Function_BannerAd_DidShow    		=	"MS_BannerAd_DidShow";      	// 横幅广告展示
		private readonly static string Unity_Callback_Message_Function_BannerAd_DidClick   		=	"MS_BannerAd_DidClick";     	// 插屏广告点击
  
		private MSSDKNativeCall adCall;
		 
		public static MSSDKNativeCallback getInstance()
		{
			if (instance == null) {
				GameObject nativeCallback = new GameObject (Unity_Callback_Class_Name);
				nativeCallback.hideFlags = HideFlags.HideAndDontSave;
				DontDestroyOnLoad (nativeCallback);

				instance = nativeCallback.AddComponent<MSSDKNativeCallback> ();
			}
			return instance;
		}

		//bool isPaused = false;

		Hashtable actionInitsFailMaps;
		Hashtable actionInitsSuccessMaps;

		Action<string> InitSDKCompletedAction;

		Action<string, string> RewardAdDidOpenAction;
		Action<string, string> RewardAdDidClickAction;
		Action<string, string> RewardAdDidCloseAction;
		Action<string, string> RewardAdDidRewardAction;

		Action<string, string> InterstitialAdDidOpenAction;
		Action<string, string> InterstitialAdDidClickAction;
		Action<string, string> InterstitialAdDidCloseAction;

		Action<string, string> BannerAdDidShowAction;
		Action<string, string> BannerAdDidClickAction;


		List<string> cachedMessages = new List<string> (12);
		bool isAppFocus = false;

		bool enableCallbackAfterAppFocus = false;
		bool canObserverAppFocusCall = false;

		void OnGUI()
		{
			//Debug.Log ("===> Game onGUI Call");
		}

		public void setMSSDKNativeCall(MSSDKNativeCall call) {
			adCall = call;
		}

		public void setInitSDKCallback (Action<string> completed) {
			this.InitSDKCompletedAction = completed;
		}

		public void setRewardCallback (Action<string, string> didOpen, Action<string, string> didClick, Action<string, string> didClose, Action<string, string> didReward) {
			this.RewardAdDidOpenAction = didOpen;
			this.RewardAdDidClickAction = didClick;
			this.RewardAdDidCloseAction = didClose;
			this.RewardAdDidRewardAction = didReward;
		}

		public void setInterstitialCallback (Action<string, string> didOpen, Action<string, string> didClick, Action<string, string> didClose) {
			this.InterstitialAdDidOpenAction = didOpen;
			this.InterstitialAdDidClickAction = didClick;
			this.InterstitialAdDidCloseAction = didClose;
		}

		public void setBannerCallback (Action<string, string> didShow, Action<string, string> didClick) {
			this.BannerAdDidShowAction = didShow;
			this.BannerAdDidClickAction = didClick;
		}

		public void onNativeCallback(string message) {
			// Debug.Log ("===> onJavaCallback enableCallbackAfterAppFocus: " + enableCallbackAfterAppFocus +",canObserverAppFocusCall: " + canObserverAppFocusCall);
			// if (false && enableCallbackAfterAppFocus) {
			// 	if (canObserverAppFocusCall) {
			// 		if (isAppFocus) {
			// 			if (cachedMessages.Count > 0) {
			// 				foreach (string s in cachedMessages) {
			// 					doCallback (s);
			// 				}
			// 				cachedMessages.Clear();
			// 			}
			// 			doCallback (message);
			// 		} else {
			// 			cachedMessages.Add (message);
			// 		}
			// 	} else {
			// 		Hashtable jsonObj = (Hashtable)MSSDKJSON.MiniJSON.jsonDecode (message);
			// 		if (jsonObj.ContainsKey (Unity_Callback_Message_Key_Function)) {
			// 			string function = (string)jsonObj [Unity_Callback_Message_Key_Function];
			// 			if (   function.Equals (Unity_Callback_Message_Function_RewardAd_DidOpen)
			// 				|| function.Equals (Unity_Callback_Message_Function_RewardAd_DidClick)
			// 			    || function.Equals (Unity_Callback_Message_Function_RewardAd_DidReward)
			// 				|| function.Equals (Unity_Callback_Message_Function_InterstitialAd_DidOpen)
			// 				|| function.Equals (Unity_Callback_Message_Function_InterstitialAd_DidClick)) {
			// 				cachedMessages.Add (message);
			// 			} 
			// 			else {
			// 				if (	function.Equals (Unity_Callback_Message_Function_RewardAd_DidClose)
			// 					||  function.Equals (Unity_Callback_Message_Function_InterstitialAd_DidClose)) {
			// 					if (cachedMessages.Count > 0) {
			// 						foreach (string s in cachedMessages) {
			// 							doCallback (s);
			// 						}
			// 						cachedMessages.Clear ();
			// 					}
			// 				}
			// 				doCallback (message);
			// 			}
			// 		}

			// 	}

			// } else {
				doCallback (message);
			// }
		}

		public void doCallback(string message) {
			
			Debug.Log (message);
			Hashtable jsonObj = (Hashtable)MSSDKJSON.MiniJSON.jsonDecode (message);
			if (jsonObj.ContainsKey (Unity_Callback_Message_Key_Function)) {
				string function = (string)jsonObj[Unity_Callback_Message_Key_Function];
				string placeId = "";
				if (jsonObj.ContainsKey (Unity_Callback_Message_Key_Function)) {
					placeId = (string)jsonObj[Unity_Callback_Message_Key_Parameter];
				}
                
                string strFu = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                Debug.Log ("===> function: " + function +",cpadsid: " + placeId + ", time at:" + strFu);

                // init
                if (function.Equals (Unity_Callback_Message_Function_Init_Complete)) {
                	if (this.InitSDKCompletedAction != null) {
                		this.InitSDKCompletedAction ("SDK initialization completed");
                	}
				} 
				//reward callback
				else if (function.Equals (Unity_Callback_Message_Function_RewardAd_DidOpen)) {
					if (this.RewardAdDidOpenAction != null) {
						this.RewardAdDidOpenAction(placeId, "");
					} 
				} 
				else if (function.Equals (Unity_Callback_Message_Function_RewardAd_DidClick)) {
					if (this.RewardAdDidClickAction != null) {
						this.RewardAdDidClickAction (placeId, "");
					}
				} 
				else if (function.Equals (Unity_Callback_Message_Function_RewardAd_DidClose)) {
					if (this.RewardAdDidCloseAction != null) {
						this.RewardAdDidCloseAction (placeId, "");
					}
				} 
				else if (function.Equals (Unity_Callback_Message_Function_RewardAd_DidReward)) {
					if (this.RewardAdDidRewardAction != null) {
						this.RewardAdDidRewardAction (placeId, "");
					}
				}

				//Interstitial callback
				else if (function.Equals (Unity_Callback_Message_Function_InterstitialAd_DidOpen)) {
					if (this.InterstitialAdDidOpenAction != null) {
						this.InterstitialAdDidOpenAction (placeId, "");
					}
				} 
				else if (function.Equals (Unity_Callback_Message_Function_InterstitialAd_DidClick)) {
					if (this.InterstitialAdDidClickAction != null) {
						this.InterstitialAdDidClickAction (placeId, "");
					}
				} 
				else if (function.Equals (Unity_Callback_Message_Function_InterstitialAd_DidClose)) {
					if (this.InterstitialAdDidCloseAction != null) {
						this.InterstitialAdDidCloseAction (placeId, "");
					}
				} 
				
				//banner callback
				else if (function.Equals (Unity_Callback_Message_Function_BannerAd_DidShow)) {
					if (this.BannerAdDidShowAction != null) {
						this.BannerAdDidShowAction (placeId, "");
					}
				} 
				else if (function.Equals (Unity_Callback_Message_Function_BannerAd_DidClick)) {
					if (this.BannerAdDidClickAction != null) {
						this.BannerAdDidClickAction (placeId, "");
					}
				} 
					 
				//unkown call
				else {
					Debug.Log ("unkown function:" + function);
				}
			}
		}
	}

}

