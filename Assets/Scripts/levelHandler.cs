using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;


public class levelHandler : MonoBehaviour
{
    XmlDocument levelDoc;
    XmlNodeList levelList;
    List<string> levelArray; // Donde guardamos la data despues de cargarla del xml.

    void Start()
    {
        levelArray = new List<string>();
        levelDoc = new XmlDocument();

        // Cargamos el XML desde recursos
        TextAsset xmlFile = Resources.Load("levels", typeof(TextAsset)) as TextAsset;
        // Lo pasamos a texto ???
        levelDoc.LoadXml(xmlFile.text);
        // Cargamos los niveles buscandolos por su tag en el XML.
        levelList = levelDoc.GetElementsByTagName("level");

        // Cargamos los datos si existen al levelArray.
        foreach(XmlNode levelData in levelList)
        {
            XmlNodeList levelInfo = levelData.ChildNodes;
            foreach(XmlNode data in levelInfo)
            {
                if(data.Name == "setup")
                {
                    levelArray.Add(data.InnerText);
                    //Debug.Log("Data Added");
                }
            }
        }
        this.gameObject.GetComponent<engine>().init(levelArray.Count);
    }

    public void loadLevel(int nr) // Toma como arg, el numero de lv.
    {
        GameObject.Find("Canvas").GetComponent<menu>().SetLevelText(nr.ToString());
        //Debug.Log(levelArray);
        // Buscamos el nivel correspondiente en el array, y separamos todos los numeros de los cubos a prender en un nuevo array.
        string[] levSting = levelArray[nr -1].Split(',');
        foreach(string brick in levSting)
        {
            GameObject.Find(brick).GetComponent<lightSwitch>().change();
        }
    }
}
