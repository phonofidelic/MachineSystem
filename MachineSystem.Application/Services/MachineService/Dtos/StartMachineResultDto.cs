using System;
using System.Collections.Generic;
using System.Text;

namespace MachineSystem.Application.Services.MachineService.Dtos;

public class StartMachineResultDto(bool isError)
{
    public static bool IsError { get; } = IsError;
}
