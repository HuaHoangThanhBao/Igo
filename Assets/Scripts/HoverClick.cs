using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverClick : MonoBehaviour
{
    public GameObject White_chees;
    public GameObject Black_chess;
    public GameObject Hover;
    public AIBot bot = new AIBot();
    private bool isBlack;
    public bool my_turn = true;
    public static HoverClick instance;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        //if (my_turn)
        //{
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
                            isBlack = !isBlack;
                            if (isBlack)
                            {
                                my_turn = false;
                                GameObject black = Instantiate(Black_chess, hit.transform.GetComponent<MapEdge>().pos, Quaternion.identity);
                                hit.transform.GetComponent<MapEdge>().placed = true;
                                hit.transform.GetComponent<MapEdge>().chess = black;

                                bot.AddNewPos(hit.transform.GetComponent<MapEdge>());
                                //StartCoroutine(AI_Turn(hit));
                            }
                            else
                            {
                                GameObject white = Instantiate(White_chees, hit.transform.GetComponent<MapEdge>().pos, Quaternion.identity);
                                hit.transform.GetComponent<MapEdge>().placed = true;
                                hit.transform.GetComponent<MapEdge>().chess = white;

                                bot.AddNewPos(hit.transform.GetComponent<MapEdge>());
                            }
                            hit.transform.GetComponent<MapEdge>().isBlack = isBlack;
                            Map.instance.mapControl.changeType.Update_Chess(hit.transform.GetComponent<MapEdge>());
                        }
                    }
                }
                else Hover.SetActive(false);
            }
            //}
        }
        else Hover.SetActive(false);
    }

    IEnumerator AI_Turn(RaycastHit hit)
    {
        yield return new WaitForSeconds(1f);
        GameObject black = Instantiate(White_chees, bot.Auto(hit.transform.GetComponent<MapEdge>().pos), Quaternion.identity);
        hit.transform.GetComponent<MapEdge>().placed = true;
        my_turn = true;
    }
}
