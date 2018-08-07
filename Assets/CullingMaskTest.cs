using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[RequireComponent(typeof(Camera))]
public class CullingMaskTest : MonoBehaviour
{
    Camera camera;
    int lastCullingMask;
    void Start()
    {
        camera = GetComponent<Camera>();
        lastCullingMask = camera.cullingMask;

        Debug.Log("Everything: " + IntToBin(LayerMask.NameToLayer("Everything")));
        Debug.Log("Default: " + IntToBin(LayerMask.NameToLayer("Default")));
        Debug.Log("_10: " + IntToBin(LayerMask.NameToLayer("_10")));
        Debug.Log("_11: " + IntToBin(LayerMask.NameToLayer("_11")));
        Debug.Log("_12: " + IntToBin(LayerMask.NameToLayer("_12")));
        Debug.Log("Layer _10 is: " + CheckTheMaskIsOn("_10"));
        Debug.Log("Layer _11 is: " + CheckTheMaskIsOn("_11"));
        Debug.Log("Layer _12 is: " + CheckTheMaskIsOn("_12"));

        StudyBitOperation();
    }

    void Update()
    {
        if (lastCullingMask != camera.cullingMask)
        {
            lastCullingMask = camera.cullingMask;
            Debug.Log("cullingMask updated: " + IntToBin(lastCullingMask));
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            if(CheckTheMaskIsOn("_10"))
            {
                DisableCullingMaskByName(camera, "_10");
            }
            else
            {
                EnableCullingMaskByName(camera, "_10");
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (CheckTheMaskIsOn("_11"))
            {
                DisableCullingMaskByName(camera, "_11");
            }
            else
            {
                EnableCullingMaskByName(camera, "_11");
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Layer Reset (Everything)");
            camera.cullingMask = -1;    // -1 は int (= System.Int32) の 32bit すべてが 1で埋まった最大値であり、つまりすべてのレイヤーが ON の状態を意味する。
        }
    }

    void StudyBitOperation()
    {
        Debug.Log("**** CHECK BIT OPERATIONS ****");
        int a = BinToInt("0001");
        int b = BinToInt("1010");
        int c = BinToInt("1011");
        Debug.Log(IntToBin(a) + ": " + a);
        Debug.Log(IntToBin(b) + ": " + b);
        Debug.Log(IntToBin(c) + ": " + c);
        Debug.Log(IntToBin(a) + "&" + IntToBin(b) + " : " + IntToBin(a & b));
        Debug.Log(IntToBin(a) + "&" + IntToBin(c) + " : " + IntToBin(a & c));
        Debug.Log(IntToBin(b) + "&" + IntToBin(c) + " : " + IntToBin(b & c));
        Debug.Log("~ means NOT: ~" + IntToBin(a) + " = " + IntToBin(~a));
        Debug.Log("~ means NOT: ~" + IntToBin(b) + " = " + IntToBin(~b));
        Debug.Log("~ means NOT: ~" + IntToBin(c) + " = " + IntToBin(~c));
        Debug.Log("<< means LEFTSHIFT : 1 << " + a + " = " + IntToBin(1 << a));
        Debug.Log("<< means LEFTSHIFT : 1 << " + b + " = " + IntToBin(1 << b));
        Debug.Log("<< means LEFTSHIFT : 1 << " + c + " = " + IntToBin(1 << c));
        Debug.Log("**** END BIT OPERATIONS ****");
    }

    // Utility
    int BinToInt(string bin)
    {
        return Convert.ToInt16(bin, 2);
    }

    string IntToBin(int _in)
    {
        return Convert.ToString(_in, 2);
    }

    // Mask Funcs
    bool CheckTheMaskIsOn(string layername)
    {
        int layerId = LayerMask.NameToLayer(layername);
        return (camera.cullingMask & (1 << layerId)) != 0;   // 現在のマスクの (layerId+1) bit 目が on なら、その Layer に対するマスクは On.
    }

    void EnableCullingMaskByName(Camera _camera, string layername)
    {
        _camera.cullingMask |= (1 << LayerMask.NameToLayer(layername)); // layer + 1 bit 目を ON にする
    }

    void DisableCullingMaskByName(Camera _camera, string layername)
    {
        _camera.cullingMask &= ~(1 << LayerMask.NameToLayer(layername)); // l;ayer + 1 bit 目のみ OFF にする
    }
}
