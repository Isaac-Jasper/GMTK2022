using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTexture : MonoBehaviour
{
    [SerializeField]
    private MeshFilter cubeMesh;
    [SerializeField]
    private Mesh mesh;

    private void Start() {
        mesh = cubeMesh.mesh;
        Vector2[] uvMap = mesh.uv;

        //front
        uvMap[0] = new Vector2(0, 0);
        uvMap[1] = new Vector2(1 / 3f, 0);
        uvMap[2] = new Vector2(0, 1/3f);
        uvMap[3] = new Vector2(1 / 3f, 1 / 3f);

        //top
        uvMap[4] = new Vector2(1 / 3f, 1 / 3f);
        uvMap[5] = new Vector2(2 / 3f, 1 / 3f);
        uvMap[8] = new Vector2(1 / 3f, 0);
        uvMap[9] = new Vector2(2 / 3f, 0);

        //back
        uvMap[6] = new Vector2(1, 0);
        uvMap[7] = new Vector2(2 / 3f, 0);
        uvMap[10] = new Vector2(1, 1 / 3f);
        uvMap[11] = new Vector2(2 / 3f, 1 / 3f);

        //bottom
        uvMap[12] = new Vector2(0, 1 / 3f);
        uvMap[13] = new Vector2(0, 2 / 3f);
        uvMap[14] = new Vector2(1 / 3f, 2 / 3f);
        uvMap[15] = new Vector2(1 / 3f, 1 / 3f);

        //left
        uvMap[16] = new Vector2(1 / 3f, 1 / 3f);
        uvMap[17] = new Vector2(1 / 3f, 2 / 3f);
        uvMap[18] = new Vector2(2 / 3f, 2 / 3f);
        uvMap[19] = new Vector2(2 / 3f, 1 / 3f);

        //right
        uvMap[20] = new Vector2(2 / 3f, 1 / 3f);
        uvMap[21] = new Vector2(2 / 3f, 2 / 3f);
        uvMap[22] = new Vector2(1, 2 / 3f);
        uvMap[23] = new Vector2(1, 1 / 3f);

        mesh.uv = uvMap;
    }
}
