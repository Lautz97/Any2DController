using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
*Copyright(c) 
*Davide "Lautz" Lauterio
*/
public interface IMoveable<velocity, collisionMask> {

    void Move(Vector3 velocity, LayerMask collisionMask);

}
