namespace HR.LeaveManagement.Application.Contracts.Logging;

#pragma warning disable S2326
// ReSharper disable once UnusedTypeParameter
public interface IAppLogger<T>
#pragma warning restore S2326

{
    void LogInformation(string message, params object?[] args);
    void LogWarning(string message, params object?[] args);
    void LogError(string message, params object?[] args);
}