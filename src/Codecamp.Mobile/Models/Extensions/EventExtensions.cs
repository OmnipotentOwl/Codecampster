using System;
using Codecamp.Mobile.DataObjects;

namespace Codecamp.Mobile.Clients.Portable.Models.Extensions
{
    public static class EventExtensions
    {
        public static string GetSortName(this FeaturedEvent e)
        {
            if (!e.StartTime.HasValue || !e.EndTime.HasValue)
                return "TBA";

            var start = e.StartTime.Value.ToEasternTimeZone();

            if (DateTime.Today.Year == start.Year)
            {
                if (DateTime.Today.DayOfYear == start.DayOfYear)
                    return $"Today";

                if (DateTime.Today.DayOfYear + 1 == start.DayOfYear)
                    return $"Tomorrow";
            }
            var monthDay = start.ToString("M");
            return $"{monthDay}";
        }

        public static string GetDisplayName(this FeaturedEvent e)
        {
            if (!e.StartTime.HasValue || !e.EndTime.HasValue || e.StartTime.Value.IsTBA())
                return "To be announced";

            var start = e.StartTime.Value.ToEasternTimeZone();

            if (e.IsAllDay)
                return "All Day";

            var startString = start.ToString("t");
            var end = e.EndTime.Value.ToEasternTimeZone();
            var endString = end.ToString("t");

            if (DateTime.Today.Year == start.Year)
            {
                if (DateTime.Today.DayOfYear == start.DayOfYear)
                    return $"Today {startString}–{endString}";

                if (DateTime.Today.DayOfYear + 1 == start.DayOfYear)
                    return $"Tomorrow {startString}–{endString}";
            }

            var day = start.DayOfWeek.ToString();
            var monthDay = start.ToString("M");
            return $"{day}, {monthDay}, {startString}–{endString}";
        }
        public static string GetDisplayTime(this FeaturedEvent e)
        {
            if (!e.StartTime.HasValue || !e.EndTime.HasValue || e.StartTime.Value.IsTBA())
                return "To be announced";

            var start = e.StartTime.Value.ToEasternTimeZone();
            if (e.IsAllDay)
                return "All Day";

            var startString = start.ToString("t");
            var end = e.EndTime.Value.ToEasternTimeZone();
            var endString = end.ToString("t");

            return $"{startString}–{endString}";
        }
    }
}