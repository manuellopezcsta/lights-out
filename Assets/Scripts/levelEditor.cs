using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; // Para escribir al editor de texto.

public class levelEditor : MonoBehaviour
{
    public string levlNumber;
    string runtimeLevels;
    void Start()
    {
        
    }

    public void setLvName(string nr)
    {
        levlNumber = nr;
    }

    public void clearButton()
    {
        for(int i=1; i<26; i++)
        {
            GameObject tmpObject = GameObject.Find(i.ToString());
            if(tmpObject.GetComponent<lightSwitch>().isOn) // If this light is on
            {
                tmpObject.GetComponent<lightSwitch>().change();
            }
        }        
    }


    public void saveButton()
    {
        string levelString = "";
        // If they are on, we save them.

        for(int i=1; i<26; i++)
        {
            GameObject tmpObject = GameObject.Find(i.ToString());
            if(tmpObject.GetComponent<lightSwitch>().isOn) // If this light is on
            {
                if(levelString.Length == 0)
                {
                    levelString = i.ToString();
                } else{
                    levelString += "," + i;
                }
            }
        }

        // Ya que genere la data y la guarde en el string, ahora la pasamos al xml.
            runtimeLevels += 
                             "\n\n" + 
                             "<level>" + "\n" +
                             "<levelname>" + levlNumber + "</levelname>" + "\n" +
                             "<setup>" + levelString + "</setup>" + "\n" +
                             "</level>";
            System.IO.File.WriteAllText("D:/Documentos/Proyectos Unity/Lights Out Mobile/Assets/Resources/editor.txt", runtimeLevels);        
    }
}


