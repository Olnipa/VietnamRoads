using System;

public class ProvinceModel
{
	private ProvinceLevelIncreaser _provinceLevelCalculator;

	public bool _isActive { get; private set; }
	public int Level { get; private set; }

	public event Action Activated;

	public ProvinceModel(ProvinceLevelIncreaser provinceLevelCalculator)
	{
		_provinceLevelCalculator = provinceLevelCalculator;
    }

	public void Unlock()
	{
        _isActive = true;
		
        Level = _provinceLevelCalculator.GetNextProvinceLevel();
        Activated.Invoke();
    }
}