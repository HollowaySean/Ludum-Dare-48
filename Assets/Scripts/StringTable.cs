using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringTable
{
    public static Dictionary<int, string> Deep = new Dictionary<int, string>() {
        {0, "Give peace a chance" },
        {1, "Time is fleeting" },
        {2, "Two paths diverged in a yellow wood" },
        {3, "Why does suffering exist" },
        {4, "What if your green isn't the same as my green"}
    };

    public static int NumDeep { get { return Deep.Count; } }

    public static Dictionary<int, string> Shallow = new Dictionary<int, string>() {
        {0, "The world is a vampire" },
        {1, "Why am I always so itchy" },
        {2, "I should text my ex" },
        {3, "I need to pee" },
        {4, "Did I leave the stove on" }
    };
    
    public static int NumShallow { get { return Deep.Count; } }

    public static Dictionary<(int, int), string> Combos = new Dictionary<(int, int), string>() {
        {(0, 0), "Give vampires a chance" },
        {(0, 1), "Give me some ointment" },
        {(0, 2), "I should give my ex another chance" },
        {(0, 3), "Give pee a chance" },
        {(0, 4), "Give the stove a chance" },
        {(1, 0), "Time is a vampire" },
        {(1, 1), "Time is itchy" },
        {(1, 2), "Time to text my ex" },
        {(1, 3), "Time is peeing" },
        {(1, 4), "Time to leave the stove on" },
        {(2, 0), "Two vampires diverged in a yellow wood" },
        {(2, 1), "Two paths diverged in an itchy wood" },
        {(2, 2), "Two paths texted my ex" },
        {(2, 3), "Two me's need to pee" },
        {(2, 4), "Did I leave two stoves on" },
        {(3, 0), "Why do vampires exist" },
        {(3, 1), "Why does itchiness exist" },
        {(3, 2), "Why does my ex make me suffer" },
        {(3, 3), "Why does pee exist" },
        {(3, 4), "Why is my stove suffering" },
        {(4, 0), "What if a vampire is green" },
        {(4, 1), "What if your itchy isn't the same as my itchy" },
        {(4, 2), "What if your ex is my ex" },
        {(4, 3), "What if your pee isnt' the same as my pee" },
        {(4, 4), "What if your stove isn't the same as my stove" }

    };
}
