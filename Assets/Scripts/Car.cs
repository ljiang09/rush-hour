using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {
    // if true, the car moves horizontally. otherwise, vertical
    public bool horizontal;
    
    private bool isDragging = false;
    private bool stopMoving = false;    // true to stop moving. helps with "collisions"
    private float offset = 0.0f;


    void Awake() {
        if (horizontal) {
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
        } else {
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
        }
    }


    public void OnMouseDown() {
        isDragging = true;
        if (horizontal) {
            offset = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
        } else {
            offset = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;
        }
        GetComponent<Collider2D>().isTrigger = true;
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
        GetComponent<Collider2D>().isTrigger = false;
    }


    void OnTriggerEnter2D(Collider2D other) {
        stopMoving = true;
    }

    void OnTriggerExit2D(Collider2D other) {
        stopMoving = false;
    }

    
    void Update() {
        if (isDragging && !stopMoving) {
            if (horizontal) {
                // TODO: change this to not rely on `point` since the mouse can move past it (we want the block to not be able to)
                Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                float bounds = 1.55f;

                if (transform.position.x < -bounds && point.x - offset < -bounds) {
                    return;
                } else if (transform.position.x > bounds && point.x - offset > bounds) {
                    return;
                }
                point.y = transform.position.y;
                point.x = point.x - offset;
                transform.position = point;
            } else {
                Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                float bounds = 1.55f;

                if (transform.position.y < -bounds && point.y - offset < -bounds) {
                    return;
                } else if (transform.position.y > bounds && point.y - offset > bounds) {
                    return;
                }
                point.x = transform.position.x;
                point.y = point.y - offset;
                transform.position = point;
            }
        }
    }
}