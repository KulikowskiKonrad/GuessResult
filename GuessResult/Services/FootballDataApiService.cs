using GuessResult.DB.Models;
using GuessResult.Helpers;
using GuessResult.Repositories;
using GuessResult.Repositories.Interfaces;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuessResult.Services
{
    public class FootballDataApiService : IFootballDataApiService
    {
        private IEventRepository _eventRepository;

        public FootballDataApiService()
        {

        }

        public FootballDataApiService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public void ImportEvents()
        {
            try
            {
                var client = new RestClient("http://api.football-data.org/");
                // client.Authenticator = new HttpBasicAuthenticator(username, password);

                var request = new RestRequest("v2/matches", Method.GET);
                request.AddParameter("dateFrom", DateTime.Today.AddDays(-3).ToString("yyyy-MM-dd")); // adds to POST or URL querystring based on Method
                request.AddParameter("dateTo", DateTime.Today.AddDays(7).ToString("yyyy-MM-dd"));

                // easily add HTTP Headers
                request.AddHeader("X-Auth-Token", "aab12198943d4ab0a9b56e7f520e9826");

                // or automatically deserialize result
                // return content type is sniffed but can be explicitly set via RestClient.AddHandler();
                var response2 = client.Execute<RootObject>(request);

                foreach (var item in response2.Data.matches)
                {
                    GREvent gREvent = _eventRepository.GetByExternalMatchId(item.id);
                    if (gREvent == null)
                    {
                        gREvent = new GREvent()
                        {
                            AwayTeamName = item.awayTeam.name,
                            AwayTeamScore = (byte?)item.score.fullTime.awayTeam,
                            ExternalMatchId = item.id,
                            HomeTeamName = item.homeTeam.name,
                            HomeTeamScore = (byte?)item.score.fullTime.homeTeam,
                            StartDate = item.utcDate,
                            PredictionType = Enum.EventPredictionType.ExactScore
                        };
                        _eventRepository.Save(gREvent);
                    }
                    else if (gREvent.HomeTeamScore != item.score.fullTime.homeTeam && gREvent.AwayTeamScore != item.score.fullTime.awayTeam)
                    {
                        gREvent.AwayTeamScore = (byte?)item.score.fullTime.awayTeam;
                        gREvent.HomeTeamScore = (byte?)item.score.fullTime.homeTeam;
                        _eventRepository.Save(gREvent);
                        UserEventRepository userEventRepository = new UserEventRepository();
                        userEventRepository.UpdateIsPredictionCorrect(gREvent.Id);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
            }
        }

        #region GeneratedClasses
        private class Filters
        {
            public string dateFrom { get; set; }
            public string dateTo { get; set; }
            public string permission { get; set; }
            public List<int> competitions { get; set; }
        }

        private class Competition
        {
            public int id { get; set; }
            public string name { get; set; }
        }

        private class Season
        {
            public int id { get; set; }
            public string startDate { get; set; }
            public string endDate { get; set; }
            public int currentMatchday { get; set; }
        }

        private class HomeTeam
        {
            public int id { get; set; }
            public string name { get; set; }
        }

        private class AwayTeam
        {
            public int id { get; set; }
            public string name { get; set; }
        }

        private class FullTime
        {
            public int? homeTeam { get; set; }
            public int? awayTeam { get; set; }
        }

        private class HalfTime
        {
            public int? homeTeam { get; set; }
            public int? awayTeam { get; set; }
        }

        private class ExtraTime
        {
            public object homeTeam { get; set; }
            public object awayTeam { get; set; }
        }

        private class Penalties
        {
            public object homeTeam { get; set; }
            public object awayTeam { get; set; }
        }

        private class Score
        {
            public string winner { get; set; }
            public string duration { get; set; }
            public FullTime fullTime { get; set; }
            public HalfTime halfTime { get; set; }
            public ExtraTime extraTime { get; set; }
            public Penalties penalties { get; set; }
        }

        private class Match
        {
            public int id { get; set; }
            public Competition competition { get; set; }
            public Season season { get; set; }
            public DateTime utcDate { get; set; }
            public string status { get; set; }
            public int matchday { get; set; }
            public string stage { get; set; }
            public string group { get; set; }
            public DateTime lastUpdated { get; set; }
            public HomeTeam homeTeam { get; set; }
            public AwayTeam awayTeam { get; set; }
            public Score score { get; set; }
            public List<object> referees { get; set; }
        }

        private class RootObject
        {
            public int count { get; set; }
            public Filters filters { get; set; }
            public List<Match> matches { get; set; }
        }
        #endregion
    }
}