using PlasticPipe.PlasticProtocol.Messages;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.Oculus;
using UnityEditorInternal;
using UnityEngine;

public class camera
{
    private List<boundary> camBoundaries = new List<boundary>();
    private Vector3 position;
    private Vector3 angle;
    private bool locked = false;

    // TODO: Make camera settings changable
    private float camSpeed = 0.055f;
    private float camSensitivity = 0.45f;

    public camera(Vector3 pos, Vector3 ang)
    {
        this.position = pos;
        this.angle = ang;
    }
    public Vector3 getPos()
    {
        return position;
    }
    public void setPos(Vector3 pos)
    {
        this.position = pos;
    }
    public Vector3 getAng()
    {
        return angle;
    }
    public void setAng(Vector3 ang)
    {
        this.angle = ang;
    }

    public void addBoundaries(boundary boundary)
    {
        camBoundaries.Add(boundary);
    }

    public List<boundary> getBoundaries()
    {
        return camBoundaries;
    }

    public float getSpeed()
    {
        return camSpeed;
    }
    public float getSensitivity()
    {
        return camSensitivity;
    }
    public bool getLock()
    {
        return locked;
    }
    public void changeLock()
    {
        locked = !locked;
    }
}
