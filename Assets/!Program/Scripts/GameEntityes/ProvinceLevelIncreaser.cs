using System;

public class ProvinceLevelIncreaser
{
	private static int _currentMaxLevel = 1;

	public event Action LevelIncreased;

	public int GetNextProvinceLevel()
	{
		_currentMaxLevel++;
		LevelIncreased.Invoke();
		return _currentMaxLevel;
	}
}