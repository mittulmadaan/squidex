﻿using FluentAssertions;
using Squidex.Domain.Apps.Core.Contents;
using Squidex.Domain.Apps.Core.HandleRules.EnrichedEvents;
using Squidex.ICIS.Kafka.Entities;
using Squidex.ICIS.Kafka.Services;
using System;
using Xunit;

namespace Squidex.ICIS.Test.Kafka.Services
{
    public class CommentaryTypeMapperTests
    {
        [Fact]
        public void Should_map_commentary_type_from_value()
        {
            var data =
                new NamedContentData()
                    .AddField("id",
                        new ContentFieldData()
                            .AddValue("/ref/123"))
                    .AddField("name",
                        new ContentFieldData()
                            .AddValue("my-name"));

            var @event = new EnrichedContentEvent
            {
                Data = data
            };

            var result = CommentaryTypeMapper.ToAvro(@event);

            result.Should().BeEquivalentTo(new CommentaryType
            {
                Id = "/ref/123",
                Name = "my-name"
            });
        }

        [Fact]
        public void Should_throw_exception_if_data_not_found()
        {
            var data =
                new NamedContentData()
                    .AddField("id",
                        new ContentFieldData()
                            .AddValue("/ref/123"));

            var @event = new EnrichedContentEvent
            {
                Data = data
            };

            Assert.Throws<ArgumentException>(() => CommentaryTypeMapper.ToAvro(@event));
        }
    }
}