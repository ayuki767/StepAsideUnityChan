using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private GameObject cameraObject;

    // Start is called before the first frame update
    void Start()
    {
        cameraObject = Camera.main.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(cameraObject.transform.position.z > gameObject.transform.position.z)
        {
            Debug.Log("Destroy(" + gameObject.name + ")");
            Destroy(gameObject);
        }
        
    }
}
