using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*class GameCenterAchievement
	{
		public string identifier;
		public bool isHidden;
		public bool completed;
		public DateTime lastReportedDate;
		public float percentComplete;
	}
*/
using System;

public class AchievementTrackerScript : MonoBehaviour {

	public enum Achievement {
		OFF_SCREEN,
		CHOPPED_BUNNIES,
		HURDLED_BUNNIES,
		HURDLED_ROCKS,
		DIST_TRAV_IN_SINGLE_GAME,
		TOTAL_DIST_TRAVELED,
	}

	public enum Leaderboard {
		HIGH_SCORE,
		DIST_TRAV_IN_SINGLE_GAME,
		TOTAL_DIST_TRAVELED,
	}

	GUIController guiController;
	public GameCenterJF gameCenter;

	public int bunniesChoppedGoal = 10;
	public int rocksHurdledGoal = 10;
	public int bunniesHurdledGoal = 10;
	public int distTraveledSingleGameGoal = 1000;
	public int totalDistanceTraveledGoal = 10000;
	
	//achievement consts
	public const string  DIST_TRAV_IN_SINGLE_GAME = "beartear_distTaveled_singleGame";
	public const string  TOTAL_DIST_TRAVELED = "beartear_distTaveled_total";
	//public const string  HIGH_SCORE = "HighScore";
	public const string  OFF_SCREEN = "beartear_jumpedoffscreen";
	public const string  HURDLED_ROCKS = "beartear_hurdledroicks";
	public const string  HURDLED_BUNNIES = "beartear_hurdledbunnies";
	public const string  CHOPPED_BUNNIES = "beartear_choppedbunnies";
	//leaderboard consts
	public const string  DIST_TRAV_IN_SINGLE_GAME_LDRBRD = "beartear_distTraveled_singleGame_leaderboard";
	public const string  TOTAL_DIST_TRAVELED_LDRBRD = "beartear_distTraveled_total_leaderboard";
	public const string  HIGH_SCORES_LDRBRD = "beartear_topScore_leaderboard";

	public const int INDEX_NOT_FOUND =-1;
	public const string  ERROR_ACH_NOT_LOAD = "GameCenterAchievements not downloaded";
	
	static Dictionary<Achievement,int> GameCenterAchievementQuantitiesMap = new Dictionary<Achievement, int>();
	static Dictionary<Achievement,string> GameCenterAchievementsIDMap = new Dictionary<Achievement, string>();
	static Dictionary<Leaderboard,string> GameCenterLeaderboardIDMap = new Dictionary<Leaderboard, string>();
	static Dictionary<Achievement,JFGameCenterAchievement> PlayerGameCenterAchievementMap = new Dictionary<Achievement, JFGameCenterAchievement>();
	static Dictionary<Leaderboard,JFGameCenterScore> PlayerGameCenterScoreMap = new Dictionary<Leaderboard, JFGameCenterScore>();


	static List<GameCenterAchievement> GameCenterPlayerAchievementsList = new List<GameCenterAchievement>();
	static List<GameCenterScore> GameCenterPlayerScoresList = new List<GameCenterScore>();
	//List<Achievement> GameCenterAchievementsList = new List<Achievement>();
	//List<Leaderboard> GameCenterLeaderboardList = new List<Leaderboard>();

	static bool didLoadGameCenterAchievements = false;
	DateTime timeUpdateGClastCalled = new DateTime();
	int prevPosition = 0;
	int currDistanceTraveled = 0;

	static bool isFirstBoot =  true;

	//METHODS

	#region listener methods
	void OnEnable()
	{
		// Listens to all the GameCenter events.  All event listeners MUST be removed before this object is disposed!
		// Player events
		//GameCenterManager.loadPlayerDataFailed += loadPlayerDataFailed;
		//GameCenterManager.playerDataLoaded += playerDataLoaded;
		GameCenterManager.playerAuthenticated += playerAuthenticated;
		GameCenterManager.playerFailedToAuthenticate += playerFailedToAuthenticate;
		//GameCenterManager.playerLoggedOut += playerLoggedOut;
		//GameCenterManager.profilePhotoLoaded += profilePhotoLoaded;
		//GameCenterManager.profilePhotoFailed += profilePhotoFailed;
		
		// Leaderboards and scores
		//GameCenterManager.loadCategoryTitlesFailed += loadCategoryTitlesFailed;
		//GameCenterManager.categoriesLoaded += categoriesLoaded;
		GameCenterManager.reportScoreFailed += reportScoreFailed;
		GameCenterManager.reportScoreFinished += reportScoreFinished;
		GameCenterManager.retrieveScoresFailed += retrieveScoresFailed;
		GameCenterManager.scoresLoaded += scoresLoaded;
		GameCenterManager.retrieveScoresForPlayerIdFailed += retrieveScoresForPlayerIdFailed;
		GameCenterManager.scoresForPlayerIdLoaded += scoresForPlayerIdLoaded;
		
		// Achievements
		GameCenterManager.reportAchievementFailed += reportAchievementFailed;
		GameCenterManager.reportAchievementFinished += reportAchievementFinished;
		GameCenterManager.loadAchievementsFailed += loadAchievementsFailed;
		GameCenterManager.achievementsLoaded += achievementsLoaded;
		GameCenterManager.resetAchievementsFailed += resetAchievementsFailed;
		GameCenterManager.resetAchievementsFinished += resetAchievementsFinished;
		//GameCenterManager.retrieveAchievementMetadataFailed += retrieveAchievementMetadataFailed;
		//GameCenterManager.achievementMetadataLoaded += achievementMetadataLoaded;
		
	}

	void OnDisable()
	{
		// Remove all the event handlers
		// Player events
		//GameCenterManager.loadPlayerDataFailed -= loadPlayerDataFailed;
		//GameCenterManager.playerDataLoaded -= playerDataLoaded;
		GameCenterManager.playerAuthenticated -= playerAuthenticated;
		//GameCenterManager.playerLoggedOut -= playerLoggedOut;
		//GameCenterManager.profilePhotoLoaded -= profilePhotoLoaded;
		//GameCenterManager.profilePhotoFailed -= profilePhotoFailed;
		
		// Leaderboards and scores
		//GameCenterManager.loadCategoryTitlesFailed -= loadCategoryTitlesFailed;
		//GameCenterManager.categoriesLoaded -= categoriesLoaded;
		GameCenterManager.reportScoreFailed -= reportScoreFailed;
		GameCenterManager.reportScoreFinished -= reportScoreFinished;
		GameCenterManager.retrieveScoresFailed -= retrieveScoresFailed;
		GameCenterManager.scoresLoaded -= scoresLoaded;
		GameCenterManager.retrieveScoresForPlayerIdFailed -= retrieveScoresForPlayerIdFailed;
		GameCenterManager.scoresForPlayerIdLoaded -= scoresForPlayerIdLoaded;
		
		// Achievements
		GameCenterManager.reportAchievementFailed -= reportAchievementFailed;
		GameCenterManager.reportAchievementFinished -= reportAchievementFinished;
		GameCenterManager.loadAchievementsFailed -= loadAchievementsFailed;
		GameCenterManager.achievementsLoaded -= achievementsLoaded;
		GameCenterManager.resetAchievementsFailed -= resetAchievementsFailed;
		GameCenterManager.resetAchievementsFinished -= resetAchievementsFinished;
		//GameCenterManager.retrieveAchievementMetadataFailed -= retrieveAchievementMetadataFailed;
		//GameCenterManager.achievementMetadataLoaded -= achievementMetadataLoaded;
		
	}
	#endregion
	
	void Awake() {
		//LoadGameCenterLeaderboardList();
		//LoadGameCenterAchievementList();
		LoadGameCenterLeaderboardIDMap();
		LoadGameCenterAchievementsIDMap();
		LoadGameCenterAchievementQuantitiesMap();
	}

	void Start(){
		guiController = (GUIController) gameObject.GetComponent(typeof(GUIController));
	}
	
	void Update (){
		if (gameCenter.IsPlayerAuthenticated() && ! didLoadGameCenterAchievements)
			DownloadGameCenterAchievements();
	}
	#region Load Maps region

	/*void LoadGameCenterAchievementList(){
		GameCenterAchievementsList.Add (Achievement.DIST_TRAV_IN_SINGLE_GAME);
		GameCenterAchievementsList.Add (Achievement.TOTAL_DIST_TRAVELED);
		GameCenterAchievementsList.Add (Achievement.HURDLED_ROCKS);
		GameCenterAchievementsList.Add (Achievement.HURDLED_BUNNIES);
		GameCenterAchievementsList.Add (Achievement.CHOPPED_BUNNIES);
		GameCenterAchievementsList.Add (Achievement.OFF_SCREEN);
		
		//Debug.Log("[   DEBUG   ] Listing AchievementsList");
		//ListAllObjectsInList(AchievementsList);
	}*/

	/*void LoadGameCenterLeaderboardList(){
		GameCenterLeaderboardList.Add (Leaderboard.DIST_TRAV_IN_SINGLE_GAME);
		GameCenterLeaderboardList.Add (Leaderboard.TOTAL_DIST_TRAVELED);
		GameCenterLeaderboardList.Add (Leaderboard.HIGH_SCORE);
		
		//Debug.Log("[   DEBUG   ] Listing LeaderboardList");
		//ListAllObjectsInList(LeaderboardList);
	}*/

	void LoadGameCenterLeaderboardIDMap(){
		if (!GameCenterLeaderboardIDMap.ContainsKey(Leaderboard.DIST_TRAV_IN_SINGLE_GAME))
			GameCenterLeaderboardIDMap.Add (Leaderboard.DIST_TRAV_IN_SINGLE_GAME,	DIST_TRAV_IN_SINGLE_GAME_LDRBRD);

		if (!GameCenterLeaderboardIDMap.ContainsKey(Leaderboard.TOTAL_DIST_TRAVELED))
			GameCenterLeaderboardIDMap.Add (Leaderboard.TOTAL_DIST_TRAVELED,		TOTAL_DIST_TRAVELED_LDRBRD);

		if (!GameCenterLeaderboardIDMap.ContainsKey(Leaderboard.HIGH_SCORE))
			GameCenterLeaderboardIDMap.Add (Leaderboard.HIGH_SCORE,					HIGH_SCORES_LDRBRD);
		
		//Debug.Log("[   DEBUG   ] Listing LeaderboardIDMap");
		//ListAllObjectsInMap(GameCenterLeaderboardIDMap);
	}

	void LoadGameCenterAchievementsIDMap(){
		if (!GameCenterAchievementsIDMap.ContainsKey(Achievement.DIST_TRAV_IN_SINGLE_GAME))
			GameCenterAchievementsIDMap.Add (Achievement.DIST_TRAV_IN_SINGLE_GAME, 	DIST_TRAV_IN_SINGLE_GAME);

		if (!GameCenterAchievementsIDMap.ContainsKey(Achievement.TOTAL_DIST_TRAVELED))
			GameCenterAchievementsIDMap.Add (Achievement.TOTAL_DIST_TRAVELED, 		TOTAL_DIST_TRAVELED);

		if (!GameCenterAchievementsIDMap.ContainsKey(Achievement.HURDLED_ROCKS))
			GameCenterAchievementsIDMap.Add (Achievement.HURDLED_ROCKS, 			HURDLED_ROCKS);

		if (!GameCenterAchievementsIDMap.ContainsKey(Achievement.HURDLED_BUNNIES))
			GameCenterAchievementsIDMap.Add (Achievement.HURDLED_BUNNIES, 			HURDLED_BUNNIES);

		if (!GameCenterAchievementsIDMap.ContainsKey(Achievement.CHOPPED_BUNNIES))
			GameCenterAchievementsIDMap.Add (Achievement.CHOPPED_BUNNIES, 			CHOPPED_BUNNIES);

		if (!GameCenterAchievementsIDMap.ContainsKey(Achievement.OFF_SCREEN))
			GameCenterAchievementsIDMap.Add (Achievement.OFF_SCREEN, 				OFF_SCREEN);

		//Debug.Log("[   DEBUG   ] Listing AchievementIdMap");
		//ListAllObjectsInMap(GameCenterAchievementsIDMap);

	}

	void LoadGameCenterAchievementQuantitiesMap(){
		if (!GameCenterAchievementQuantitiesMap.ContainsKey(Achievement.DIST_TRAV_IN_SINGLE_GAME))
			GameCenterAchievementQuantitiesMap.Add (Achievement.DIST_TRAV_IN_SINGLE_GAME,	distTraveledSingleGameGoal);

		if (!GameCenterAchievementQuantitiesMap.ContainsKey(Achievement.TOTAL_DIST_TRAVELED))
			GameCenterAchievementQuantitiesMap.Add (Achievement.TOTAL_DIST_TRAVELED,		totalDistanceTraveledGoal);

		if (!GameCenterAchievementQuantitiesMap.ContainsKey(Achievement.CHOPPED_BUNNIES))
			GameCenterAchievementQuantitiesMap.Add (Achievement.CHOPPED_BUNNIES,			bunniesChoppedGoal);

		if (!GameCenterAchievementQuantitiesMap.ContainsKey(Achievement.HURDLED_ROCKS))
			GameCenterAchievementQuantitiesMap.Add (Achievement.HURDLED_ROCKS,				rocksHurdledGoal);

		if (!GameCenterAchievementQuantitiesMap.ContainsKey(Achievement.HURDLED_BUNNIES))
			GameCenterAchievementQuantitiesMap.Add (Achievement.HURDLED_BUNNIES,			bunniesHurdledGoal);

		if (!GameCenterAchievementQuantitiesMap.ContainsKey(Achievement.OFF_SCREEN))
			GameCenterAchievementQuantitiesMap.Add (Achievement.OFF_SCREEN,					1);

		//Debug.Log("[   DEBUG   ] Listing AchievementValuesMap");
		//ListAllObjectsInMap(AchievementValuesMap);
	}
	
	#endregion

	public void Achieved(Achievement achievement)
	{
		if ( didLoadGameCenterAchievements ){

			VerifyAchievementInPlayerGameCenterAchievementMap(achievement);

			PlayerGameCenterAchievementMap[achievement].isCompleted = true;

			ReportGameCenterAchievementPercentage(achievement);

		} //else Debug.Log ("Achieved: " + ERROR_ACH_NOT_LOAD);
	}

	public void IncrementCount(Achievement achievement)
	{
		//Debug.Log("IncrementCount called " + achievement);

		if (didLoadGameCenterAchievements ){
			VerifyAchievementInPlayerGameCenterAchievementMap(achievement);
			if ( ! PlayerGameCenterAchievementMap[achievement].isCompleted ){
				PlayerGameCenterAchievementMap[achievement].playerQuantity++;
				Debug.Log("IncrementCount: GameCenterPlayerAchievementProgressMap: " + achievement + " incremented to " + PlayerGameCenterAchievementMap[achievement].playerQuantity);
				if ( PlayerGameCenterAchievementMap[achievement].isCompleted ){
					Achieved(achievement);
				}
			} else
				Debug.Log ("IncrementCount: " + achievement + " already completed");
		} //else Debug.Log ("IncrementCount: " + ERROR_ACH_NOT_LOAD); 
	}

	public bool AchievementHasBeenMet (Achievement achievement)
	{
		if ( didLoadGameCenterAchievements ){
			VerifyAchievementInPlayerGameCenterAchievementMap(achievement);

			return PlayerGameCenterAchievementMap[achievement].isCompleted;
		} else { 
			//Debug.Log ("AchievementHasBeenMet: " + ERROR_ACH_NOT_LOAD);
			return false;
		}
	}

	public void UpdateDistanceAchievements(int newPos){
		if ( didLoadGameCenterAchievements ){
			if (prevPosition != 0) {
				int distTraveledThisFrame = newPos - prevPosition;

				/*	Total distance traveled Section	*/
				VerifyAchievementInPlayerGameCenterAchievementMap(Achievement.TOTAL_DIST_TRAVELED);
			

				//save previous isCompleted status to variable
				bool playerHasPreviouslyAchievedTotalDistanceGoal = PlayerGameCenterAchievementMap[Achievement.TOTAL_DIST_TRAVELED].isCompleted;
				//add this frames distance traveled
				PlayerGameCenterAchievementMap[Achievement.TOTAL_DIST_TRAVELED].playerQuantity += distTraveledThisFrame;

				if ( ! playerHasPreviouslyAchievedTotalDistanceGoal && PlayerGameCenterAchievementMap[Achievement.TOTAL_DIST_TRAVELED].isCompleted ){
					Achieved(Achievement.TOTAL_DIST_TRAVELED);
				}

				/*	distance traveled this game section	*/
				VerifyAchievementInPlayerGameCenterAchievementMap(Achievement.DIST_TRAV_IN_SINGLE_GAME);

				//save previous isCompleted status to variable
				bool playerHasPreviouslyAchievedSingleGameDistanceGoal = PlayerGameCenterAchievementMap[Achievement.DIST_TRAV_IN_SINGLE_GAME].isCompleted;
				//add this frames distance traveled
				currDistanceTraveled += distTraveledThisFrame;

				if (currDistanceTraveled > PlayerGameCenterAchievementMap[Achievement.DIST_TRAV_IN_SINGLE_GAME].playerQuantity){
					PlayerGameCenterAchievementMap[Achievement.DIST_TRAV_IN_SINGLE_GAME].playerQuantity = currDistanceTraveled;
				}

				if ( ! playerHasPreviouslyAchievedSingleGameDistanceGoal && PlayerGameCenterAchievementMap[Achievement.DIST_TRAV_IN_SINGLE_GAME].isCompleted)
				{
					Achieved(Achievement.DIST_TRAV_IN_SINGLE_GAME);
				}
			} else {
				currDistanceTraveled = 0;
			}

		} else {
			//Debug.Log ("UpdateDistanceAchievements: " + ERROR_ACH_NOT_LOAD);
			if (gameCenter.IsPlayerAuthenticated())
				DownloadGameCenterAchievements();

			if (prevPosition != 0) {
				int distTraveledThisFrame = newPos - prevPosition;
				currDistanceTraveled += distTraveledThisFrame;
			} else {
				currDistanceTraveled = 0;
			}
		}

		prevPosition = newPos;
	}

	public void UpdateHighScore (int currentScore){
		if ( didLoadGameCenterAchievements ){

			VerifyLeaderboardInPlayerGameCenterScoreMap(Leaderboard.HIGH_SCORE);

			if (currentScore > PlayerGameCenterScoreMap[Leaderboard.HIGH_SCORE].value){
				PlayerGameCenterScoreMap[Leaderboard.HIGH_SCORE].value = currentScore;
				guiController.isNewHighScore = true;
			}
		} //else Debug.Log ("UpdateHighScore: " + ERROR_ACH_NOT_LOAD);
	}

	#region GameCenter Methods
	void DownloadGameCenterAchievements(){
		gameCenter.LoadPlayerAchievements();
	}

	void DownloadGameCenterScoresForThisPlayer(){
		string playerId = gameCenter.GetPlayerIdentifier();
		gameCenter.RetrieveScoresForPlayerId(playerId);
	}

	/**************************************************************************************
	*  Compiles PlayerGameCenterAchievementMap<Achievement,JFGameCenterAchievement> from
	*  GameCenterPlayerAchievementsList<GameCenterAchievement> downloaded from Apple
	***************************************************************************************/
	void LoadPlayerGameCenterAchievementMap(){
		LoadPlayerGameCenterAchievement(Achievement.DIST_TRAV_IN_SINGLE_GAME);
		LoadPlayerGameCenterAchievement(Achievement.TOTAL_DIST_TRAVELED);
		LoadPlayerGameCenterAchievement(Achievement.CHOPPED_BUNNIES);
		LoadPlayerGameCenterAchievement(Achievement.HURDLED_ROCKS);
		LoadPlayerGameCenterAchievement(Achievement.HURDLED_BUNNIES);
		LoadPlayerGameCenterAchievement(Achievement.OFF_SCREEN);
		//Debug.Log("[   DEBUG   ] Listing PlayerAchievementMap");
		//ListAllObjectsInMap(PlayerGameCenterAchievementMap);
	}
	
	void LoadPlayerGameCenterAchievement(Achievement achievement){
		int index = GetGameCenterPlayerAchievementListIndex(achievement);
		if (index != INDEX_NOT_FOUND){
			if (PlayerGameCenterAchievementMap.ContainsKey(achievement))
				PlayerGameCenterAchievementMap[achievement] = new JFGameCenterAchievement(GameCenterPlayerAchievementsList[index], GameCenterAchievementQuantitiesMap[achievement]);
			else 
				PlayerGameCenterAchievementMap.Add(achievement,new JFGameCenterAchievement(GameCenterPlayerAchievementsList[index], GameCenterAchievementQuantitiesMap[achievement]));
		} else {
		 	if ( PlayerGameCenterAchievementMap.ContainsKey(achievement)) 
				PlayerGameCenterAchievementMap[achievement] = GetNewJFGameCenterAchievementForAchievement(achievement);
			else  
				PlayerGameCenterAchievementMap.Add(achievement,GetNewJFGameCenterAchievementForAchievement(achievement));
		}
	}

	void VerifyAchievementInPlayerGameCenterAchievementMap(Achievement achievement){
		if (!PlayerGameCenterAchievementMap.ContainsKey(achievement))
			LoadPlayerGameCenterAchievement(achievement);
		else 
			return;
	}

	/**************************************************************************************
	*  Compiles PlayerGameCenterScoreMap<Achievement,JFGameCenterScore> from
	*  GameCenterPlayerScoresList<GameCenterScore> downloaded from Apple
	***************************************************************************************/
	void LoadPlayerLeaderboardScoresMap(){
		LoadPlayerLeaderboardScore(Leaderboard.DIST_TRAV_IN_SINGLE_GAME);
		LoadPlayerLeaderboardScore(Leaderboard.TOTAL_DIST_TRAVELED);
		LoadPlayerLeaderboardScore(Leaderboard.HIGH_SCORE);
		
		Debug.Log("[   DEBUG   ] Listing LoadPlayerLeaderboardScoresMap");
		ListAllObjectsInMap(PlayerGameCenterScoreMap);
	}

	void LoadPlayerLeaderboardScore(Leaderboard leaderboard){
		int index = GetGameCenterScoresListIndex(leaderboard);
		if ( index != INDEX_NOT_FOUND ){
			if (PlayerGameCenterScoreMap.ContainsKey(leaderboard) )
				PlayerGameCenterScoreMap[leaderboard] = new JFGameCenterScore(GameCenterPlayerScoresList[index]);
			else 
				PlayerGameCenterScoreMap.Add(leaderboard, new JFGameCenterScore(GameCenterPlayerScoresList[index]));
		} else {
			if (PlayerGameCenterScoreMap.ContainsKey(leaderboard) )
				PlayerGameCenterScoreMap[leaderboard] = GetNewGameCenterScoreForLeaderBoard(leaderboard);
			else 
				PlayerGameCenterScoreMap.Add(leaderboard,GetNewGameCenterScoreForLeaderBoard(leaderboard));
		}
	}
	
	void VerifyLeaderboardInPlayerGameCenterScoreMap(Leaderboard leaderboard){
		if (!PlayerGameCenterScoreMap.ContainsKey(leaderboard))
			LoadPlayerLeaderboardScore(leaderboard);
		else 
			return;
	}

	/**************************************************************************************
	*  Helper method to retrieve index of Achievement from
	*  GameCenterPlayerAchievementsList<GameCenterAchievement>  downloaded from Apple
	***************************************************************************************/
	int GetGameCenterPlayerAchievementListIndex(Achievement achievement){
		int iii = 0;
		foreach (GameCenterAchievement gcAch in GameCenterPlayerAchievementsList )
		{
			if ( gcAch.identifier == GameCenterAchievementsIDMap[achievement] )
			{
				return iii;
			}
			iii++;
		}
		return INDEX_NOT_FOUND;
	}

	/**************************************************************************************
	*  Helper method to retrieve index of Leaderboard from
	*  GameCenterPlayerScoresList<GameCenterScore>  downloaded from Apple
	***************************************************************************************/
	int GetGameCenterScoresListIndex(Leaderboard leaderboard){
		int iii = 0;
		foreach (GameCenterScore gcScr in GameCenterPlayerScoresList )
		{
			if ( gcScr.category == GameCenterLeaderboardIDMap[leaderboard] )
			{
				return iii;
			}
			iii++;
		}
		return INDEX_NOT_FOUND;
	}
	
	public void UpdateGameCenter()
	{
		Debug.Log("[ DEBUG  ] - UpdateGameCenter called");
		if (DateTime.Now.Subtract(timeUpdateGClastCalled) < new TimeSpan(0,0,3)){
			Debug.Log ("UpdateGameCenter called less than 3 seconds apart");
			return;
		}
		timeUpdateGClastCalled = DateTime.Now;

		if( didLoadGameCenterAchievements && gameCenter.IsPlayerAuthenticated()){
			VerifyLeaderboardInPlayerGameCenterScoreMap(Leaderboard.TOTAL_DIST_TRAVELED);
			VerifyLeaderboardInPlayerGameCenterScoreMap(Leaderboard.DIST_TRAV_IN_SINGLE_GAME);

			if ( PlayerGameCenterAchievementMap[Achievement.TOTAL_DIST_TRAVELED].playerQuantity != PlayerGameCenterScoreMap[Leaderboard.TOTAL_DIST_TRAVELED].value){
				if (PlayerGameCenterAchievementMap[Achievement.TOTAL_DIST_TRAVELED].playerQuantity > PlayerGameCenterScoreMap[Leaderboard.TOTAL_DIST_TRAVELED].value)
					PlayerGameCenterScoreMap[Leaderboard.TOTAL_DIST_TRAVELED].value = PlayerGameCenterAchievementMap[Achievement.TOTAL_DIST_TRAVELED].playerQuantity;
				else 
					PlayerGameCenterAchievementMap[Achievement.TOTAL_DIST_TRAVELED].playerQuantity = PlayerGameCenterScoreMap[Leaderboard.TOTAL_DIST_TRAVELED].value;
			}

			if ( PlayerGameCenterAchievementMap[Achievement.DIST_TRAV_IN_SINGLE_GAME].playerQuantity != PlayerGameCenterScoreMap[Leaderboard.DIST_TRAV_IN_SINGLE_GAME].value){
				if (PlayerGameCenterAchievementMap[Achievement.DIST_TRAV_IN_SINGLE_GAME].playerQuantity > PlayerGameCenterScoreMap[Leaderboard.DIST_TRAV_IN_SINGLE_GAME].value)
					PlayerGameCenterScoreMap[Leaderboard.DIST_TRAV_IN_SINGLE_GAME].value = PlayerGameCenterAchievementMap[Achievement.DIST_TRAV_IN_SINGLE_GAME].playerQuantity;
				else 
					PlayerGameCenterAchievementMap[Achievement.DIST_TRAV_IN_SINGLE_GAME].playerQuantity = PlayerGameCenterScoreMap[Leaderboard.DIST_TRAV_IN_SINGLE_GAME].value;
			}

			foreach(KeyValuePair<Achievement, JFGameCenterAchievement> entry in PlayerGameCenterAchievementMap)
			{
				ReportGameCenterAchievementPercentage(entry.Key);
			}

			foreach(KeyValuePair<Leaderboard, JFGameCenterScore> entry in PlayerGameCenterScoreMap)
			{
				ReportToLeaderboard(entry.Key);
			}

		} else {

			foreach(KeyValuePair<Achievement,string> entry in GameCenterAchievementsIDMap)
			{
				VerifyAchievementInPlayerGameCenterAchievementMap(entry.Key);
			}
			
			foreach(KeyValuePair<Leaderboard,string> entry in GameCenterLeaderboardIDMap)
			{
				VerifyLeaderboardInPlayerGameCenterScoreMap(entry.Key);
			}
			Debug.Log(string.Format ("UpdateGameCenter: {0}\nDistTraveledAch: {1},\nDistTraveledTotalAch: {2},\nDistTraveledLdr: {3},\nDistTraveledTotalLdr: {4},\nHiScoreLdr: {5},\nOffScreen: {6},\nChoppedBunnies: {7},\nHurdledBunnies: {8},\nHurdledRocks: {9}",
			                         ERROR_ACH_NOT_LOAD,
			                         PlayerGameCenterAchievementMap[Achievement.DIST_TRAV_IN_SINGLE_GAME].playerQuantity,
			                         PlayerGameCenterAchievementMap[Achievement.TOTAL_DIST_TRAVELED].playerQuantity,
			                         PlayerGameCenterScoreMap[Leaderboard.DIST_TRAV_IN_SINGLE_GAME].value,
			                         PlayerGameCenterScoreMap[Leaderboard.TOTAL_DIST_TRAVELED].value,
			                         PlayerGameCenterScoreMap[Leaderboard.HIGH_SCORE].value,
			                         PlayerGameCenterAchievementMap[Achievement.OFF_SCREEN].playerQuantity,
			                         PlayerGameCenterAchievementMap[Achievement.CHOPPED_BUNNIES].playerQuantity,
			                         PlayerGameCenterAchievementMap[Achievement.HURDLED_BUNNIES].playerQuantity,
			                         PlayerGameCenterAchievementMap[Achievement.HURDLED_ROCKS].playerQuantity));
		}
		
	}


	void ReportGameCenterAchievementPercentage (Achievement achievement){
		Debug.Log(" DEBUG  ] - Reporting Achievement: "+ PlayerGameCenterAchievementMap[achievement].identifier + " - " + PlayerGameCenterAchievementMap[achievement].percentComplete);
		gameCenter.ReportAchievement(PlayerGameCenterAchievementMap[achievement].identifier,PlayerGameCenterAchievementMap[achievement].percentComplete);
	}

	void ReportToLeaderboard(Leaderboard leaderboard){
		Debug.Log(" DEBUG  ] - Reporting Leaderboard: "+ PlayerGameCenterScoreMap[leaderboard].category + " - " + PlayerGameCenterScoreMap[leaderboard].value);
		gameCenter.ReportScore( PlayerGameCenterScoreMap[leaderboard].value, PlayerGameCenterScoreMap[leaderboard].category);
	}
	
	void ClearAllAchievements(){
		//PlayerPrefs.DeleteAll();
		gameCenter.ResetAchievements();
	}

 	#endregion
	
	#region Listener Events
	
	void playerAuthenticated()
	{
		Debug.Log( "playerAuthenticated" );
		if (isFirstBoot){
			ClearAllAchievements();
			isFirstBoot = false;
		}
		DownloadGameCenterAchievements();
		DownloadGameCenterScoresForThisPlayer();
	}

	void playerFailedToAuthenticate( string error )
	{
		Debug.Log( "playerFailedToAuthenticate: " + error );
	}


	void resetAchievementsFinished()
	{
		Debug.Log( "resetAchievmenetsFinished" );
		DownloadGameCenterAchievements();
		DownloadGameCenterScoresForThisPlayer();
	}
	
	
	void resetAchievementsFailed( string error )
	{
		Debug.Log( "resetAchievementsFailed: " + error );
	}

	void achievementsLoaded( List<GameCenterAchievement> achievements )
	{
		Debug.Log( "Achievements downloaded successfully" );
		GameCenterPlayerAchievementsList = achievements;
		didLoadGameCenterAchievements = true;
			//foreach( GameCenterAchievement s in GameCenterPlayerAchievementsList ){
			//	Debug.Log( s );
			//}
		LoadPlayerGameCenterAchievementMap();
		
	}

	void loadAchievementsFailed( string error )
	{
		Debug.Log( "loadAchievementsFailed: " + error );
	}

	void reportAchievementFinished( string identifier )
	{
		Debug.Log( "reportAchievementFinished: " + identifier );
	}

	void reportAchievementFailed( string error )
	{
		Debug.Log( "reportAchievementFailed: " + error );
	}

	void scoresLoaded( List<GameCenterScore> scores )
	{
		Debug.Log( "scoresLoaded" );
		foreach( GameCenterScore s in scores )
			Debug.Log( s );
	}

	void retrieveScoresFailed( string error )
	{
		Debug.Log( "retrieveScoresFailed: " + error );
	}

	void retrieveScoresForPlayerIdFailed( string error )
	{
		Debug.Log( "retrieveScoresForPlayerIdFailed: " + error );
	}
	
	void scoresForPlayerIdLoaded( List<GameCenterScore> scores )
	{
		Debug.Log( "scoresForPlayerIdLoaded" );
		GameCenterPlayerScoresList = scores;
		//foreach( GameCenterScore s in GameCenterPlayerScoresList )
		//	Debug.Log( s );
		
		LoadPlayerLeaderboardScoresMap();
	}

	void reportScoreFinished( string category )
	{
		Debug.Log( "reportScoreFinished for category: " + category );
	}

	void reportScoreFailed( string error )
	{
		Debug.Log( "reportScoreFailed: " + error );
	}

	#endregion

	#region Unused Listeners
	/*
	void playerLoggedOut()
	{
		Debug.Log( "playerLoggedOut" );
	}
	 */
	/*
	void playerDataLoaded( List<GameCenterPlayer> players )
	{
		Debug.Log( "playerDataLoaded" );
		foreach( GameCenterPlayer p in players )
			Debug.Log( p );
	}
	
	
	void loadPlayerDataFailed( string error )
	{
		Debug.Log( "loadPlayerDataFailed: " + error );
	}
	*/
	
	/*void profilePhotoLoaded( string path )
	{
		Debug.Log( "profilePhotoLoaded: " + path );
	}
	
	
	void profilePhotoFailed( string error )
	{
		Debug.Log( "profilePhotoFailed: " + error );
	}*/

	/*
	void categoriesLoaded( List<GameCenterLeaderboard> leaderboards )
	{
		Debug.Log( "categoriesLoaded" );
		foreach( GameCenterLeaderboard l in leaderboards )
			Debug.Log( l );
	}
	
	
	void loadCategoryTitlesFailed( string error )
	{
		Debug.Log( "loadCategoryTitlesFailed: " + error );
	}
	*/

	/*
	void achievementMetadataLoaded( List<GameCenterAchievementMetadata> achievementMetadata )
	{
		Debug.Log( "achievementMetadatLoaded" );
		foreach( GameCenterAchievementMetadata s in achievementMetadata )
			Debug.Log( s );
	}
	
	
	void retrieveAchievementMetadataFailed( string error )
	{
		Debug.Log( "retrieveAchievementMetadataFailed: " + error );
	}
	
	*/

	
	#endregion;

	#region Debug Methods
	void ListAllObjectsInMap<T,M>(Dictionary<T,M> dict)
	{
		foreach( KeyValuePair<T, M> kvp in dict )
		{

			Debug.Log(string.Format ("Key = {0}, Value = {1}", kvp.Key, kvp.Value));
		}
	}
	
	void ListAllObjectsInList<T>(List<T> list){
		int iii = 0;
		foreach ( T obj in list){
			Debug.Log("Index: "+ iii +" Value: " + obj);
			iii++;
		}
	}
	#endregion

	JFGameCenterAchievement GetNewJFGameCenterAchievementForAchievement (Achievement achievement)
	{
		string Id = GameCenterAchievementsIDMap[achievement];
		int playQuan = 0;
		int achQuan = GameCenterAchievementQuantitiesMap[achievement];

		JFGameCenterAchievement gcAch = new JFGameCenterAchievement(Id,playQuan,(int)achQuan);
		return gcAch;
	}
	
	JFGameCenterScore GetNewGameCenterScoreForLeaderBoard (Leaderboard leaderboard)
	{
		string Id = GameCenterLeaderboardIDMap[leaderboard];
		int value = 0;
		
		JFGameCenterScore gcSco = new JFGameCenterScore(Id,value);
		return gcSco;
	}


}
