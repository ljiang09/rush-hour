using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LevelManager : MonoBehaviour
{
    // score is the text object. counter is the code's version to easily keep track
    // public Text score;
    // private int counter = 0;

    // private GameObject currentCar;
    // private Vector2 carPos;  // for checking position before dragging, and comparing to position after

    // private int timer;  // basic counter that increments every frame

    // private Camera m_Camera;

    // void Awake()
    // {
    //     m_Camera = Camera.main;
    // }

    public void ChangeScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }


    // void Update() {
    //     if (Input.GetMouseButtonDown(0) && timer != 0) {
    //         Debug.Log("Mouse button down");
    //         timer = 0;

    //         var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //         RaycastHit hit;

    //         if (Physics.Raycast(ray, out hit, 100)) {
    //             Debug.Log(hit.collider.tag);
    //             if (hit.collider.tag == "Car") {
    //                 // get the current car being clicked
    //                 Debug.Log("Found car");
    //                 currentCar = hit.collider.gameObject;
    //                 carPos = currentCar.transform.position;
    //             }
    //         }
    //     } else if (timer == 200) {
    //         Debug.Log("timer reached 200!");

    //         Vector2 currentPos = new Vector2();
    //         currentPos.x = currentCar.transform.position.x;
    //         currentPos.y = currentCar.transform.position.y;
    //         if (currentPos == carPos) {
    //             Debug.Log("Didn't move.");
    //         } else {
    //             counter++;
    //             score.text = counter.ToString();
    //         }
    //     } else {
    //         timer++;
    //     }
    // }
}
