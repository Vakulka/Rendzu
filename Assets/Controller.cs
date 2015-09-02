using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class Controller : MonoBehaviour {

    private Players[,] field;

    private List<Button> buttons = new List<Button>();

    public Transform button;
    public Vector2 sizeOfCell = new Vector2(70, 70);

    public Sprite MarkA;
    public Sprite MarkB;
    public Sprite MarkEmpty;

    public Button ButtonReset;
    public Button ButtonOfCurrentPlayer;
    private enum Players { A=1, B=2, None=0}
    private Players CurrentPlayer=Players.A;
	// Use this for initialization
	void Start () {
        field = new Players[16, 12];

        for (int i = 0; i < field.GetLength(0); i++)
            for (int j = 0; j < field.GetLength(1); j++)
                field[i, j] = Players.None;

        //Canvas cc = GetComponent<Canvas>();
        //Vector2 size = new Vector2(70, 70);
        //Vector2 startPoint = new Vector2(cc.GetComponent<RectTransform>().rect.xMin, cc.GetComponent<RectTransform>().rect.yMax);
        //for (int i = 0; i < field.GetLength(0); i++)
        //    for (int j = 0; j < field.GetLength(1); j++)
        //    {
        //        buttons.Add((Component)Instantiate(button, new Vector3(i * size.x, j * size.y,0),new Quaternion()));
        //        //GUI.Button(new Rect(startPoint + new Vector2(i * size.x, j * size.y), size), "Button " + i + ":" + j);
        //    }

        //Canvas canvas = GetComponent<Canvas>();
        Vector2 size = sizeOfCell;
        Vector2 startPoint = new Vector2(0, 0);
        for (int i = 0; i < field.GetLength(0); i++)
        {
            for (int j = 0; j < field.GetLength(1); j++)
            {
                Transform b = (Transform)Instantiate(button, new Vector3(startPoint.x + i * size.x + size.x / 2, startPoint.y + j * size.y + size.y / 2, 0), Quaternion.identity);
                RectTransform rect = b.GetComponent<RectTransform>();
                rect.sizeDelta = size;

                Button tempButton = b.gameObject.GetComponent<Button>();
                tempButton.onClick.AddListener(() => { buttonOfFieldClick(tempButton); });
                tempButton.name = i + ";" + j;
                buttons.Add(tempButton);
                b.SetParent(transform);
            }
        }

        ButtonReset.onClick.AddListener(() => { buttonResetClick(ButtonReset); });

        SetColorOfCurrentPlayer();

	}
    private void buttonOfFieldClick(object sender)
    {
        try
        {
            Button button = sender as Button;
            string tag = button.name;
            int X = Convert.ToInt32(tag.Split(';')[0]);
            int Y = Convert.ToInt32(tag.Split(';')[1]);
            //int currentPlayerNumber= GetCurrentPlayerNumber(CurrentPlayer);
            switch (field[X, Y])
            {
                case Players.None: 
                    field[X, Y] = CurrentPlayer;
                    SetButton(button, CurrentPlayer);
                    SetNextPlayer();
                    break;
                //default: field[X, Y] = CurrentPlayer; SetButton(button, CurrentPlayer); break;
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
    private void SetNextPlayer(Players value)
    {
        CurrentPlayer = value;
        SetColorOfCurrentPlayer();
    }
    /// <summary>
    /// set next player according to order of players
    /// </summary>
    private void SetNextPlayer()
    {
        switch (CurrentPlayer)
        {
            case Players.A: CurrentPlayer = Players.B; break;
            case Players.B: CurrentPlayer = Players.A; break;
        }
        SetColorOfCurrentPlayer();
    }
    private void SetColorOfCurrentPlayer()
    {
        switch (CurrentPlayer)
        {
            case Players.A: ButtonOfCurrentPlayer.image.color=Color.red; break;
            case Players.B: ButtonOfCurrentPlayer.image.color = new Color(0,0,64); break;
            default: ButtonOfCurrentPlayer.image.color = Color.white; break;
        }
    }
    private int GetCurrentPlayerNumber(Players currentPlayer)
    {
        int currentPlayerNumber;
        switch (currentPlayer)
        {
            case Players.A: currentPlayerNumber = 1; break;
            case Players.B: currentPlayerNumber = 2; break;
            case Players.None: currentPlayerNumber = 0; break;
            default: currentPlayerNumber = 0; break;
        }
        return currentPlayerNumber;
    }

    private void buttonResetClick(object sender)
    {
        SetButton(null, Players.None);
    }

    private void SetButton(Button button = null, Players player = Players.None)
    {
        Sprite sprite;
        switch (player)
        {
            case Players.A: sprite=MarkA;break;
            case Players.B: sprite = MarkB; break;
            case Players.None: sprite = MarkEmpty; break;
            default: sprite = MarkEmpty; break;
        }
        if (button == null)
        {
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    field[i, j] = player;
                }
            }
            foreach (var butt in buttons)
            {
                butt.image.sprite = sprite;
            }
        }
        else
        {
            button.image.sprite = sprite;
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
