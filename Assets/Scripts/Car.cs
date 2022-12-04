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
                pos.x = -1.739573f;
                transform.position = pos;
            } else {
                Vector2 pos = transform.position;
                pos.x = 1.260427f;
                transform.position = pos;
            }
        }
        else {
            if (transform.position.y < 0) {
                Vector2 pos = transform.position;
                pos.y = -1.739573f;
                transform.position = pos;
            } else {
                Vector2 pos = transform.position;
                pos.y = 1.260427f;
                transform.position = pos;
            }
        }

        isDragging = false;
    }

    
    void Update() {
        if (isDragging) {
            if (horizontal) {
                Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                // TODO: change this to not rely on `point` since the mouse can move past it (we want the block to not be able to)
                if (point.x < -1.739573) {
                    return;
                } else if (point.x > 1.260427) {
                    return;
                }
                point.y = transform.position.y;
                transform.position = point;
            } else {
                Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (point.y < -1.739573) {
                    return;
                } else if (point.y > 1.260427) {
                    return;
                }
                point.x = transform.position.x;
                transform.position = point;
            }
        }
    }
}