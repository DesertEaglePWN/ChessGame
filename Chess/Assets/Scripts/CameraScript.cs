using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {
    Vector3 CounterClockwise = new Vector3(0, -1, 0);
    Vector3 Clockwise = new Vector3(0, 1, 0);
    private float localY;

    [SerializeField]
    private float minCameraZoom;

    [SerializeField]
    private float maxCameraZoom;
    
    
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(2) || (Input.GetMouseButton(0) && Input.GetMouseButton(1)))
        {
            if (Input.GetAxis("Mouse X") > 0)
            {
                this.transform.parent.Rotate(CounterClockwise * Time.deltaTime * 50);

            }
            else if (Input.GetAxis("Mouse X") < 0)
            {
                this.transform.parent.Rotate(Clockwise * Time.deltaTime * 50);
            }
        
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0) 
        {
            localY = this.transform.localPosition.y;
            if (localY > minCameraZoom){
                this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y - 1, this.transform.localPosition.z + 1);
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            localY = this.transform.localPosition.y;
            if (localY < maxCameraZoom)
            {
                this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y + 1, this.transform.localPosition.z - 1);
            }
        }
	}


}
