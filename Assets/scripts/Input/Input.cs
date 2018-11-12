using UnityEngine;
using System.Collections;

using UnityGameBase;
using UnityGameBase.Core.Input;


/// <summary>
/// This class is a base-class for all Input-devices.
/// </summary>
public class Input : GameComponent<GameLogic> {

    // Keycode to pause the game
    [SerializeField]
    protected KeyCode keyCodePauseGame = KeyCode.P;

    // The vehicle which shall be controlled by this input-class
    protected PlayerVehicle toControl;

    private bool shallMoveLeft = false;
    private bool shallMoveRight = false;
    private bool shallShoot = false;



    // Use this for initialization
    void Start() {
        Initialize();
    }

    void Update() {

        if (shallMoveLeft) {
            ICommand moveCommand = new MoveCommand(Direction.left);
            moveCommand.Execute(toControl);
        }
        if (shallMoveRight) {
            ICommand moveCommand = new MoveCommand(Direction.right);
            moveCommand.Execute(toControl);
        }
        if (shallShoot) {
            ICommand shootCommant = new ShootCommand();
            shootCommant.Execute(toControl);
        }
    }


    protected void Initialize() {
        toControl = this.GetComponent<PlayerVehicle>();

        UnityGameBase.Game.Instance.gameInput.InputEnabled = false;

        //UnityGameBase.Core.Input.KeyMapping mappingLeft = new UnityGameBase.Core.Input.KeyMapping();
        //mappingLeft.name = "InputLeft";
        //mappingLeft.relativeScreenRect = new Rect(0, 0, 1000, 1000);
        //mappingLeft.swipeDirection = TouchInformation.ESwipeDirection.Left;
        //mappingLeft.keyCode = KeyCode.LeftArrow;

        //UnityGameBase.Core.Input.KeyMapping mappingRight = new UnityGameBase.Core.Input.KeyMapping();
        //mappingRight.name = "InputRight";
        //mappingRight.relativeScreenRect = new Rect(0, 0, 1000, 1000);
        //mappingRight.swipeDirection = TouchInformation.ESwipeDirection.Right;
        //mappingRight.keyCode = KeyCode.RightArrow;

        //UnityGameBase.Core.Input.KeyMapping mappingShoot = new UnityGameBase.Core.Input.KeyMapping();
        //mappingRight.name = "Shoot";
        //mappingRight.relativeScreenRect = new Rect(0, 0, 1000, 1000);
        //mappingRight.swipeDirection = TouchInformation.ESwipeDirection.Right;
        //mappingRight.keyCode = KeyCode.Space;

        //UnityGameBase.Game.Instance.gameInput.keyMappings.Add(mappingLeft);
        //UnityGameBase.Game.Instance.gameInput.keyMappings.Add(mappingRight);
        //UnityGameBase.Game.Instance.gameInput.keyMappings.Add(mappingShoot);

        UnityGameBase.Game.Instance.gameInput.KeyUp += gameInput_KeyUp;
        UnityGameBase.Game.Instance.gameInput.KeyDown += gameInput_KeyDown;
        UnityGameBase.Game.Instance.gameInput.SwipeEvent += gameInput_SwipeEvent;


        //UnityGameBase.Game.Instance.gameInput.KeyUp += gameInput_KeyUp;
        
    }

    void gameInput_SwipeEvent(TouchInformation touchInfo) {
        TouchInformation.ESwipeDirection swipeDirection = touchInfo.GetSwipeDirection();

        if (swipeDirection == TouchInformation.ESwipeDirection.Right) {
            ICommand moveCommand = new MoveCommand(Direction.right);
            moveCommand.Execute(toControl);
        }

        if (swipeDirection == TouchInformation.ESwipeDirection.Left) {
            ICommand moveCommand = new MoveCommand(Direction.left);
            moveCommand.Execute(toControl);
        }
    }

   
    void gameInput_KeyUp(string keyMappingName) {
        if (keyMappingName.Equals("InputLeft")) {
            shallMoveLeft = false;    
        }

        if (keyMappingName.Equals("InputRight")) {
            shallMoveRight = false;
        }

        if (keyMappingName.Equals("Shoot")) {
            shallShoot = false;
        }
    }


    void gameInput_KeyDown(string keyMappingName) {
        if (keyMappingName.Equals("InputLeft")) {
            shallMoveLeft = true;
        }

        if (keyMappingName.Equals("InputRight")) {
            shallMoveRight = true;
        }

        if (keyMappingName.Equals("Shoot")) {
            shallShoot = true;
        }
    }

    void gameInput_GestureEnd(UnityGameBase.Core.Input.BaseGesture gesture) {
        foreach(TouchInformation touchInfo in gesture.RelatedTouches) {
        //    touchInfo.
        }
    }

    void gameInput_GestureStart(UnityGameBase.Core.Input.BaseGesture gesture) {
        
    }
}
