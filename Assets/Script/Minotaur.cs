using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minotaur : MonoBehaviour
{
    static public Minotaur instance;
    public int[] minotaurCurrentposition;
    public string MinotaurInitialPosition;
    public float movementAnimTime;
    public bool MinotaurTurn = false;
    public int MinotaurMovementNumber = 0;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        FindInitialPosition();
    }
    public IEnumerator MinotaurMovement(Vector3 destination, Vector3 InitialPosition)//Basic Animation movement, its the minoutaur Turn that is run twice
    {
        
        float timePassed = 0;
        while (timePassed < movementAnimTime)
        {
            transform.position = Vector3.Lerp(InitialPosition, destination, timePassed / movementAnimTime);
            timePassed += Time.deltaTime;
            yield return null;
        }
        this.transform.position = destination;
        MinotaurMovementNumber++;
        if (MinotaurMovementNumber < 2) ChargeTheseus();
        else {
            MinotaurMovementNumber = 0;
            Theseus_behviour.Theseus.paused = false;
        }
    }

    public void FindInitialPosition() // finds the initial Position according to the string MinotaurInitialPosition
    {
        minotaurCurrentposition = new int[2];
        string[] axisPostions = MinotaurInitialPosition.Split(';');
        minotaurCurrentposition[0] = int.Parse(axisPostions[0]); // Collum
        minotaurCurrentposition[1] = int.Parse(axisPostions[1]); // Line
    }
    public void ChargeTheseus()
    { // behaviour of the minotaur, in case Theseus is in a diferent position than the minotaur, it will move in order to close the gap first left or right, when in the same collum index then up or down
        if (minotaurCurrentposition[0] > Theseus_behviour.Theseus.TheseusCurrentPosition[0] && Mapping.Instance.Map[minotaurCurrentposition[0] - 1, minotaurCurrentposition[1]] != 1)
        {
            minotaurCurrentposition[0] -= 1;
            StartCoroutine(MinotaurMovement(new Vector3(this.transform.position.x - 1, this.transform.position.y, this.transform.position.z), this.transform.position));
        }
        else if (minotaurCurrentposition[0] < Theseus_behviour.Theseus.TheseusCurrentPosition[0] && Mapping.Instance.Map[minotaurCurrentposition[0] + 1, minotaurCurrentposition[1]] != 1)
        {
            minotaurCurrentposition[0] += 1;
            StartCoroutine(MinotaurMovement(new Vector3(this.transform.position.x + 1, this.transform.position.y, this.transform.position.z), this.transform.position));
        }
        else if (minotaurCurrentposition[1] > Theseus_behviour.Theseus.TheseusCurrentPosition[1] && Mapping.Instance.Map[minotaurCurrentposition[0], minotaurCurrentposition[1] - 1] != 1)
        {
            minotaurCurrentposition[1] -= 1;
            StartCoroutine(MinotaurMovement(new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z), this.transform.position));
        }
        else if (minotaurCurrentposition[1] < Theseus_behviour.Theseus.TheseusCurrentPosition[1] && Mapping.Instance.Map[minotaurCurrentposition[0], minotaurCurrentposition[1] + 1] != 1)
        {
            minotaurCurrentposition[1] += 1;
            StartCoroutine(MinotaurMovement(new Vector3(this.transform.position.x, this.transform.position.y - 1, this.transform.position.z), this.transform.position));
        }

        if (minotaurCurrentposition[0] == Theseus_behviour.Theseus.TheseusCurrentPosition[0] && minotaurCurrentposition[1] == Theseus_behviour.Theseus.TheseusCurrentPosition[1])
        {
            SceneStatesControl.instance.defeat();
        }
    }
}
