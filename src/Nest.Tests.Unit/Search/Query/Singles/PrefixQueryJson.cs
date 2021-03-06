﻿using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Query.Singles
{
	[TestFixture]
	public class PrefixQueryJson
	{
		[Test]
		public void TestPrefixQuery()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.Prefix(f => f.Name, "elasticsearch.*")
				);
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, query : 
			{ prefix: { name : { value : ""elasticsearch.*"" } }}}";
			Assert.True(json.JsonEquals(expected));
		}
		[Test]
		public void TestPrefixWithBoostQuery()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Query(q => q
					.Prefix(f => f.Name, "el", Boost: 1.2)
				);
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, query : 
			{ prefix: { name : { value : ""el"", boost: 1.2 } }}}";
			Assert.True(json.JsonEquals(expected), json);
		}

	}
}
