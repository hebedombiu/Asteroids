namespace Asteroids {

public class Controls : IControls {
    public bool IsForwardPressed { get; private set; }
    public bool IsLeftPressed { get; private set; }
    public bool IsRightPressed { get; private set; }
    public bool IsShootPressed { get; private set; }
    public bool IsLaserPressed { get; private set; }

    void IControls.SetForwardPressed(bool isPressed) => IsForwardPressed = isPressed;
    void IControls.SetLeftPressed(bool isPressed) => IsLeftPressed = isPressed;
    void IControls.SetRightPressed(bool isPressed) => IsRightPressed = isPressed;
    void IControls.SetShootPressed(bool isPressed) => IsShootPressed = isPressed;
    void IControls.SetLaserPressed(bool isPressed) => IsLaserPressed = isPressed;

    public void Reset() {
        IsForwardPressed = false;
        IsLeftPressed = false;
        IsRightPressed = false;
        IsShootPressed = false;
        IsLaserPressed = false;
    }
}

}