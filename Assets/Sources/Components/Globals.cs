using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Unique, CreateAssetMenu]
public class Globals : ScriptableObject
{
    public GameObject playerPrefab;
    public Vector2 playersInitialPosition;
    public float maxRangePlayers;
    public GameObject ball;
    public float ballRange;
    public int amountOfBalls;
    public GameObject longBorder;
    public Vector2 longBordersPosition; 
    public float speed;
    public Vector2 velocity;
    public int winningScore;    
}