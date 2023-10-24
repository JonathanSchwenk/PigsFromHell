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

    public static float GetAngleFromVectorFloat(Vector3 dir) {
            dir = dir.normalized;
            float n = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;
            if (n < 0) n += 360;

            return n;
        }

}
