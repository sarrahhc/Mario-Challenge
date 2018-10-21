using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;
    public float maxX;
    public float maxY;
    public float minX;
    public float minY;

	void Start () {	
	}
	
	void LateUpdate () {
        Vector3 newPosition = new Vector3 (player.transform.position.x, player.transform.position.y, transform.position.z);
        newPosition.x = Mathf.Min(Mathf.Max(newPosition.x, minX), maxX);
        newPosition.y = Mathf.Min(Mathf.Max(newPosition.y, minY), maxY);
        transform.position = newPosition;
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
