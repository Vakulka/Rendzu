using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class Controller : MonoBehaviour {

    private byte[,] field;

    private List<Button> buttons = new List<Button>();

    public Transform button;
    public Vector2 sizeOfCell = new Vector2(70, 70);

    public Sprite MarkX;
    public Sprite MarkO;
    public Sprite MarkEmpty;

	// Use this for initialization
	void Start () {
        field = new byte[10, 10];

        for (int i = 0; i < field.GetLength(0); i++)
            for (int j = 0; j < field.GetLength(1); j++)
                field[i, j] = 0;

        //Canvas cc = GetComponent<Canvas>();
        //Vector2 size = new Vector2(70, 70);
        //Vector2 startPoint = new Vector2(cc.GetComponent<RectTransform>().rect.xMin, cc.GetComponent<RectTransform>().rect.yMax);
        //for (int i = 0; i < field.GetLength(0); i++)
        //    for (int j = 0; j < field.GetLength(1); j++)
        //    {
        //        buttons.Add((Component)Instantiate(button, new Vector3(i * size.x, j * size.y,0),new Quaternion()));
        //        //GUI.Button(new Rect(startPoint + new Vector2(i * size.x, j * size.y), size), "Button " + i + ":" + j);
        //    }

        Canvas canvas = GetComponent<Canvas>();
        Vector2 size = sizeOfCell;
        Vector2 startPoint = new Vector2(0, 0);
        for (int i = 0; i < 16; i++) {
            for (int j = 0; j <12; j++) {
                Transform b = (Transform)Instantiate(button, new Vector3(startPoint.x + i * size.x + size.x / 2, startPoint.y + j * size.y + size.y / 2, 0), Quaternion.identity);
                RectTransform rect = b.GetComponent<RectTransform>();
                rect.sizeDelta = size;

                Button tempButton = b.gameObject.GetComponent<Button>();
                tempButton.onClick.AddListener(() => { buttonOfFieldClick(tempButton); });
                tempButton.name = i + ";" + j;
                
                
                buttons.Add(tempButton);
                b.SetParent(transform);
                //canvas.gameObject.AddComponent(Button);
                //canvas.AddComponent(
                    
            }
        }

	}
    private void buttonOfFieldClick(object sender)
    {
        try
        {

            Button button = sender as Button;
            string tag = button.name;
            int X = Convert.ToInt32(tag.Split(';')[0]);
            int Y = Convert.ToInt32(tag.Split(';')[1]);
            switch (field[X, Y])
            {
                case 1: field[X, Y] = 2; button.image.sprite = MarkO; break;
                case 2: field[X, Y] = 0; button.image.sprite = MarkEmpty; break;
                default: field[X, Y] = 1; button.image.sprite = MarkX; break;
            }
            Debug.Log("X = " + X + " Y = " + Y);
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }	

    
	// Update is called once per frame
	void Update () {

    }

    
    void OnGUI()
    {


        //GameObject myObj = GameObject.Find("ButtonOfField");


        //UnityEngine.Object myClone = Instantiate(button, transform.position, transform.rotation);
        

        
    }
}
