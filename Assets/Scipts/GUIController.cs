using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Prime31;

public class GUIController : MonoBehaviour {

	public enum CurrentScreen {
		START,
		GAME_OVER,
		GAME_PLAY,
		GAME_PAUSED,
		SPLASH,
	};

	//ObstacleController obstacleController;
	public GameObject ScoreLabelObj;
	public GameObject PauseLabel;
	public GameObject GameScoreLabelObj;
	public GameObject PauseButtonObj;
	public GameObject SoundButtonObj;
	//public GameObject StartButtonObj;
	//public GameObject MainMenuButtonObj;
	public GameObject StartScreen;
	public GameObject GameOverScreen;
	public GameObject GamePlayScreen;
	public GameObject SplashScreen;

	public GameObject TwitterObj;
	public GameObject FacebookObj;
	public GameObject iOsSharingObj;
	public GameObject GameCenterObj;
	public GameObject ChartbooostObj;
	public GameObject AdMobObj;
	public GameObject EtceteraObj;
	public static  bool cBinterstitialIsCached = false;
	public static  bool AMinterstitialIsCached = false;
	public CurrentScreen currentScreen;
	public bool isPaused = false;
	public static bool soundisOn = true;
	static bool isRestarting = false;
	public bool isNewHighScore = false;
	UILabel scoreLabel;
	UILabel gameScoreLabel;
	UIButton pauseButton;
	UIButton soundButton;
	AchievementTrackerScript achievementTracker;
	TwitterJF twitter;
	FacebookJF facebook;
	iOsSharingJF sharing;
	GameCenterJF gameCenter;
	ChartboostController chartboost;
	iOsAdMobController adMob;
//	iOsEtceteraController etcetera;

	// Fired when the more apps screen is cached
	public static event Action<bool> soundWasToggled;


	//public Texture pauseButton;
	//public Texture playButton;

	void OnEnable()
	{
		// Listen to all events for illustration purposes
		ChartboostManager.didFailToCacheInterstitialEvent += didFailToLoadInterstitialEvent;
		ChartboostManager.didFinishInterstitialEvent += didFinishInterstitialEvent;
		ChartboostManager.didCacheInterstitialEvent += didCacheInterstitialEvent;
		/*ChartboostManager.didFailToCacheMoreAppsEvent += didFailToLoadMoreAppsEvent;
		ChartboostManager.didFinishMoreAppsEvent += didFinishMoreAppsEvent;
		ChartboostManager.didCacheMoreAppsEvent += didCacheMoreAppsEvent;*/

		AdMobManager.receivedAdEvent += adViewDidReceiveAdEvent;
		AdMobManager.failedToReceiveAdEvent += adViewFailedToReceiveAdEvent;
		AdMobManager.interstitialReceivedAdEvent += interstitialDidReceiveAdEvent;
		AdMobManager.interstitialFailedToReceiveAdEvent += interstitialFailedToReceiveAdEvent;

		/*EtceteraManager.dismissingViewControllerEvent += dismissingViewControllerEvent;
		EtceteraManager.imagePickerCancelledEvent += imagePickerCancelled;
		EtceteraManager.imagePickerChoseImageEvent += imagePickerChoseImage;
		EtceteraManager.saveImageToPhotoAlbumSucceededEvent += saveImageToPhotoAlbumSucceededEvent;
		EtceteraManager.saveImageToPhotoAlbumFailedEvent += saveImageToPhotoAlbumFailedEvent;
		EtceteraManager.alertButtonClickedEvent += alertButtonClicked;
		EtceteraManager.promptCancelledEvent += promptCancelled;
		EtceteraManager.singleFieldPromptTextEnteredEvent += singleFieldPromptTextEntered;
		EtceteraManager.twoFieldPromptTextEnteredEvent += twoFieldPromptTextEntered;
		EtceteraManager.remoteRegistrationSucceededEvent += remoteRegistrationSucceeded;
		EtceteraManager.remoteRegistrationFailedEvent += remoteRegistrationFailed;
		EtceteraManager.pushIORegistrationCompletedEvent += pushIORegistrationCompletedEvent;
		EtceteraManager.urbanAirshipRegistrationSucceededEvent += urbanAirshipRegistrationSucceeded;
		EtceteraManager.urbanAirshipRegistrationFailedEvent += urbanAirshipRegistrationFailed;
		EtceteraManager.remoteNotificationReceivedEvent += remoteNotificationReceived;
		EtceteraManager.remoteNotificationReceivedAtLaunchEvent += remoteNotificationReceivedAtLaunch;
		EtceteraManager.localNotificationWasReceivedAtLaunchEvent += localNotificationWasReceivedAtLaunchEvent;
		EtceteraManager.localNotificationWasReceivedEvent += localNotificationWasReceivedEvent;
		EtceteraManager.mailComposerFinishedEvent += mailComposerFinished;
		EtceteraManager.smsComposerFinishedEvent += smsComposerFinished;*/

		TwitterManager.loginSucceededEvent += loginSucceeded;
		TwitterManager.loginFailedEvent += loginFailed;
		TwitterManager.postSucceededEvent += postSucceeded;
		TwitterManager.postFailedEvent += postFailed;
		TwitterManager.homeTimelineReceivedEvent += homeTimelineReceived;
		TwitterManager.homeTimelineFailedEvent += homeTimelineFailed;
		TwitterManager.requestDidFinishEvent += requestDidFinishEvent;
		TwitterManager.requestDidFailEvent += requestDidFailEvent;
		TwitterManager.tweetSheetCompletedEvent += tweetSheetCompletedEvent;
	}
	
	
	void OnDisable()
	{
		// Remove all event handlers
		ChartboostManager.didFailToCacheInterstitialEvent -= didFailToLoadInterstitialEvent;
		ChartboostManager.didFinishInterstitialEvent -= didFinishInterstitialEvent;
		ChartboostManager.didCacheInterstitialEvent -= didCacheInterstitialEvent;
		/*ChartboostManager.didFailToCacheMoreAppsEvent -= didFailToLoadMoreAppsEvent;
		ChartboostManager.didFinishMoreAppsEvent -= didFinishMoreAppsEvent;
		ChartboostManager.didCacheMoreAppsEvent -= didCacheMoreAppsEvent;*/

		AdMobManager.receivedAdEvent -= adViewDidReceiveAdEvent;
		AdMobManager.failedToReceiveAdEvent -= adViewFailedToReceiveAdEvent;
		AdMobManager.interstitialReceivedAdEvent -= interstitialDidReceiveAdEvent;
		AdMobManager.interstitialFailedToReceiveAdEvent -= interstitialFailedToReceiveAdEvent;

		/*EtceteraManager.dismissingViewControllerEvent += dismissingViewControllerEvent;
		EtceteraManager.imagePickerCancelledEvent -= imagePickerCancelled;
		EtceteraManager.imagePickerChoseImageEvent -= imagePickerChoseImage;
		EtceteraManager.saveImageToPhotoAlbumSucceededEvent -= saveImageToPhotoAlbumSucceededEvent;
		EtceteraManager.saveImageToPhotoAlbumFailedEvent -= saveImageToPhotoAlbumFailedEvent;
		EtceteraManager.alertButtonClickedEvent -= alertButtonClicked;
		EtceteraManager.promptCancelledEvent -= promptCancelled;
		EtceteraManager.singleFieldPromptTextEnteredEvent -= singleFieldPromptTextEntered;
		EtceteraManager.twoFieldPromptTextEnteredEvent -= twoFieldPromptTextEntered;
		EtceteraManager.remoteRegistrationSucceededEvent -= remoteRegistrationSucceeded;
		EtceteraManager.remoteRegistrationFailedEvent -= remoteRegistrationFailed;
		EtceteraManager.pushIORegistrationCompletedEvent -= pushIORegistrationCompletedEvent;
		EtceteraManager.urbanAirshipRegistrationSucceededEvent -= urbanAirshipRegistrationSucceeded;
		EtceteraManager.urbanAirshipRegistrationFailedEvent -= urbanAirshipRegistrationFailed;
		EtceteraManager.remoteNotificationReceivedAtLaunchEvent -= remoteNotificationReceivedAtLaunch;
		EtceteraManager.localNotificationWasReceivedAtLaunchEvent -= localNotificationWasReceivedAtLaunchEvent;
		EtceteraManager.localNotificationWasReceivedEvent -= localNotificationWasReceivedEvent;
		EtceteraManager.mailComposerFinishedEvent -= mailComposerFinished;
		EtceteraManager.smsComposerFinishedEvent -= smsComposerFinished;*/

		TwitterManager.loginSucceededEvent -= loginSucceeded;
		TwitterManager.loginFailedEvent -= loginFailed;
		TwitterManager.postSucceededEvent -= postSucceeded;
		TwitterManager.postFailedEvent -= postFailed;
		TwitterManager.homeTimelineReceivedEvent -= homeTimelineReceived;
		TwitterManager.homeTimelineFailedEvent -= homeTimelineFailed;
		TwitterManager.requestDidFinishEvent -= requestDidFinishEvent;
		TwitterManager.requestDidFailEvent -= requestDidFailEvent;
		TwitterManager.tweetSheetCompletedEvent -= tweetSheetCompletedEvent;
	}

	void Start () {
		scoreLabel =  (UILabel)ScoreLabelObj.GetComponent(typeof(UILabel));
		gameScoreLabel =  (UILabel)GameScoreLabelObj.GetComponent(typeof(UILabel));
		pauseButton = (UIButton)PauseButtonObj.GetComponent(typeof(UIButton));
		soundButton = (UIButton)SoundButtonObj.GetComponent(typeof(UIButton));
		twitter = (TwitterJF)TwitterObj.GetComponent(typeof(TwitterJF));
		facebook = (FacebookJF)FacebookObj.GetComponent(typeof(FacebookJF));
		sharing = (iOsSharingJF)iOsSharingObj.GetComponent(typeof(iOsSharingJF));
	    gameCenter = (GameCenterJF)GameCenterObj.GetComponent(typeof(GameCenterJF));
		chartboost = (ChartboostController)ChartbooostObj.GetComponent(typeof(ChartboostController));
		adMob = (iOsAdMobController)AdMobObj.GetComponent(typeof(iOsAdMobController));
		achievementTracker = (AchievementTrackerScript)gameObject.GetComponent(typeof(AchievementTrackerScript));
		//etcetera = (iOsEtceteraController)EtceteraObj.GetComponent(typeof(iOsEtceteraController));
		isNewHighScore = false;
		UpdateScore(0);
		SetSoundButton();
		//OpenGameSequence();
		//if(!isRestarting){
		//	OpenGameSequence();
		//} else {
			ShowGamePlayScreen();
		//}
	}

	public void OpenGameSequence(){
		ShowSplashScreen();
	}

	public void  ShowSplashScreen (){
		currentScreen = CurrentScreen.SPLASH;
		SplashScreen.SetActive(true);
		GamePlayScreen.SetActive(false);
		GameOverScreen.SetActive(false);
		StartScreen.SetActive(false);

		Time.timeScale = 0;

	}

	public void ShowGameLogoScreen(){
		SplashScreen.SetActive(false);
		GamePlayScreen.SetActive(false);
		GameOverScreen.SetActive(false);
		StartScreen.SetActive(false);
		Time.timeScale = 0;
	}

	public void OpenGameScreen(){
		currentScreen = CurrentScreen.START;		
		SplashScreen.SetActive(false);
		GamePlayScreen.SetActive(false);
		GameOverScreen.SetActive(false);
		StartScreen.SetActive(true);

		Time.timeScale = 0;
	}

	public void ShowGamePlayScreen(){
		currentScreen = CurrentScreen.GAME_PLAY;
		if (!cBinterstitialIsCached)
			chartboost.CacheInterstitial();
		SplashScreen.SetActive(false);
		GamePlayScreen.SetActive(true);
		GameOverScreen.SetActive(false);
		StartScreen.SetActive(false);
		PauseLabel.SetActive(false);

		Time.timeScale = 1;
	}

	public void ShowGameOverScreen(){
		achievementTracker.UpdateGameCenter();
		currentScreen = CurrentScreen.GAME_OVER;
		//Debug.Log("Gameoverscreen called");
		if (cBinterstitialIsCached||AMinterstitialIsCached){
			if ( cBinterstitialIsCached )
				chartboost.ShowInterstitial();
			else if ( AMinterstitialIsCached )
				adMob.DisplayInterstital();

			    
			cBinterstitialIsCached = false;
			AMinterstitialIsCached = false;
		}
		else 
		{
			Debug.Log("Interstitial was NOT cached");
		}

		SplashScreen.SetActive(false);
		GameOverScreen.SetActive(true);
		StartScreen.SetActive(false);
		GamePlayScreen.SetActive(false);
		if (isNewHighScore)
			gameScoreLabel.text = ("New High Score: "+ scoreLabel.text);
		else 
			gameScoreLabel.text = ("Score: "+ scoreLabel.text);

		Time.timeScale = 0;
	}


	public void PauseButtonClicked(){
		isPaused = !isPaused;
		if (isPaused){
			currentScreen = CurrentScreen.GAME_PAUSED;
			PauseLabel.SetActive(true);
			pauseButton.normalSprite = "PauseButton-03";
			Time.timeScale = 0;
		}
		else 
		{
			currentScreen = CurrentScreen.GAME_PLAY;
			PauseLabel.SetActive(false);
			pauseButton.normalSprite = "PauseButton-01";
			Time.timeScale = 1;
		}
		
		return;
	}

	public void SoundButtonClicked(){
		soundisOn = !soundisOn;
		soundWasToggled(soundisOn);
		SetSoundButton();
		return;
	}

	public void SetSoundButton(){
		if (soundisOn)
			soundButton.normalSprite = "SoundButtons-05";
		else
			soundButton.normalSprite = "SoundButtons-06";
	}
	
	public void UpdateScore( int newScore ){
		scoreLabel.text = newScore.ToString ();
	}

	public void ShowMainMenu(){

	}

	public void ShowGameCenter(){
		//Debug.Log("Showing Game center");
		gameCenter.ShowGameCenter();
	}

	public void TwitterButtonClicked(){
		//Debug.Log("Twitter Button clicked");
		if (isNewHighScore){
			twitter.PostNewHighScore(scoreLabel.text,"BearTear");
		} else {
			twitter.PostScore(scoreLabel.text,"BearTear");
		}

	}

	public void FacebookButtonClicked(){
		//Debug.Log("Facebook Button clicked");
		if (isNewHighScore){
			facebook.PostNewHighScore(scoreLabel.text,"BearTear");
		} else {
			facebook.PostScore(scoreLabel.text,"BearTear");
		}
		
	}

	public void SharingButtonClicked(){
		//Debug.Log("Sharing Button clicked");
		if (isNewHighScore){
			sharing.PostNewHighScore(scoreLabel.text,"BearTear");
		} else {
			sharing.PostScore(scoreLabel.text,"BearTear");
		}
		
	}

	public void ResetLevel(){
		isRestarting = true;
		Application.LoadLevel (Application.loadedLevelName);
	}


	//CHARTBOOST LISTENER EVENTS
	void didFailToLoadInterstitialEvent( string location )
	{
		cBinterstitialIsCached = false;
		//Debug.Log( "didFailToLoadInterstitialEvent: " + location );
		if(!AMinterstitialIsCached)
			adMob.RequestInterstital();
	}
	
	
	void didFinishInterstitialEvent( string reason )
	{
		//Debug.Log( "didFinishInterstitialEvent: " + reason );
	}
	

	void didCacheInterstitialEvent( string location )
	{
		cBinterstitialIsCached = true;
		//Debug.Log( "didCacheInterstitialEvent: " + location );
	}
	
	/*
	void didFailToLoadMoreAppsEvent()
	{
		Debug.Log( "didFailToLoadMoreAppsEvent" );
	}
	
	
	void didFinishMoreAppsEvent( string param )
	{
		Debug.Log( "didFinishMoreAppsEvent: " + param );
	}
	
	
	void didCacheMoreAppsEvent()
	{
		Debug.Log( "didCacheMoreAppsEvent" );
	}
*/
	//ETCETERA LISTENERS
	/*
	void dismissingViewControllerEvent()
	{
		Debug.Log( "dismissingViewControllerEvent" );
	}
	
	
	void imagePickerCancelled()
	{
		Debug.Log( "imagePickerCancelled" );
	}
	
	
	void imagePickerChoseImage( string imagePath )
	{
		Debug.Log( "image picker chose image: " + imagePath );
	}
	
	
	void saveImageToPhotoAlbumSucceededEvent()
	{
		Debug.Log( "saveImageToPhotoAlbumSucceededEvent" );
	}
	
	
	void saveImageToPhotoAlbumFailedEvent( string error )
	{
		Debug.Log( "saveImageToPhotoAlbumFailedEvent: " + error );
	}
	
	
	void alertButtonClicked( string text )
	{
		Debug.Log( "alert button clicked: " + text );
	}
	
	
	void promptCancelled()
	{
		Debug.Log( "promptCancelled" );
	}
	
	
	void singleFieldPromptTextEntered( string text )
	{
		Debug.Log( "field : " + text );
	}
	
	
	void twoFieldPromptTextEntered( string textOne, string textTwo )
	{
		Debug.Log( "field one: " + textOne + ", field two: " + textTwo );
	}
	
	
	void remoteRegistrationSucceeded( string deviceToken )
	{
		Debug.Log( "remoteRegistrationSucceeded with deviceToken: " + deviceToken );
	}
	
	
	void remoteRegistrationFailed( string error )
	{
		Debug.Log( "remoteRegistrationFailed : " + error );
	}
	
	
	void pushIORegistrationCompletedEvent( string error )
	{
		if( error != null )
			Debug.Log( "pushIORegistrationCompletedEvent failed with error: " + error );
		else
			Debug.Log( "pushIORegistrationCompletedEvent successful" );
	}
	
	
	void urbanAirshipRegistrationSucceeded()
	{
		Debug.Log( "urbanAirshipRegistrationSucceeded" );
	}
	
	
	void urbanAirshipRegistrationFailed( string error )
	{
		Debug.Log( "urbanAirshipRegistrationFailed : " + error );
	}
	
	
	void remoteNotificationReceived( IDictionary notification )
	{
		Debug.Log( "remoteNotificationReceived" );
		Prime31.Utils.logObject( notification );
	}
	
	
	void remoteNotificationReceivedAtLaunch( IDictionary notification )
	{
		Debug.Log( "remoteNotificationReceivedAtLaunch" );
		Prime31.Utils.logObject( notification );
	}
	
	
	void localNotificationWasReceivedEvent( IDictionary notification )
	{
		Debug.Log( "localNotificationWasReceivedEvent" );
		Prime31.Utils.logObject( notification );
	}
	
	
	void localNotificationWasReceivedAtLaunchEvent( IDictionary notification )
	{
		Debug.Log( "localNotificationWasReceivedAtLaunchEvent" );
		Prime31.Utils.logObject( notification );
	}
	
	
	void mailComposerFinished( string result )
	{
		Debug.Log( "mailComposerFinished : " + result );
	}
	
	
	void smsComposerFinished( string result )
	{
		Debug.Log( "smsComposerFinished : " + result );
	}*/
	/// <summary>
	///  TWITER LISTENER EVENTS
	/// </summary>
	void loginSucceeded()
	{
		Debug.Log( "Successfully logged in to Twitter" );
	}
	
	
	void loginFailed( string error )
	{
		Debug.Log( "Twitter login failed: " + error );
	}
	
	
	void postSucceeded()
	{
		Debug.Log( "Successfully posted to Twitter" );
	}
	
	
	void postFailed( string error )
	{
		Debug.Log( "Twitter post failed: " + error );
	}
	
	
	void homeTimelineFailed( string error )
	{
		Debug.Log( "Twitter HomeTimeline failed: " + error );
	}
	
	
	void homeTimelineReceived( List<object> result )
	{
		Debug.Log( "received home timeline with tweet count: " + result.Count );
		Prime31.Utils.logObject( result );
	}
	
	
	void requestDidFailEvent( string error )
	{
		Debug.Log( "requestDidFailEvent: " + error );
	}
	
	
	void requestDidFinishEvent( object result )
	{
		if( result != null )
		{
			Debug.Log( "requestDidFinishEvent: " + result.GetType().ToString() );
			Prime31.Utils.logObject( result );
		}
		else
			Debug.Log( "twitterRequestDidFinishEvent with no data" );
	}
	
	
	void tweetSheetCompletedEvent( bool didSucceed )
	{
		Debug.Log( "tweetSheetCompletedEvent didSucceed: " + didSucceed );
	}

	void adViewDidReceiveAdEvent()
	{
		Debug.Log( "adViewDidReceiveAdEvent" );
	}
	
	
	void adViewFailedToReceiveAdEvent( string error )
	{
		Debug.Log( "adViewFailedToReceiveAdEvent: " + error );
	}
	
	
	void interstitialDidReceiveAdEvent()
	{
		Debug.Log( "interstitialDidReceiveAdEvent" );
		AMinterstitialIsCached = true;
	}
	
	
	void interstitialFailedToReceiveAdEvent( string error )
	{
		Debug.Log( "interstitialFailedToReceiveAdEvent: " + error );
		AMinterstitialIsCached = false;
	}

}
