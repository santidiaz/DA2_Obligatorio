using BusinessEntities;
using BusinessEntities.Exceptions;
using SportsWebApi.Models.EventModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsWebApi.Utilities
{
    public static class Utility
    {
        public static int GetStatusResponse(EntitiesException exception)
        {
            int response = 500;
            switch (exception.StatusCode)
            {
                case ExceptionStatusCode.NotModified:
                    response = 304;
                    break;
                case ExceptionStatusCode.NotFound:
                    response = 404;
                    break;
                case ExceptionStatusCode.InvalidData:
                    response = 400;
                    break;
                case ExceptionStatusCode.Conflict:
                    response = 409; // Conflict with the resource, for example trying to add an object that already exists in DB.
                    break;
                case ExceptionStatusCode.Undefined:
                    response = 500;
                    break;
                default:
                    break;
            }
            return response;
        }

        public static List<EventDTO> TransformEventsToDTOs(List<Event> events)
        {
            var result = new List<EventDTO>();
            EventDTO newDTO;
            events?.ForEach(
                e =>
                {
                    result.Add(newDTO = new EventDTO
                    {
                        Id = e.Id,
                        InitialDate = e.InitialDate,
                        Teams = e.EventTeams.Select(t => t.Team).ToList(),
                        Comments = e.Comments,
                        AllowMultipleTeams = e.MultipleTeamsEvent,
                        SportId = e.Sport.Id,
                        SportName = e.Sport.Name,
                        Result = e.Result
                    });
                });
            return result;
        }

        public static EventDTO TransformEventToDTO(Event events)
        {
            return new EventDTO
            {
                Id = events.Id,
                InitialDate = events.InitialDate,
                Teams = events.EventTeams?.Select(t => t.Team)?.ToList(),
                Comments = events.Comments ?? null,
                AllowMultipleTeams = events.MultipleTeamsEvent,
                SportId = events.Sport?.Id ?? 0,
                SportName = events.Sport?.Name,
                Result = events.Result
            };
        }
    }
}
