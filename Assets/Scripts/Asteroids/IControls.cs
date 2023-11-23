namespace Asteroids {

public interface IControls {
    public bool IsForwardPressed { get; }
    public bool IsLeftPressed { get; }
    public bool IsRightPressed { get; }
    public bool IsShootPressed { get; }
    public bool IsLaserPressed { get; }

    public void SetForwardPressed(bool isPressed);
    public void SetLeftPressed(bool isPressed);
    public void SetRightPressed(bool isPressed);
    public void SetShootPressed(bool isPressed);
    public void SetLaserPressed(bool isPressed);
}

}