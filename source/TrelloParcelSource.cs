using Manatee.Trello;
using ParcelTracker.courier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelTracker.source
{
    class TrelloParcelSource : IParcelSource
    {
        private const string DELIVERED_LIST_NAME = "Delivered";
        private const string INCOMPLETE_LIST_NAME = "Incomplete";
        private readonly IBoard board;

        public TrelloParcelSource(Settings settings)
        {
            var appKey = settings.TrelloAppKey;
            var userToken = settings.TrelloUserToken;
            var boardId = settings.TrelloBoardId;

            if (appKey == null || userToken == null || boardId == null)
            {
                throw new ArgumentException("Please provide appKey, userToken, and boardId in order to use the Trello parcel source");
            }

            var trelloFactory = new TrelloFactory();
            var auth = new TrelloAuthorization
            {
                AppKey = appKey,
                UserToken = userToken
            };

            board = trelloFactory.Board(boardId, auth);
        }

        public async Task<List<ParcelDefinition>> GetParcels()
        {
            var cards = await FetchCards();
            // TODO: Handle incomplete parcel definitions, will throw and crash now
            var parcels = cards.Select(ParseParcelDefinition);

            return parcels.ToList();
        }

        private async Task<IEnumerable<ICard>> FetchCards()
        {
            await board.Cards.Refresh();
            var cards = board.Cards.Where(card => card.List.Name != DELIVERED_LIST_NAME);

            var refreshTasks = cards.Aggregate(new List<Task>(), (tasks, card) =>
            {
                tasks.Add(card.Refresh());
                tasks.Add(card.CustomFields.Refresh());
                return tasks;
            });

            await Task.WhenAll(refreshTasks);

            return cards;
        }

        private ParcelDefinition ParseParcelDefinition(ICard card)
        {
            var description = card.Description;
            var barcode = ParseCustomFieldValue(card, "Barcode");
            var postalCode = ParseCustomFieldValue(card, "PostalCode");
            var destinationCountry = ParseCustomFieldValue(card, "DestinationCountry");
            var courierName = ParseCustomFieldValue(card, "Courier");
            try
            {
                var courier = Enum.Parse<CourierImplementation>(courierName, true);

                return new ParcelDefinition
                {
                    Description = description,
                    Barcode = barcode,
                    PostalCode = postalCode,
                    DestinationCountry = destinationCountry,
                    Courier = courier
                };
            }
            catch (ArgumentException)
            {
                throw new Exception($"No tracker implememented for courier '{courierName}'");
            }
        }

        private string ParseCustomFieldValue(ICard card, string customFieldName)
        {
            var customField = card.CustomFields.First((customField) => customField.Definition.Name == customFieldName);

            if (customField == null) return null;

            return ((CustomField<string>)customField).Value;
        }
    }
}
