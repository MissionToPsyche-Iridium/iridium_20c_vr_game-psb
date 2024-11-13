using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boundary
{
    /* Class used to store data boundary data for scene objects
     * This data will prevent the camera from going horizontally
     * in bounds of the objects
     */
    private string objectname;
    private float minZ;
    private float maxZ;
    private float minX;
    private float maxX;
    private float buffer;

    public boundary(float[] horizontal_boundaries, float buffer, string name)
    {
        this.minZ = horizontal_boundaries[0];
        this.maxZ = horizontal_boundaries[1];
        this.minX = horizontal_boundaries[2];
        this.maxX = horizontal_boundaries[3];

        this.buffer = buffer;
        this.objectname = name;
    }

    public float[] getBoundaries()
    {
        return new float[] { minZ, maxZ, minX, maxX };
    }
    public float getBuffer()
    {
        return buffer;
    }
}
