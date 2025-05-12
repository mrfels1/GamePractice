using UnityEngine;

public class PickUp : MonoBehaviour
{
    private Vector3 MouseOffset;
    private float MouseZCoord;

    void OnMouseDown() {
        MouseZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        MouseOffset = gameObject.transform.position - GetMousePosition();
    }



    // Получение координат курсора
    public Vector3 GetMousePosition() {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = MouseZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }


    //Основная логика
    void OnMouseDrag() {
        transform.position = GetMousePosition() + MouseOffset;
    }
}
