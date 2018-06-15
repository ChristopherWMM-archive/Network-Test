using UnityEngine;
using UnityEngine.Networking;
using Prototype.NetworkLobby;

public class NetworkHook : LobbyHook {

    public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer)
    {
        LobbyPlayer lobby = lobbyPlayer.GetComponent<LobbyPlayer>();
        NetworkPlayer networkPlayer = gamePlayer.GetComponent<NetworkPlayer>();

        networkPlayer.playerName = lobby.playerName;
        networkPlayer.playerColor = lobby.playerColor;
        networkPlayer.transform.position = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0);

    }
}
