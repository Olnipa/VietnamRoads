using System;

public class ProvinceModel : Model
{
	private ProvinceLevelIncreaser _provinceLevelIncreaser;

	public bool IsActive { get; private set; }
	public int Level { get; private set; }

	public event Action Activated;

	public ProvinceModel(ProvinceLevelIncreaser provinceLevelIncreaser)
	{
		_provinceLevelIncreaser = provinceLevelIncreaser;
    }

	public void Unlock()
	{
        IsActive = true;
		
        Level = _provinceLevelIncreaser.GetNextProvinceLevel();
        Activated.Invoke();
    }
}