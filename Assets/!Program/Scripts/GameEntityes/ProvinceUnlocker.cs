using UnityEngine;

public class ProvinceUnlocker : MonoBehaviour
{
	[SerializeField] private ProvinceModel _province;

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
			_province.Unlock();
		}
	}
}