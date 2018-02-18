using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace MarshalClass
{
    [StructLayout(LayoutKind.Sequential)]
    public struct researchStats
    {
        Int32 military_tradition;
        Int32 iron_working;
        Int32 medicine;
        Int32 compass;
        Int32 horseback_riding;
        Int32 archery;
        Int32 machinery;
    };
    [StructLayout(LayoutKind.Sequential)]
    public struct heroStat
    {
        double attack;
        double intel;
        double leadership;
        [MarshalAs(UnmanagedType.U1)]
        bool ok;
    };
    [StructLayout(LayoutKind.Sequential)]
    public struct attacker
    {
        researchStats research;
        heroStat hero;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public Int64[] troops;
        public double attack_modifier;
        public double defence_modifier;
        public double life_modifier;
    };
    [StructLayout(LayoutKind.Sequential)]
    public struct defender
    {
        researchStats research;
        heroStat hero;
        public double attack_modifier;
        public double defence_modifier;
        public double life_modifier;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public Int64[] troops;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
        Int64[] fortifications;
        Int32 wallLevel;
        [MarshalAs(UnmanagedType.U1)]
        bool gateOpen;
    };
    [StructLayout(LayoutKind.Sequential)]
    public struct battleResult
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public Int64[] attackerTroops;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public Int64[] defenderTroops;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
        public Int64[] fortification;
        public Int32 result;
        public Int32 totalRounds;
    };
    public class Wrapper
    {
#if DEBUG
        [DllImport("EvoDebug.dll")]
        public static extern void simulateBattle(attacker atk, defender def, out battleResult result);
#else
        [DllImport("EvoRelease.dll")]
        public static extern void simulateBattle(attacker atk, defender def, out battleResult result);
#endif
    }
}
