using UnityEngine;

public class ProvinceUnlocker : MonoBehaviour
{
	[SerializeField] private ProvinceView _provinceView;

	private MainParameterModel _moneyModel;

	public void Initialize(MainParameterModel moneyModel)
	{
		_moneyModel = moneyModel;
    }

	private void OnMouseDown()
	{
		OnUnlockButtonClick();
    }

	private void OnUnlockButtonClick()
	{
		if (_moneyModel.TryRemoveValue(PriceList.UnlockProvincePrice) == false)
		{
            _provinceView.ProvinceModel.Unlock();
		}
	}
}