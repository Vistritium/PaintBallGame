using System.Collections.Generic;
using AssemblyCSharp;
using Assets.Communication;
using UnityEngine;
using Input = UnityEngine.Input;
using Random = System.Random;

[RequireComponent(typeof (Player))]
public class PlayerController : MonoBehaviour
{
    //Transform transform;

    public static Output Output;
    private readonly List<MovingState> activeStates = new List<MovingState>();
    private Player player;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    public void FinishGame()
    {
        activeStates.Clear();
        UpdateState();
    }

    // Update is called once per frame
    private void Update()
    {
        bool changed = false;
        //activeStates.ha
        if (Input.GetKeyDown(KeyCode.A))
        {
            activeStates.Add(MovingState.LEFT);
            changed = true;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            activeStates.Add(MovingState.RIGHT);
            changed = true;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            activeStates.Add(MovingState.UP);
            changed = true;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            activeStates.Add(MovingState.DOWN);
            changed = true;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            activeStates.Remove(MovingState.LEFT);
            changed = true;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            activeStates.Remove(MovingState.RIGHT);
            changed = true;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            activeStates.Remove(MovingState.UP);
            changed = true;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            activeStates.Remove(MovingState.DOWN);
            changed = true;
        }

        if (Input.GetKeyUp(KeyCode.Z))
        {
            Output.Logg(transform.position.ToString());
        }

        if (changed) UpdateState();

        if (Input.GetKeyDown(KeyCode.C))
        {
            var random = new Random();
            player.Color = new Color(random.Next(256)/256.0f, random.Next(256)/256.0f, random.Next(256)/256.0f);
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        activeStates.Clear();
        UpdateState();
    }

    private void UpdateState()
    {
        MovingState newState;
        int listCount = activeStates.Count;
        if (listCount == 0)
        {
            newState = MovingState.NONE;
        }
        else if (listCount == 1)
        {
            newState = activeStates[0];
        }
        else
        {
            newState = activeStates[listCount - 1].With(activeStates[listCount - 2]);
        }
        player.State = newState;
        Output.SendState(player.Id, newState, transform.position);
    }

    public void MoveRight(string amount)
    {
        float value;
        bool parsed = float.TryParse(amount, out value);
        if (parsed)
            transform.Translate(value, 0, 0);
        else
            Debug.Log("Couldn't parse!");
    }

    private class ListWithInfo<T>
    {
    }
}