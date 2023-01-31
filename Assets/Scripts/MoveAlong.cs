using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAlong : MonoBehaviour
{
    private CountItHigher cih;
    // Start is called before the first frame update
    void Start()
    { 
        cih = gameObject.GetComponent<CountItHigher>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        if (cih != null)
        {
            var tX = cih.currentNum/10f;
            var tempLoc = pos;
            tempLoc.x = tX;
            pos = tempLoc;
        }

    }
    
    public Vector3 pos
    {
        get => transform.position;
        set => transform.position = value;
    }
}
