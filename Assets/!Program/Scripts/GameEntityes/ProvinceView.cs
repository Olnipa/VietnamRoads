using System;
using UnityEngine;

public class ProvinceView : MonoBehaviour
{
    [SerializeField] private bool _isActive;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public bool IsActive => _isActive;

    public event Action ProvinceViewInitialized;

    public ProvinceModel ProvinceModel { get; private set; }

	public void Initialize(ProvinceModel provinceModel)
	{
		ProvinceModel = provinceModel;
        ProvinceModel.Activated += OnProvinceUnlock;
        ProvinceViewInitialized.Invoke();

        if (_isActive)
            ProvinceModel.Unlock();
        else
            _spriteRenderer.enabled = false;
    }

	private void OnProvinceUnlock()
	{
        gameObject.SetActive(true);
        _spriteRenderer.enabled = true;

        ProvinceModel.Activated -= OnProvinceUnlock;
    }
}