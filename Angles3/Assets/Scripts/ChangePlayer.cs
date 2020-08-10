using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChangePlayer : MonoBehaviour
{

    public CheckerBoard checkerboard;

    void Start()
    {


    }


    void Update()
    {

        if (checkerboard.isWhite) GetComponentInChildren<Text>().color = Color.white;
        else GetComponentInChildren<Text>().color = Color.black;



    }
}

