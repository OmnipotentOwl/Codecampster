using System;
using System.Collections.Generic;
using System.Linq;
using Codecamp.Mobile.Clients.Portable.Helpers;
using Codecamp.Mobile.DataObjects;
using MvvmHelpers;

namespace Codecamp.Mobile.Clients.Portable.Models.Extensions
{
    public static class SessionStateExtensions
    {

        public static string GetSortName(this Session session)
        {

            if (!session.StartTime.HasValue || !session.EndTime.HasValue || session.StartTime.Value.IsTBA())
                return "To be announced";

            var start = session.StartTime.Value.ToEasternTimeZone();
            var startString = start.ToString("t");

            if (DateTime.Today.Year == start.Year)
            {
                if (DateTime.Today.DayOfYear == start.DayOfYear)
                    return $"Today {startString}";

                if (DateTime.Today.DayOfYear + 1 == start.DayOfYear)
                    return $"Tomorrow {startString}";
            }
            var day = start.ToString("M");
            return $"{day}, {startString}";
        }

        public static string GetDisplayName(this Session session)
        {
            if (!session.StartTime.HasValue || !session.EndTime.HasValue || session.StartTime.Value.IsTBA())
                return "TBA";

            var start = session.StartTime.Value.ToEasternTimeZone();
            var startString = start.ToString("t");
            var end = session.EndTime.Value.ToEasternTimeZone();
            var endString = end.ToString("t");



            if (DateTime.Today.Year == start.Year)
            {
                if (DateTime.Today.DayOfYear == start.DayOfYear)
                    return $"Today {startString}–{endString}";

                if (DateTime.Today.DayOfYear + 1 == start.DayOfYear)
                    return $"Tomorrow {startString}–{endString}";
            }
            var day = start.ToString("M");
            return $"{day}, {startString}–{endString}";
        }

        public static string GetDisplayTime(this Session session)
        {
            if (!session.StartTime.HasValue || !session.EndTime.HasValue || session.StartTime.Value.IsTBA())
                return "TBA";
            var start = session.StartTime.Value.ToEasternTimeZone();


            var startString = start.ToString("t");
            var end = session.EndTime.Value.ToEasternTimeZone();
            var endString = end.ToString("t");
            return $"{startString}–{endString}";
        }

        

        public static IEnumerable<Session> Search(this IEnumerable<Session> sessions, string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                return sessions;

            var searchSplit = searchText.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            //search title, then category, then speaker name
            return sessions.Where(session =>
                                  searchSplit.Any(search =>
                                session.Haystack.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0));
        }
    }
}