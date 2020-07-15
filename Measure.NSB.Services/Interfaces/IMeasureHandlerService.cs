using Messages.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Measure.NSB.Services.Interfaces
{
    public interface IMeasureHandlerService
    {
        Task UpdateStatusAsync(SubscriberUpdated message);
    }
}
