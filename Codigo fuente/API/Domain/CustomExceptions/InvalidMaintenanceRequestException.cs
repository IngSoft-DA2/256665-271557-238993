using Microsoft.Extensions.Logging;

namespace Domain;

public class InvalidMaintenanceRequestException : Exception
{
    public InvalidMaintenanceRequestException(string message) : base(message)
    {
    }
}