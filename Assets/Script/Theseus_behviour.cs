using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Theseus_behviour : MonoBehaviour
{
    public string TheseusInitialPosition; // theseus position based on 2 numbers separated by ';' where the first is the x axis and second the y axis; 
    public int[] TheseusCurrentPosition;
    static public Theseus_behviour Theseus;
    public bool paused = false; // if Theseus is still alive/not caught by minotaur, its also paused when reaching the exit
    public float movementAnimTime;

    private void Awake()
    {
        if (Theseus == null) {
            Theseus = this;
        }
    }

    void Start()
    {
        FindInitialPosition();
    }
    public void FindInitialPosition() {
        TheseusCurrentPosition = new int[2];
        string[] axisPostions = TheseusInitialPosition.Split(';');
        //Mapping.Instance.Map[int.Parse(axisPostions[0]), int.Parse(axisPostions[1])] = 3;
        TheseusCurrentPosition[0] = int.Parse(axisPostions[0]); // collum 
        TheseusCurrentPosition[1] = int.Parse(axisPostions[1]); // line 
    }

    public IEnumerator Movement(Vector3 destination, Vector3 InitialPosition) {//THeseus Movement, using Coroutine and Lerp
        float timePassed = 0;
        paused = true;
        while (timePassed < movementAnimTime) {
            transform.position = Vector3.Lerp(InitialPosition, destination, timePassed / movementAnimTime);
            timePassed += Time.deltaTime;
            yield return null;
        }
        paused = false;
        this.transform.position = destination;

    }

    // Update is called once per frame
    void Update()
    {
        if (paused == false)
        {
            if (Input.GetKeyDown("right") && Mapping.Instance.Map[TheseusCurrentPosition[0] + 1, TheseusCurrentPosition[1]] != 1)
            {
                TheseusCurrentPosition[0] += 1;
                StartCoroutine(Movement(new Vector3(this.transform.position.x + 1, this.transform.position.y, this.transform.position.z), this.transform.position));
               
                //this.transform.position = new Vector3(this.transform.position.x + 1, this.transform.position.y, this.transform.position.z);
                Minotaur.instance.ChargeTheseus();               
            }
            else if (Input.GetKeyDown("up") && Mapping.Instance.Map[TheseusCurrentPosition[0], TheseusCurrentPosition[1] - 1] != 1)
            {
                TheseusCurrentPosition[1] -= 1;
                StartCoroutine(Movement(new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z), this.transform.position));
                Minotaur.instance.ChargeTheseus();
            }
            else if (Input.GetKeyDown("down") && Mapping.Instance.Map[TheseusCurrentPosition[0], TheseusCurrentPosition[1] + 1] != 1)
            {
                TheseusCurrentPosition[1] += 1;
                StartCoroutine(Movement(new Vector3(this.transform.position.x, this.transform.position.y - 1, this.transform.position.z), this.transform.position));
                Minotaur.instance.ChargeTheseus();
            }
            else if (Input.GetKeyDown("left") && Mapping.Instance.Map[TheseusCurrentPosition[0] - 1, TheseusCurrentPosition[1]] != 1)
            {
                TheseusCurrentPosition[0] -= 1;
                StartCoroutine(Movement(new Vector3(this.transform.position.x - 1, this.transform.position.y, this.transform.position.z), this.transform.position));
                Minotaur.instance.ChargeTheseus();
            }
            else if (Input.GetKeyDown("w")) {

                Minotaur.instance.ChargeTheseus();
            }
            if (Mapping.Instance.Map[TheseusCurrentPosition[0], TheseusCurrentPosition[1]] == 2)
            {
                SceneStatesControl.instance.victory();
            }
        }
    }
}
