using UnityEngine;

public class ProvinceLocker : MonoBehaviour
{
	[SerializeField] private ProvinceView _provinceView;

	private UIPanelsSwitcher _uiPanelsSwitcher;

	private void OnDisable()
	{
		//_provinceView.ProvinceModel.Activated -= OnProvinceActivated;
	}

	public void Initialize(UIPanelsSwitcher uiPanelsSwitcher)
	{
		_uiPanelsSwitcher = uiPanelsSwitcher;

		if (_provinceView.IsActive)
			gameObject.SetActive(false);

		//_provinceView.ProvinceModel.Activated += OnProvinceActivated;
    }

	private void OnMouseDown()
	{
        OnProvinceUnlockClick();
    }

	private void OnProvinceUnlockClick()
	{
        _uiPanelsSwitcher.OnLockProvinceClick(_provinceView);
	}

	private void OnProvinceActivated()
	{
		gameObject.SetActive(false);
	}
}