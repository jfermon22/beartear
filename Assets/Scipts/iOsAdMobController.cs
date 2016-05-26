using UnityEngine;
using System.Collections;

public class iOsAdMobController : MonoBehaviour {

	public string PublisherId = "pub-6388066413075233";
	public string InterstialUnitId;

	void Awake() {
		Initialize(PublisherId);
	}

	// Sets the publiser Id and prepares AdMob for action.  Must be called before any other methods!
	public void Initialize( string newPublisherId) {
		PublisherId = newPublisherId;
		AdMobBinding.init(PublisherId);
	}

	// Sets the publiser Id and prepares AdMob for action.  Must be called before any other methods!
	public void Initialize( string newPublisherId, bool isTesting) {
		PublisherId = newPublisherId;
		AdMobBinding.init(PublisherId,isTesting);
	}

	// Adds a test device
	public void SetTestDevices(string[] deviceIds){
		AdMobBinding.setTestDevices( deviceIds );
	}

	// Creates a banner of the given type at the given position
	public void createBanner(AdMobBannerType bannerType, AdMobAdPosition position ){
		AdMobBinding.createBanner( bannerType, position );
	}

	// Destroys the banner and removes it from view
	public void DestroyBanner(){
		AdMobBinding.destroyBanner();
	}

	// Starts loading an interstitial ad

	public void RequestInterstital(){
		if (InterstialUnitId.Length != 0)
			AdMobBinding.requestInterstital(InterstialUnitId);
		else 
			Debug.Log ("iOsAdMobController::RequestInterstital - InterstialUnitId is empty");
	}

	public void RequestInterstital(string interstitialUnitId){
		AdMobBinding.requestInterstital(interstitialUnitId);
	}

	// Checks to see if the interstitial ad is loaded and ready to show
	public bool IsInterstitialAdReady(){
		return AdMobBinding.isInterstitialAdReady();
	}

	// If an interstitial ad is loaded this will take over the screen and show the ad
	public void DisplayInterstital(){
		AdMobBinding.displayInterstital();
	}

}
