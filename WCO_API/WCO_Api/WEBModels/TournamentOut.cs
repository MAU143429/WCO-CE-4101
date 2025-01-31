﻿namespace WCO_Api.WEBModels
{
    public class TournamentOut
    {

        public string? ToId { get; set; }
        public string Name { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }

        public List<TeamWEB> teams { get; set; }
        public List<BracketWEB> brackets { get; set; }

    }
}
