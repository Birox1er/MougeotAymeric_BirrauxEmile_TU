using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackHeap : MonoBehaviour
{

    private void Start()
    {
        

        Vector3 position = new Vector3(1, 2, 3);

        Debug.Log($" ma valeur avant {position}");
        Function1(ref position);
        Debug.Log($" ma valeur avant {position}");
    }

    void Function1(ref Vector3 param)
    {
        param.Set(0,0,0);
        Debug.Log($" ma valeur {param}");

        //string name = "Hello";
        //float count = 3.5f;
        //Function2();
    }

    void Function2()
    {
        Debug.Log("World");
    }
}
