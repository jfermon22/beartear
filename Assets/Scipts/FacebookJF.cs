using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Prime31;

public class FacebookJF : MonoBehaviour {
	public string AppStoreAddress;
	public string AppImagePath;


	// Use this for initialization
	void Start () {
		if ( AppStoreAddress == null)
			Debug.Log("FacebookJF::Start - AppStoreAddress is null");

		Initialize();
	}

	// Initializes the Facebook plugin for you application
	public void Initialize (){
		FacebookBinding.init();
	}

	// Gets the url used to launch the application. If no url was used returns string.Empty
	public string GetLaunchUrl(){
		return FacebookBinding.getAppLaunchUrl();
	}

	// Sets the login behavior. Must be called before any login calls! Understand what the login behaviors are and how they work before using this!
	public void GetUsername(FacebookSessionLoginBehavior loginBehavior){
		FacebookBinding.setSessionLoginBehavior(loginBehavior);
	}
	
	// Opens the Facebook single sign on login in Safari, the official Facebook app or uses iOS 6 Accounts if available
	public void Login(){
		FacebookBinding.login();
	}

	// Logs the user out and invalidates the token
	public void Logout(){
		FacebookBinding.logout();
	}
	
	// Enables frictionless requests
	public void EnableFrictionlessRequests(){
		FacebookBinding.enableFrictionlessRequests();
	}
	
	// Checks to see if the current session is valid
	public bool IsLoggedIn(){
		return FacebookBinding.isSessionValid();
	}

	// Allows you to use any available Facebook Graph API method
	public  void UseGraphApiMethod(string graphPath, string httpMethod, Hashtable keyValueHash){
		FacebookBinding.graphRequest(graphPath,httpMethod,keyValueHash);
	}

	// iOS 6 only. Renews the credentials that iOS stores for any native Facebook accounts. You can safely call this at app launch or when logging a user out.
	public void RenewAllAccountsCredentials(){
		FacebookBinding.renewCredentialsForAllFacebookAccounts();
	}

	// Gets the current access token
	public string GetAccessToken(){
		return FacebookBinding.getAccessToken();
	}

	// Gets the permissions granted to the current access token
	public List<object> GetSessionPermissions(){
		return FacebookBinding.getSessionPermissions();
	}
	
	public void LoginWithReadPermissions(string[] permissions){
		FacebookBinding.loginWithReadPermissions(permissions);
	}

	// Shows the native authorization dialog (iOS 6), opens the Facebook single sign on login in Safari or the official Facebook app with the requested read (not publish!) permissions
	public void LoginWithReadPermissions(string[] permissions, string urlSchemeSuffix ){
		FacebookBinding.loginWithReadPermissions(permissions, urlSchemeSuffix );
	}

	// Reauthorizes with the requested read permissions
	public void ReauthorizeWithReadPermissions(string[] permissions){
		FacebookBinding.reauthorizeWithReadPermissions(permissions);
	}

	// Reauthorizes with the requested publish permissions and audience
	public void ReautorizeWithPublishPermissions(string[] permissions, FacebookSessionDefaultAudience defaultAudience){
		FacebookBinding.reauthorizeWithPublishPermissions(permissions,defaultAudience);
	}

	// Full access to any existing or new Facebook dialogs that get added.  See Facebooks documentation for parameters and dialog types
	public void ShowDialog(string dialogType, Dictionary<string,string> options){
		FacebookBinding.showDialog( dialogType, options);
	}

	// Allows you to use any available Facebook REST API method
	public void RestRequest(string restMethod, string httpMethod, Hashtable keyValueHash){
		FacebookBinding.restRequest(restMethod,httpMethod, keyValueHash );
	}

	// Facebook Composer methods
	public bool IsFacebookComposerSupported(){
		return FacebookBinding.isFacebookComposerSupported();
	}

	// Checks to see if the user is using a version of iOS that supports the Facebook composer and if they have an account setup
	public bool CanUserUseFacebookComposer(){
		return FacebookBinding.canUserUseFacebookComposer();
	}

	public void ShowFacebookComposer(string message){
		FacebookBinding.showFacebookComposer(message );
	}

	// Shows the Facebook composer with optional image path and link
	public void ShowFacebookComposer(string message, string imagePath, string link ){
		FacebookBinding.showFacebookComposer( message,imagePath,link );
	}

	//MY METHODS
	
	public void PostNewHighScore(string newHighScore, string nameOfGame , string appStoreAddress = null, string appImagePath = null){
		if ( AppStoreAddress != null  && appStoreAddress == null ){
			appStoreAddress = AppStoreAddress;
		}
		if ( AppImagePath != null  && appImagePath == null ){
			appImagePath = AppImagePath;
		}
		if (IsFacebookComposerSupported() ){
			if (CanUserUseFacebookComposer()){
				if ( appImagePath == null )
					ShowFacebookComposer("I just set a new high score of " + newHighScore + " in " + nameOfGame +"!" + appStoreAddress);
				else 
					ShowFacebookComposer("I just set a new high score of " + newHighScore + " in " + nameOfGame +"!", appImagePath, appStoreAddress );
			} 
		} else {
			EtceteraBinding.showAlertWithTitleMessageAndButtons("Error", "Posting to Facebook not supported in your version of iOS", new string[] {"OK"});
		}
	}
	
	public void PostScore(string newScore, string nameOfGame,string appStoreAddress = null, string appImagePath = null){
		if ( AppStoreAddress != null  && appStoreAddress == null ){
			appStoreAddress = AppStoreAddress;
		}

		if ( AppImagePath != null  && appImagePath == null ){
			appImagePath = AppImagePath;
		}

		if (IsFacebookComposerSupported() ){
			if (CanUserUseFacebookComposer()){
				if ( appImagePath == null )
					ShowFacebookComposer("I just scored " + newScore + " in " + nameOfGame + "! " + appStoreAddress);
				else 
					ShowFacebookComposer("I just scored " + newScore + " in " + nameOfGame + "! ", appImagePath, appStoreAddress);

			} 
		} else {
			EtceteraBinding.showAlertWithTitleMessageAndButtons("Error", "Posting to Facebook not supported in your version of iOS", new string[] {"OK"});
		}
	}

}
