using System;
using UnityEngine;

public class Province : MonoBehaviour
{
	[SerializeField] private bool _isActive;
	[SerializeField] private SpriteRenderer _spriteRenderer;

	private ProvinceLevelIncreaser _provinceLevelCalculator;

	public int Level { get; private set; }

	public event Action Activated;

	public void Initialize(ProvinceLevelIncreaser provinceLevelCalculator)
	{
		_provinceLevelCalculator = provinceLevelCalculator;

		if (_isActive)
			Unlock();
		else
	        _spriteRenderer.enabled = false;
    }

	public void Unlock()
	{
        _isActive = true;
		gameObject.SetActive(true);
		_spriteRenderer.enabled = true;
        Level = _provinceLevelCalculator.GetNextProvinceLevel();
        Activated.Invoke();
    }
}
