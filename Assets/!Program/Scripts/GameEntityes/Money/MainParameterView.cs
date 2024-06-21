using TMPro;
using UnityEngine;
using Zenject;

public abstract class MainParameterView : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI _valueView;

    protected MainParameterModel _mainParameterModel;

    [Inject]
    protected void Initialize(MainParameterModel mainParameterModel)
    {
        _mainParameterModel = mainParameterModel;
        OnValueUpdate();

        _mainParameterModel.Updated += OnValueUpdate;
    }

    protected void OnDestroy()
    {
        _mainParameterModel.Updated -= OnValueUpdate;
    }

    protected virtual void OnValueUpdate()
    {
        _valueView.text = _mainParameterModel.Value.ToString();
    }
}