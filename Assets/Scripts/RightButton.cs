//not used anymore
using UnityEngine;
using UnityEngine.EventSystems;

public class RightButton : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        GameObject.Find("GameManager").GetComponent<Game_Manager>().horizontalInput = 1;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        GameObject.Find("GameManager").GetComponent<Game_Manager>().horizontalInput = 0;
    }

   
}
