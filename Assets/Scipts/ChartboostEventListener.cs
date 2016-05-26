using UnityEngine;
using System.Collections;

public class ChartboostEventListener : MonoBehaviour {

	void OnEnable()
	{
		Chartboost.didCacheInterstitialEvent += didCacheInterstitialEvent;
		Chartboost.didFailToCacheInterstitialEvent += didFailToCacheInterstitialEvent;
		Chartboost.didFinishInterstitialEvent += didFinishInterstitialEvent;
		Chartboost.didCacheMoreAppsEvent += didCacheMoreAppsEvent;
		Chartboost.didFailToCacheMoreAppsEvent += didFailToCacheMoreAppsEvent;
		Chartboost.didFinishMoreAppsEvent += didFinishMoreAppsEvent;
	}
	
	
	void OnDisable()
	{
		Chartboost.didCacheInterstitialEvent += didCacheInterstitialEvent;
		Chartboost.didFailToCacheInterstitialEvent += didFailToCacheInterstitialEvent;
		Chartboost.didFinishInterstitialEvent += didFinishInterstitialEvent;
		Chartboost.didCacheMoreAppsEvent += didCacheMoreAppsEvent;
		Chartboost.didFailToCacheMoreAppsEvent += didFailToCacheMoreAppsEvent;
		Chartboost.didFinishMoreAppsEvent += didFinishMoreAppsEvent;
	}

	void didCacheInterstitialEvent( string location )
	{
		Debug.Log( "didCacheInterstitialEvent: " + location );
	}
	
	
	void didFailToCacheInterstitialEvent( string location )
	{
		Debug.Log( "didFailToCacheInterstitialEvent: " + location );
	}
	
	
	void didFinishInterstitialEvent( string reason )
	{
		Debug.Log( "didFinishInterstitialEvent: " + reason );
	}
	
	
	void didCacheMoreAppsEvent()
	{
		Debug.Log( "didCacheMoreAppsEvent" );
	}
	
	
	void didFailToCacheMoreAppsEvent()
	{
		Debug.Log( "didFailToCacheMoreAppsEvent" );
	}
	
	
	void didFinishMoreAppsEvent( string reason )
	{
		Debug.Log( "didFinishMoreAppsEvent: " + reason );
	}

}
