using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FloatingJoystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public RectTransform background;
    public RectTransform handle;
    public JoystickDirection joystickDirection = JoystickDirection.Both;
    [Range(0, 2f)] public float handleLimit = 1f;
    private Vector2 input = Vector2.zero;
    //output
    public float Veritcal { get { return input.y; } }
    public float Horizontal { get { return input.x; } }
    Vector2 JoyPosition = Vector2.zero;
    public void OnPointerDown(PointerEventData eventData)
    {
        background.gameObject.SetActive(true);
        OnDrag(eventData);
        JoyPosition = eventData.position;
        background.position = eventData.position;
        handle.anchoredPosition = (input * background.sizeDelta.x / 2f) * handleLimit;
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 JoyDirection = eventData.position - JoyPosition;
        input = (JoyDirection.magnitude > background.sizeDelta.x / 2f) ? JoyDirection.normalized :
            JoyDirection / (background.sizeDelta.x / 2f);
        if (joystickDirection == JoystickDirection.Horizontal)
            input = new Vector2(input.x, 0f);
        if (joystickDirection == JoystickDirection.Vertical)
            input = new Vector2(input.y, 0f);
        handle.anchoredPosition = (input * background.sizeDelta.x / 2f) * handleLimit;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        background.gameObject.SetActive(false);
        input = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }
}
