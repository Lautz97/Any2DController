using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
*Copyright(c) 
*Davide "Lautz" Lauterio
*/
public class TVCharacterEngine : MonoBehaviour
{

    Vector3 velocity;
    
    //maniglia del component responsabile del movimento
    TVCharacterMotor2D motor;

    //maniglia del component responsabile dell'input
    TVCharacterInputSystem input;

    //cosa è definito come collisione
    public LayerMask collisionMask;

    //velocità di movimento del player
    [SerializeField]
    float stepSpeed;

    void Awake()
    {
        motor = GetComponent<TVCharacterMotor2D>();
        input = GetComponent<TVCharacterInputSystem>();
    }

    void Update() {

        //richiede l'input dal sistema di gestione input e lo manda al motore con la maschera di collisione
        velocity.x = input.Input2DTop().x * stepSpeed;
        velocity.y = input.Input2DTop().y * stepSpeed;
        motor.Move(velocity * Time.deltaTime, collisionMask);
    }

}
