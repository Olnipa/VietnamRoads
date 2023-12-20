using UnityEngine;

public class ProvinceLocker : MonoBehaviour
{
	[SerializeField] private ProvinceView _provinceView;

    public ProvinceView ProvinceView => _provinceView;

	public void Initialize()
	{
		if (_provinceView.IsActive)
		{
            gameObject.SetActive(false);
            return;
		}

        _provinceView.ProvinceModel.Activated += OnProvinceActivated;
    }

	private void OnProvinceActivated()
	{
		gameObject.SetActive(false);
        _provinceView.ProvinceModel.Activated -= OnProvinceActivated;
    }
}