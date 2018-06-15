using UnityEngine;
using UnityEngine.Networking;

public class NetworkPlayer : NetworkBehaviour {

    private Rigidbody localRigidbody;
    private Renderer localRenderer;
    private AudioSource audioSource;
    private ShakeEffect shakeEffect;

    [SyncVar]
    public string playerName = "";

    [SyncVar]
    public Color playerColor = Color.white;

    public float speedBoost;
    

    public void Start()
    {
        localRigidbody = GetComponent<Rigidbody>();
        localRenderer = GetComponent<Renderer>();
        audioSource = GetComponent<AudioSource>();
        shakeEffect = Camera.main.GetComponent<ShakeEffect>();

        localRenderer.material.color = playerColor;
    }

    public void FixedUpdate()
    {
        // Player hosting the server
        if (isServer && isLocalPlayer)
        {
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
                RpcMove(Vector3.up * speedBoost);
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
                RpcMove(Vector3.down * speedBoost);
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
                RpcMove(Vector3.left * speedBoost);
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                RpcMove(Vector3.right * speedBoost);
        }
        // Client playing on the server
        else if (isClient && isLocalPlayer)
        {
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
                CmdMove(Vector3.up * speedBoost);
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
                CmdMove(Vector3.down * speedBoost);
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
                CmdMove(Vector3.left * speedBoost);
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                CmdMove(Vector3.right * speedBoost);
        }
    }

    [Command] 
    public void CmdMove(Vector3 vector)
    {
        RpcMove(vector);
    }

    [ClientRpc]
    public void RpcMove(Vector3 vector)
    {
        localRigidbody.AddForce(vector);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 3.5)
        {
            if(!shakeEffect.isShaking)
            {
                shakeEffect.StartShaking(0.1f, 0.1f);
            }

            audioSource.volume = collision.relativeVelocity.magnitude/10;
            audioSource.Play();
        }

    }
}
