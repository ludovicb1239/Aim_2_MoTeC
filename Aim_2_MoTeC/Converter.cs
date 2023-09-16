﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Data;

namespace Aim_2_MoTeC
{
    using static XRK;
    public static class Converter
    {
        public static void Convert(string path, bool usingRAW_GPS, bool convertName, BackgroundWorker worker)
        {

            Console.WriteLine("Converting " + path);

            if (!getID(path, out int id)) throw new Exception("Failed to get ID using dll");


            ChannelNamesConvert nameConverter = new ChannelNamesConvert();
            DataLog data_log = new();
            data_log.nameConverter = nameConverter;
            data_log.fromXRK(id, worker, usingRAW_GPS, convertName);

            if (data_log.channels.Count == 0) throw new Exception("Failed to find any channels in log data");

            DateTimeStruct dateTime = GetDateAndTime(id);

            MotecLog motec_log = new()
            {
                driver = GetRacerName(id),
                vehicle_id = GetVehiculeName(id),
                venue_name = GetTrackName(id),
                event_session = GetVenueTypeName(id),
                short_comment = GetChampionshipName(id),
                datetime = dateTime
            };

            CloseFile(path);

            motec_log.Initialize();
            motec_log.AddDataLog(data_log);

            Console.WriteLine("Done adding data log");

            int year = dateTime.tm_year + 1900;
            int month = dateTime.tm_mon + 1;
            int day = dateTime.tm_mday;
            int hour = dateTime.tm_hour;
            int minute = dateTime.tm_min;
            int second = dateTime.tm_sec;

            Console.WriteLine("Saving MoTeC log...");
            string directoryPath = path == "" ? Path.GetDirectoryName(path) : path;
            string newFileName = string.Format("{0}{1:D2}{2:D2}-{3:D2}{4:D2}{5:D2}.ld", year, month, day, hour, minute, second);
            string newFileNameExtra = string.Format("{0}{1:D2}{2:D2}-{3:D2}{4:D2}{5:D2}.ldx", year, month, day, hour, minute, second);
            string newFilePath = Path.Combine(directoryPath, newFileName);
            string newFilePathExtra = Path.Combine(directoryPath, newFileNameExtra);

            if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
            {
                Console.WriteLine("Directory '{0}' does not exist, will create it", directoryPath);
                Directory.CreateDirectory(directoryPath);
            }

            Console.WriteLine("Output path " + newFilePath);

            motec_log.Write(newFilePath, newFilePathExtra);
            data_log.Clear();

            Console.WriteLine("Done working on " + path);
        }
        public static void Read(string filePath, bool useRawGPS, out List<string> data, out List<string> names)
        {
            data = new List<string>();
            names = new List<string>();

            if (!getID(filePath, out int id)) throw new Exception("Failed to get ID using dll");

            data = new()
            {
                "Vehicule name:     " + GetVehiculeName(id),
                "Track name:        " + GetTrackName(id),
                "Racer name:        " + GetRacerName(id),
                "Championship name: " + GetChampionshipName(id),
                "Venue type name:   " + GetVenueTypeName(id)
            };

            DateTimeStruct dateTime = GetDateAndTime(id);

            int year = dateTime.tm_year + 1900;
            int month = dateTime.tm_mon + 1;
            int day = dateTime.tm_mday;
            int hour = dateTime.tm_hour;
            int minute = dateTime.tm_min;
            int second = dateTime.tm_sec;
            data.Add(string.Format("Date:              {0}-{1:D2}-{2:D2}", year, month, day));
            data.Add(string.Format("Time:              {0:D2}:{1:D2}:{2:D2}", hour, minute, second));

            int lapsCount = GetLapsCount(id);
            data.Add("Laps count:        " + lapsCount);

            ChannelNamesConvert nameConverter = new ChannelNamesConvert();

            int channelCount = GetChannelsCount(id);
            names.Add("-- From --" + new string('\t', 2) + "-- Rename --");
            for (int c = 0; c < channelCount; c++)
            {
                string name = GetChannelName(id, c);
                if (nameConverter.containsName(name, out nameConvert convert))
                    names.Add(name + new string('\t', 4 - ((name.Length + 1) / 4)) + "--> " + convert.to);
                else
                    names.Add(name);
            }
            int gpsChannelCount = useRawGPS ? GetGPSRawChannelsCount(id) : GetGPSChannelsCount(id);
            names.Add("-- GPS --" + new string('\t', 3) + "-- Rename --");
            for (int c = 0; c < gpsChannelCount; c++)
            {
                string name = useRawGPS ? GetGPSRawChannelName(id, c) : GetGPSChannelName(id, c);
                if (nameConverter.containsName(name, out nameConvert convert))
                    names.Add(name + new string('\t', 4 - ((name.Length + 1) / 4)) + "--> " + convert.to);
                else
                    names.Add(name);
            }
            CloseFile(filePath);
        }
        private static void SearchForDrkFiles(string directoryPath, List<string> paths)
        {
            try
            {
                // Search for .drk files in the current directory
                paths.AddRange(Directory.GetFiles(directoryPath, "*.drk"));

                // Recursively search subdirectories
                string[] subdirectories = Directory.GetDirectories(directoryPath);

                foreach (string subdirectory in subdirectories)
                    SearchForDrkFiles(subdirectory, paths);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        public static List<string> getFileList(string path, bool folderMode)
        {
            List<string> filePaths = new();

            if (folderMode)
                SearchForDrkFiles(path, filePaths);
            else
                filePaths.Add(path);

            return filePaths;
        }
    }
}
