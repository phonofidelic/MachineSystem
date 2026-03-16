using System.ComponentModel.DataAnnotations;
using MachineSystem.Application.Commands;
using MachineSystem.Application.ServiceContracts;
using MachineSystem.Application.ViewModels;
using MachineSystem.Domain.ValueObjects;
using Microsoft.AspNetCore.Components;

namespace MachineSystem.BlazorClient.Components.CreateMachineForm;

public partial class CreateMachineForm
{
    [Parameter]
    public EventCallback<Func<IReadOnlyList<MachineListItem>, IReadOnlyList<MachineListItem>>> OnMachinesListUpdated { get; set; }

    [Inject]
    private IMachineApiClient MachineApiClient { get; set; } = default!;

    [SupplyParameterFromForm]
    [Required]
    [MaxLength(30, ErrorMessage = "Name cannot contain more than 30 characters")]
    [MinLength(3, ErrorMessage = "Name must be at least 3 characters long")]
    [RegularExpression(@"[A-Za-z][A-Za-z0-9\-]*", ErrorMessage = "Name can only contain alphanumeric characters")]
    private string? MachineName { get; set; } = null;

    [SupplyParameterFromForm]
    [Required]
    private string? MachineType { get; set; } = null;

    private ErrorBoundaryBase? CreateMachineFormErrorBoundary;

    private async Task PostCreateMachineRequest()
    {
        if (MachineName is null || MachineType is null)
            return;

        var result = await MachineApiClient.CreateMachineAsync(
            new CreateMachineCommand(
                Name: MachineName, 
                Type: new MachineType(MachineType))) 
            ?? throw new Exception("Could not create machine");

        if (OnMachinesListUpdated.HasDelegate)
            await OnMachinesListUpdated.InvokeAsync((list) => [result.CreatedMachine, ..list]);

        MachineName = null;
        MachineType = null;
    }
}
