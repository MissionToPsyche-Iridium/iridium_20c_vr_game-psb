using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] LineRenderer rend;

    Vector3[] points;

    void Start()
    {
        rend = GameObject.GetComponent<LineRenderer>();

        points = new Vector3[2];

        points[0]= Vector3.zero;
        points[1] = transform.position + new Vector3(0, 0, 20);

        rend.SetPositions(points);
        rend.enabled = true;

    }


    public LayerMask layerMask;

    public void AlignLineRenderer(LineRenderer rend)
    {
        Ray ray;
        ray = new Ray(transfrom.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, layerMask))
        {
            points[1]=transform.forward+new Vector3(0,0,hit.distance);       
        }
        else
        {
            points[1] = transform.forward + new Vector3(0, 0, 20);

        }

        rend.SetPositions(points);

    }

    private void Update()
    {
        AlignLineRenderer(rend);

    }

}

