using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car6x6 : MonoBehaviour
{
    // `positions` is for the 2 available positions along the long side of the car
    private float[] positions = new float[] { -2.92f, -1.75f, -0.583f, 0.583f, 1.75f, 2.92f };

    private float midpoint;

    // if true, the car moves horizontally. otherwise, vertical
    public bool horizontal;

    // if goalCar, it can bypass the right wall
    public bool goalCar;

    [SerializeField]
    private GameObject overlay;
    
    private bool isDragging = false;
    private bool stopMoving = false;    // true to stop moving. helps with "collisions"
    private float offset = 0.0f;

    void Awake() {
        overlay.SetActive(false);

        // half the distance between points
        midpoint = (positions[2]-positions[1])/2;

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
        // check if goal car is past the right wall bound (aka success)
        if (goalCar) {
            Vector2 pos = transform.position;
            if (pos.x > positions[positions.Length-1]+midpoint) {
                isDragging = false;
                GetComponent<Collider2D>().isTrigger = false;

                overlay.SetActive(true);

                return;
            }
        }

        // snap to place
        if (horizontal) {
            Vector2 pos = transform.position;
            if (pos.x < positions[0]+midpoint) {
                pos.x = positions[0];
            } else if (pos.x < positions[1]+midpoint) {
                pos.x = positions[1];
            } else if (pos.x < positions[2]+midpoint) {
                pos.x = positions[2];
            } else if (pos.x < positions[3]+midpoint) {
                pos.x = positions[3];
            } else if (pos.x < positions[4]+midpoint) {
                pos.x = positions[4];
            } else {
                pos.x = positions[5];
            }
            transform.position = pos;
        }
        else {
            Vector2 pos = transform.position;
            if (pos.y < positions[0]+midpoint) {
                pos.y = positions[0];
            } else if (pos.y < positions[1]+midpoint) {
                pos.y = positions[1];
            } else if (pos.y < positions[2]+midpoint) {
                pos.y = positions[2];
            } else if (pos.y < positions[3]+midpoint) {
                pos.y = positions[3];
            } else if (pos.y < positions[4]+midpoint) {
                pos.y = positions[4];
            } else {
                pos.y = positions[5];
            }
            transform.position = pos;
        }

        isDragging = false;
        GetComponent<Collider2D>().isTrigger = false;

        Debug.Log(transform.position.x);
        Debug.Log(transform.position.y);
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
                float bounds = positions[positions.Length-1];

                // left wall, right wall
                if (transform.position.x < -bounds && point.x - offset < -bounds) {
                    return;
                } else if (!goalCar && transform.position.x > bounds && point.x - offset > bounds) {
                    return;
                }
                point.y = transform.position.y;
                point.x = point.x - offset;
                transform.position = point;
            } else {
                Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                float bounds = positions[positions.Length-1];

                // bottom wall, top wall
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
