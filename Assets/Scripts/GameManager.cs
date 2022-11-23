using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{

    public GameObject BlackSton;
    public GameObject WhiteSton;
    public Transform BlackFeild;
    public Transform WhiteFeild;

    private int[,] _gameboard;
    private int _boardSize;
    private bool _bTurn;

    // Start is called before the first frame update
    void Start()
    {
        _bTurn = true;
        _boardSize = 19;
        _gameboard = new int[19, 19];
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            // 입력 마우스 좌표
            Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
            
            point.x = Mathf.Round(point.x * 2) / 2;
            point.y = Mathf.Round(point.y * 2) / 2;

            int column = (int)((point.x + 4.5) * 2);
            int row = (int)((point.y + 4.5) * 2);

            if(row < 0 || row >= _boardSize || column < 0 || column >= _boardSize) return;
            if(_gameboard[column, row] != 0) return;
            
            GameObject inst;

            if(_bTurn)
            {
                inst = Instantiate(BlackSton, BlackFeild);
                inst.transform.position = point;
                _gameboard[column, row] = 1;
            }
            else
            {
                inst = Instantiate(WhiteSton, WhiteFeild);
                inst.transform.position = point;
                _gameboard[column, row] = -1;
            }

            if(bWinCheck(column, row))
            {
                Debug.Log("player" + (_bTurn ? 1 : -1) + " is win");
            }


            _bTurn = !_bTurn;
        }
    }

    bool bWinCheck(int x, int y)
    {
        int color = _bTurn ? 1 : -1;
        int left = 0, right =  0, up = 0, down = 0;

        // 가로
        for(int i = 0; i < 4; i++)
        {
            if(x - left - 1 >= 0)
            {
                if(_gameboard[x - left - 1, y] == color)
                {
                    left++;
                    continue;
                }
            }
            if(x + right + 1 <= 19)
            {
                if(_gameboard[x + right +1, y] == color)
                {
                    right++;
                }
            }
        }

        if(left + right == 4) return true;

        // 세로
        for(int i = 0; i < 4; i++)
        {
            if(y - down - 1 >= 0)
            {
                if(_gameboard[x, y - down - 1] == color)
                {
                    down++;
                    continue;
                }
            }
            if(y + up + 1 <= 19)
            {
                if(_gameboard[x, y + up + 1] == color)
                {
                    up++;
                }
            }
        }
        if(down + up == 4) return true;

        
        // 대각 1
        left = 0;
        right =  0;
        up = 0;
        down = 0;

        for(int i = 0; i < 4; i++)
        {
            if(x - left - 1 >= 0 && y - down - 1 >= 0)
            {
                if(_gameboard[x - left - 1, y - down - 1] == color)
                {
                    left++;
                    down++;
                    continue;
                }
            }
            if(x + right + 1 <= 19 && y + up + 1 <= 19)
            {
                if(_gameboard[x + right + 1, y + up + 1] == color)
                {
                    right++;
                    up++;
                }
            }
        }
        if(left + right == 4) return true;
        
              
        // 대각 2
        left = 0;
        right =  0;
        up = 0;
        down = 0;

        for(int i = 0; i < 4; i++)
        {
            if(x - left - 1 >= 0 && y + up + 1 <= 19)
            {
                if(_gameboard[x - left - 1, y + up + 1] == color)
                {
                    left++;
                    up++;
                    continue;
                }
            }
            if(x + right + 1 <= 19 && y - down - 1 >= 0)
            {
                if(_gameboard[x + right + 1, y - down - 1] == color)
                {
                    right++;
                    down++;
                }
            }
        }
        if(left + right == 4) return true;

        return false;
    }
}
