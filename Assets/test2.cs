using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test2 : MonoBehaviour
{
    [SerializeField] private GameObject obj2;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log(MyMath.GetAngleBetweenObjects(gameObject.transform, obj2.transform));
        }
    }
}
