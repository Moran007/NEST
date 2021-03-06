﻿using System.Linq;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Search.ScriptFields
{
	[TestFixture]
	public class ScriptFieldsTest : CleanStateIntegrationTests
	{
		private string _LookFor = NestTestData.Data.First().Followers.First().FirstName;

		[Test]
		public void SimpleExplain()
		{
			var queryResults = this._client.Search<ElasticSearchProject>(s=>s
				.From(0)
				.Size(10)
				.MatchAll()
				.Fields(f=>f.Name)
				.ScriptFields(sf=>sf
					.Add("locscriptfield", sff=>sff
						.Script("doc['loc'].value * multiplier")
						.Params(sp=>sp
							.Add("multiplier", 4)
						)
					)
				)
			);
			Assert.True(queryResults.IsValid);
			Assert.True(queryResults.Documents.Any());
			Assert.True(queryResults.Documents.All(d=>d.LocScriptField != 0));
		}
	}
}