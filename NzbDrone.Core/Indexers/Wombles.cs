﻿using System.Linq;
using System;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using NzbDrone.Common;
using NzbDrone.Core.Configuration;
using NzbDrone.Core.Model;
using NzbDrone.Core.Providers.Core;

namespace NzbDrone.Core.Indexers
{
    public class Wombles : IndexerBase
    {
        public Wombles(HttpProvider httpProvider, IConfigService configService) : base(httpProvider, configService)
        {
        }

        protected override string[] Urls
        {
            get
            {
                return new[]
                           {
                               string.Format("http://nzb.isasecret.com/rss")
                           };
            }
        }

        public override bool IsConfigured
        {
            get
            {
                return true;
            }
        }

        public override string Name
        {
            get { return "WomblesIndex"; }
        }

        protected override string NzbDownloadUrl(SyndicationItem item)
        {
            return item.Links[0].Uri.ToString();
        }

        protected override string NzbInfoUrl(SyndicationItem item)
        {
            return null;
        }

        protected override IList<string> GetEpisodeSearchUrls(string seriesTitle, int seasonNumber, int episodeNumber)
        {
            return new List<string>();
        }

        protected override IList<string> GetSeasonSearchUrls(string seriesTitle, int seasonNumber)
        {
            return new List<string>();
        }

        protected override IList<string> GetDailyEpisodeSearchUrls(string seriesTitle, DateTime date)
        {
            return new List<string>();
        }

        protected override IList<string> GetPartialSeasonSearchUrls(string seriesTitle, int seasonNumber, int episodeWildcard)
        {
            return new List<string>();
        }

        protected override EpisodeParseResult CustomParser(SyndicationItem item, EpisodeParseResult currentResult)
        {
            if (currentResult != null)
            {
                currentResult.Size = 0;
            }

            return currentResult;
        }

        public override bool EnabledByDefault
        {
            get { return true; }
        }
    }
}