using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ReactiveButton : Button
{
    private bool _isPressed;

    public bool Pressed => _isPressed;

    public override void OnPointerDown(PointerEventData eventData)
    {
        _isPressed = true;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        _isPressed = false;
    }
}
