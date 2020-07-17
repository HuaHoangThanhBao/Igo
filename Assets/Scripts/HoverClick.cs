using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverClick : MonoBehaviour
{
    public GameObject White_chees;
    public GameObject Black_chess;
    public GameObject Hover;
    private bool isBlack;
    public bool my_turn = true;

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (my_turn)
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider != null)
                {
                    if (hit.transform.GetComponent<MapEdge>() != null)
                    {
                        if (!hit.transform.GetComponent<MapEdge>().placed)
                        {
                            Hover.SetActive(true);
                            Hover.transform.localPosition =
                                new Vector3(hit.transform.GetComponent<MapEdge>().pos.x, 0.52f, hit.transform.GetComponent<MapEdge>().pos.z);

                            if (Input.GetMouseButtonDown(0))
                            {
                                MapEdge current = hit.transform.GetComponent<MapEdge>();

                                isBlack = !isBlack;

                                GameObject chess = null;

                                chess = Instantiate(Black_chess, current.pos, Quaternion.identity);

                                Map.instance.board.Placed_Chess(current, chess, isBlack);
                                Map.instance.mapControl.changeType.Update_Chess(current);

                                StartCoroutine(AI_Turn());
                            }
                        }
                    }
                    else Hover.SetActive(false);
                }
            }
        }
        else Hover.SetActive(false);
    }

    IEnumerator AI_Turn()
    {
        my_turn = false;
        isBlack = !isBlack;

        yield return new WaitForSeconds(0.2f);

        Move move = Map.instance.board.findBestMove();
        Debug.Log(move.pos);
        MapEdge current = Map.instance.board.Find_Pos(move.pos);

        if (current != null)
        {
            GameObject black = Instantiate(White_chees, move.pos, Quaternion.identity);

            Map.instance.board.Placed_Chess(current, black, isBlack);
            Map.instance.mapControl.changeType.Update_Chess(current);
        }

        my_turn = true;
    }
}
