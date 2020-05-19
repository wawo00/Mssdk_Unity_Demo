 using System;

namespace Polymer
{
	public class MSSDK
	{
		// 插件版本号
		private readonly static string Version_Of_Ios_In_Plugin 	= "1001.1";
		private readonly static string Version_Of_Android_In_Plugin = "1002.3";
		private readonly static string Version_Of_Plugin 			= "1001.1";

		private static bool _sInited;
		private static MSSDKNativeCall nativeCall = null;

		public static string getVersionOfPlugin () {
			return Version_Of_Plugin;
		}

		/*
		 * 获取插件的版本号
		 * 此版本号一般取android与ios两者最大版本号
		 * 
		 */
		public static string getVersionOfPlatform () {
			#if UNITY_IOS && !UNITY_EDITOR
			return Version_Of_Ios_In_Plugin;
			#elif UNITY_ANDROID && !UNITY_EDITOR
			return Version_Of_Android_In_Plugin;
			#else
			return "undfined";
			#endif
		}

		/*
		 * 初始化聚合广告
		 * 即使多次调用，此方法也仅会初始化一次 
		 */
		public static void initSdk (Action<string> completed) {
			if (_sInited) {
				completed("The sdk has been initialized, please do not repeat the initialization");
				return;
			}
			if (nativeCall == null) {
				nativeCall = new MSSDKNativeCall ();
			}
			_sInited = true;
			nativeCall.initSDK (completed);
		}

		/*
		 * 判断激励视屏广告是否填充成功，此方法可用于检查广告是否可以展示
		 * 返回结果为bool值
		 * 
		 */
		public static bool isRewardReady () {
			if (nativeCall != null) {
				return nativeCall.isRewardAdReady ();
			}
			return false;
		}

		/*
		 * 设置激励视频广告回调
		 * @param didOpen 广告展示的回调方法
		 * @param didClick 广告点击的回调方法
		 * @param didClose 广告关闭的回调方法
		 * @param didReward 广告发放奖励的回调方法 
		 */
		public static void setRewardCallback (Action<string, string> didOpen, Action<string, string> didClick, Action<string, string> didClose, Action<string, string> didReward) {
			if (nativeCall != null) {
				nativeCall.setRewardCallback (didOpen, didClick, didClose, didReward);
			}
		}

		/*
		 * 用于展示激励视屏广告
		 * @param cpAdUnitID 用户自定义广告位，区分收益来源，不能为空，否则广告无法显示
		 */
		public static void showRewardAd (string cpAdUnitID) {
			if (nativeCall != null) {
				nativeCall.showRewardAd (cpAdUnitID);
			}
		}

		/*
		 * 判断插屏广告是否填充成功，此方法可用于检查广告是否可以展示
		 * @param cpAdUnitID: 插屏广告位标识符
		 * 返回结果为bool值
		 * 
		 */
		public static bool isInterstitialReady (string cpAdUnitID) {
			if (nativeCall != null) {
				return nativeCall.isInterstitialAdReady (cpAdUnitID);
			}
			return false;
		}

		/*
		 * 设置插屏广告回调
		 * @param didOpen 广告展示的回调方法
		 * @param didClick 广告点击的回调方法
		 * @param didClose 广告关闭的回调方法
		 */
		public static void setInterstitialCallback (Action<string, string> didOpen, Action<string, string> didClick, Action<string, string> didClose) {
			if (nativeCall != null) {
				nativeCall.setInterstitialCallback (didOpen, didClick, didClose);
			}
		}

		/*
		 * 用于展示插屏广告
		 * @param cpAdUnitID: 插屏广告位标识符
		 * 用于替换showIntersitialAd()
		 */
		public static void showInterstitialAd (string cpAdUnitID) {
			if (nativeCall != null) {
				nativeCall.showInterstitialAd (cpAdUnitID);
			}
		}

		/*
		 * 设置横幅广告回调
		 * @param didShow 广告展示的回调方法
		 * @param didClick 广告点击的回调方法
		 */
		public static void setBannerCallback (Action<string, string> didShow, Action<string, string> didClick) {
			if (nativeCall != null) {
				nativeCall.setBannerCallback (didShow, didClick);
			}
		}

		/*
		 * 用于展示Banner广告
		 * @param cpAdUnitID: 插屏广告位标识符
		 * @param x: 起始位横坐标
		 * @param y: 起始位纵坐标
		 * @param width: 宽度
		 * @param height: 高度
		 * 
		 */
		public static void showBannerAd (string cpAdUnitID, double x, double y, double width, double height) {
			if (nativeCall != null) {
				nativeCall.showBannerAd (cpAdUnitID, x, y, width, height);
			}
		}

        public static void showAndroidBannerAdAtBottom(string cpAdUnitID) {
            if (nativeCall != null) {
                nativeCall.showAndroidBannerAdAtBottom(cpAdUnitID);
            }
        }

        public static void showAndroidBannerAdAtTop(string cpAdUnitID)
        {
            if (nativeCall != null)
            {
                nativeCall.showAndroidBannerAdAtTop(cpAdUnitID);
            }
        }

        /*
		 * 根据广告位，删除Banner广告
		 * @param cpAdUnitID: 插屏广告位标识符
		 */
        public static void removeBannerAd (string cpAdUnitID) {
			if (nativeCall != null) {
				nativeCall.removeBannerAd (cpAdUnitID);
			}
		}

		/*
		 * 开启日志
		 * @param oepn: 是否开启日志 true开启
		 */
		public static void openLog (bool open) {
			if (nativeCall != null) {
				nativeCall.openLogNativeCall (open);
			}
		}

		/*
		 * 同意使用隐私信息
		 */
		public static void grantConsent () {
			if (nativeCall != null) {
				nativeCall.grantConsentNativeCall ();
			}
		}

		/*
		 * 拒绝使用隐私信息
		 */
		public static void revokeConsent () {
			if (nativeCall != null) {
				nativeCall.revokeConsentNativeCall ();
			}
		}
	}
}

