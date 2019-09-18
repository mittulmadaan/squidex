﻿// ==========================================================================
//  Squidex Headless CMS
// ==========================================================================
//  Copyright (c) Squidex UG (haftungsbeschraenkt)
//  All rights reserved. Licensed under the MIT license.
// ==========================================================================

using System;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace Squidex.ICIS.Kafka.Producer
{
    public interface IKafkaProducer<T> : IDisposable
    {
        Task<DeliveryResult<string, T>> Send(string topicName, string key, T val);
    }
}