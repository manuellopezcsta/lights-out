using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnManager : MonoBehaviour
{
    [SerializeField] private int spawnOffsetY = 3;
    [SerializeField] private int spawnOffsetX = 1;
    
    levelHandler levelHandler;
    [SerializeField] engine engine;

    // Start is called before the first frame update
    void Start()
    {
        levelHandler = GetComponent<levelHandler>();

        int count = 1;
        // its a 5x5 grid
        for(int i=0; i<5; i++)
        {
            for(int j=0; j<5; j++)
            {
                GameObject tmpGameObject = Instantiate(Resources.Load("Plane", typeof(GameObject))) as GameObject;
                tmpGameObject.transform.position = new Vector3(j*1.5f - spawnOffsetX, i*-1.5f -spawnOffsetY, 0);
                tmpGameObject.name = count.ToString();
                count++;
            }
        }
        //levelHandler.loadLevel(1);
    }

    // Update is called once per frame
    void Update()
    {
        // Cast a Ray where you click, if it hits a game object call the function.
        if(Input.GetMouseButtonUp(0))
        {
            RaycastHit hit = new RaycastHit();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit, 100)){
                makeMove(int.Parse(hit.collider.gameObject.name));
            }
        }
    }

    void makeMove(int name)
    {
        // Se afecta a si mismo
        turn(name);
        // Al de abajo
        turn(name + 5);
        // Al de arriba
        turn(name - 5);
        // Casos especiales

        // Si no esta en el borde derecho
        if(name % 5 != 0)
        {
            turn(name + 1);
        }
        // Si no es 1,6,11,16 (porque estos divididos a 5, te sobra 1)
        if(name % 5 != 1)
        {
            turn(name - 1);
        }
        checkIfFinished();
    }

    void turn(int name)
    {
        engine.PlayClick();
        // Esto es para que en los casos de los bordes no tire error tratando de pintar cubos que no existen.
        if(name < 1 || name > 25)
        {
            return;
        }
        GameObject turnObj = GameObject.Find(name.ToString()).gameObject;
        turnObj.GetComponent<lightSwitch>().change();
    }

    void checkIfFinished()
    {
        // Cada vez q se realiza un turno, aumentamos el numero de movimientos.
        gameObject.GetComponent<engine>().nrOfMoves++;
        // Hacemos un for loop para verificar la condicion de victoria.
        int offLightsTotal = 0;

        for(int i=1; i<26; i++)
        {
            //Debug.Log("Checking Light N" + i);
            if(GameObject.Find(i.ToString()).GetComponent<lightSwitch>().isOn)
            {
                // Si hay alguna luz prendida, nos salimos de la funcion xq no se termino el juego.
                return;
            } else{
                offLightsTotal++;
            }
            if(offLightsTotal == 25)
            {
                // Si estan todas off le decimos al engine q se termino ese nivel.
                //Debug.Log("Checking finished, todas off");
                gameObject.GetComponent<engine>().gameFinished();
                return;                
            }
        }        
    }
}
