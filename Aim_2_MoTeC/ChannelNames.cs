namespace Aim_2_MoTeC
{
    public struct nameConvert
    {
        public string from;
        public string to;
        public string to_short;

        public nameConvert(string from, string to, string to_short = "")
        {
            this.from = from;
            this.to = to;
            this.to_short = to_short;
        }
    }
    public static class ChannelNamesConvert
    {
        public static nameConvert[] NAMES =
        {
            new nameConvert("ASteering", "Steered Angle", "Str Ang"),
            new nameConvert("Brake", "Brake Pos", "Brk Pos"),
            new nameConvert("SpeedVeh", "Ground Speed", "Gnd Spd"),
            new nameConvert("RPM", "Engine RPM", "RPM"),
            new nameConvert("GLat", "G Force Lat", "G Lat"),
            new nameConvert("GVert", "G Force Vert", "G Vert"),
            new nameConvert("GLong", "G Force Long", "G Long"),
            new nameConvert("GLong", "G Force Long", "G Long"),
            new nameConvert("VWheelFL", "Wheel Speed FL", "WSpd FL"),
            new nameConvert("VWheelFR", "Wheel Speed FR", "WSpd FR"),
            new nameConvert("VWheelRL", "Wheel Speed RL", "WSpd RL"),
            new nameConvert("VWheelRR", "Wheel Speed RR", "WSpd RR"),
            new nameConvert("WaterTemps", "Eng Water Temp", "WaterTemp"),
        };

        public static bool containsName(string from, out nameConvert nameStruct)
        {
            foreach (nameConvert str in NAMES)
            {
                if (str.from == from)
                {
                    nameStruct = str;
                    return true;
                }
            }
            nameStruct = new nameConvert();
            return false;
        }
    }
}
