
namespace MachineSystem.Client.Components.MachineList;

public class MachineCommandState
{
    public bool IsPending { get; set; }
    public bool IsError { get; set; }

    public void Set(bool? isPending = null, bool? isError = null)
    {
        IsPending = isPending ?? IsPending;
        IsError = isError ?? IsError;
    }
};
