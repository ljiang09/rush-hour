using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Car4x4 : MonoBehaviour
{
    // pos_length is for the 2 available positions along the long side of the car
    private float[] pos_length = new float[] {-1.84f, 0.0f, 1.84f};

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


    // variables for counting moves
    private Vector2 prev_pos;

    [SerializeField]
    private Text moves;   // attach the canvas element here


    void Awake() {
        overlay.SetActive(false);

        prev_pos = transform.position;

        midpoint = (pos_length[2]+pos_length[1])/2;

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
        StartCoroutine(CheckPosition());

        // check if goal car is past the right wall bound (aka success)
        if (goalCar) {
            Vector2 pos = transform.position;
            if (pos.x > pos_length[2]*2) {
                isDragging = false;
                GetComponent<Collider2D>().isTrigger = false;

                overlay.SetActive(true);

                return;
            }
        }

        // snap to place
        if (horizontal) {
            Vector2 pos = transform.position;
            if (pos.x < -midpoint) {
                pos.x = pos_length[0];
            } else if (pos.x < midpoint) {
                pos.x = pos_length[1];
            } else {
                pos.x = pos_length[2];
            }
            transform.position = pos;
        }
        else {
            Vector2 pos = transform.position;
            if (pos.y < -midpoint) {
                pos.y = pos_length[0];
            } else if (pos.y < midpoint) {
                pos.y = pos_length[1];
            } else {
                pos.y = pos_length[2];
            }
            transform.position = pos;
        }

        isDragging = false;
        GetComponent<Collider2D>().isTrigger = false;
    }

    // wait 10 frames for the car to snap into place, then check position
    IEnumerator CheckPosition() {
        for (int i = 0; i < 10; i++) {
            yield return null;
        }

        // check if the car has moved. if so, update #moves
        if (transform.position.x != prev_pos.x || transform.position.y != prev_pos.y) {
            int curr_moves = System.Int32.Parse(moves.text);  // parse as int
            curr_moves++;
            moves.text = curr_moves.ToString();
            prev_pos = transform.position;
        }
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
                float bounds = pos_length[2];

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
                float bounds = pos_length[2];

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
