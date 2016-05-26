using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class JFGameCenterScore {

	public string category;
	public string formattedValue;
	public Int64 value;
	public UInt64 context;
	public DateTime date;
	public string playerId;
	public int rank;
	public bool isFriend;
	public string alias;
	public int maxRange; // this is only properly set when retrieving all scores without limiting by playerId


	public JFGameCenterScore( string newCategory, Int64 newValue  )
	{
		category = newCategory;
		value = newValue;
	}

	public JFGameCenterScore( GameCenterScore gcScore  )
	{
		category = gcScore.category;
		value = gcScore.value;
	}
	
	
	public override string ToString()
	{
		return string.Format( "<Score> category: {0}, formattedValue: {1}, date: {2}, rank: {3}, alias: {4}, maxRange: {5}, value: {6}, context: {7}",
		                     category, formattedValue, date, rank, alias, maxRange, value, context );
	}

}
