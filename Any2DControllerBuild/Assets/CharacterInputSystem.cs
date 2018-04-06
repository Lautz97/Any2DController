using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
*Copyright(c) 
*Davide "Lautz" Lauterio
*/
public class CharacterInputSystem : MonoBehaviour {

    public Vector2 Input2DTop()
    {
        return (new Vector2 (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical") ) );
    }
}
