﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NLog;
using Newtonsoft.Json;
using NzbDrone.Common;
using NzbDrone.Core.Configuration;
using NzbDrone.Core.Providers.Core;
using NzbDrone.Core.Repository;
using PetaPoco;

namespace NzbDrone.Core.Providers
{
    public class ReferenceDataProvider
    {
        private readonly IDatabase _database;
        private readonly HttpProvider _httpProvider;
        private readonly IConfigService _configService;

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public ReferenceDataProvider(IDatabase database, HttpProvider httpProvider, IConfigService configService)
        {
            _database = database;
            _httpProvider = httpProvider;
            _configService = configService;
        }

        public virtual void UpdateDailySeries()
        {
            //Update all series in DB
            //DailySeries.csv

            var seriesIds = GetDailySeriesIds();

            if (seriesIds.Any())
            {
                var dailySeriesString = String.Join(", ", seriesIds);
                var sql = String.Format("UPDATE Series SET IsDaily = 1 WHERE SeriesId in ({0})", dailySeriesString);

                _database.Execute(sql);
            }
        }

        public virtual bool IsSeriesDaily(int seriesId)
        {
            return GetDailySeriesIds().Contains(seriesId);
        }

        public List<int> GetDailySeriesIds()
        {
            try
            {
                var dailySeriesIds = _httpProvider.DownloadString(_configService.ServiceRootUrl + "/DailySeries/AllIds");

                var seriesIds = JsonConvert.DeserializeObject<List<int>>(dailySeriesIds);

                return seriesIds;
            }
            catch (Exception ex)
            {
                Logger.WarnException("Failed to get Daily Series", ex);
                return new List<int>();
            }

        }
    }
}
