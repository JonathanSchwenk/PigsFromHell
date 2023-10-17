using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
using System;

public static class Extensions 
{
    public static int MidnightsBetween(DateTime from, DateTime to)
    {
        if (from > to) {
            throw new ArgumentException("from must not be after to");
        }

        // Trim to days
        DateTime fromTrimmed = new DateTime(from.Year, from.Month, from.Day);
        DateTime toTrimmed = new DateTime(to.Year, to.Month, to.Day);

        int days = (toTrimmed - fromTrimmed).Days;

        return days;
    }
}
