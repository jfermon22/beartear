using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using Prime31;

public class GameCenterJF : MonoBehaviour {

	public bool ShouldShowBannerOnAchievementReached = false;
	string PlayerIdentifier;
	string PlayerAlias;

	//public enum GameCenterLeaderboardTimeScope
	//{
	//	Today = 0,
	//	Week,
	//	AllTime
	//};
	
	//public enum GameCenterViewControllerState
	//{
	//	Default = -1,
	//	Leaderboards,
	//	Achievements,
	//	Challenges
	//}
	
	void Awake () {
		AuthenticatePlayer();
	}

	// Use this for initialization
	void Start () {
		if (ShouldShowBannerOnAchievementReached){
			ShowCompletionBannerForAchevements();
		}
	}
	// Authenticates the player.  This needs to be called before using anything in GameCenter and should
	// preferalbly be called shortly after application launch.
	public void AuthenticatePlayer(){
		//Debug.Log ("GameCenter::AuthenticatePlayer");
		if (! IsPlayerAuthenticated()){
			GameCenterBinding.authenticateLocalPlayer();
		}
	}

		
	// Checks to see if the current player is authenticated.
	public bool IsPlayerAuthenticated(){
		//Debug.Log ("GameCenter::IsPlayerAuthenticated");
		return GameCenterBinding.isPlayerAuthenticated();
	}
			
	// Gets the alias of the current player.
	public string GetPlayerAlias(){
		//Debug.Log ("GameCenter::GetAlias");
		PlayerAlias = GameCenterBinding.playerAlias();
		return PlayerAlias;
	}
			
	// Gets the playerIdentifier of the current player.
	public string GetPlayerIdentifier(){
		//Debug.Log ("GameCenter::getIdentifier");
		PlayerIdentifier = GameCenterBinding.playerIdentifier();
		return PlayerIdentifier;
	}
			
	// Checks to see if the current player is underage.
	public bool PlayerIsUnderage(){
		//Debug.Log ("GameCenter::isPlayerUnderage");
		return GameCenterBinding.isUnderage();
	}
			
	// Sends off a request to get the current users friend list and optionally loads profile images asynchronously
	public void LoadFriends(bool shouldLoadProfileImages, bool shouldLoadLargeProfileImages){
		GameCenterBinding.retrieveFriends( shouldLoadProfileImages, shouldLoadLargeProfileImages );
	}
			
	// Gets GameCenterPlayer objects for all the given playerIds and optionally loads the profile images asynchronously
	public void LoadPlayerData(string[] playerIdArray, bool shouldLoadProfileImages, bool shouldLoadLargeProfileImages = true){
		GameCenterBinding.loadPlayerData(playerIdArray, shouldLoadProfileImages, shouldLoadLargeProfileImages);
	}
			
	// Starts the loading of the profile image for the currently logged in player
	public void LoadPlayerProfilePic(){
		GameCenterBinding.loadProfilePhotoForLocalPlayer();
	}

	// iOS 6+ only! Shows a specific Game Center view controller. timeScope and leaderboardId are only valid for GameCenterViewControllerState.Leaderboards
	public void ShowGameCenterViewController( GameCenterViewControllerState viewState )
	{
		GameCenterBinding.showGameCenterViewController(viewState );
	}
	
	public  void ShowGameCenterViewController( GameCenterViewControllerState viewState, GameCenterLeaderboardTimeScope timeScope, string leaderboardId )
	{
		GameCenterBinding.showGameCenterViewController(viewState,timeScope, leaderboardId );
	}
	
	// Sends off a request to get all the currently live leaderboards including leaderboardId and title.
	public void LoadLeaderboardTitles(){
		GameCenterBinding.loadLeaderboardTitles();
	}
			
	// Reports a score for the given leaderboardId.
	public void ReportScore(System.Int64 score, string leaderboardId ){
		GameCenterBinding.reportScore(score, leaderboardId );
	}
			
	//public void ReportScore(System.Int64 score, System.UInt64 context, string leaderboardId ){
	public void ReportScore(System.Int64 score, System.UInt64 context, string leaderboardId){
		GameCenterBinding.reportScore(score,context,leaderboardId );
	}
			
	// Shows the standard GameCenter leaderboard with the given time scope.
	public void ShowLeaderBoardWithTimeScope(GameCenterLeaderboardTimeScope timeScope){
		GameCenterBinding.showLeaderboardWithTimeScope( timeScope );
	}
			
	// Shows the standard GameCenter leaderboard for the given leaderboardId with the given time scope.
	public void ShowLeaderBoardWithTimeScope(GameCenterLeaderboardTimeScope timeScope,string leaderboardId ){
		GameCenterBinding.showLeaderboardWithTimeScopeAndLeaderboard( timeScope, leaderboardId );
	}
			
	// Sends a request to get the current scores with the given criteria. End MUST be between 1 and 100 inclusive.
	public void LoadScores(bool shouldOnlyLoadFriends, GameCenterLeaderboardTimeScope timeScope, int start, int end){
		if (end < 1 || end > 100){
			Debug.Log("GameCenter::LoadScores - Variable 'end' not within bounds  1 < end < 100 : "+ end);
			Debug.Log("GameCenter::LoadScores - Failed to load scores");
			return;
		}
		GameCenterBinding.retrieveScores( shouldOnlyLoadFriends, timeScope, start, end );
	}
			
	// Sends a request to get the current scores with the given criteria.  End MUST be between 1 and 100 inclusive.
	public void LoadScores(bool shouldOnlyLoadFriends, GameCenterLeaderboardTimeScope timeScope, int start, int end, string leaderboardId ){
		if (end < 1 || end > 100){
			Debug.Log("GameCenter::LoadScores - Variable 'end' not within bounds  1 < end < 100 : "+ end);
			Debug.Log("GameCenter::LoadScores - Failed to load scores");
			return;
		}
		GameCenterBinding.retrieveScores(shouldOnlyLoadFriends,timeScope,start,end,leaderboardId );
	}
			
	// Sends a request to get the current scores for the given playerId. scoresForPlayerIdLoaded/retrieveScoresForPlayerIdFailed will fire with the results.
	public void RetrieveScoresForPlayerId(string playerId ){
		GameCenterBinding.retrieveScoresForPlayerId( playerId );
	}
	
	// Sends a request to get the current scores for the given playerId and leaderboardId. retrieveScoresForPlayerIdLoaded/retrieveScoresForPlayerIdFailed will fire with the results.
	public void RetrieveScoresForPlayerId(string playerId, string leaderboardId  ){
		GameCenterBinding.retrieveScoresForPlayerId( playerId,leaderboardId );
	}
			
	// Reports an achievement with the given identifier and percent complete
	public void ReportAchievement(string identifier, float percent){
		GameCenterBinding.reportAchievement(identifier,percent );
	}
			
	// Sends a request to get a list of all the current achievements for the authenticated player.
	public void LoadPlayerAchievements(){
		GameCenterBinding.getAchievements();
	}
			
	// Resets all the achievements for the authenticated player.
	public void ResetAchievements(){
		GameCenterBinding.resetAchievements();
	}
			
	// Shows the standard, GameCenter achievement list
	public void ShowAchievements(){
		GameCenterBinding.showAchievements();
	}
			
	// Sends a request to get the achievements for the current game.
	public void LoadAchievementListForGame(){
		GameCenterBinding.retrieveAchievementMetadata();
	}
			
	// Shows a completion banner for achievements if when reported they are at 100%.  Only has an effect on iOS 5+
	public void ShowCompletionBannerForAchevements(){
		GameCenterBinding.showCompletionBannerForAchievements();
	}
			
	// Configures when challenge banners will be shown
	public void ConfigureChallengeBanners(bool showBannerForLocallyCompletedChallenge, bool showBannerForLocallyReceivedChallenge, bool showBannerForRemotelyCompletedChallenge){
		GameCenterBinding.configureChallengeBanners(showBannerForLocallyCompletedChallenge,showBannerForLocallyReceivedChallenge, showBannerForRemotelyCompletedChallenge );
	}
			
	// Sends a request to load all received challenges. iOS 6+ only@
	public void LoadReceivedChallenges(){
		GameCenterBinding.loadReceivedChallenges();
	}
			
	// iOS 6+ only! Issues a score challenge to the given players for the leaderboard
	public void IssueScoreChallenge( System.Int64 score, System.Int64 context, string leaderboardId, string[] playerIds, string message ){
		GameCenterBinding.issueScoreChallenge(score, context, leaderboardId, playerIds,message);
	}
		
	// iOS 6+ only! Checks the given playerIds to see if any are eligible for the achievement challenge
	public void SelectChallegeablePlayerIdsForAchievement(string identifier, string[] playerIds){
		GameCenterBinding.selectChallengeablePlayerIDsForAchievement( identifier, playerIds);
	}
			
	// iOS 6+ only! Issues an achievement challenge to the players for the given identifier
	public void IssueAchievementChallenge(string identifier, string[] playerIds, string message ){
		GameCenterBinding.issueAchievementChallenge(identifier,playerIds, message );
	}

	//MY METHODS
	public void ShowGameCenter(){
		ShowGameCenterViewController( GameCenterViewControllerState.Default );
	}

}
