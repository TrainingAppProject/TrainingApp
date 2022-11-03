using System;
using HotChocolate.Execution;
using HotChocolate.Execution.Processing;
using HotChocolate.Subscriptions;
using Microsoft.AspNetCore.Cors.Infrastructure;
using TrainingAppAPI.Schema.Mutations;

namespace TrainingAppAPI.Schema
{
    public class Subscription
    {
        [Subscribe]
        public TemplateResult TemplateCreated([EventMessage] TemplateResult course) => course;


        [SubscribeAndResolve]
        public ValueTask<ISourceStream<TemplateResult>> TemplateUpdated(Guid courseId, [Service] ITopicEventReceiver topicEventReceiver)
        {
            string topicName = $"{courseId}_{nameof(Subscription.TemplateUpdated)}";

            return topicEventReceiver.SubscribeAsync<string, TemplateResult>(topicName);
        }
    }
}

    