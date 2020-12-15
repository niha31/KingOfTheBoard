using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject cameraObj;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator SetCameraToPlayerPos(float x, float y)
    {
        yield return new WaitForSeconds(0.5f);
        cameraObj.transform.position = new Vector3(x, y, -10);
        Debug.Log(Camera.main.transform.position);
        Debug.Log("move camera");
    }


}
