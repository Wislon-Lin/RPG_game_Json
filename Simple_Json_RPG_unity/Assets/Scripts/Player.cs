using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public string Name;
    public int Money;
    public int Exp;
    public int HP;
    public void Save()
    {
        JSONObject playerJson = new JSONObject();
        playerJson.Add("Name",Name);
        playerJson.Add("Money", Money);
        playerJson.Add("Exp", Exp);
        playerJson.Add("HP", HP);

        JSONArray position = new JSONArray();
        position.Add(transform.position.x);
        position.Add(transform.position.y);
        position.Add(transform.position.z);

        JSONArray rotation = new JSONArray();
        rotation.Add(this.gameObject.transform.localEulerAngles.x);
        rotation.Add(this.gameObject.transform.localEulerAngles.y);
        rotation.Add(this.gameObject.transform.localEulerAngles.z);

        playerJson.Add("Position",position);
        playerJson.Add("Rotation", rotation);

        string path = Application.persistentDataPath + "/PlayerSave.json";
        File.WriteAllText(path,playerJson.ToString());
       
       
    }
    public void Load()
    {
        string path = Application.persistentDataPath + "/PlayerSave.json";
        string jsonString = File.ReadAllText(path);
        JSONObject playerJson = (JSONObject)JSON.Parse(jsonString);

        Name = playerJson["Name"];
        Money = playerJson["Money"];
        Exp = playerJson["Exp"];
        HP = playerJson["HP"];

        transform.position = new Vector3
            (
            playerJson["Position"].AsArray[0],
            playerJson["Position"].AsArray[1],
            playerJson["Position"].AsArray[2]
            );
       
        transform.Rotate(
            playerJson["Rotation"].AsArray[0],
            playerJson["Rotation"].AsArray[1],
            playerJson["Rotation"].AsArray[2]
		);

    }

    void Start ()
    {
        Player_Animator = this.GetComponent<Animator>();

    }

    private Animator Player_Animator;
    public Text MoneyText;
    public void IncreasMoney()
    {
        Money+=10;
    }
    public void DecreaseMoney()
    {
        Money-=10;
    }


    void Update ()
    {
        MoneyText.text = Money.ToString();

        if (Input.GetKey("up"))
        {
            Player_Animator.SetBool("walk",true);
            transform.Translate(0, 0, 0.5f);
        }
        if (Input.GetKey("down"))
        {
            Player_Animator.SetBool("walk", true);
            Player_Animator.SetBool("idel", false);
            transform.Translate(0, 0, -0.5f);
        }
        if (Input.GetKey("left"))
        {
            Player_Animator.SetBool("walk", true);
            Player_Animator.SetBool("idel", false);
            transform.Rotate(0, -3, 0);
        }
        if (Input.GetKey("right"))
        {
            Player_Animator.SetBool("walk", true);
            Player_Animator.SetBool("idel", false);
            transform.Rotate(0, 3, 0);
        }
        if (!Input.anyKey)
        {
            Player_Animator.SetBool("walk", false);
            Player_Animator.SetBool("idel", true);
        }    

    }

}
