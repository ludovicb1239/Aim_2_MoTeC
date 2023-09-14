using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Aim_2_MoTeC
{
    class MotecLog
    {
        public string driver;
        public string vehicle_id;
        public string venue_name;
        public string event_session;
        public string short_comment;
        public DateTimeStruct datetime;

        // File components from ldparser
        private LdHead ld_header;
        private List<LdChan> ld_channels;
        private List<LdBeacon> ld_beacons;
        private LdLapInfo ld_lapInfo;

        public MotecLog()
        {
            driver = "";
            vehicle_id = "";
            venue_name = "";
            event_session = "";
            short_comment = "";
            datetime = new DateTimeStruct();

            ld_header = null;
            ld_channels = new List<LdChan>();
            ld_beacons = new List<LdBeacon>();
            ld_lapInfo = null;
        }

        public void Initialize()
        {
            ld_header = new LdHead(driver, vehicle_id, venue_name, datetime, short_comment, event_session);
        }

        public void AddDataLog(DataLog data_log)
        {
            foreach (KeyValuePair<string, Channel> channel in data_log.channels)
            {
                Channel log_channel = channel.Value;
                LdChan ld_channel = new(log_channel.freq, (ushort)0, (ushort)1, (ushort)1, (short)0, log_channel.name, log_channel.short_name, log_channel.units);

                // Add in the channel data
                ld_channel.Data = log_channel.values.ToArray();

                // Add the ld channel
                ld_channels.Add(ld_channel);
            }

            int fastestLap = 0;
            double fastestLapTime = 0;
            for (int i = 1; i < data_log.lapTimes.Count; i++)
            {
                double time = data_log.lapTimes[i];
                ld_beacons.Add(new LdBeacon(100, "BCN", i.ToString(), 77, time));

                if (i == 1 || fastestLapTime > time)
                {
                    fastestLap = i;
                    fastestLapTime = time;
                }
            }
            ld_lapInfo = new LdLapInfo(data_log.lapTimes.Count, fastestLapTime, fastestLap);
        }

        public void Write(string logFilename, string extensionFilename)
        {
            if (ld_channels.Any())
            {
                LdData ld_data = new(ld_header, ld_channels, ld_beacons, ld_lapInfo);
                ld_data.prepPointers();
                ld_data.Write(logFilename, extensionFilename);

                Clear();
            }
            else
                MessageBox.Show("No channels", "Error", MessageBoxButtons.OK);
        }
        public void Clear()
        {
            ld_header = null;
            ld_channels.Clear();
            ld_beacons.Clear();
            ld_lapInfo = null;
        }
    }
}