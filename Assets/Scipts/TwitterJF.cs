using UnityEngine;
using System.Collections;
using Prime31;

/****************************************
 * This class is designed to instantiate
 * twitter access in an ios app
 * **************************************/


public class TwitterJF : MonoBehaviour {
	string Username;
	string Password;
	public string ConsumerKey;
	public string ConsumerSecret;
	public string AppStoreAddress;
	
	// Use this for initialization
	void Start () {
		if ( AppStoreAddress == null)
			Debug.Log("TwitterJF::Start - AppStoreAddress is null");

		if (ConsumerKey == null ){
			Debug.Log("TwitterJF::Start -  ConsumerKey is null");
			Debug.Break();
		}

		if (ConsumerSecret == null ){
			Debug.Log("Twitter::Start -  ConsumerSecret is null");
			Debug.Break();
		}

		Initialize();
	}

	// Initializes the Twitter plugin and sets up the required oAuth information
	public void Initialize(string newConsumerKey, string newConsumerSecret )
	{
		ConsumerKey = newConsumerKey;
		ConsumerSecret = newConsumerSecret;
		Initialize();
	}

	public void Initialize()
	{
		TwitterBinding.init( ConsumerKey, ConsumerSecret );
	}

	// Logs in the user using xAuth
	public void Login(string newUsername, string newPassword)
	{
		//Debug.Log("[   DEBUG   ] Twitter::Login");
		Username = newUsername;
		Password = newPassword;
		Login();
	}

	public void Login()
	{
		//Debug.Log("[   DEBUG   ] Twitter::Login");
		if (Username == null || Password == null ){
			Debug.Log("Twitter::Logon - Username or password are null");
			Debug.Break();
		} else {
			TwitterBinding.login(Username, Password);
		}
	}

	public void ShowOauthLoginDialog(){
		TwitterBinding.showOauthLoginDialog();
	}

	// Checks to see if there is a currently logged in user
	public bool IsLoggedOn()
	{
		bool isLoggedIn = TwitterBinding.isLoggedIn();
		Debug.Log( "Twitter is logged in: " + isLoggedIn );
		return isLoggedIn;
	}
	
	// Retuns the currently logged in user's username
	public string GetUsername()
	{
		Username = TwitterBinding.loggedInUsername();
		Debug.Log( "Twitter username: " + Username );
		return Username;
	}

	// Logs out the current user
	public void Logout(){

		Debug.Log( "Logging out user: " + Username);
		TwitterBinding.logout();
	}

	// Posts the status text.  Be sure status text is less than 140 characters!
	public void PostUpdate(string status)
	{
		if (status.Length > 140 ){
			Debug.Log("Error: Update longer than 140 charachters");
			return;
		}
		TwitterBinding.postStatusUpdate(status);

	}
	
	// Receives tweets from the users home timeline
	public void GetHomeTimeline()
	{
		TwitterBinding.getHomeTimeline();
	}

	public bool IsTweetSheetSupported()
	{
		return TwitterBinding.isTweetSheetSupported();
	}

	// Checks to see if a user can tweet (are they logged in with a Twitter account)?
	public bool CanUserTweet()
	{
		return TwitterBinding.canUserTweet();
	}

	public void ShowTweetComposer( string status )
	{
		TwitterBinding.showTweetComposer( status );
	}
	
	public void ShowTweetComposer( string status, string pathToImage )
	{
		TwitterBinding.showTweetComposer( status, pathToImage );
	}
	
	// Shows the tweet composer with the status message and optional image and link
	public void ShowTweetComposer( string status, string pathToImage, string link )
	{
		TwitterBinding.showTweetComposer(status, pathToImage,link );
	}

	//MY METHODS

	public void PostNewHighScore(string newHighScore, string nameOfGame , string appStoreAddress = null){
		if ( AppStoreAddress != null  && appStoreAddress == null ){
			appStoreAddress = AppStoreAddress;
		}
		if (IsTweetSheetSupported() ){
			if (CanUserTweet()){
				ShowTweetComposer("I just set a new high score of " + newHighScore + " in " + nameOfGame +"! " + appStoreAddress);
			} 
		} else {
			EtceteraBinding.showAlertWithTitleMessageAndButtons("Error", "Posting to Twitter not supported in your version of iOS", new string[] {"OK"});
		}
	}

	public void PostScore(string newScore, string nameOfGame,string appStoreAddress = null){
		if ( AppStoreAddress != null  && appStoreAddress == null ){
			appStoreAddress = AppStoreAddress;
		}
		if (IsTweetSheetSupported() ){
			if (CanUserTweet()){
				ShowTweetComposer("I just scored " + newScore + " in " + nameOfGame + "! " + appStoreAddress);
			} 
		} else {
			EtceteraBinding.showAlertWithTitleMessageAndButtons("Error", "Posting to Twitter not supported in your version of iOS", new string[] {"OK"});
		}
	}
	
}
