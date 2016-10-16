using DesignPattern;

public class Floor : Singleton<Floor>
{
    public GroundProperty[] Grounds;

    protected Floor()
    {
    }
}