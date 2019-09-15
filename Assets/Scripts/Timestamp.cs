using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Timestamp
{
	const float MAX_SUBDIVISION = 1000.0f;
	const int MAX_BEAT_PER_MEASURE = 100;

	public int bars;
	public int beats;
	public float subdivision;

	public Timestamp(int bars = 0, int beats = 0, float subdivision = 0)
	{
		if(bars < 0)
		{
			throw new System.ArgumentException("Parameter cannot be negative", "bars");
		}
		else
		{
			this.bars = bars;
		}

		if(beats < 0)
		{
			throw new System.ArgumentException("Parameter cannot be negative", "beats");
		}
		else if(beats > MAX_BEAT_PER_MEASURE)
		{
			throw new System.ArgumentException("Parameter too large", "beats");
		}
		else
		{
			this.beats = beats;
		}

		if(subdivision < 0)
		{
			throw new System.ArgumentException("Parameter cannot be negative", "subdivision");
		}
		else if(subdivision > MAX_SUBDIVISION)
		{
			throw new System.ArgumentException("Parameter too large", "subdivision");
		}
		else
		{
			this.subdivision = subdivision;
		}
	}

	public Timestamp(float secondsPassed, int beatsPerMeasure, float beatsPerMinute)
	{
		subdivision = MAX_SUBDIVISION * secondsPassed * beatsPerMinute / 60.0f;
		normalize(beatsPerMeasure);
	}


	private void normalize(int beatsPerMeasure)
	{
		while(subdivision >= MAX_SUBDIVISION)
		{
			subdivision -= MAX_SUBDIVISION;
			beats += 1;
		}

		while(beats >= beatsPerMeasure)
		{
			beats -= beatsPerMeasure;
			bars += 1;
		}
	}

	public float getTicks(int beatsPerMeasure = MAX_BEAT_PER_MEASURE)
	{	
		if(beatsPerMeasure > MAX_BEAT_PER_MEASURE)
		{
			throw new System.ArgumentException("Parameter too large", "beatsPerMeasure");
		}
		return MAX_SUBDIVISION * (bars * beatsPerMeasure + beats) + subdivision;
	}

	public void addTime(int beatsPerMeasure, float timePassed, float bpm)
	{
		if(beatsPerMeasure > MAX_BEAT_PER_MEASURE)
		{
			throw new System.ArgumentException("Parameter too large", "beatsPerMeasure");
		}
		//timePassed is in seconds
		float minutesPassed = timePassed / 60;
		float beatsPassed = bpm * minutesPassed;
		float subdivisionPassed = beatsPassed * MAX_SUBDIVISION;
		
		subdivision += subdivisionPassed;
		normalize(beatsPerMeasure);
	}

	public void addTicks(int beatsPerMeasure, float toAdd)
	{
		subdivision += toAdd;
		normalize(beatsPerMeasure);
	}

	public static bool operator==(Timestamp a, Timestamp b)
	{
		//If one is null and the other isn't
		if(object.ReferenceEquals(b,null) != object.ReferenceEquals(a,null))
			return false;
		if(object.ReferenceEquals(a,null) && object.ReferenceEquals(b,null))
			return true;
		return a.bars == b.bars &&
			   a.beats == b.beats &&
			   a.subdivision == b.subdivision;
	}

	public override bool Equals(object other)
	{
		return this == (other as Timestamp);
	}

	public override int GetHashCode()
	{
		return getTicks().GetHashCode();
	}

	public int CompareTo(Timestamp other)
	{
		if(this < other)
		{
			return -1;
		}
		else if(other < this)
		{
			return 1;
		}
		else
		{
			return 0;
		}
	}
	
	public static bool operator!=(Timestamp a, Timestamp b)
	{
		return !(a == b);
	}
	public static bool operator<(Timestamp a, Timestamp b)
	{
		return a.getTicks() < b.getTicks();
	}

	public static bool operator>(Timestamp a, Timestamp b)
	{
		return a.getTicks() > b.getTicks();
	}
}
