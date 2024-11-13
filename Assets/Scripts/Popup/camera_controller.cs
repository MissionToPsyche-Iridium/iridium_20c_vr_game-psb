
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using System;
using UnityEngine;

public class camera_controller
{
    private static camera_controller cameraC = new camera_controller();
    private camera officeCam;
    private camera conferenceCam;
    private camera spaceCam;
    private camera currentCam;
    private camera_controller()
    {
        // Create camera objects to save data for each room's camera
        this.officeCam = new camera(new Vector3(7.01f, 58f, 40.1f), new Vector3(0f, 132.93f, 0f));
        this.conferenceCam = new camera(new Vector3(12.92f, 5.51f, 5.37f), new Vector3(0f, 240.434f, 0f));
        this.spaceCam = new camera(new Vector3(0f, -0.554f, 0f), new Vector3(0f, 0f, 0f));
        this.currentCam = officeCam;
    }
    public static camera_controller getInstance()
    {
        return cameraC;
    }
    public bool pathBlocked(Vector3 nextPos)
    {//checks if the camera can go that direction
        foreach (boundary b in currentCam.getBoundaries())
        {
            float[] bounds = b.getBoundaries();
            float minZ = (float)Math.Round(bounds[0] - b.getBuffer(), 2);
            float maxZ = (float)Math.Round(bounds[1] + b.getBuffer(), 2);
            float minX = (float)Math.Round(bounds[2] - b.getBuffer(), 2);
            float maxX = (float)Math.Round(bounds[3] + b.getBuffer(), 2);
            if ((nextPos.z > minZ && nextPos.z < maxZ) && (nextPos.x > minX && nextPos.x < maxX))
            {
                return true;
            }
        }
        return false;
    }
    public void setBoundaries(float[] horzMinMax, float buffer, string name)
    {
        currentCam.addBoundaries(new boundary(horzMinMax, buffer, name));
    }

    public void setCamera(string sceneName)
    {
        if (sceneName == "Office")
        {
            currentCam = officeCam;
        }
        else if (sceneName == "ConferenceRoom")
        {
            currentCam = conferenceCam;
        }
        else if (sceneName == "Space Explorer")
        {
            currentCam = spaceCam;
        }
    }

    public Vector3 getCamPos()
    {
        return currentCam.getPos();
    }
    public void saveCamPos(Vector3 pos)
    {
        currentCam.setPos(pos);
    }
    public Vector3 getCamAng()
    {
        return currentCam.getAng();
    }
    public void savecamAng(Vector3 ang)
    {
        currentCam.setAng(ang);
    }
    public float getSpeed()
    {
        return currentCam.getSpeed();
    }
    public float getSensitivity()
    {
        return currentCam.getSensitivity();
    }
    public bool getLock()
    {
        return currentCam.getLock();
    }
    public void changeLock()
    {
        currentCam.changeLock();
    }

}
