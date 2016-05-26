using UnityEngine;
using System.Collections.Generic;
using Prime31;


public class AdMobGUIManager : MonoBehaviourGUI
{
#if UNITY_IPHONE

	void OnGUI()
	{
		beginColumn();


		if( GUILayout.Button( "Initialize AdMob" ) )
		{
			AdMobBinding.setTestDevices( new string[] { "079adeed23ef3e9a9ddf0f10c92b8e18" } ); // iPad mini
			AdMobBinding.init( "YOUR_PUBLISHER_ID", true );
		}


		if( GUILayout.Button( "Destroy Banner" ) )
		{
			AdMobBinding.destroyBanner();
		}


		if( GUILayout.Button( "Portrait Smart Banner (top right)" ) )
		{
			AdMobBinding.createBanner( AdMobBannerType.SmartBannerPortrait, AdMobAdPosition.TopRight );
		}


		if( GUILayout.Button( "Landscape Smart Banner (bottom)" ) )
		{
			AdMobBinding.createBanner( AdMobBannerType.SmartBannerLandscape, AdMobAdPosition.BottomCenter );
		}


		if( UnityEngine.iOS.Device.generation != UnityEngine.iOS.DeviceGeneration.iPad1Gen && UnityEngine.iOS.Device.generation != UnityEngine.iOS.DeviceGeneration.iPad2Gen && UnityEngine.iOS.Device.generation != UnityEngine.iOS.DeviceGeneration.iPad3Gen )
		{
			if( GUILayout.Button( "320x50 Banner (bottom right)" ) )
			{
				AdMobBinding.createBanner( AdMobBannerType.iPhone_320x50, AdMobAdPosition.BottomRight );
			}
		}
		else
		{
			if( GUILayout.Button( "320x250 Banner (bottom)" ) )
			{
				AdMobBinding.createBanner( AdMobBannerType.iPad_320x250, AdMobAdPosition.BottomCenter );
			}


			if( GUILayout.Button( "468x60 Banner (top)" ) )
			{
				AdMobBinding.createBanner( AdMobBannerType.iPad_468x60, AdMobAdPosition.TopCenter );
			}


			if( GUILayout.Button( "728x90 Banner (bottom)" ) )
			{
				AdMobBinding.createBanner( AdMobBannerType.iPad_728x90, AdMobAdPosition.BottomCenter );
			}
		}


		endColumn( true );


		if( GUILayout.Button( "Request Interstitial" ) )
		{
			AdMobBinding.requestInterstital( "a14d3e67dfeb7ba" );
			//AdMobBinding.requestInterstitalAd( "YOUR_INTERSTITIAL_UNIT_ID" );
		}


		if( GUILayout.Button( "Is Interstial Loaded?" ) )
		{
			Debug.Log( "is interstitial loaded: " + AdMobBinding.isInterstitialAdReady() );
		}


		if( GUILayout.Button( "Show Interstitial" ) )
		{
			AdMobBinding.displayInterstital();
		}

		endColumn();
	}
#endif
}
