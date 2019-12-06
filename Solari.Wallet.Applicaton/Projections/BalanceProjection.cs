﻿using LiquidProjections;
using Newtonsoft.Json;
using Solari.Wallet.Domain.Events;
using Solari.Wallet.Domain.Models;
using SqlStreamStore;
using SqlStreamStore.Streams;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Solari.Wallet.Application.Projections
{
	public class BalanceProjection : IBalanceProjection
	{
		private readonly IEventMap<Balance> _map;
		public Balance Balance { get; } = new Balance(0, DateTime.UtcNow);
		public Balance GetBalance()
		{
			return Balance;
		}
		public BalanceProjection(IStreamStore streamStore, StreamId streamId)
		{
			var mapBuilder = new EventMapBuilder<Balance>();
			mapBuilder.Map<Deposited>().As((deposited, balance) =>
			{
				balance.Add(deposited.Amount);
			});
			mapBuilder.Map<Deposited>()
					  .When(deposited => deposited.Amount == 100)
					  .As((deposited, balance) =>
					  {
						  //Bonus
						  balance.Add(deposited.Amount);
					  });

			mapBuilder.Map<Withdrawn>().As((withdrawn, balance) =>
			{
				balance.Subtract(withdrawn.Amount);
			});

			_map = mapBuilder.Build(new ProjectorMap<Balance>()
			{
				Custom = (context, projector) => projector()
			});

			streamStore.SubscribeToStream(streamId, null, StreamMessageReceived);
		}

		private async Task<object> DeserializeJsonEvent(StreamMessage streamMessage, CancellationToken cancellationToken)
		{
			var json = await streamMessage.GetJsonData(cancellationToken);

			switch (streamMessage.Type)
			{
				case "Deposited":
					return JsonConvert.DeserializeObject<Deposited>(json);
				case "Withdrawn":
					return JsonConvert.DeserializeObject<Withdrawn>(json);
				default:
					throw new InvalidOperationException("Unknown event type.");
			}
		}

		private async Task StreamMessageReceived(IStreamSubscription subscription, StreamMessage streamMessage, CancellationToken cancellationToken)
		{
			var @event = await DeserializeJsonEvent(streamMessage, cancellationToken);
			await _map.Handle(@event, Balance);
		}

	}
}
