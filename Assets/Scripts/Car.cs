using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {
    // if true, the car moves horizontally. otherwise, vertical
    public bool horizontal;

    private bool isDragging;

    public void OnMouseDown() {
        isDragging = true;
    }

    public void OnMouseUp() {
        // snap to place
        if (horizontal) {
            if (transform.position.x < 0) {
                Vector2 pos = transform.position;
                pos.x = -1.5f;
                transform.position = pos;
            } else {
                Vector2 pos = transform.position;
                pos.x = 1.5f;
                transform.position = pos;
            }
        }
        else {
            if (transform.position.y < 0) {
                Vector2 pos = transform.position;
                pos.y = -1.5f;
                transform.position = pos;
            } else {
                Vector2 pos = transform.position;
                pos.y = 1.5f;
                transform.position = pos;
            }
        }

        isDragging = false;
    }
    
    void Update() {
        if (isDragging) {
            if (horizontal) {
                Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                point.y = transform.position.y;
                transform.position = point;
            } else {
                Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                point.x = transform.position.x;
                transform.position = point;
            }
            
        }
    }
}