using Azure.Cosmos;
using System;
using System.Threading.Tasks;
using TaleLearnCode.SpeakingEngagementManager.Domain;

namespace TaleLearnCode.SpeakingEngagementManager.Services
{

	public class ShindigManager
	{

		private CosmosContainer _CosmosContainer;

		public ShindigManager(CosmosConnection cosmosConnection)
		{
			_CosmosContainer = cosmosConnection.Container;
		}

		public async Task<Shindig> AddShindigAsync(Shindig shindig)
		{
			shindig.IsValid(); // Method will throw exception is document is not valid
			return await Common.CreateDocumentAsync<Shindig>(_CosmosContainer, shindig);
		}

		public async Task<ShindigSubmission> SubmitPresentationToShindigAsync(Shindig shindig, Presentation presentation, SessionType sessionType, DateTime? submissionDate = null, DateTime? notificiationDate = null, bool? accepted = null)
		{
			return await Common.CreateDocumentAsync<ShindigSubmission>(
				_CosmosContainer,
				new ShindigSubmission()
				{
					PresentationId = presentation.Id,
					PresentationTitle = presentation.Name,
					ShindigId = shindig.Id,
					ShindigName = shindig.Name,
					ShindigStartDate = shindig.StartDate,
					ShindigEndDate = shindig.EndDate,
					SessionType = sessionType,
					SubmissionDate = submissionDate,
					NotificationDate = notificiationDate,
					Accepted = accepted
				});
		}

		public async Task<ShindigPresentation> PresentationScheuledAsync(Shindig shindig, Presentation presentation, SessionType sessionType, DateTime? dateTime = null, string room = null)
		{
			return await Common.CreateDocumentAsync<ShindigPresentation>(
				_CosmosContainer,
				new ShindigPresentation()
				{
					PresentationId = presentation.Id,
					PresentationTitle = presentation.Name,
					ShindigId = shindig.Id,
					ShindigName = shindig.Name,
					DateTime = dateTime,
					SessionType = sessionType,
					Room = room
				});
		}

		public async Task<ShindigPresentation> SubmissionAcceptedAsync(ShindigSubmission shindigSubmission, DateTime? dateTime = null, string room = null)
		{

			shindigSubmission.Accepted = true;
			await Common.UpdateDocumentAsync<ShindigSubmission>(_CosmosContainer, shindigSubmission);

			return await Common.CreateDocumentAsync<ShindigPresentation>(
				_CosmosContainer,
				new ShindigPresentation()
				{
					PresentationId = shindigSubmission.PresentationId,
					PresentationTitle = shindigSubmission.PresentationTitle,
					ShindigId = shindigSubmission.ShindigId,
					ShindigName = shindigSubmission.ShindigName,
					DateTime = dateTime,
					SessionType = shindigSubmission.SessionType,
					Room = room
				});
		}

	}

}