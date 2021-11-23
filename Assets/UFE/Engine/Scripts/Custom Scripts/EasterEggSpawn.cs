using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class EasterEggSpawn : MonoBehaviour
{
    [Header ("X Values")]
    public int xVal1 = 1;
    public int xVal2 = 5;
    [Header("Y Values")]
    public int yVal1 = 1;
    public int yVal2 = 5;
    [Header("Z Values")]
    public int zVal1 = 1;
    public int zVal2 = 5;

    [DllImport("EasterEggDLL")]
    private static extern int RandomPosition(int num1, int num2, int randnum);


    // Start is called before the first frame update
    void Start()
    {
        //int x = RandomPosition(xVal1, xVal2, 0);
        //int y = RandomPosition(yVal1, yVal2, 0);
        //int z = RandomPosition(zVal1, zVal2, 0);
        //this.gameObject.transform.localPosition = new Vector3(x, y, z);
    }

    private void Update()
    {
        if(Input.GetMouseButton(1))
        {
            int x = RandomPosition(xVal1, xVal2, 0);
            int y = RandomPosition(yVal1, yVal2, 0);
            int z = RandomPosition(zVal1, zVal2, 0);
            this.gameObject.transform.localPosition = new Vector3(x, y, z);
        }
    }
}
