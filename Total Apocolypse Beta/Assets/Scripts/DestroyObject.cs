using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class destroys the fire object after a period of time
public class DestroyObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 8); // destroy particle after X seconds
    }
}
