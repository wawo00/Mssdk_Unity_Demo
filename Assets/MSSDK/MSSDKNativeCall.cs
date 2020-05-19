
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Polymer
{
	public class MSSDKNativeCall
	{
#if UNITY_IOS && !UNITY_EDITOR

			// Init
			[DllImport("__Internal")]
			private static extern void initIosSDK(string callbackClassName, string callbackFunctionName);

			// Reward
			[DllImport("__Internal")]
			private static extern bool isRewardReady();
			[DllImport("__Internal")]
			private static extern void showReward(string cpAdUnitID);

			// Interstitial
			[DllImport("__Internal")]
			private static extern bool isInterstitialReady(string cpAdUnitID);
			[DllImport("__Internal")]
			private static extern void showInterstitial(string cpAdUnitID);
			
			// Banner
			[DllImport("__Internal")]
			private static extern void showBanner(string cpAdUnitID, double x, double y, double width, double height);
			[DllImport("__Internal")]
			private static extern void removeBanner(string cpAdUnitID);
			
			// Debug
			[DllImport("__Internal")]
			private static extern void openLog(bool open);  
			
			// Privacy
			[DllImport("__Internal")]
			private static extern void grantConsent();  
			[DllImport("__Internal")]
			private static extern void revokeConsent();  
		
#elif UNITY_ANDROID && !UNITY_EDITOR
			private static AndroidJavaClass jc = null;
			private readonly static string JavaClassName = "com.ms.sdk.unity.MsPolyProxy";
			private readonly static string JavaClassStaticMethod_InitSDK = "initSDK";
			private readonly static string JavaClassStaticMethod_ShowTopBanner = "showTopBanner";
			private readonly static string JavaClassStaticMethod_ShowBottomBanner = "showBottomBanner";
			private readonly static string JavaClassStaticMethod_RemoveBanner = "removeBanner";
			private readonly static string JavaClassStaticMethod_ShowInterstitial = "showInterstitial";
			private readonly static string JavaClassStaticMethod_ShowRewardVideo = "showRewardVideo";
			private readonly static string JavaClassStaticMethod_IsInterstitialReady = "isInterstitialReady";
			private readonly static string JavaClassStaticMethod_IsRewardReady = "isRewardReady";
			private readonly static string JavaClassStaticMethod_SetInterstitialCallbackAt = "setInterstitialCallbackAt";
			private readonly static string JavaClassStaticMethod_SetRewardVideoLoadCallback = "setRewardVideoLoadCallback";
			private readonly static string JavaClassStaticMethod_HideTopBanner = "hideTopBanner";
			private readonly static string JavaClassStaticMethod_HideBottomBanner = "hideBottomBanner";
			private readonly static string JavaClassStaticMethod_updateAccessPrivacyInfoStatus = "updateAccessPrivacyInfoStatus";
			private readonly static string JavaClassStaticMethod_getAccessPrivacyInfoStatus = "getAccessPrivacyInfoStatus";
			private readonly static string JavaClassStaticMethod_notifyAccessPrivacyInfoStatus = "notifyAccessPrivacyInfoStatus";
			
			private readonly static string JavaClassStaticMethod_ReportRDShowDid = "reportRDShowDid";
			private readonly static string JavaClassStaticMethod_ReportRDRewardGiven = "reportRDRewardGiven";
			private readonly static string JavaClassStaticMethod_ReportRDRewardCancel = "reportRDRewardCancel";
			private readonly static string JavaClassStaticMethod_ReportRDRewardClick = "reportRDRewardClick";
			private readonly static string JavaClassStaticMethod_ReportRDRewardClose = "reportRDRewardClose";

			private readonly static string JavaClassStaticMethod_ReportILShowDid = "reportILShowDid";
			private readonly static string JavaClassStaticMethod_ReportILClick = "reportILClick";
			private readonly static string JavaClassStaticMethod_ReportILClose = "reportILClose";

			private readonly static string JavaClassStaticMethod_IsReportOnlineEnable = "isReportOnlineEnable";
			private readonly static string JavaClassStaticMethod_ReportIvokePluginMethodReceive = "reportIvokePluginMethodReceive";
            private readonly static string JavaClassStaticMethod_GrantConsent = "grantConsent";
            private readonly static string JavaClassStaticMethod_RevokeConsent = "revokeConsent";
        private readonly static string JavaClassStaticMethod_SetDebuggable = "setDebuggable";
        
#else
        // "do nothing";
#endif

        public string getPlatformName ()
		{
			#if UNITY_IOS && !UNITY_EDITOR
			return "UNITY_IOS";
			#elif UNITY_ANDROID && !UNITY_EDITOR
			return "UNITY_ANDROID";
			#else
			return "unkown";
			#endif
		}

		public MSSDKNativeCall ()
		{
			MSSDKNativeCallback.getInstance ().setMSSDKNativeCall (this);
			#if UNITY_IOS && !UNITY_EDITOR
			#elif UNITY_ANDROID && !UNITY_EDITOR
			 if (jc == null) {
			 	Debug.Log ("===> MSSDKNativeCall instanced");
			 	jc = new AndroidJavaClass (JavaClassName);
			 }
			#endif
		}

		// ***************  Init  ***************

		public void initSDK (Action<string> completed)
		{
#if UNITY_IOS && !UNITY_EDITOR

				MSSDKNativeCallback.getInstance ().setInitSDKCallback(completed);
				initIosSDK(MSSDKNativeCallback.Unity_Callback_Class_Name, MSSDKNativeCallback.Unity_Callback_Function_Name);

#elif UNITY_ANDROID && !UNITY_EDITOR
                //Debug
                Debug.Log ("===> init android call:" + MSSDKNativeCallback.Unity_Callback_Class_Name);
            	Debug.Log ("===> init android fun:" + MSSDKNativeCallback.Unity_Callback_Function_Name);
				if (jc == null) {
					//Debug.Log (JavaClassName);
					jc = new AndroidJavaClass (JavaClassName);
				}
				string result = jc.CallStatic<string> (JavaClassStaticMethod_InitSDK, 
													MSSDKNativeCallback.Unity_Callback_Class_Name, MSSDKNativeCallback.Unity_Callback_Function_Name);
#else
#endif
        }

        // ***************  Reward  ***************

        public bool isRewardAdReady () {
			#if UNITY_IOS && !UNITY_EDITOR
				return isRewardReady();
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) {
				return jc.CallStatic<bool> (JavaClassStaticMethod_IsRewardReady);
			}
            return false;
			#else
			return false;
			#endif
		}

		public void setRewardCallback (Action<string, string> didOpen, Action<string, string> didClick, Action<string, string> didClose, Action<string, string> didReward) {
			MSSDKNativeCallback.getInstance ().setRewardCallback (didOpen, didClick, didClose, didReward);
		}

		public void showRewardAd (string cpAdUnitID) {
			if (cpAdUnitID == null) {
				Debug.Log ("===> call showRewardAd(), the param cpAdUnitID be null. ");
				cpAdUnitID = "reward_vedio";
			}
			#if UNITY_IOS && !UNITY_EDITOR
			showReward(cpAdUnitID);
			#elif UNITY_ANDROID && !UNITY_EDITOR
            if (jc != null) {
				jc.CallStatic (JavaClassStaticMethod_ShowRewardVideo, cpAdUnitID);
			}
			#endif
		}

		// ***************  Interstitial  ***************

		public bool isInterstitialAdReady (string cpAdUnitID) {
			if (cpAdUnitID == null) {
				Debug.Log ("===> call isInterstitialAdReady(), the param cpAdUnitID can't be null. ");
				return false;
			}
			#if UNITY_IOS && !UNITY_EDITOR
				return isInterstitialReady(cpAdUnitID);
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) {
				return jc.CallStatic<bool> (JavaClassStaticMethod_IsInterstitialReady, cpAdUnitID);
			}
			return false;
			#else
			return false;
			#endif

		}

		public void setInterstitialCallback (Action<string, string> didOpen, Action<string, string> didClick, Action<string, string> didClose) {
			MSSDKNativeCallback.getInstance ().setInterstitialCallback (didOpen, didClick, didClose);
		}

		public void showInterstitialAd (string cpAdUnitID) {
			if (cpAdUnitID == null) {
				Debug.Log ("===> call isInterstitialAdReady(), the param cpAdUnitID can't be null. ");
				return;
			}
			#if UNITY_IOS && !UNITY_EDITOR
				showInterstitial(cpAdUnitID);
			#elif UNITY_ANDROID && !UNITY_EDITOR
            if (jc != null) {
				jc.CallStatic (JavaClassStaticMethod_ShowInterstitial, cpAdUnitID);
			}
			#endif
		}

		// ***************  Banner  ***************

		public void setBannerCallback (Action<string, string> didShow, Action<string, string> didClick) {
			MSSDKNativeCallback.getInstance ().setBannerCallback (didShow, didClick);
		}

		public void showBannerAd (string cpAdUnitID, double x, double y, double width, double height) {
			if (cpAdUnitID == null) {
				Debug.Log ("===> call showBannerAd(), the param cpAdUnitID can't be null. ");
				return;
			}
			#if UNITY_IOS && !UNITY_EDITOR
			showBanner(cpAdUnitID, x, y, width, height);
			#elif UNITY_ANDROID && !UNITY_EDITOR
			#endif
		}

        public void showAndroidBannerAdAtBottom(string cpPlaceId)
        {
            if (cpPlaceId == null)
            {
                Debug.Log("===> call showBannerAdAtBottom(), the param cpPlaceId can't be null. ");
                return;
            }
        #if UNITY_IOS && !UNITY_EDITOR
			//showBannerBottom (cpPlaceId);
        #elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) {
				jc.CallStatic (JavaClassStaticMethod_ShowBottomBanner, cpPlaceId);
			}
        #endif
        }

        public void showAndroidBannerAdAtTop(string cpPlaceId)
        {
            if (cpPlaceId == null)
            {
                Debug.Log("===> call showBannerAdAtTop(), the param cpPlaceId can't be null. ");
                return;
            }
        #if UNITY_IOS && !UNITY_EDITOR
			//showBannerTop(cpPlaceId);
        #elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) {
				jc.CallStatic (JavaClassStaticMethod_ShowTopBanner, cpPlaceId);
			}   
        #endif
        }


        public void removeBannerAd (string cpAdUnitID) {
			if (cpAdUnitID == null) {
				Debug.Log ("===> call removeBanner(), the param cpAdUnitID can't be null. ");
				return;
			}

			#if UNITY_IOS && !UNITY_EDITOR
				removeBanner(cpAdUnitID);
			#elif UNITY_ANDROID && !UNITY_EDITOR
            if (jc != null) {
				jc.CallStatic (JavaClassStaticMethod_RemoveBanner, cpAdUnitID);
			}
			#endif
		}

		// ***************  Debug  ***************

		public void openLogNativeCall (bool oepn) {
			#if UNITY_IOS && !UNITY_EDITOR
				openLog(oepn);
			#elif UNITY_ANDROID && !UNITY_EDITOR
            if (jc != null) {
				 jc.CallStatic (JavaClassStaticMethod_SetDebuggable,oepn);
			}
			#endif
		}

		// ***************  Privacy  ***************

		public void grantConsentNativeCall () {
			#if UNITY_IOS && !UNITY_EDITOR
				grantConsent();
			#elif UNITY_ANDROID && !UNITY_EDITOR
            if (jc != null) {
				jc.CallStatic (JavaClassStaticMethod_GrantConsent);
			}
			#endif
		}

		public void revokeConsentNativeCall () {
			#if UNITY_IOS && !UNITY_EDITOR
				revokeConsent();
			#elif UNITY_ANDROID && !UNITY_EDITOR
             if (jc != null) {
				jc.CallStatic (JavaClassStaticMethod_RevokeConsent);
			}
			#endif
		}
	}
}
