﻿using System;
using NLog;
using NzbDrone.Core.Configuration;
using NzbDrone.Core.Tv;
using NzbDrone.Core.Providers.Core;
using NzbDrone.Core.Repository;
using Prowlin;

namespace NzbDrone.Core.Providers.ExternalNotification
{
    public class Prowl : ExternalNotificationBase
    {
        private readonly ProwlProvider _prowlProvider;

        public Prowl(IConfigService configService, ProwlProvider prowlProvider)
            : base(configService)
        {
            _prowlProvider = prowlProvider;
        }

        public override string Name
        {
            get { return "Prowl"; }
        }

        public override void OnGrab(string message)
        {
            try
            {
                if(_configService.GrowlNotifyOnGrab)
                {
                    _logger.Trace("Sending Notification to Prowl");
                    const string title = "Episode Grabbed";

                    var apiKeys = _configService.ProwlApiKeys;
                    var priority = _configService.ProwlPriority;

                    _prowlProvider.SendNotification(title, message, apiKeys, (NotificationPriority)priority);
                }
            }

            catch (Exception ex)
            {
                _logger.WarnException(ex.Message, ex);
            }
        }

        public override void OnDownload(string message, Series series)
        {
            try
            {
                if (_configService.GrowlNotifyOnDownload)
                {
                    _logger.Trace("Sending Notification to Prowl");
                    const string title = "Episode Downloaded";

                    var apiKeys = _configService.ProwlApiKeys;
                    var priority = _configService.ProwlPriority;

                    _prowlProvider.SendNotification(title, message, apiKeys, (NotificationPriority)priority);
                }
            }

            catch (Exception ex)
            {
                _logger.WarnException(ex.Message, ex);
            }
        }

        public override void OnRename(string message, Series series)
        {
            
        }

        public override void AfterRename(string message, Series series)
        {

        }
    }
}
