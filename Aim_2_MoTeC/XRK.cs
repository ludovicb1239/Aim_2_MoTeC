using System;
using System.Runtime.InteropServices;

namespace Aim_2_MoTeC
{
    [StructLayout(LayoutKind.Sequential)]
    public struct DateTimeStruct
    {
        public int tm_sec;   // seconds after the minute (0-61)
        public int tm_min;   // minutes after the hour (0-59)
        public int tm_hour;  // hours since midnight (0-23)
        public int tm_mday;  // day of the month (1-31)
        public int tm_mon;   // months since January (0-11)
        public int tm_year;  // years since 1900
        public int tm_wday;  // days since Sunday (0-6)
        public int tm_yday;  // days since January 1 (0-365)
        public int tm_isdst; // Daylight Saving Time flag
    }
    class XRK
    {
        /// <summary>Name of the library</summary>
        const string libName = "MatLabXRK-2017-64-ReleaseU.dll";

        /// <summary>Get the compile date of this library</summary>
        /// <returns>Compile date</returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr get_library_date();

        /// <summary>Get the compile time of this library </summary>
        /// <returns>Compile time</returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr get_library_time();

        /// <summary>Open a xrk file</summary>
        /// <param name="full_path_name">full path to the file to be opened, it cannot be a relative path</param>
        /// <returns>graeter than 0 in case of success the internal index of the file opened, 0 in case the file is opened but can't be parsed, less than 0 in case of problems </returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int open_file(string full_path_name);

        /// <summary>Close a xrk file</summary>
        /// <param name="full_path_name">full path to the file to be closed, it cannot be a relative path</param>
        /// <returns> greater than 0 the internal index of the file closed, less than 0 in case of problems </returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int close_file_n(string full_path_name);

        /// <summary>Close a xrk file</summary>
        /// <param name="idx">the internal file index returned by open function</param>
        /// <returns> greater than 0 the internal index of the file closed, less than 0 in case of problems </returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl)]
        private static extern int close_file_i(int idx);

        /// <summary>Get vehicle info</summary>
        /// <param name="idx">the internal file index returned by open function</param>
        /// <returns> a null terminated char pointer with the required info, NULL in case of problems </returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr get_vehicle_name(int idx);

        /// <summary>Get track info</summary>
        /// <param name="idx">the internal file index returned by open function</param>
        /// <returns> a null terminated char pointer with the required info, NULL in case of problems </returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr get_track_name(int idx);

        /// <summary>Get racer info</summary>
        /// <param name="idx">the internal file index returned by open function</param>
        /// <returns> a null terminated char pointer with the required info, NULL in case of problems </returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr get_racer_name(int idx);

        /// <summary>Get championship info</summary>
        /// <param name="idx">the internal file index returned by open function</param>
        /// <returns> a null terminated char pointer with the required info, NULL in case of problems </returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr get_championship_name(int idx);

        /// <summary>Get venue type info</summary>
        /// <param name="idx">the internal file index returned by open function</param>
        /// <returns> a null terminated char pointer with the required info, NULL in case of problems </returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr get_venue_type_name(int idx);

        /// <summary>Get session date and time</summary>
        /// <param name="idx">the internal file index returned by open function</param>
        /// <returns> a struct tm pointer with the required info, NULL in case of problems </returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr get_date_and_time(int idx);

        /// <summary>Get laps count of a xrk file</summary>
        /// <param name="idx">the internal file index returned by open function</param>
        /// <returns> greater than 0 the laps count, less than 0 in case of problems, 0 in case the xrk file has no laps (theorically not possible) </returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl)]
        private static extern int get_laps_count(int idx);

        /// <summary>Get lap info</summary>
        /// <param name="idxf">the internal file index returned by open function</param>
        /// <param name="idxl">the lap index</param>
        /// <param name="pstart">pointer to an already allocated double in which the start time of the lap will be stored</param>
        /// <param name="pduration">pointer to an already allocated double in which the lap duration will be stored</param>
        /// <returns> 1 if all is OK, less than 0 in case of problems, 0 in case the xrk file has no laps (theorically not possible) </returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl)]
        private static extern int get_lap_info(int idxf, int idxl, out double pstart, out double pduration);

        /// <summary> Get channels count of a xrk file</summary>
        /// <param name="idx">the internal file index returned by open function</param>
        /// <returns> 1 if all is OK, less than 0 in case of problems, 0 in case the xrk file has no laps (theorically not possible) </returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl)]
        private static extern int get_channels_count(int idx);

        /// <summary>Get channel name</summary>
        /// <param name="idxf">the internal file index returned by open function</param>
        /// <param name="idxc">the channel index</param>
        /// <returns> a null terminated char pointer with the required info, NULL in case of problems </returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr get_channel_name(int idxf, int idxc);

        /// <summary>Get channel units</summary>
        /// <param name="idxf">the internal file index returned by open function</param>
        /// <param name="idxc">the channel index</param>
        /// <returns> a null terminated char pointer with the required info, NULL in case of problems </returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr get_channel_units(int idxf, int idxc);

        /// <summary>Get channel samples count</summary>
        /// <param name="idxf">the internal file index returned by open function</param>
        /// <param name="idxc">the channel index</param>
        /// <returns> greater than 0 the samples count, less than 0 in case of problems, 0 in case the channel has no samples (theorically not possible) </returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl)]
        private static extern int get_channel_samples_count(int idxf, int idxc);

        /// <summary>Get channel samples</summary>
        /// <param name="idxf">the internal file index returned by open function</param>
        /// <param name="idxc">the channel index</param>
        /// <param name="ptimes">pointer to an already allocated buffer in which sample times will be stored</param>
        /// <param name="pvalues">pointer to an already allocated buffer in which sample values will be stored</param>
        /// <param name="cnt">the number of samples of the buffer, that is the samples count returned by get channel samples count function</param>
        /// <returns>greater than 0 the samples count, less than 0 in case of problems, 0 in case the cnt param doesn't match the samples count or in case the channel has no samples (theorically not possible)</returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl)]
        private static extern int get_channel_samples(int idxf, int idxc, double[] ptimes, double[] pvalues, int cnt);

        /// <summary>Get channel samples count in a given lap</summary>
        /// <param name="idxf">the internal file index returned by open function</param>
        /// <param name="idxl">the lap index</param>
        /// <param name="idxc">the channel index</param>
        /// <returns> greater than 0 the samples count, less than 0 in case of problems, 0 in case the channel has no samples (theorically not possible) </returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl)]
        private static extern int get_lap_channel_samples_count(int idxf, int idxl, int idxc);

        /// <summary>Get channel samples in a given lap</summary>
        /// <param name="idxf">the internal file index returned by open function</param>
        /// <param name="idxl">the lap index</param>
        /// <param name="idxc">the channel index</param>
        /// <param name="ptimes">pointer to an already allocated buffer in which sample times will be stored</param>
        /// <param name="pvalues">pointer to an already allocated buffer in which sample values will be stored</param>
        /// <param name="cnt">the number of samples of the buffer, that is the samples count returned by get channel samples count function</param>
        /// <returns> greater than 0 the samples count, less than 0 in case of problems, 0 in case the cnt param doesn't match the samples count or in case the channel has no samples (theorically not possible) </returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl)]
        private static extern int get_lap_channel_samples(int idxf, int idxl, int idxc, double[] ptimes, double[] pvalues, int cnt);

        /// <summary> Get GPS channels count of a xrk file</summary>
        /// <param name="idx">the internal file index returned by open function</param>
        /// <returns>greater than 0 the channel count, less than 0 in case of problems, 0 in case the xrk file has no channels (theorically not possible)</returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl)]
        private static extern int get_GPS_channels_count(int idx);

        /// <summary>Get GPS channel name</summary>
        /// <param name="idxf">the internal file index returned by open function</param>
        /// <param name="idxc">the channel index</param>
        /// <returns> a null terminated char pointer with the required info, NULL in case of problems </returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr get_GPS_channel_name(int idxf, int idxc);

        /// <summary>Get GPS channel units</summary>
        /// <param name="idxf">the internal file index returned by open function</param>
        /// <param name="idxc">the channel index</param>
        /// <returns> a null terminated char pointer with the required info, NULL in case of problems </returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr get_GPS_channel_units(int idxf, int idxc);

        /// <summary>Get GPS channel samples count</summary>
        /// <param name="idxf">the internal file index returned by open function</param>
        /// <param name="idxc">the channel index</param>
        /// <returns> greater than 0 the samples count, less than 0 in case of problems, 0 in case the channel has no samples (theorically not possible) </returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl)]
        private static extern int get_GPS_channel_samples_count(int idxf, int idxc);

        /// <summary>Get GPS channel samples</summary>
        /// <param name="idxf">the internal file index returned by open function</param>
        /// <param name="idxc">the channel index</param>
        /// <param name="ptimes">pointer to an already allocated buffer in which sample times will be stored</param>
        /// <param name="pvalues">pointer to an already allocated buffer in which sample values will be stored</param>
        /// <param name="cnt">the number of samples of the buffer, that is the samples count returned by get channel samples count function</param>
        /// <returns>greater than 0 the samples count, less than 0 in case of problems, 0 in case the cnt param doesn't match the samples count or in case the channel has no samples (theorically not possible)</returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl)]
        private static extern int get_GPS_channel_samples(int idxf, int idxc, double[] ptimes, double[] pvalues, int cnt);

        /// <summary>Get GPS channel samples count in a given lap</summary>
        /// <param name="idxf">the internal file index returned by open function</param>
        /// <param name="idxl">the lap index</param>
        /// <param name="idxc">the channel index</param>
        /// <returns> greater than 0 the samples count, less than 0 in case of problems, 0 in case the channel has no samples (theorically not possible) </returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl)]
        private static extern int get_lap_GPS_channel_samples_count(int idxf, int idxl, int idxc);

        /// <summary>Get GPS channel samples in a given lap</summary>
        /// <param name="idxf">the internal file index returned by open function</param>
        /// <param name="idxl">the lap index</param>
        /// <param name="idxc">the channel index</param>
        /// <param name="ptimes">pointer to an already allocated buffer in which sample times will be stored</param>
        /// <param name="pvalues">pointer to an already allocated buffer in which sample values will be stored</param>
        /// <param name="cnt">the number of samples of the buffer, that is the samples count returned by get channel samples count function</param>
        /// <returns> greater than 0 the samples count, less than 0 in case of problems, 0 in case the cnt param doesn't match the samples count or in case the channel has no samples (theorically not possible) </returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl)]
        private static extern int get_lap_GPS_channel_samples(int idxf, int idxl, int idxc, double[] ptimes, double[] pvalues, int cnt);

        /// <summary> Get GPS raw channels count of a xrk file</summary>
        /// <param name="idx">the internal file index returned by open function</param>
        /// <returns>greater than 0 the channel count, less than 0 in case of problems, 0 in case the xrk file has no channels (theorically not possible)</returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl)]
        private static extern int get_GPS_raw_channels_count(int idx);

        /// <summary>Get GPS raw channel name</summary>
        /// <param name="idxf">the internal file index returned by open function</param>
        /// <param name="idxc">the channel index</param>
        /// <returns> a null terminated char pointer with the required info, NULL in case of problems </returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr get_GPS_raw_channel_name(int idxf, int idxc);

        /// <summary>Get GPS raw channel units</summary>
        /// <param name="idxf">the internal file index returned by open function</param>
        /// <param name="idxc">the channel index</param>
        /// <returns> a null terminated char pointer with the required info, NULL in case of problems </returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr get_GPS_raw_channel_units(int idxf, int idxc);

        /// <summary>Get GPS raw channel samples count</summary>
        /// <param name="idxf">the internal file index returned by open function</param>
        /// <param name="idxc">the channel index</param>
        /// <returns> greater than 0 the samples count, less than 0 in case of problems, 0 in case the channel has no samples (theorically not possible) </returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl)]
        private static extern int get_GPS_raw_channel_samples_count(int idxf, int idxc);

        /// <summary>Get GPS raw channel samples</summary>
        /// <param name="idxf">the internal file index returned by open function</param>
        /// <param name="idxc">the channel index</param>
        /// <param name="ptimes">pointer to an already allocated buffer in which sample times will be stored</param>
        /// <param name="pvalues">pointer to an already allocated buffer in which sample values will be stored</param>
        /// <param name="cnt">the number of samples of the buffer, that is the samples count returned by get channel samples count function</param>
        /// <returns>greater than 0 the samples count, less than 0 in case of problems, 0 in case the cnt param doesn't match the samples count or in case the channel has no samples (theorically not possible)</returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl)]
        private static extern int get_GPS_raw_channel_samples(int idxf, int idxc, double[] ptimes, double[] pvalues, int cnt);

        /// <summary>Get GPS raw channel samples count in a given lap</summary>
        /// <param name="idxf">the internal file index returned by open function</param>
        /// <param name="idxl">the lap index</param>
        /// <param name="idxc">the channel index</param>
        /// <returns> greater than 0 the samples count, less than 0 in case of problems, 0 in case the channel has no samples (theorically not possible) </returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl)]
        private static extern int get_lap_GPS_raw_channel_samples_count(int idxf, int idxl, int idxc);

        /// <summary>Get GPS raw channel samples in a given lap</summary>
        /// <param name="idxf">the internal file index returned by open function</param>
        /// <param name="idxl">the lap index</param>
        /// <param name="idxc">the channel index</param>
        /// <param name="ptimes">pointer to an already allocated buffer in which sample times will be stored</param>
        /// <param name="pvalues">pointer to an already allocated buffer in which sample values will be stored</param>
        /// <param name="cnt">the number of samples of the buffer, that is the samples count returned by get channel samples count function</param>
        /// <returns> greater than 0 the samples count, less than 0 in case of problems, 0 in case the cnt param doesn't match the samples count or in case the channel has no samples (theorically not possible) </returns>
        [DllImport(libName, CallingConvention = CallingConvention.Cdecl)]
        private static extern int get_lap_GPS_raw_channel_samples(int idxf, int idxl, int idxc, double[] ptimes, double[] pvalues, int cnt);


        /// <summary>Get the compile date of this library</summary>
        /// <returns>Compile date</returns>
        public static string GetLibraryDate()
        {
            IntPtr datePtr = get_library_date();
            string date = Marshal.PtrToStringAnsi(datePtr);
            return date;
        }
        /// <summary>Get the compile time of this library </summary>
        /// <returns>Compile time</returns>
        public static string GetLibraryTime()
        {
            IntPtr timePtr = get_library_time();
            string time = Marshal.PtrToStringAnsi(timePtr);
            return time;
        }
        /// <summary>Open a xrk file</summary>
        /// <param name="path">full path to the file to be opened, it cannot be a relative path</param>
        /// <returns>graeter than 0 in case of success the internal index of the file opened, 0 in case the file is opened but can't be parsed, less than 0 in case of problems </returns>
        public static int OpenFile(string path)
        {
            return open_file(path);
        }
        /// <summary>Close a xrk file</summary>
        /// <param name="path">full path to the file to be closed, it cannot be a relative path</param>
        /// <returns> greater than 0 the internal index of the file closed, less than 0 in case of problems </returns>
        public static int CloseFile(string path)
        {
            return close_file_n(path);
        }
        /// <summary>Get vehicle info</summary>
        /// <param name="idx">the internal file index returned by open function</param>
        /// <returns>the vehicule name </returns>
        public static string GetVehiculeName(int idx)
        {
            IntPtr namePtr = get_vehicle_name(idx);
            string name = Marshal.PtrToStringAnsi(namePtr);
            return name;
        }
        /// <summary>Get track info</summary>
        /// <param name="idx">the internal file index returned by open function</param>
        /// <returns>the track name </returns>
        public static string GetTrackName(int idx)
        {
            IntPtr namePtr = get_track_name(idx);
            string name = Marshal.PtrToStringAnsi(namePtr);
            return name;
        }
        /// <summary>Get racer info</summary>
        /// <param name="idx">the internal file index returned by open function</param>
        /// <returns>the racer name </returns>
        public static string GetRacerName(int idx)
        {
            IntPtr namePtr = get_racer_name(idx);
            string name = Marshal.PtrToStringAnsi(namePtr);
            return name;
        }
        /// <summary>Get championship info</summary>
        /// <param name="idx">the internal file index returned by open function</param>
        /// <returns>the championship name </returns>
        public static string GetChampionshipName(int idx)
        {
            IntPtr namePtr = get_championship_name(idx);
            string name = Marshal.PtrToStringAnsi(namePtr);
            return name;
        }
        /// <summary>Get venue type info</summary>
        /// <param name="idx">the internal file index returned by open function</param>
        /// <returns>the venue type </returns>
        public static string GetVenueTypeName(int idx)
        {
            IntPtr namePtr = get_venue_type_name(idx);
            string name = Marshal.PtrToStringAnsi(namePtr);
            return name;
        }
        /// <summary>Get session date and time</summary>
        /// <param name="idx">the internal file index returned by open function</param>
        /// <returns>a DateTimeStruct containing the session date and time </returns>
        public static DateTimeStruct GetDateAndTime(int idx)
        {
            IntPtr dateTimePtr = get_date_and_time(idx);
            DateTimeStruct dateTime = Marshal.PtrToStructure<DateTimeStruct>(dateTimePtr);
            return dateTime;
        }
        /// <summary>Get laps count of a xrk file</summary>
        /// <param name="idx">the internal file index returned by open function</param>
        /// <returns> greater than 0 the laps count, less than 0 in case of problems, 0 in case the xrk file has no laps (theorically not possible) </returns>
        public static int GetLapsCount(int idx)
        {
            return get_laps_count(idx);
        }
        /// <summary>Get lap info</summary>
        /// <param name="idxf">the internal file index returned by open function</param>
        /// <param name="idxl">the lap index</param>
        /// <param name="pstart">output of the start time of the lap</param>
        /// <param name="pduration">output of the lap duration</param>
        /// <returns> 1 if all is OK, less than 0 in case of problems, 0 in case the xrk file has no laps (theorically not possible) </returns>
        public static int GetLapInfo(int idxf, int idxl, out double pstart, out double pduration)
        {
            return get_lap_info(idxf, idxl, out pstart, out pduration);
        }
        /// <summary> Get channels count of a xrk file</summary>
        /// <param name="idx">the internal file index returned by open function</param>
        /// <returns> less than 0 in case of problems, 0 in case the xrk file has no laps (theorically not possible) </returns>
        public static int GetChannelsCount(int idx)
        {
            return get_channels_count(idx);
        }
        /// <summary>Get channel name</summary>
        /// <param name="idxf">the internal file index returned by open function</param>
        /// <param name="idxc">the channel index</param>
        /// <returns>the channel name </returns>
        public static string GetChannelName(int idxf, int idxc)
        {
            IntPtr namePtr = get_channel_name(idxf, idxc);
            string name = Marshal.PtrToStringAnsi(namePtr);
            return name;
        }
        /// <summary>Get channel units</summary>
        /// <param name="idxf">the internal file index returned by open function</param>
        /// <param name="idxc">the channel index</param>
        /// <returns>the channel units </returns>
        public static string GetChannelUnits(int idxf, int idxc)
        {
            IntPtr unitsPtr = get_channel_units(idxf, idxc);
            string units = Marshal.PtrToStringAnsi(unitsPtr);
            return units;
        }
        /// <summary>Get channel samples count</summary>
        /// <param name="idxf">the internal file index returned by open function</param>
        /// <param name="idxc">the channel index</param>
        /// <returns> greater than 0 the samples count, less than 0 in case of problems, 0 in case the channel has no samples (theorically not possible) </returns>
        public static int GetChannelSamplesCount(int idxf, int idxc)
        {
            return get_channel_samples_count(idxf, idxc);
        }
        /// <summary>Get channel samples</summary>
        /// <param name="idxf">the internal file index returned by open function</param>
        /// <param name="idxc">the channel index</param>
        /// <param name="ptimes">already allocated array in which sample times will be stored</param>
        /// <param name="pvalues">already allocated array in which sample values will be stored</param>
        /// <param name="cnt">the number of samples of the buffer, that is the samples count returned by get channel samples count function</param>
        /// <returns>greater than 0 the samples count, less than 0 in case of problems, 0 in case the cnt param doesn't match the samples count or in case the channel has no samples (theorically not possible)</returns>
        public static int GetChannelSamples(int idxf, int idxc, double[] ptimes, double[] pvalues, int cnt)
        {
            return get_channel_samples(idxf, idxc, ptimes, pvalues, cnt);
        }
        /// <summary>Get channel samples count in a given lap</summary>
        /// <param name="idxf">the internal file index returned by open function</param>
        /// <param name="idxl">the lap index</param>
        /// <param name="idxc">the channel index</param>
        /// <returns> greater than 0 the samples count, less than 0 in case of problems, 0 in case the channel has no samples (theorically not possible) </returns>
        public static int GetLapChannelSamplesCount(int idxf, int idxl, int idxc)
        {
            return get_lap_channel_samples_count(idxf, idxl, idxc);
        }
        /// <summary>Get channel samples in a given lap</summary>
        /// <param name="idxf">the internal file index returned by open function</param>
        /// <param name="idxl">the lap index</param>
        /// <param name="idxc">the channel index</param>
        /// <param name="ptimes">already allocated array in which sample times will be stored</param>
        /// <param name="pvalues">already allocated array in which sample values will be stored</param>
        /// <param name="cnt">the number of samples of the buffer, that is the samples count returned by get channel samples count function</param>
        /// <returns> greater than 0 the samples count, less than 0 in case of problems, 0 in case the cnt param doesn't match the samples count or in case the channel has no samples (theorically not possible) </returns>
        public static int GetLapChannelSamples(int idxf, int idxl, int idxc, double[] ptimes, double[] pvalues, int cnt)
        {
            return get_lap_channel_samples(idxf, idxl, idxc, ptimes, pvalues, cnt);
        }
        /// <summary> Get GPS channels count of a xrk file</summary>
        /// <param name="idx">the internal file index returned by open function</param>
        /// <returns>greater than 0 the channel count, less than 0 in case of problems, 0 in case the xrk file has no channels (theorically not possible)</returns>
        public static int GetGPSChannelsCount(int idx)
        {
            return get_GPS_channels_count(idx);
        }
        /// <summary>Get GPS channel name</summary>
        /// <param name="idxf">the internal file index returned by open function</param>
        /// <param name="idxc">the channel index</param>
        /// <returns> the name of a GPS channel </returns>
        public static string GetGPSChannelName(int idxf, int idxc)
        {
            IntPtr namePtr = get_GPS_channel_name(idxf, idxc);
            string name = Marshal.PtrToStringAnsi(namePtr);
            return name;
        }
        /// <summary>Get GPS channel units</summary>
        /// <param name="idxf">the internal file index returned by open function</param>
        /// <param name="idxc">the channel index</param>
        /// <returns> the units of a GPS channel </returns>
        public static string GetGPSChannelUnits(int idxf, int idxc)
        {
            IntPtr unitsPtr = get_GPS_channel_units(idxf, idxc);
            string units = Marshal.PtrToStringAnsi(unitsPtr);
            return units;
        }
        /// <summary>Get GPS channel samples count</summary>
        /// <param name="idxf">the internal file index returned by open function</param>
        /// <param name="idxc">the channel index</param>
        /// <returns> greater than 0 the samples count, less than 0 in case of problems, 0 in case the channel has no samples (theorically not possible) </returns>
        public static int GetGPSChannelSamplesCount(int idxf, int idxc)
        {
            return get_GPS_channel_samples_count(idxf, idxc);
        }
        /// <summary>Get GPS channel samples</summary>
        /// <param name="idxf">the internal file index returned by open function</param>
        /// <param name="idxc">the channel index</param>
        /// <param name="ptimes">already allocated array in which sample times will be stored</param>
        /// <param name="pvalues">already allocated array in which sample values will be stored</param>
        /// <param name="cnt">the number of samples of the buffer, that is the samples count returned by get channel samples count function</param>
        /// <returns>greater than 0 the samples count, less than 0 in case of problems, 0 in case the cnt param doesn't match the samples count or in case the channel has no samples (theorically not possible)</returns>
        public static int GetGPSChannelSamples(int idxf, int idxc, double[] ptimes, double[] pvalues, int cnt)
        {
            return get_GPS_channel_samples(idxf, idxc, ptimes, pvalues, cnt);
        }
        /// <summary>Get GPS channel samples count in a given lap</summary>
        /// <param name="idxf">the internal file index returned by open function</param>
        /// <param name="idxl">the lap index</param>
        /// <param name="idxc">the channel index</param>
        /// <returns> greater than 0 the samples count, less than 0 in case of problems, 0 in case the channel has no samples (theorically not possible) </returns>
        public static int GetLapGPSChannelSamplesCount(int idxf, int idxl, int idxc)
        {
            return get_lap_GPS_channel_samples_count(idxf, idxl, idxc);
        }
        /// <summary>Get GPS channel samples in a given lap</summary>
        /// <param name="idxf">the internal file index returned by open function</param>
        /// <param name="idxl">the lap index</param>
        /// <param name="idxc">the channel index</param>
        /// <param name="ptimes">already allocated array in which sample times will be stored</param>
        /// <param name="pvalues">already allocated array in which sample values will be stored</param>
        /// <param name="cnt">the number of samples of the buffer, that is the samples count returned by get channel samples count function</param>
        /// <returns> greater than 0 the samples count, less than 0 in case of problems, 0 in case the cnt param doesn't match the samples count or in case the channel has no samples (theorically not possible) </returns>
        public static int GetLapGPSChannelSamples(int idxf, int idxl, int idxc, double[] ptimes, double[] pvalues, int cnt)
        {
            return get_lap_GPS_channel_samples(idxf, idxl, idxc, ptimes, pvalues, cnt);
        }
        /// <summary> Get GPS raw channels count of a xrk file</summary>
        /// <param name="idx">the internal file index returned by open function</param>
        /// <returns>greater than 0 the channel count, less than 0 in case of problems, 0 in case the xrk file has no channels (theorically not possible)</returns>
        public static int GetGPSRawChannelsCount(int idx)
        {
            return get_GPS_raw_channels_count(idx);
        }
        /// <summary>Get GPS raw channel name</summary>
        /// <param name="idxf">the internal file index returned by open function</param>
        /// <param name="idxc">the channel index</param>
        /// <returns>the name of the GPS raw channel </returns>
        public static string GetGPSRawChannelName(int idxf, int idxc)
        {
            IntPtr namePtr = get_GPS_raw_channel_name(idxf, idxc);
            string name = Marshal.PtrToStringAnsi(namePtr);
            return name;
        }
        /// <summary>Get GPS raw channel units</summary>
        /// <param name="idxf">the internal file index returned by open function</param>
        /// <param name="idxc">the channel index</param>
        /// <returns>the units of the GPS raw channel </returns>
        public static string GetGPSRawChannelUnits(int idxf, int idxc)
        {
            IntPtr unitsPtr = get_GPS_raw_channel_units(idxf, idxc);
            string units = Marshal.PtrToStringAnsi(unitsPtr);
            return units;
        }
        /// <summary>Get GPS raw channel samples count</summary>
        /// <param name="idxf">the internal file index returned by open function</param>
        /// <param name="idxc">the channel index</param>
        /// <returns> greater than 0 the samples count, less than 0 in case of problems, 0 in case the channel has no samples (theorically not possible) </returns>
        public static int GetGPSRawChannelSamplesCount(int idxf, int idxc)
        {
            return get_GPS_raw_channel_samples_count(idxf, idxc);
        }
        /// <summary>Get GPS raw channel samples</summary>
        /// <param name="idxf">the internal file index returned by open function</param>
        /// <param name="idxc">the channel index</param>
        /// <param name="ptimes">already allocated array in which sample times will be stored</param>
        /// <param name="pvalues">already allocated array in which sample values will be stored</param>
        /// <param name="cnt">the number of samples of the buffer, that is the samples count returned by get channel samples count function</param>
        /// <returns>greater than 0 the samples count, less than 0 in case of problems, 0 in case the cnt param doesn't match the samples count or in case the channel has no samples (theorically not possible)</returns>
        public static int GetGPSRawChannelSamples(int idxf, int idxc, double[] ptimes, double[] pvalues, int cnt)
        {
            return get_GPS_raw_channel_samples(idxf, idxc, ptimes, pvalues, cnt);
        }
        /// <summary>Get GPS raw channel samples count in a given lap</summary>
        /// <param name="idxf">the internal file index returned by open function</param>
        /// <param name="idxl">the lap index</param>
        /// <param name="idxc">the channel index</param>
        /// <returns> greater than 0 the samples count, less than 0 in case of problems, 0 in case the channel has no samples (theorically not possible) </returns>
        public static int GetLapGPSRawChannelSamplesCount(int idxf, int idxl, int idxc)
        {
            return get_lap_GPS_raw_channel_samples_count(idxf, idxl, idxc);
        }
        /// <summary>Get GPS raw channel samples in a given lap</summary>
        /// <param name="idxf">the internal file index returned by open function</param>
        /// <param name="idxl">the lap index</param>
        /// <param name="idxc">the channel index</param>
        /// <param name="ptimes">already allocated array in which sample times will be stored</param>
        /// <param name="pvalues">already allocated array in which sample values will be stored</param>
        /// <param name="cnt">the number of samples of the buffer, that is the samples count returned by get channel samples count function</param>
        /// <returns> greater than 0 the samples count, less than 0 in case of problems, 0 in case the cnt param doesn't match the samples count or in case the channel has no samples (theorically not possible) </returns>
        public static int GetLapGPSRawChannelSamples(int idxf, int idxl, int idxc, double[] ptimes, double[] pvalues, int cnt)
        {
            return get_lap_GPS_raw_channel_samples(idxf, idxl, idxc, ptimes, pvalues, cnt);
        }
    }
}