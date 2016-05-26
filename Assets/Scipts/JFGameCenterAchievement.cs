using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class JFGameCenterAchievement {
	
	public string identifier;
	public bool isHidden;
	public Int64 playerQuantity;
	public Int64 quantityToAchieve;
	public DateTime lastReportedDate;

	public bool isCompleted {
		get { 
			return (playerQuantity >= quantityToAchieve);
		}

		set { 
			if (value){
				if ( playerQuantity < quantityToAchieve ){
					playerQuantity = quantityToAchieve;
				}
			}
		}
	}

	public float percentComplete {
		get { 
			return ((float)playerQuantity/(float)quantityToAchieve)*100f;
		}
		
		set { 
			playerQuantity =(int) ((value * quantityToAchieve)/100);
		}
	}

	public JFGameCenterAchievement( GameCenterAchievement achievement,int nQuantityToAchieve )
	{
		identifier = achievement.identifier;
		playerQuantity = (int)(achievement.percentComplete * nQuantityToAchieve)/100;;
		quantityToAchieve = nQuantityToAchieve;
		isHidden = achievement.isHidden;
		
	}

	public JFGameCenterAchievement( string strIdentifier, int nPlayerQuantity, int nQuantityToAchieve, bool bIsHidden = false  )
	{
		identifier = strIdentifier;
		playerQuantity = nPlayerQuantity;
		quantityToAchieve = nQuantityToAchieve;
		isHidden = bIsHidden;
		
	}

	/*
	public JFGameCenterAchievement( string strIdentifier, int nQuantityToAchieve, float fPercentComplete,  bool bIsHidden)
	{
		identifier = strIdentifier;
		playerQuantity = (int)(fPercentComplete * nQuantityToAchieve)/100;
		quantityToAchieve = nQuantityToAchieve;
		isHidden = bIsHidden;

	}*/

	public override string ToString()
	{
		return string.Format( "<Achievement> identifier: {0}, hidden: {1}, playerQuantity: {2}, quantityToAchieve: {3}, completed: {4}, percentComplete: {5}, lastReported: {6}",
		                     identifier, isHidden,playerQuantity,quantityToAchieve, isCompleted, percentComplete, lastReportedDate );
	}

}
