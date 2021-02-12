using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    private bool firing;
    public LayerMask layerE;

    public void OnFire(InputAction.CallbackContext context)
    {
        firing = context.performed;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (firing)
        {
            firing = false;
            RaycastHit hit;
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //Get the cursor position in screen (Reference 1)
            //if (Physics.Raycast(ray.origin, Vector3.forward, out hit, Mathf.Infinity, layerE))
            //{
            //    Debug.DrawRay(ray.origin, Vector3.forward * hit.distance, Color.green);
            //    if (hit.collider.gameObject.layer == 7)
            //    {
            //        Destroy(hit.collider.gameObject);
            //    }
            //}
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerE))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
                if (hit.collider.tag == "Enemy")
                {
                    Destroy(hit.collider.gameObject);
                }
            }

        }
    }
}

//Reference
//1 - https://docs.unity3d.com/ScriptReference/Input-mousePosition.html