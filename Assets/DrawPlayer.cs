using System;
using AssemblyCSharp;
using Assets.Drawing;
using UnityEngine;

[RequireComponent(typeof (Player))]
public class DrawPlayer : MonoBehaviour
{
    private static Texture2D _texture;
    private GameObject _plane;
    private Drawer drawer;
    private Boolean gameStarted;
    private float planeHeight;
    private float planeWidth;
    private Player player;

    private void Awake()
    {
        //    _texture = new Texture2D(512, 512);
        player = GetComponent<Player>();
    }

    private void Start()
    {
        _plane = GameObject.Find("Plane");
        planeWidth = _plane.transform.localScale.x*10f;
        planeHeight = _plane.transform.localScale.y*10f;
    }

    public void StartGame()
    {
        _texture = (Texture2D) _plane.renderer.material.mainTexture;
        drawer = GameObject.Find("Systems").GetComponent<PlayerManager>().Drawer;
        gameStarted = true;
    }

    public void FinishGame()
    {
        gameStarted = false;
    }

    private Vector2 GetPercentagePlanePosition()
    {
        return new Vector2((transform.position.x + planeWidth*0.5f)/planeWidth,
            (transform.position.y + planeHeight*0.5f)/planeHeight);
    }

    // Update is called once per frame
    private void Update()
    {
        if (gameStarted)
        {
            Texture2D textureToChange = _texture;

            Vector2 percentagePlanePosition = GetPercentagePlanePosition();

            drawer.Paint(percentagePlanePosition, 30 * player.transform.localScale.x, player.Color, textureToChange,
                _plane.transform.localScale.y/_plane.transform.localScale.x, player);
            _texture.Apply();
        }
    }
}