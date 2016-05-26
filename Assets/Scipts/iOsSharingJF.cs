using UnityEngine;
using System.Collections;

public class iOsSharingJF : MonoBehaviour {
	public string AppStoreAddress;



	// Use this for initialization
	void Start () {
	  if ( AppStoreAddress == null)
			Debug.Log("iOsSharingJF::Start - AppStoreAddress is null");
	}
	
	// Shows the share sheet with the given items. Items can be text, urls or full and proper paths to sharable files
	public void ShareItems( string[] items )
	{
		SharingBinding.shareItems(items);
	}
	
	// Shows the share sheet with the given items with a list of excludedActivityTypes. See Apple's docs for more information on excludedActivityTypes.
	public void shareItems( string[] items, string[] excludedActivityTypes )
	{
		SharingBinding.shareItems(items, excludedActivityTypes);
	}

	//MY METHODS
	
	public void PostNewHighScore(string newHighScore, string nameOfGame , string appStoreAddress = null){
		if ( AppStoreAddress != null  && appStoreAddress == null ){
			appStoreAddress = AppStoreAddress;
		}

		ShareItems(new string[] {"I just set a new high score of " + newHighScore + " in " + nameOfGame +"! " + appStoreAddress});

	}
	
	public void PostScore(string newScore, string nameOfGame,string appStoreAddress = null){
		if ( AppStoreAddress != null  && appStoreAddress == null ){
			appStoreAddress = AppStoreAddress;
		}

		ShareItems(new string[] {"I just scored " + newScore + " in " + nameOfGame + "! " + appStoreAddress});
	 
	}
}
