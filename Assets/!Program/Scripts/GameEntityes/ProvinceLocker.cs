using UnityEngine;

public class ProvinceLocker : MonoBehaviour
{
	[SerializeField] private ProvinceView _provinceView;
    [SerializeField] private InputManager _input;

	private UIPanelsSwitcher _uiPanelsSwitcher;

	public void Initialize(UIPanelsSwitcher uiPanelsSwitcher)
	{
        _input.ButtonDownClicked += OnMouseDownClick;

        _uiPanelsSwitcher = uiPanelsSwitcher;

		if (_provinceView.IsActive)
			gameObject.SetActive(false);

		_provinceView.ProvinceModel.Activated += OnProvinceActivated;
    }

    private void OnMouseDownClick()
    {
        ProvinceLocker provinceLocker = TryGetProvinceLockerUnderMouse();

        if (provinceLocker != null)
            OnProvinceUnlockClick();
    }

    private ProvinceLocker TryGetProvinceLockerUnderMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction);

        foreach (var hit in hits)
        {
            if (hit == false)
                continue;

            if (hit.collider.TryGetComponent(out ProvinceLocker provincelocker))
            {
                return provincelocker;
            }
        }

        return null;
    }

 //   private void OnMouseDown()
	//{
 //       OnProvinceUnlockClick();
 //   }

	private void OnProvinceUnlockClick()
	{
        _uiPanelsSwitcher.OnLockProvinceClick(_provinceView.ProvinceModel);
	}

	private void OnProvinceActivated()
	{
		gameObject.SetActive(false);
        _provinceView.ProvinceModel.Activated -= OnProvinceActivated;
    }
}