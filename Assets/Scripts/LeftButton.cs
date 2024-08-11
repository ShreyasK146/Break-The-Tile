//not used anymore
using UnityEngine;
using UnityEngine.EventSystems;

public class LeftButton : MonoBehaviour,IPointerUpHandler,IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        GameObject.Find("GameManager").GetComponent<Game_Manager>().horizontalInput = -1;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        GameObject.Find("GameManager").GetComponent<Game_Manager>().horizontalInput = 0;
    }
}
