using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringTable
{
    public static Dictionary<int, string> Deep = new Dictionary<int, string>() {
        {0, "The world is a vampire" },
        {1, "Time is fleeting" },
        {2, "Two paths diverged in a yellow wood" }
    };

    public static int NumDeep { get { return Deep.Count; } }

    public static Dictionary<int, string> Shallow = new Dictionary<int, string>() {
        {0, "The world is a vampire" },
        {1, "Why am I always so itchy" },
        {2, "I should text my ex" }
    };
    
    public static int NumShallow { get { return Deep.Count; } }

    public static Dictionary<(int, int), string> Combos = new Dictionary<(int, int), string>() {
        {(0, 0), "The world is two vampires" },
        {(0, 1), "The world is itchy" },
        {(0, 2), "My ex is the world" },
        {(1, 0), "Time is a vampire" },
        {(1, 1), "Time is itchy" },
        {(1, 2), "My ex was always on time" },
        {(2, 0), "The world diverged in a yellow wood" },
        {(2, 1), "Why am I always in a yellow wood" },
        {(2, 2), "Two exes texted me back" },

    };
}
