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
        /// <summary>
        /// Adds the channel data and lap information from a DataLog object to the current log.
        /// </summary>
        /// <param name="data_log">The DataLog object containing channel data and lap information to be added.</param>
        public void AddDataLog(DataLog data_log)
        {
            foreach (KeyValuePair<string, Channel> channel in data_log.channels)
            {
                Channel log_channel = channel.Value;

                LdChan ld_channel = new LdChan(log_channel.freq, (ushort)0, (ushort)1, (ushort)1, (short)0, log_channel.name, log_channel.short_name, log_channel.units);

                ld_channel.Data = log_channel.values.ToArray();

                ld_channels.Add(ld_channel);
            }

            int fastestLap = 0;
            double fastestLapTime = 0;

            // Iterate through lap times in the DataLog and add them as LdBeacon objects
            for (int i = 1; i < data_log.lapTimes.Count; i++)
            {
                double time = data_log.lapTimes[i];

                // Create an LdBeacon object representing a lap time beacon and add it to the list
                ld_beacons.Add(new LdBeacon(100, "BCN", i.ToString(), 77, time));

                // Determine the fastest lap and its time
                if (i == 1 || fastestLapTime > time)
                {
                    fastestLap = i;
                    fastestLapTime = time;
                }
            }

            // Create an LdLapInfo object representing lap information and assign it to the log
            ld_lapInfo = new LdLapInfo(data_log.lapTimes.Count, fastestLapTime, fastestLap);
        }

        /// <summary>
        /// Writes the logged data to two separate files: a main log file and an extension file.
        /// </summary>
        /// <param name="logFilename">The path to the main log file where the logged data will be stored.</param>
        /// <param name="extensionFilename">The path to the extension file where additional data will be stored.</param>
        public void Write(string logFilename, string extensionFilename)
        {
            if (ld_channels.Any())
            {
                // Create an instance of LdData to prepare and write the logged data
                LdData ld_data = new LdData(ld_header, ld_channels, ld_beacons, ld_lapInfo);

                // Prepare pointers and write data to the main log and extension files
                ld_data.prepPointers();
                ld_data.Write(logFilename, extensionFilename);

                // Clear the logged data after writing
                Clear();
            }
            else
            {
                // Show a message box indicating that no channels exist to log
                MessageBox.Show("No channels", "Error", MessageBoxButtons.OK);
            }
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