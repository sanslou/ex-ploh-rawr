using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MobileController : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{

    private Image joystickBG;
    [SerializeField]
    private Image joystickFG;
    private Vector2 inputVector;

    public CharacterController characterController;

    public Vector2 lastMoveDirection = Vector2.down; // added: remembers last joystick direction.
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        joystickBG = GetComponent<Image>();
        joystickFG = transform.GetChild(0).GetComponent<Image>();

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //inputVector = Vector2.zero;
        //joystickFG.rectTransform.anchoredPosition = Vector2.zero;
        OnDrag(eventData);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;
        joystickFG.rectTransform.anchoredPosition = Vector2.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBG.rectTransform, eventData.position, eventData.pressEventCamera, out pos))
        {
            pos.x = (pos.x / joystickBG.rectTransform.sizeDelta.x);
            pos.y = (pos.y / joystickBG.rectTransform.sizeDelta.y);

            inputVector = new Vector2(pos.x * 2 - 1, pos.y * 2 - 1);
            inputVector = (inputVector.magnitude > 1.0f) ? (inputVector.normalized) : (inputVector);

            joystickFG.rectTransform.anchoredPosition = new Vector2(inputVector.x * (joystickBG.rectTransform.sizeDelta.x / 2), inputVector.y * (joystickBG.rectTransform.sizeDelta.y / 2));
            
            if (inputVector.sqrMagnitude > 0.01f) lastMoveDirection = inputVector.normalized; // added this logic: Remembers the last joystick direction.
        }
    }

    public float Horizontal() {
        if (inputVector.x != 0) return inputVector.x;
        return Input.GetAxis("Horizontal");
    }

    public float Vertical() {
        if (inputVector.y != 0) return inputVector.y;
        return Input.GetAxis("Vertical");
    }


    




}
