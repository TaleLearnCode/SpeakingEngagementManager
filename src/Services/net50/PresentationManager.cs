using Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleLearnCode.SpeakingEngagementManager.Domain;

namespace TaleLearnCode.SpeakingEngagementManager.Services
{
	
	public class PresentationManager
	{

		private CosmosContainer _WriteContainer;
		private CosmosContainer _ReadContainer;

		public PresentationManager(CosmosContainer writeContainer, CosmosContainer readContainer)
		{
			_WriteContainer = writeContainer;
			_ReadContainer = readContainer;
		}

		//public async Task<Presentation> CreatePresentationAsync(Presentation presentation)
		//{
			
		//	if (presentation.SessionTypes.Any())
		//	{

		//	}
		//}

	}

}