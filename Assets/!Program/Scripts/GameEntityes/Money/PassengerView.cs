public class PassengerView : MainParameterView
{
    protected override void OnValueUpdate()
    {
        _valueView.text = _mainParameterModel.Value.ToString() + " / " + PriceList.PassengersToAllowBuyingNextProvince;
    }
}