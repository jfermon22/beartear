using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChartboostController : MonoBehaviour {

	public string AndroidAppId;
	public string AndroidAppSignature;
	public string IosAppId;
	public string IosAppSignature;




	void Start(){
		Initialize ();
	}

	// Starts up Chartboost and records an app install
	public void Initialize(string newDroidAppId, string newDroidAppSig,string newIosAppId, string newIosAppSig ){
		AndroidAppId = newDroidAppId;
		AndroidAppSignature = newDroidAppSig;
		IosAppId = newIosAppId;
		IosAppSignature = newIosAppSig;
		Initialize();
	}
	
	public void Initialize(){
		Chartboost.init( AndroidAppId, AndroidAppSignature, IosAppId, IosAppSignature );
	}

	// Caches an interstitial. Location is optional. Pass in null if you do not want to specify the location.
	public void CacheInterstitial(string location = null){
		Chartboost.cacheInterstitial( location );
	}

	// Checks for a cached an interstitial. Location is optional. Pass in null if you do not want to specify the location.
	public bool IsInterstitialCached(string location = null){
		return Chartboost.hasCachedInterstitial( location );
	}

	// Loads an interstitial. Location is optional. Pass in null if you do not want to specify the location.
	public void ShowInterstitial(string location = null){
			Chartboost.showInterstitial( location );
	}

	// Caches the more apps screen
	public void  CacheMoreApps(){
		Chartboost.cacheMoreApps();
	}

	// Shows the more apps screen
	public void ShowMoreApps(){
		Chartboost.showMoreApps();
	}

	// Tracks an event with optional meta data	
	public void TrackEvent( string eventIdentifier, double value, Dictionary<string,object> metaData ){
		Chartboost.trackEvent( eventIdentifier, value, metaData );
	}


}
