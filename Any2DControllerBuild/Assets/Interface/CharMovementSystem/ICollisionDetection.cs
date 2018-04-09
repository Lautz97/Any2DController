using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
*Copyright(c) 
*Davide "Lautz" Lauterio
*/
public interface ICollisionDetection<velocity, collisionMask> {

    void VerticalCollision(ref Vector3 velocity, LayerMask collisionMask);

    void HorizontalCollision(ref Vector3 velocity, LayerMask collisionMask);

    void UpdateRcOrigin();

}
