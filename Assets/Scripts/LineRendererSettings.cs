using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineRendererSettings : MonoBehaviour
{
    [SerializeField] LineRenderer rend;

    Vector3[] points;

    void Start()
    {
        rend = gameObject.GetComponent<LineRenderer>();
        img = panel.GetComponent<Image>();
        points = new Vector3[2];

        points[0]= Vector3.zero;
        points[1] = transform.position + new Vector3(0, 0, 20);

        rend.SetPositions(points);
        rend.enabled = true;

    }


    public LayerMask layerMask;

    public bool AlignLineRenderer(LineRenderer rend)
    {
        Ray ray;
        ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        bool hitbtn = false;
        if (Physics.Raycast(ray, out hit, layerMask))
        {
            hitbtn = true;

            points[1]=transform.forward+new Vector3(0,0,hit.distance);
            rend.startColor = Color.red;
            rend.endColor = Color.red;
            btn = hit.collider.gameObject.GetComponent<Button>();

        }
        else
        {
            hitbtn = false;

            points[1] = transform.forward + new Vector3(0, 0, 20);
            rend.startColor = Color.green;
            rend.endColor = Color.green;

        }

        rend.SetPositions(points);
        rend.material.color = rend.startColor;
    }

    private void Update()
    {
        AlignLineRenderer(rend);

    }
    public GameObject panel;
    public Image img;
    public Button btn;

    public void ColorChangeOnClick()
    {

        if(btn!=null)
        {
            if (btn.name == "play_btn")
            {
                img.color = Color.green;
                
            }
        }
    }
}

