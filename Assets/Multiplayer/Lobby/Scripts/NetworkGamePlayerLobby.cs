using Mirror;
using UnityEngine;
using TMPro;
using System;
using Cinemachine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DapperDino.Mirror.Tutorials.Lobby
{
    public class NetworkGamePlayerLobby : NetworkBehaviour
    {
        [Header("UI")]
        [SerializeField] private GameObject lobbyUI = null;
        [SerializeField] private Canvas buildUI = null;

        [Header("Values")]
        public float timerCountDown;
        public TextMeshProUGUI countDownText;
        [SerializeField] Vector3 startPos;
        [SerializeField] Vector3 zoomPos;
        public bool ready = false;
        [SerializeField] public bool isLeader;

        [Header("Components")]
        [SerializeField] private MultiplayerCamera playerCam;
        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        [SerializeField] private RoundScript roundScript;
        [SerializeField] private Inventory inventory;
        [SerializeField] private ItemSelected itemSelected;
        [SerializeField] private Image countDownTextBackground;

        [SyncVar]
        private string displayName = "Loading...";

        private NetworkManagerLobby room;

        public override void OnStartAuthority()
        {
            if (!hasAuthority)
            {
                lobbyUI.SetActive(false);
                isLeader = false;
            }

            if (hasAuthority)
            {
                gameObject.tag = "Player1";
                isLeader = true;
            }
        }

        void Update()
        {

            GetComponents();
            StartGameCode();
           // StartGameFunction();
        }

        //Getting Startcomponents
        private void GetComponents()
        {

            buildUI = GameObject.FindGameObjectWithTag("buildUI").GetComponent<Canvas>();
            roundScript = GameObject.FindGameObjectWithTag("roundmanager").GetComponent<RoundScript>();
            startPos = new Vector3(0, 0, 0);
            if (isLeader == true)
            {
                zoomPos = GameObject.FindGameObjectWithTag("focus1").transform.position;
                itemSelected = GameObject.FindGameObjectWithTag("item").GetComponent<ItemSelected>();
                itemSelected.gameplayer = true;
                inventory.wichPlayer = 1;
            }

            if (isLeader == false)
            {
                zoomPos = GameObject.FindGameObjectWithTag("focus2").transform.position;
                itemSelected = GameObject.FindGameObjectWithTag("item").GetComponent<ItemSelected>();
                itemSelected.gameplayer = false;
                inventory.wichPlayer = 2;
            }
        }

        //Check if players has joined
        private void StartGameCode()
        {
            StartCountDown();
        }

        //Start CountDown
        private void StartCountDown()
        {
            buildUI.enabled = false;
            if (!hasAuthority)
            {
                lobbyUI.SetActive(false);
                isLeader = false;
            }

            if (hasAuthority)
            {
                isLeader = true;
            }

            timerCountDown -= Time.deltaTime;

            countDownText.text = "Starting In:" + " " + timerCountDown.ToString("0");

            if (timerCountDown < 0)
            {
                StartBuildingFace();
            }
        }

        //Starting Building Face
        private void StartBuildingFace()
        {
            Physics2D.gravity = new Vector2(0, 0);
            countDownTextBackground.enabled = false;
            countDownText.enabled = false;
            if(isLeader == true)
            {
                roundScript.startCountDown = true;
            }
            buildUI.enabled = true;
            transform.position = zoomPos;
            virtualCamera.m_Lens.OrthographicSize = 12;
            timerCountDown = 0;
        }

        //Starting BattleGame
        public void StartGameFunction()
        {
            inventory.item = itemType.empty;
            Physics2D.gravity = new Vector2(0, -9.8f);
            buildUI.enabled = false;
            transform.position = startPos;
            virtualCamera.m_Lens.OrthographicSize = 18;      
           
        }

        private NetworkManagerLobby Room
        {
            get
            {
                if (room != null) { return room; }
                return room = NetworkManager.singleton as NetworkManagerLobby;
            }
        }

        public override void OnStartClient()
        {
            DontDestroyOnLoad(gameObject);
            if (!hasAuthority)
            {
                gameObject.tag = "Player2";
            }
            Room.GamePlayers.Add(this);
        }

        public override void OnNetworkDestroy()
        {
            Room.GamePlayers.Remove(this);
        }

        public void HandleReadyStatusChanged(bool oldValue, bool newValue) => UpdateDisplay();

        private void UpdateDisplay()
        {
            if (!hasAuthority)
            {
                lobbyUI.SetActive(false);
            }
        }

        [Server]
        public void SetDisplayName(string displayName)
        {
            this.displayName = displayName;
        }
    }
}
