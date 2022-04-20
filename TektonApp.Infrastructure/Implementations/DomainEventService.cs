using MediatR;
using Microsoft.Extensions.Logging;
using TektonApp.Application.Common;
using TektonApp.Application.Common.Interfaces;
using TektonApp.Common;
using TektonApp.Infrastructure.Attributes;

namespace TektonApp.Infrastructure.Implementations
{
    [ScopedService]
    public class DomainEventService : IDomainEventService
    {
        private readonly ILogger<DomainEventService> _logger;

        public DomainEventService(ILogger<DomainEventService> logger)
        {
            _logger = logger;
        }

        public async Task Publish(DomainEvent domainEvent)
        {
            _logger.LogInformation("Publishing domain event. Event - {event}", domainEvent.GetType().Name);
        }

        private INotification GetNotificationCorrespondingToDomainEvent(DomainEvent domainEvent)
        {
            return (INotification)Activator.CreateInstance(
                typeof(DomainEventNotification<>).MakeGenericType(domainEvent.GetType()), domainEvent);
        }
    }
}
