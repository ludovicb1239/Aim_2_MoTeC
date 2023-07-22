﻿using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Aim_2_MoTeC
{
    class DataLog
    {
        public string name;
        public Dictionary<string, Channel> channels;
        public List<double> lapTimes;

        public DataLog(string name = "")
        {
            this.name = name;
            this.channels = new Dictionary<string, Channel>();
            this.lapTimes = new List<double>();
        }

        public void Clear()
        {
            channels.Clear();
            lapTimes.Clear();
        }

        public void AddChannel(string name, string units, ushort freq, string short_name = "")
        {
            channels[name] = new Channel(name, units, freq, short_name);
        }
        public void fromXRK(int id, BackgroundWorker sender, bool useRawGPS = false, bool convertName = false)
        {
            Clear();

            int lapsCount = XRK.GetLapsCount(id);
            double totalTime = 0;
            AddChannel("Lap Time", "s", 10, "LapTime"); //Sampling those at 10hz
            AddChannel("Lap Number", "", 10, "Lap");
            List<float> L_val = new();
            List<float> L_lap = new();
            for (int lap = 0; lap < lapsCount; lap++)
            {
                XRK.GetLapInfo(id, lap, out double t, out double v);
                totalTime += v;
                lapTimes.Add(t);
                for (double pointingTime = t; pointingTime < t + v; pointingTime += 0.1f)
                {
                    L_val.Add((float)v);
                    L_lap.Add(lap);
                }
            }
            channels["Lap Time"].setValues(L_val);
            channels["Lap Number"].setValues(L_lap);

            int channelCount = XRK.GetChannelsCount(id);
            for (int c = 0; c < channelCount; c++)
            {
                string name = XRK.GetChannelName(id, c);
                string name_short = "";
                if (convertName && ChannelNamesConvert.containsName(name, out nameConvert stru))
                {
                    name = stru.to;
                    name_short = stru.to_short;
                }

                string units = XRK.GetChannelUnits(id, c);
                int dataCount = XRK.GetChannelSamplesCount(id, c);
                double[] v = new double[dataCount];
                double[] t = new double[dataCount];
                int res = XRK.GetChannelSamples(id, c, t, v, dataCount);

                AddChannel(name, units, (ushort)Math.Round((float)dataCount / totalTime), name_short);

                channels[name].setValues(v);

                // Report the progress
                sender.ReportProgress((int)MathF.Round((c + 1f) * 100f / channelCount));
            }

            // Report the progress
            sender.ReportProgress(0);

            if (useRawGPS)
            {
                int GPSChannelCount = XRK.GetGPSRawChannelsCount(id);
                for (int c = 0; c < GPSChannelCount; c++)
                {
                    string name = XRK.GetGPSRawChannelName(id, c);
                    string units = XRK.GetGPSRawChannelUnits(id, c);
                    int dataCount = XRK.GetGPSRawChannelSamplesCount(id, c);
                    double[] v = new double[dataCount];
                    double[] t = new double[dataCount];
                    int res = XRK.GetGPSRawChannelSamples(id, c, t, v, dataCount);

                    AddChannel(name, units, (ushort)Math.Round((float)dataCount / totalTime)); //double

                    channels[name].setValues(v);

                    // Report the progress
                    sender.ReportProgress((int)MathF.Round((c + 1f) * 100f / GPSChannelCount));
                }
            }
            else
            {
                int GPSChannelCount = XRK.GetGPSChannelsCount(id);
                for (int c = 0; c < GPSChannelCount; c++)
                {
                    string name = XRK.GetGPSChannelName(id, c);
                    string units = XRK.GetGPSChannelUnits(id, c);
                    int dataCount = XRK.GetGPSChannelSamplesCount(id, c);
                    double[] v = new double[dataCount];
                    double[] t = new double[dataCount];
                    int res = XRK.GetGPSChannelSamples(id, c, t, v, dataCount);

                    AddChannel(name, units, (ushort)Math.Round((float)dataCount / totalTime)); //double

                    channels[name].setValues(v);

                    // Report the progress
                    sender.ReportProgress((int)MathF.Round((c + 1f) * 100f / GPSChannelCount));
                }
            }

        }
    }

    public class Channel
    {
        public string name;
        public string short_name;
        public string units;
        public float[] values;
        public ushort freq;

        public Channel(string name, string units, ushort freq, string short_name = "")
        {
            this.name = name;
            this.short_name = short_name;
            this.units = units;
            this.freq = freq;
        }
        public void setValues(List<float> v)
        {
            values = v.ToArray();
        }
        public void setValues(double[] v)
        {
            values = Array.ConvertAll(v, d => (float)d);
        }
    }
}
