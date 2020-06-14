using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCharacter : MonoBehaviour
{
    //this behaviour can be diffenrt for differnent movement styles;
    public int ForwardRange = 3;
    public int GeneralRange = 1;


    public List<Vector2> GetCharacterRange(int posx, int posy, Vector2[,] grid, int width, int depth)
    {
        List<Vector2> canmoveto = new List<Vector2>();
        for (int i = -GeneralRange; i <= GeneralRange; i++)
        {
            for (int j = -GeneralRange; j <= GeneralRange; j++)
            {

                if ((posx + i < width && posx + i >= 0) && (posy + j < depth && posy + j >= 0))
                {
                    canmoveto.Add(new Vector2(posx + i, posy + j));
                }
            }
        }
        for (int i = 0; i <= ForwardRange; i++)
        {
            if ((posx + i < width && posx + i >= 0))
            {
                canmoveto.Add(new Vector2(posx + i, posy ));
            }
        }
        return canmoveto;
    }
}
