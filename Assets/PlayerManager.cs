using System.Collections.Generic;
using System.Linq;
using AssemblyCSharp;
using Assets;
using Assets.Communication;
using Assets.Communication.Packet;
using Assets.Drawing;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public readonly Dictionary<uint, GameObject> players = new Dictionary<uint, GameObject>();
    public Drawer Drawer;
    public GameObject MainPlayer;
    public PixelTracker PixelTracker;

    // Use this for initialization
    private void Start()
    {
        GameObject.Find("Plane").renderer.material.mainTexture = new Texture2D(512, 512);
    }

    // Update is called once per frame
    private void Update()
    {
    }

/*    public void AddMainPlayer(PlayerData player)
    {
        var player = GetPlayer(player);
        MainPlayer = player;
    }*/

    public void StartGame()
    {
        MainPlayer.GetComponent<PlayerController>().enabled = true;
        Texture texture = GameObject.Find("Plane").renderer.material.mainTexture;
        int pixelAmount = texture.height*texture.width;
        PixelTracker = new PixelTracker(players.Select(x => x.Value.GetComponent<Player>()).ToList(), (uint) pixelAmount);
        Drawer = new Drawer(PixelTracker.PixelChanged);
        players.Values.ToList().ForEach(x => x.GetComponent<DrawPlayer>().StartGame());
        GetComponent<StateUpdater>().StartTime();
    }

    public void RemovePlayerAndDestroy(uint id)
    {
        GameObject player = players[id];
        players.Remove(id);
        Destroy(player);
    }

    public void AddPlayerToGame(PlayerData playerData)
    {
        if (!players.ContainsKey(playerData.id))
        {
            var player = (GameObject) Instantiate(Resources.Load("PlayerPrefab"));
            player.transform.position = new Vector3(playerData.x, playerData.y, 0);
            float radius = playerData.radius;
            player.transform.localScale = new Vector3(radius, radius, radius);
            var playerScript = (Player) player.GetComponent(typeof (Player));
            playerScript.Color = new Color(playerData.colorRed, playerData.colorGreen, playerData.colorBlue);
            playerScript.Id = playerData.id;
            playerScript.Name = name;
            players.Add(playerData.id, player);
            if (playerData.isMain)
            {
                MainPlayer = player;
                MainPlayer.AddComponent(typeof (PlayerController));
                MainPlayer.GetComponent<PlayerController>().enabled = false;
            }
            GetComponent<Output>()
                .Log("OK ADDED PLEJER " + playerData.id + " name " + playerData.name + " main:" + playerData.isMain);
        }
    }

    public void AddPowerup(PowerupData powerup)
    {
        GameObject player = players[powerup.PlayerId];
        powerup.AddPowerupToGameobject(player);
    }

    public void UpdatePlayer(uint id, Vector2 position)
    {
        players[id].transform.position = new Vector3(position.x, position.y, 0);
    }

    public void UpdatePlayer(uint id, MovingState state)
    {
        players[id].GetComponent<Player>().State = state;
    }

    public void FinishGame()
    {
        foreach (GameObject player in players.Values)
        {
            player.GetComponent<DrawPlayer>().FinishGame();
        }

        MainPlayer.GetComponent<PlayerController>().enabled = false;
        var texture = (Texture2D) GameObject.Find("Plane").renderer.material.mainTexture;
        Color[] arrayOfWhiteColors =
            Enumerable.Range(0, texture.width*texture.height).Select(x => Color.white).ToArray();

        texture.SetPixels(0, 0, texture.width, texture.height, arrayOfWhiteColors);
        foreach (var player in players)
        {
            player.Value.transform.position = Vector3.zero;
            player.Value.GetComponent<Player>().State = MovingState.NONE;
        }
        MainPlayer.GetComponent<PlayerController>().FinishGame();
        texture.Apply();
        GameObject.Find("Plane").renderer.material.mainTexture = texture;

        //map bonuses
        GameObject[] bonuses = GameObject.FindGameObjectsWithTag("MapBonus");
        foreach (GameObject bonus in bonuses)
        {
            Destroy(bonus);
        }
    }


    private GameObject GetPlayer(string name, uint id, Color color, Vector2 position)
    {
        var player = (GameObject) Instantiate(Resources.Load("Player"));
        player.transform.position = new Vector3(position.x, position.y, 0);
        var playerScript = player.GetComponent<Player>();
        playerScript.Color = color;
        playerScript.Id = id;
        playerScript.name = name;
        players.Add(id, player);
        return player;
    }
}