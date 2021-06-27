using System;
using System.Collections.Generic;
using System.Text;

namespace AAIFinalAssignment
{
    public class Settings
    {
        // Fuzzy Logic

        public static bool DoFuzzyLogic { get; set; } = false;

        // Rendering behaviours
        public static bool RenderBehaviour { get; set; } = true;
        public static bool RenderSeeking { get; set; } = true;
        public static bool RenderDistancing { get; set; } = true;
        public static int DistancingRange { get; set; } = 100;

        public static bool RenderGroupPressure { get; set; } = true;
        public static int GroupPressureRange { get; set; } = 120;
        public static bool RenderWander { get; set; } = true;

        public static bool RenderGrid { get; set; } = true;

        public static float WaypointSwitchDistance { get; set; } = 50;

        public static int MaxSpeed { get; set; } = 200;
        public static int MinSpeed { get; set; } = 0;
        public static float MaxAccel { get; set; } = 1;
        public static float Drag { get; set; } = 0.99f;


        public static float ScrollSpeed { get; set; } = 10;

        public static bool RenderObstacles { get; set; } = true;

        public static float SharkEatingRange { get; set; } = 20;

        public static float FishEatingRange { get; set; } = 10;
        public static bool RenderStates { get; set; } = true;
    }
}
