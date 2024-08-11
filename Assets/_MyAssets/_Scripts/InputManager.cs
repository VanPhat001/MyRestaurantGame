using UnityEngine;

public class InputManager : MonoBehaviour {
    public static InputManager Singleton { get; private set; }
    public Vector2 JoystikcDiretion { get; private set; }

    void Awake()
    {
        Singleton = this;
    }

    void Update()
    {
        JoystikcDiretion = GameSceneUIManager.Singleton.Joystick.Direction;
    }
}
