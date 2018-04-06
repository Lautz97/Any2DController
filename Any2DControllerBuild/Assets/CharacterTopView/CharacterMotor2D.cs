using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
*Copyright(c) 
*Davide "Lautz" Lauterio
*/
public class CharacterMotor2D : MonoBehaviour {

    //maniglia del component
    CollisionDetection2D cd;

    private void Awake()
    {
        //collegamento del component alla maniglia
        cd = this.GetComponent<CollisionDetection2D>();
    }

    public void Move(Vector3 velocity, LayerMask collisionMask) {
        //aggiorna la posizione delle origini dei raggi
        cd.UpdateRcOrigin();

        if (velocity.y != 0)
        {
            //se voglio muovermi sull'asse verticale controllo le collisioni
            cd.VerticalCollisions(ref velocity, collisionMask);
        }

        if (velocity.x != 0)
        {
            //se voglio muovermi sull'asse orizzontale controllo le collisioni
            cd.HorizontalCollisions(ref velocity, collisionMask);
        }

        //con le velocity aggiornate eseguo il movimento se possibile
        transform.Translate(velocity);
    }
}
