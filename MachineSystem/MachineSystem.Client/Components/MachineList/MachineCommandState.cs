
namespace MachineSystem.BlazorClient.Components.MachineList;

public class MachineCommandState
{
    public bool IsPending { get; set; }

    public void Set(
        bool isPending)
    {
        IsPending = isPending;
    }
};
