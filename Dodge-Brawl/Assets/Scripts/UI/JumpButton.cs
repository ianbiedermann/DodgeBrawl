using UnityEngine;
using UnityEngine.EventSystems;

public class JumpButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [HideInInspector]
    public bool pressed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        pressed = true;
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        pressed = false;
    }

}
