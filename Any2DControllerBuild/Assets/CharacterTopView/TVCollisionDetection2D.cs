using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
*Copyright(c) 
*Davide "Lautz" Lauterio
*/
public class TVCollisionDetection2D : MonoBehaviour, ICollisionDetection<Vector3,LayerMask> {
    //DEBUG
    [SerializeField]
    bool debug = true;
    //se settata = true verranno visualizzati i "debug ray"

    ///LayerMask a cui è assegnato l'oggetto con cui si vuole rilevare la collisione
    ///public LayerMask collisionMask;                     

    //spessore della pelle: il raycast se partisse dal bordo potrebbe mal interpretare l'input
    const float skinWidth = 0.015f;

    //numero di raycast da sx e dx
    [SerializeField]
    int horizontalRayCount = 4;
    //numero di raycast da top e bot    
    [SerializeField]
    int verticalRayCount = 4;                    

    //variabile in cui verrà memorizzato quanto spazio è stato calcolato tra raggio e raggio per sx e dx
    float horizontalRaySpacing;                         
    //variabile in cui verrà memorizzato quanto spazio è stato calcolato tra raggio e raggio per top e bot
    float verticalRaySpacing;                           

    //un riferimento al component "boxCollider2D"
    BoxCollider2D bc;                                   

    //riferimento alla struct instanziata per tenere in memoria gli angoli del boxCollider
    RaycastOrigin rcOrigin;                                                                     

    void Awake () {
        //Memorizza l'istanza del collisore
        bc = GetComponent<BoxCollider2D>();

        //funzione che si occupa di riempire le variabili "horizontalRayCount" e "verticalRayCount"
        CalculateRaySpacing();                                                                  
    }

    //metodo che ricevendo il vettore "velocity" e la maschera di collisione rileva le collisioni sull'asse verticale ed in caso di collisione "ferma" l'oggetto
    public void VerticalCollision(ref Vector3 velocity, LayerMask collisionMask) {
        
		float directionY = Mathf.Sign (velocity.y);
		float rayLenght = Mathf.Abs (velocity.y) + skinWidth;
		float newVel = 0;

		for (int i = 0; i < verticalRayCount; i++) {
			
			Vector2 rayOrigin = (directionY == -1) ? rcOrigin.bottomLeft : rcOrigin.topLeft;

			rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x);

			RaycastHit2D hit = Physics2D.Raycast (rayOrigin, Vector2.up * directionY, rayLenght, collisionMask);

			if (debug) Debug.DrawRay (rayOrigin, Vector2.up * directionY * rayLenght, Color.red);

			if (hit) 
			{
				newVel = (hit.distance - skinWidth);
				velocity.y = (newVel < 0 ? 0 : newVel) * directionY;
				rayLenght = hit.distance;
			}
		}
    }

    //metodo che ricevendo il vettore "velocity" e la maschera di collisione rileva le collisioni sull'asse orizzontale ed in caso di collisione "ferma" l'oggetto
    public void HorizontalCollision(ref Vector3 velocity, LayerMask collisionMask) {

        float directionX = Mathf.Sign(velocity.x);
        float rayLenght = Mathf.Abs(velocity.x) + skinWidth;
        float newvel = 0;

        for (int i = 0; i < horizontalRayCount; i++) {
            Vector2 rayOrigin = (directionX == -1) ? rcOrigin.bottomLeft : rcOrigin.bottomRight;

            rayOrigin += Vector2.up * (horizontalRaySpacing * i);

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLenght, collisionMask);

            if (debug) Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLenght, Color.red);

            if (hit) {
                newvel = hit.distance - skinWidth;
                velocity.x = (newvel < 0 ? 0 : newvel) * directionX;
                rayLenght = hit.distance;
            }
        }
    }

    struct RaycastOrigin {
        public Vector2  topLeft,
                        topRight, 
                        bottomLeft, 
                        bottomRight;
    }

    //funzione che si occupa di riempire le variabili "horizontalRayCount" e "verticalRayCount"
    void CalculateRaySpacing() {
        Bounds bounds = bc.bounds;
        bounds.Expand(skinWidth * -2);

        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);

        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }

    //definisce gli angoli del box collider
    public void UpdateRcOrigin() {
        Bounds bounds = bc.bounds;
        bounds.Expand(skinWidth * -2);

        rcOrigin.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        rcOrigin.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        rcOrigin.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        rcOrigin.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

}
