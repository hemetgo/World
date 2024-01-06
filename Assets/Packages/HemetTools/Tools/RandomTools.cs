using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HemetTools.RandomTools
{
	[System.Serializable]
	public class RandomIntValue
	{
		public int MinValue;
		public int MaxValue;

		/// <summary>
		/// Returns a random value based
		/// </summary>
		public int Value => GetRandom();

		public int GetRandom()
		{
			return Random.Range(MinValue, MaxValue + 1);
		}
	}
}