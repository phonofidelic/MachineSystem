
namespace MachineSystem.BlazorClient.Components.MachineList;

public class MachineCommandState /* <TResult> */
{
    public bool IsPending { get; set; }

    public bool IsError { get; set; }

    public string? ErrorMessage { get; private set; }

    // public TResult Result { get; set; } = default!;

    public void Set(
        bool? isPending = null, 
        bool? isError = null, 
        string? errorMessage = null)
    {
        IsPending = isPending ?? IsPending;
        IsError = isError ?? IsError;
        ErrorMessage = errorMessage ?? ErrorMessage;
    }
};
