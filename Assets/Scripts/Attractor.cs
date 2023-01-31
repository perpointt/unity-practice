using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    public static Vector3 Pos = Vector3.zero;

    [Header("Set in Inspector")]
    public float radius =10;
    public float xPhase = 0.5f; 
    public float yPhase = 0.4f;
    public float zPhase = 0.1f;
        
        
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // FixedUpdate вызывается при каждом пересчете физики (50 раз в секунду)
    void FixedUpdate () {
        Vector3 tPos = Vector3.zero;
        Vector3 scale = transform.localScale;
        tPos.x = Mathf.Sin(xPhase * Time.time) * radius * scale.x; 
        tPos.y = Mathf.Sin(yPhase * Time.time) * radius * scale.y; 
        tPos.z = Mathf.Sin(zPhase * Time.time) * radius * scale.z; 
        transform.position = tPos;
        Pos = tPos;
    }
}
