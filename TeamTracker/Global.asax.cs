using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Timers;
using TeamTracker.EMS;

namespace TeamTracker
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static Timer dailyTimer;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Initialize and start the timer
            InitializeDailyTimer();
        }

        private void InitializeDailyTimer()
        {
            dailyTimer = new Timer
            {
                Interval = GetIntervalUntilNext4PM()
            };
            dailyTimer.Elapsed += ScheduleMarkAbsentTask;
            dailyTimer.Start();
        }

        private double GetIntervalUntilNext4PM()
        {
            DateTime now = DateTime.Now;
            DateTime fourPM = new DateTime(now.Year, now.Month, now.Day, 16, 0, 0);
            if (now > fourPM)
            {
                fourPM = fourPM.AddDays(1);
            }
            return (fourPM - now).TotalMilliseconds;
        }

        private void ScheduleMarkAbsentTask(object sender, ElapsedEventArgs e)
        {
            dailyTimer.Interval = 24 * 60 * 60 * 1000; // Set interval to 24 hours after the first execution

            // Access the MarkAbsentForMissedPunchIns method from a controller or service
            attendancePage attendancePage = new attendancePage();
            attendancePage.MarkAbsentForMissedPunchIns();
        }

        protected void Application_End()
        {
            dailyTimer.Stop();
        }
    }
}
