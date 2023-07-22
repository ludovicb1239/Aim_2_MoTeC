using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Aim_2_MoTeC
{
    public static class PointerConstants
    {
        public const uint EVENT_PTR = 1762;
        public const uint HEADER_PTR = 0x3448; //those should not be hardcoded
        public const uint CHANNEL_META_PRT = 13384;
        public static uint CHANNEL_DATA_PRT;
        public const uint CHANNEL_HEADER_SIZE = 124;
    }
    public class LdData
    {
        public LdHead head;
        public List<LdChan> channs;
        public List<LdBeacon> beacons;
        public LdLapInfo lapInfo;

        public LdData(LdHead head, List<LdChan> channs, List<LdBeacon> beacons, LdLapInfo lapInfo)
        {
            this.head = head;
            this.channs = channs;
            this.beacons = beacons;
            this.lapInfo = lapInfo;
        }
        public void prepPointers()
        {
            List<uint> metaAddrs = channs.Select((channel, i) =>
                {
                    int metaOffset = i * 124;
                    return (uint)(PointerConstants.HEADER_PTR + metaOffset);
                }).ToList();

            List<int> sampleByteSizes = channs.Select((channel, _) =>
                {
                    return (channel.Data.Length * 4);
                }).ToList();

            List<uint> sampleAddrs = channs.Select((channel, i) =>
                {
                    int metaOffset = channs.Count * 124;
                    int sampleOffset = sampleByteSizes.Take(i).Sum();

                    return (uint)(PointerConstants.HEADER_PTR + metaOffset + sampleOffset);
                }).ToList();


            for (int i = 0; i < channs.Count; i++)
            {
                LdChan channel = channs[i];

                channel.prev_meta_ptr = (i == 0) ? 0 : metaAddrs[i - 1];
                channel.next_meta_ptr = (i < channs.Count - 1) ? metaAddrs[i + 1] : 0;
                channel.data_len = (uint)channel.Data.Length;
                channel.data_ptr = sampleAddrs[i];

                channel.meta_ptr = metaAddrs[i];
            }

            PointerConstants.CHANNEL_DATA_PRT = sampleAddrs[0];
        }
        public void Write(string logFilename, string extensionFilename)
        {
            try
            {
                using (BinaryWriter writer = new(File.Open(logFilename, FileMode.Create)))
                {

                    head.Write(writer, channs.Count);

                    for (int i = 0; i < channs.Count; i++)
                    {
                        channs[i].Write(writer, i);
                    }

                    for (int i = 0; i < channs.Count; i++)
                    {
                        LdChan channel = channs[i];

                        writer.Seek((int)channel.data_ptr, SeekOrigin.Begin);

                        foreach (Object dat in channel.Data)
                        {
                            writer.Write((float)dat);
                        }
                    }
                }
                using (StreamWriter writer = new(File.Open(extensionFilename, FileMode.Create)))
                {
                    writer.WriteLine("<?xml version=\"1.0\"?>\n<LDXFile Locale=\"English_Canada.1252\" DefaultLocale=\"C\" Version=\"1.6\">\n <Layers>\n  <Layer>\n   <MarkerBlock>\n    <MarkerGroup Name=\"Beacons\" Index=\"3\">"); //index 5
                    for (int i = 0; i < beacons.Count; i++)
                    {
                        beacons[i].Write(writer);
                    }
                    writer.WriteLine("    </MarkerGroup>\n   </MarkerBlock>\n   <RangeBlock/>\n  </Layer>");
                    lapInfo.Write(writer);
                    writer.WriteLine(" </Layers>\n</LDXFile>");
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
            }
        }
    }
    public class LdHead
    {
        public string Driver;
        public string VehicleId;
        public string Venue;
        public DateTimeStruct DateTime;
        public string ShortComment;
        public string Session;

        public LdHead(string driver, string vehicleId, string venue, DateTimeStruct dateTime, string shortComment, string session)
        {
            Driver = driver;
            VehicleId = vehicleId;
            Venue = venue;
            DateTime = dateTime;
            ShortComment = shortComment;
            Session = session;
        }

        public void Write(BinaryWriter writer, int n)
        {
            const int MaxStringSize = 64;
            const int MaxCommentSize = 1024;

            foreach (char value in FULL_HEADER.const_data)
            {
                writer.Write(value);
            }
            writer.Seek(0, SeekOrigin.Begin);

            int year = DateTime.tm_year + 1900;
            int month = DateTime.tm_mon + 1;
            int day = DateTime.tm_mday;
            int hour = DateTime.tm_hour;
            int minute = DateTime.tm_min;
            int second = DateTime.tm_sec;

            byte[] dateBytes = EncodeString(string.Format("{0:D2}/{1:D2}/{2}", day, month, year), 16);
            byte[] timeBytes = EncodeString(string.Format("{0:D2}:{1:D2}:{2:D2}", hour, minute, second), 16);
            byte[] driverBytes = EncodeString(Driver, MaxStringSize);
            byte[] vehicleIdBytes = EncodeString(VehicleId, MaxStringSize);
            byte[] venueBytes = EncodeString(Venue, MaxStringSize);
            byte[] otherInfoBytes = EncodeString("Hello my name is harry pother !", MaxCommentSize);
            byte[] shortCommentBytes = EncodeString(ShortComment, MaxStringSize);
            byte[] sessionBytes = EncodeString(Session, MaxStringSize);
            byte[] deviceTypeBytes = EncodeString("ADL", 8); //Could be ADL or C185
            int device_serial = 12007; //Could be anything ? 12007 or 15341
            ushort device_version = 420; //420 or 613

            writer.Write(0x40); //64

            writer.Write((uint)0x00000000);

            writer.Write(PointerConstants.CHANNEL_META_PRT);
            writer.Write(PointerConstants.CHANNEL_DATA_PRT);

            writer.Write(new byte[20]);

            writer.Write(PointerConstants.EVENT_PTR);

            writer.Write(new byte[24]);

            writer.Write((ushort)0x0000);
            writer.Write((ushort)0x4240);
            writer.Write((ushort)0x000F);

            writer.Write(device_serial);
            writer.Write(deviceTypeBytes);
            writer.Write(device_version);

            writer.Write((ushort)0x0080);

            writer.Write(n);
            writer.Write((uint)0x0001_0064);

            writer.Write(dateBytes);
            writer.Write(new byte[16]);
            writer.Write(timeBytes);
            writer.Write(new byte[16]);

            writer.Write(driverBytes);
            writer.Write(vehicleIdBytes);
            writer.Write(new byte[64]);
            writer.Write(venueBytes);
            writer.Write(new byte[64]);

            writer.Write(otherInfoBytes);

            // ProLogging related ?
            writer.Write((uint)0xD20822); //0x824404
            writer.Write((ushort)0);

            writer.Write(sessionBytes);
            writer.Write(shortCommentBytes);

            writer.Write(new byte[8]);
            writer.Write((byte)99);
            writer.Write(new byte[117]);

            /*
            writer.Write(eventBytes);
            writer.Write(sessionBytes);
            writer.Write(longCommentBytes);*/

            writer.Seek((int)PointerConstants.EVENT_PTR, SeekOrigin.Begin);
        }
        public static byte[] EncodeString(string str, int size)
        {
            byte[] bytes = new byte[size];
            byte[] strBytes = Encoding.UTF8.GetBytes(str);
            Array.Copy(strBytes, bytes, Math.Min(strBytes.Length, size));
            return bytes;
        }
    }
    public class LdBeacon
    {
        public int markerVersion;
        public string className;
        public string name;
        public int flags;
        public double time;

        public LdBeacon(int markerVersion, string className, string name, int flags, double time)
        {
            this.markerVersion = markerVersion;
            this.className = className;
            this.name = name;
            this.flags = flags;
            this.time = time;
        }

        public void Write(StreamWriter f)
        {
            string scientificNotation = (time * 1000000d).ToString("0.00000000000000000E+00");
            f.WriteLine(string.Format("     <Marker Version=\"{0}\" ClassName=\"{1}\" Name=\"{2}\" Flags=\"{3}\" Time=\"{4}\"/>",
                markerVersion.ToString(), className, name, flags.ToString(), scientificNotation));
        }
    }
    public class LdLapInfo
    {
        public int totalLaps;
        public double fastestTime;
        public int fastestLap;
        public LdLapInfo(int totalLaps, double fastestTime, int fastestLap)
        {
            this.totalLaps = totalLaps;
            this.fastestTime = fastestTime;
            this.fastestLap = fastestLap;
        }

        public void Write(StreamWriter f)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(fastestTime);
            string formattedTime = string.Format("{0}:{1:00}.{2:000}", (int)timeSpan.TotalMinutes, timeSpan.Seconds, timeSpan.Milliseconds);
            f.WriteLine(string.Format("  <Details>\n   <String Id=\"Total Laps\" Value=\"{0}\"/>\n   <String Id=\"Fastest Time\" Value=\"{1}\"/>\n   <String Id=\"Fastest Lap\" Value=\"{2}\"/>\n  </Details> ",
                totalLaps.ToString(), formattedTime, fastestLap));
        }
    }
    public class LdChan
    {
        public uint meta_ptr;

        public uint prev_meta_ptr;
        public uint next_meta_ptr;
        public uint data_ptr;
        public uint data_len;
        public ushort freq;
        public ushort shift;
        public ushort mul;
        public ushort scale;
        public short dec;
        public string name;
        public string short_name;
        public string unit;

        public float[] Data;

        public LdChan(ushort freq, ushort shift, ushort mul, ushort scale, short dec, string name, string short_name, string unit)
        {
            this.freq = freq;
            this.shift = shift;
            this.mul = mul;
            this.scale = scale;
            this.dec = dec;
            this.name = name;
            this.short_name = short_name;
            this.unit = unit;
            this.Data = null;
        }

        public void Write(BinaryWriter f, int n)
        {
            //ONLY FOR FLOATS
            ushort _type = 7, _size = 4;

            f.Seek((int)meta_ptr, SeekOrigin.Begin);

            byte[] nameBytes = LdHead.EncodeString(name, 32);
            byte[] shortNameBytes = LdHead.EncodeString(short_name, 8);
            byte[] unitBytes = LdHead.EncodeString(unit, 12);

            f.Write(prev_meta_ptr);
            f.Write(next_meta_ptr);
            f.Write(data_ptr);
            f.Write(data_len);

            //f.Write(0x2ee1 + n);
            f.Write((ushort)4);

            f.Write(_type);
            f.Write(_size);
            f.Write(freq);
            f.Write(shift);
            f.Write(mul);
            f.Write(scale);
            f.Write(dec);
            f.Write(nameBytes);
            f.Write(unitBytes);
            f.Write(shortNameBytes);

            f.Write((byte)201);
            f.Write(new byte[39]);
        }
    }
}