using Microsoft.AspNetCore.Components.Server.Circuits;

namespace PlexAdmin.CircuitHandlers
{
    public class FileDownloadCircuitHandler : CircuitHandler
    {
        private readonly ILogger<FileDownloadCircuitHandler> _logger;

        public FileDownloadCircuitHandler(ILogger<FileDownloadCircuitHandler> logger)
        {
            _logger = logger;
        }

        public override Task OnCircuitOpenedAsync(Circuit circuit, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Circuit opened: {CircuitId}", circuit.Id);
            return Task.CompletedTask;
        }

        public override Task OnConnectionUpAsync(Circuit circuit, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Connection up: {CircuitId}", circuit.Id);
            return Task.CompletedTask;
        }

        public override Task OnConnectionDownAsync(Circuit circuit, CancellationToken cancellationToken)
        {
            _logger.LogWarning("Connection down: {CircuitId}", circuit.Id);
            // Don't throw or propagate errors - just log it
            return Task.CompletedTask;
        }

        public override Task OnCircuitClosedAsync(Circuit circuit, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Circuit closed: {CircuitId}", circuit.Id);
            return Task.CompletedTask;
        }
    }
}
