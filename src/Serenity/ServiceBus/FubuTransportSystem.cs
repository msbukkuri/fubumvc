﻿using System.Collections.Generic;
using FubuMVC.Core;
using FubuMVC.Core.ServiceBus.Configuration;
using FubuMVC.Core.ServiceBus.Diagnostics;
using FubuMVC.Core.ServiceBus.TestSupport;
using StoryTeller;

namespace Serenity.ServiceBus
{
    public class FubuTransportSystem<T> : FubuMvcSystem<T> where T : FubuRegistry, new()
    {

        public FubuTransportSystem() : base(null)
        {
            FubuTransport.SetupForTesting(); // Uses FubuMode.SetUpTestingMode();

            OnStartup<IMessagingSession>(x => FubuMVC.Core.Services.Messaging.EventAggregator.Messaging.AddListener(x));

            // Clean up all the existing queue state to prevent test pollution
            OnContextCreation<TransportCleanup>(cleanup => {
                cleanup.ClearAll();

                RemoteSubSystems.Each(x => x.Runner.SendRemotely(new ClearAllTransports()));
            });

            OnContextCreation<IMessagingSession>(
                x =>
                {
                    x.ClearAll();
                    RemoteSubSystems.Each(sys => sys.Runner.Messaging.AddListener(x));
                });

            OnContextCreation(TestNodes.Reset);
        }

        public override void ApplyLogging(ISpecContext context)
        {
            context.Reporting.Log(new MessageContextualInfoProvider(Application.Services.GetInstance<IMessagingSession>()));
        }
    }
}